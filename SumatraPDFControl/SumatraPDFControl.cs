using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text.RegularExpressions;

namespace SumatraPDF
{
	[ToolboxBitmap(typeof(SumatraPDFControl), "Resources.SumatraPDFControl.png")]
	public partial class SumatraPDFControl : UserControl
	{
		private struct COPYDATASTRUCT
		{
			public IntPtr dwData;
			public int cbData;
			public IntPtr lpData;
		}

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string lclassName, string windowTitle);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, int bRepaint);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern void CopyMemory(IntPtr destination, IntPtr source, uint length);

		[DllImport("user32.dll")]
		static extern bool DestroyWindow(IntPtr hWnd);

		// Windows constants and default pointers
		private const int WM_COPYDATA = 0x004A;
		private const int WM_DESTROY = 0x0002;
		private const int WM_SETFOCUS = 0x0007;
		private const int WM_PARENTNOTIFY = 0x0210;
		private const int WM_LBUTTONDOWN = 0x0201;
		private const int WM_LBUTTONUP = 0x0202;
		private const int WM_LBUTTONDBLCLK = 0x0203;
		private const int WM_RBUTTONDOWN = 0x0204;
		private const int WM_RBUTTONUP = 0x0205;
		private const int WM_RBUTTONDBLCLK = 0x0206;
		private const int WM_COMMAND = 0x0111;
		private const int WM_KEYDOWN = 0x0100;
		private const int WM_KEYUP = 0x0101;
		private const int WM_CHAR = 0x0102; // for use by event KeyPress
		private const int WM_CONTEXTMENU = 0x007B;

		private readonly IntPtr DDEW = (IntPtr)0x44646557;
		private readonly IntPtr SUMATRAPLUGIN = (IntPtr)0x44646558;

		// Sumatra Commands (get and update them from sumatra Commands.h)
		private readonly IntPtr SumatraCmdCopySelection = (IntPtr)228;
		private readonly IntPtr SumatraCmdClose = (IntPtr)204;

		private Process SumatraProcess;
		private string sCurrentFile = string.Empty;
		private string pSumatraPDFPath;
		private IntPtr pSumatraWindowHandle;
		private IntPtr SUMATRAMESSAGETYPE;

		// Do not expose MouseClick event. This event does not exist on Windows API and can be easily substituted by MouseDown and MouseUp events
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		new public event EventHandler<MouseEventArgs> MouseClick;

		// Hide but not destroy BackgroundImage. Because this image do not need to be substituted.
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override System.Drawing.Image BackgroundImage
		{
			get { return base.BackgroundImage; }
			set { base.BackgroundImage = value; }
		}

		static List<IntPtr> pSumatraWindowHandleList = new List<IntPtr> { };

		public struct ScrollStateStruct
		{
			public ScrollStateStruct(int page, double x, double y)
			{
				Page = page;
				X = x;
				Y = y;
			}
			public double X { get; }
			public double Y { get; }
			public int Page { get; }
			public override string ToString() => $"{Page},{X},{Y}";
		}
		
		private ScrollStateStruct pScrollState;
		public ScrollStateStruct ScrollState { 
			get {
				SendSumatraCommand("GetProperty", "ScrollState");
				return pScrollState;
			} 
			set {
				SendSumatraCommand("SetProperty", "ScrollState", value.ToString());
			} 
		}

		public enum DisplayModeEnum
		{
			// automatic means: the continuous form of single page, facing or
			// book view - depending on the document's desired PageLayout
			Automatic = 0,
			SinglePage,
			Facing,
			BookView,
			Continuous,
			ContinuousFacing,
			ContinuousBookView,
		};

		public enum ZoomVirtualEnum
        {
			None = 0,
			FitPage = -1, // ZoomVirtual = -1
			FitWidth = -2, // ZoomVirtual = -2
			FitContent = -3  // ZoomVirtual = -3
		}

		public enum RotationEnum
        {			
			RotNone = 0,
			Rot90 = 90,			
			Rot180 = 180,
			Rot270 = 270
		}

		private void GetZoom()
		{
			SendSumatraCommand("GetProperty", "Zoom");
		}

		private void SetZoom(float value)
        {
			SendSumatraCommand("SetProperty", "Zoom", value.ToString(new System.Globalization.CultureInfo("en-US")));
		}

		private float fZoom;
		public float Zoom
        {
			get
            {
				GetZoom();
				return fZoom;
            }
			set
            {
				SetZoom(value);
			}
        }

		private ZoomVirtualEnum fZoomVirtual;
		public ZoomVirtualEnum ZoomVirtual
		{
			get
			{
				GetZoom();
				return fZoomVirtual;
			}
			set
            {
				float fZoomVirtual = (int)value;
				if (fZoomVirtual < 0) SetZoom(fZoomVirtual);
			}
		}

		private void GetDisplayMode()
		{
			SendSumatraCommand("GetProperty", "DisplayMode");
		}
		private DisplayModeEnum eDisplayMode;
		public DisplayModeEnum DisplayMode {
			get {
				GetDisplayMode();
				return eDisplayMode;
			}
			set
            {
				SendSumatraCommand("SetProperty", "DisplayMode", ((int)value).ToString());
			}
		}

		private void GetPage()
		{
			SendSumatraCommand("GetProperty", "Page");
		}

		private void SetPage(int Page)
		{
			SendSumatraCommand("SetProperty", "Page", Page.ToString());
		}
		private int nPage;
		public int Page {
			get {
				GetPage();
				return nPage;
			}
			set {
				SetPage(value);
			} 
		}

		private RotationEnum eRotation;
		public RotationEnum Rotation
        {
			get {
				SendSumatraCommand("GetProperty", "Rotation");
				return eRotation;
            }
        }

		public void RotateBy(RotationEnum Rotation)
        {
			SendSumatraCommand("SetProperty", "RotateBy", ((int)Rotation).ToString());
		}

		private string sNamedDest;
		public string NamedDest {
			get {
				GetPage();
				return sNamedDest;
			}
			set {
				SendSumatraCommand("GotoNamedDest", value);
			} 
		}

		private Boolean bToolbarVisible;
		public Boolean ToolBarVisible
        {
			get
            {
				SendSumatraCommand("GetProperty", "ToolbarVisible");
				return bToolbarVisible;
            }
			set
            {
				SendSumatraCommand("SetProperty", "ToolbarVisible", value ? "1" : "0");
            }
        }

		private Boolean bTocVisible;
		public Boolean TocVisible
		{
			get
			{
				SendSumatraCommand("GetProperty", "TocVisible");
				return bTocVisible;
			}
			set
			{
				SendSumatraCommand("SetProperty", "TocVisible", value ? "1" : "0");
			}
		}

		public SumatraPDFControl()
        {
			pSumatraWindowHandle = (IntPtr)0;
			InitializeComponent();
        }

		private IntPtr SumatraWindowHandle
		{
			get
			{
				if (pSumatraWindowHandle == (IntPtr)0)
				{
					pSumatraWindowHandle = FindWindowEx(base.Handle, default(IntPtr), null, null);
					if (pSumatraWindowHandle != (IntPtr)0) 
						pSumatraWindowHandleList.Add(pSumatraWindowHandle);
				}		
				return pSumatraWindowHandle;
			}
		}

		public string SumatraPDFPath
		{
			get
			{
				return pSumatraPDFPath;
			}
			set
			{
				pSumatraPDFPath = value;
			}
		}

		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			if (m.Msg == WM_CHAR && LastKeyDownEventArgs.SuppressKeyPress) { 
				m.Result = (IntPtr)0; 
				return; 
			}

			base.WndProc(ref m);

			if (m.Msg == WM_SETFOCUS)
			{
				OnEnter(new EventArgs());
			}
			else if (m.Msg == WM_PARENTNOTIFY && (m.WParam.ToInt32() == WM_LBUTTONDOWN || m.WParam.ToInt32() == WM_RBUTTONDOWN))
			{
				if (!base.ContainsFocus)
				{
					OnEnter(new EventArgs());
				}
			}
			else if (m.Msg == WM_DESTROY && !base.IsDisposed && !base.Disposing)
			{
				Dispose();
			}
			else if (m.Msg == WM_COPYDATA)
			{
				object lParam = m.GetLParam(typeof(COPYDATASTRUCT));
				COPYDATASTRUCT x = (lParam != null) ? ((COPYDATASTRUCT)lParam) : default;
				byte[] strb = new byte[checked(x.cbData)];
				Marshal.Copy(x.lpData, strb, 0, x.cbData);
				string sMsg = Encoding.Default.GetString(strb);
				m.Result = ParseSumatraMessage(sMsg, x.dwData);
			}
			else if (m.Msg == WM_KEYDOWN) // UserControl maps as MouseDown event
			{			
				m.Result = LastKeyDownEventArgs.Handled ? (IntPtr) 0 : (IntPtr) 1;
            }
			else if (m.Msg == WM_CHAR ) // UserControl maps as KeyPress event
			{
				m.Result = LastKeyPressEventArgs.Handled ? (IntPtr)0 : (IntPtr)1;
			}
			else if (m.Msg == WM_KEYUP) // UserControl maps as MouseDown event
			{
				m.Result = LastKeyUpEventArgs.Handled ? (IntPtr)0 : (IntPtr)1;
			}
			else if (m.Msg == WM_RBUTTONDBLCLK || m.Msg == WM_LBUTTONDBLCLK)
            {
				IntPtr xy = m.LParam;
				int x = unchecked((short)(long)xy);
				int y = unchecked((short)((long)xy >> 16));

				OnMouseDoubleClick(new MouseEventArgs(m.Msg == WM_LBUTTONDBLCLK ? MouseButtons.Left : MouseButtons.Right, 2, x, y, 0));
			}
			else if (m.Msg == WM_LBUTTONDOWN)
            {
				if (ContextMenuStrip != null && ContextMenuStrip.Visible)
				{
					ContextMenuStrip.Close();
				}
			}
			else if (m.Msg == WM_RBUTTONUP)
			{
				if (ContextMenuStrip != null && ContextMenuStrip.Visible)
				{
					ContextMenuStrip.Focus();
				}
			}
		}

		public class PageChangedEventArgs : EventArgs
        {
			public PageChangedEventArgs(int Page, string NamedDest)
            {
				this.Page = Page;
				this.NamedDest = NamedDest;
            }

			public int Page { get; }
			public string NamedDest { get; }
		}
		public class ContextMenuOpenEventArgs : EventArgs
        {
			public ContextMenuOpenEventArgs(int X, int Y)
            {
				this.X = X;
				this.Y = Y;
				Handled = false;
			}
			public int X { get; }
			public int Y { get; }
			public Boolean Handled { get; set; }
		}
		public class SumatraMessageEventArgs : EventArgs
		{
			public int CallBackReturn { get; set; }
			public string Msg { get; set; }
			public IntPtr Data { get; set; }
		}

		public class ZoomChangedEventArgs : EventArgs
		{
			public ZoomChangedEventArgs(float Zoom, ZoomVirtualEnum ZoomVirtual, Boolean MouseWheel)
            {
				this.Zoom = Zoom;
				this.ZoomVirtual = ZoomVirtual;
				this.MouseWheel = MouseWheel;
            }
			public float Zoom { get; }
			public ZoomVirtualEnum ZoomVirtual { get; }
			public Boolean MouseWheel { get; }
		}
		public class LinkClickedEventArgs : EventArgs
        {
			public LinkClickedEventArgs(string LinkText)
            {
				this.LinkText = LinkText;
			}
			public string LinkText { get; }
        }

		public class DisplayModeChangedEventArgs : EventArgs
        {
			public DisplayModeChangedEventArgs(DisplayModeEnum DisplayMode)
            {
				this.DisplayMode = DisplayMode;
            }
			public DisplayModeEnum DisplayMode { get; }
        }

		public class ScrollStateEventArgs : EventArgs
        {
			public ScrollStateEventArgs(ScrollStateStruct ScrollState)
            {
				this.ScrollState = ScrollState;
            }
			public ScrollStateStruct ScrollState { get; }
		}

		[Description("Generic SumatraPDF message ocurred"), Category("SumatraPDF")]
		public event EventHandler<SumatraMessageEventArgs> SumatraMessage;

		[Description("Current visible page was changed"), Category("SumatraPDF")]
		public event EventHandler<PageChangedEventArgs> PageChanged;

		[Description("Right mouse button was clicked and SumatraPDF tried to open context menu"), Category("SumatraPDF")]
		public event EventHandler<ContextMenuOpenEventArgs> ContextMenuOpen;

		[Description("Zoom factor was changed"), Category("SumatraPDF")]
		public event EventHandler<ZoomChangedEventArgs> ZoomChanged;

		[Description("Document link on SumatraPDF was clicked"), Category("SumatraPDF")]
		public event EventHandler<LinkClickedEventArgs> LinkClick;

		[Description("Display mode was changed"), Category("SumatraPDF")]
		public event EventHandler<DisplayModeChangedEventArgs> DisplayModeChanged;

		[Description("Scroll position (vertical and/or horizontal) was changed"), Category("SumatraPDF")]
		public event EventHandler<ScrollStateEventArgs> ScrollStateChanged;

#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
		[Description("Key was pressed on SumatraPDF control (Event arg 'KeyChar' cannot be changed)"), Category("SumatraPDF")]
		public event EventHandler<KeyPressEventArgs> KeyPress;

		[Description("Key was pressed down on SumatraPDF control"), Category("SumatraPDF")]
		public event KeyEventHandler KeyDown;

		[Description("Key was released up on SumatraPDF control"), Category("SumatraPDF")]
		public event KeyEventHandler KeyUp;
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword

		private IntPtr ParseSumatraMessage(string sMsg, IntPtr dwData)
		{
			string sMsg0 = sMsg.Substring(0, sMsg.Length - 1);
			int CallBackReturn = 0;

			// dwData = (IntPtr)0x4C5255 it's a message to internet browser when SumatraPDF is operating in plugin mode, so raise LinkClickedMessage event			
			if (dwData == (IntPtr)0x4C5255) { 
				LinkClick?.Invoke(this, new LinkClickedEventArgs(sMsg0));
				return (IntPtr)CallBackReturn;
			} else
				if (dwData != SUMATRAPLUGIN) sMsg0 = string.Format("[UnknownMessage(\"{0}\")]", sMsg0);

			Match m = Regex.Match(sMsg0, @"\[(?<message>\w+)\((?<args>.*)\)\]");
			if (m.Success)
			{
				string mmsg = m.Result("${message}");
				switch (mmsg)
                {
					case "PageChanged":
					case "Page":
						Match mPG = Regex.Match(m.Result("${args}"), @"(?<pageNo>.+)\,\s*\u0022(?<namedDest>.*)\u0022");
						nPage = int.Parse(mPG.Result("${pageNo}"));
						sNamedDest = mPG.Result("${namedDest}");
						if (mmsg.Contains("Changed"))
							PageChanged?.Invoke(this, new PageChangedEventArgs(nPage, sNamedDest));
						break;

					case "ContextMenuOpened":
						Match m2 = Regex.Match(m.Result("${args}"), @"(?<x>.+)\,\s*(?<y>.+)");
						var cmoe = new ContextMenuOpenEventArgs(int.Parse(m2.Result("${x}")), int.Parse(m2.Result("${y}")));
						ContextMenuOpen?.Invoke(this, cmoe);						
						CallBackReturn = cmoe.Handled ? 1 : 0;
						if (!cmoe.Handled && ContextMenuStrip != null)
						{
							ContextMenuStrip.Show(this, cmoe.X, cmoe.Y);							
							CallBackReturn = 1;
						}
						break;

					case "ZoomChanged":
					case "ZoomChangedMouseWeel":
					case "Zoom":

						Match mZoom = Regex.Match(m.Result("${args}"), @"(?<x>.+)\,\s*(?<y>.+)");
						fZoom = float.Parse(mZoom.Result("${x}"), new System.Globalization.CultureInfo("en-US"));
						float ZoomVirtual0 = float.Parse(mZoom.Result("${y}"), new System.Globalization.CultureInfo("en-US"));
						if (ZoomVirtual0 < 0) fZoomVirtual = (ZoomVirtualEnum)ZoomVirtual0; else fZoomVirtual = ZoomVirtualEnum.None;
						if (mmsg.Contains("Changed"))
							ZoomChanged?.Invoke(this, new ZoomChangedEventArgs(fZoom, fZoomVirtual, (mmsg != "ZoomChanged")));
						break;

					case "Rotation":
					case "RotationChanged":
						Match mRotation = Regex.Match(m.Result("${args}"), @"(?<rot>.+)");
						eRotation = (RotationEnum)int.Parse(mRotation.Result("${rot}"));
						break;

					case "DisplayModeChanged":
					case "DisplayMode":
						eDisplayMode = (DisplayModeEnum)int.Parse(m.Result("${args}"));
						if (mmsg.Contains("Changed")) DisplayModeChanged?.Invoke(this, new DisplayModeChangedEventArgs(eDisplayMode));
						break;
					case "StartupFinished":
					case "FileOpened":
						GetPage();
						CallBackReturn = RaiseDefaultSumatraEvent(sMsg0, dwData);
						break;

					case "ToolbarVisible":
						bToolbarVisible = (int.Parse(m.Result("${args}")) == 1);
						break;

					case "TocVisible":
						bTocVisible = (int.Parse(m.Result("${args}")) == 1);
						break;

					case "ScrollState":
					case "ScrollStateChanged":
						Match mSP = Regex.Match(m.Result("${args}"), @"(?<page>.+)\,(?<x>.+)\,\s*(?<y>.+)");
						pScrollState = new ScrollStateStruct(
							int.Parse(mSP.Result("${page}")),
							Double.Parse(mSP.Result("${x}"), new System.Globalization.CultureInfo("en-US")), 
							Double.Parse(mSP.Result("${y}"), new System.Globalization.CultureInfo("en-US"))
						);
						if (mmsg.Contains("Changed")) ScrollStateChanged?.Invoke(this, new ScrollStateEventArgs(pScrollState));
						break;

					default:
						CallBackReturn = RaiseDefaultSumatraEvent(sMsg0, dwData);
						break;
				}
			}

			return (IntPtr)CallBackReturn;
		}

		private int RaiseDefaultSumatraEvent(string msg, IntPtr dwData) 
        {
			var e = new SumatraMessageEventArgs { CallBackReturn = 0, Msg = msg, Data = dwData };
			SumatraMessage?.Invoke(this, e);
			return e.CallBackReturn;
		}

		private void SendSumatraCommand(string strMessage, params Object[] parr)
		{
			// Without define current file commands cannot be send to sumatra
			if (sCurrentFile == string.Empty) return;

			if (SumatraWindowHandle == (IntPtr)0 && pSumatraWindowHandleList.Count==0) return;
			IntPtr SumatraCurrentHandle = SumatraWindowHandle == (IntPtr)0 ? pSumatraWindowHandleList[0] : SumatraWindowHandle;

			string DDEMessage = string.Empty;
			if (strMessage.StartsWith("[")) DDEMessage = String.Format(strMessage, parr); else
            {
				DDEMessage = "[" + strMessage + "("; //\"" + sCurrentFile + "\"";
				Boolean FirstParam = true;
				foreach (Object p in parr)
				{
					if (!FirstParam) DDEMessage += ","; else FirstParam = false;
					switch (p.GetType().FullName)
					{
						case "System.String":
							DDEMessage += "\"" + p.ToString() + "\"";
							break;
						case "System.Double":
							DDEMessage += ((Double)p).ToString(new System.Globalization.CultureInfo("en-US"));
							break;
						default:
							DDEMessage += p.ToString();
							break;
					}
				}
				DDEMessage += ")]";
			}

			COPYDATASTRUCT DataStruct = default;
			DDEMessage += "\0";
			DataStruct.dwData = SUMATRAMESSAGETYPE;
			DataStruct.cbData = checked(DDEMessage.Length * Marshal.SystemDefaultCharSize);
			DataStruct.lpData = Marshal.StringToHGlobalAuto(DDEMessage);
			IntPtr pDataStruct = Marshal.AllocHGlobal(Marshal.SizeOf(DataStruct));
			Marshal.StructureToPtr(DataStruct, pDataStruct, fDeleteOld: false);
			SendMessage(SumatraCurrentHandle, WM_COPYDATA, (IntPtr)0, pDataStruct);
			Marshal.FreeHGlobal(DataStruct.lpData);
			Marshal.FreeHGlobal(pDataStruct);
		}

		private void RestartSumatra(string sFile, int page = 1)
        {
			if ((SumatraWindowHandle != (IntPtr)0) & (SumatraProcess != null))
			{				
				CloseDocument();
				SumatraProcess.Kill();
				pSumatraWindowHandleList.Remove(pSumatraWindowHandle);
				pSumatraWindowHandle = (IntPtr)0;				
			}
			var PSInfo = new ProcessStartInfo
			{
				FileName = SumatraPDFPath,
				Arguments =
					"-plugin " + base.Handle.ToString() +
					//" -invert-colors" +
					//" -appdata \"c:\\users\\marco\\Downloads\"" +
					" -page " + page.ToString() +										
					" \"" + sFile + "\""
					
			};
            SumatraProcess = Process.Start(PSInfo);			
		}

		private void OpenFile()
        {
			SendSumatraCommand("OpenFile", sCurrentFile, Handle.ToString());
		}

		public void LoadFile(string PDFFile, int Page = 1, Boolean NewSumatraInstance = false)
		{
			sCurrentFile = PDFFile;
			this.Page = Page;
			NamedDest = string.Empty;
			if (SumatraWindowHandle != (IntPtr)0)
			{
				OpenFile();
				pSumatraWindowHandleList.Remove(SumatraWindowHandle);
				pSumatraWindowHandle = (IntPtr)0;				
			}
			else
			{					
				if (NewSumatraInstance || pSumatraWindowHandleList.Count==0) RestartSumatra(PDFFile, Page); 
				else OpenFile();
			}
			SetPage(Page);

		}

		private void CloseDocument()
        {
			// Ver qual o código do comando no arquivo Commands.h: CmdClose
			SendMessage(SumatraWindowHandle, WM_COMMAND, SumatraCmdClose, (IntPtr)0);
		}

		private void SumatraPDFControl_Resize(object sender, EventArgs e)
		{
			if (SumatraWindowHandle != (IntPtr)0)
			{
				MoveWindow(SumatraWindowHandle, 0, 0, base.Width, base.Height, 0);
			}
		}

		public void CopySelection()
		{
			if (SumatraWindowHandle != (IntPtr)0)
			{
				// Ver qual o código do comando no arquivo Commands.h: CmdCopySelection
				SendMessage(SumatraWindowHandle, WM_COMMAND, SumatraCmdCopySelection, (IntPtr)0);
			}
		}

		public void TextSearch(string searchText, Boolean matchCase)
		{
			SendSumatraCommand("TextSearch", searchText, (matchCase ? 1 : 0));
		}

		public void TextSearchNext(Boolean forward)
        {
			SendSumatraCommand("TextSearchNext", (forward ? 1 : 0));
		}

        private void SumatraPDFControl_Load(object sender, EventArgs e)
        {
			SUMATRAMESSAGETYPE = SUMATRAPLUGIN;
			base.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SumatraPDFControl_KeyDown);
			base.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SumatraPDFControl_KeyUp);
			base.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SumatraPDFControl_KeyPress);
		}

		private KeyEventArgs LastKeyDownEventArgs;
		private void SumatraPDFControl_KeyDown(object sender, KeyEventArgs e)
		{
			this.KeyDown?.Invoke(this, e);
			LastKeyDownEventArgs = e;
		}

		private KeyEventArgs LastKeyUpEventArgs;
		private void SumatraPDFControl_KeyUp(object sender, KeyEventArgs e)
		{
			this.KeyUp?.Invoke(this, e);
			LastKeyUpEventArgs = e;
		}

		private KeyPressEventArgs LastKeyPressEventArgs;
		private void SumatraPDFControl_KeyPress(object sender, KeyPressEventArgs e)
		{
			this.KeyPress?.Invoke(this, e);
			// Do not allow SumatraPDF close current window when pressing 'q' key
			if (e.KeyChar == 'q' || e.KeyChar == 'Q') e.Handled = true;
			LastKeyPressEventArgs = e;
			
		}

		/* TODO: 
		 * Concentrate call to SendPluginWndProcMessage in WndProcCanvas instead of WndProcCanvasFixedPageUI. 
		 *	 Treat if event is Handled by SumatraPDFControl or not
		 *   Analyze possible to send other messages from SumatraPDF Canvas WndProc to SumatraPDFControl *	 
		 * Bug: Toc window title is not being repainted after another window pass over it
		 * Hide properites and events not used and implement others
		 * Special keys events 
		 *   3. Block WM_SYSCHAR message (handled by FrameOnSysChar on SumatraPDF.cpp) because ALT+Space can give user control of current plugin window 
		 *   4. Treat ALT key
		 * LastPage - Get property
		 * Commands call
		 * - Page first, previous, next, last 
		 * - Close Document
		 * - Copy Selection
		 * - Select All
		 * - Print - Show Dialog and direct print
		 * Global Config - auto update,  etc
		 * Bookmarks - Context Menu event
		 * Do a revision in GEDVISA PDFXChange used properties
		 * Commenting all methods, properties and events
		 * Implement FileOpened and StartupFinished events
		 * Maps UserControl Scroll event to SumatraPDF Scroll 
		 * Page rotation event is more complicated to implement because current Window information (WindowInfo*) does not exists in DisplayModel sumatrapdf object. 
		 *   So its impossible to send PluginHostCallBack message without replicate this call in all points of source code calling method DisplayModel::RotateBy.
		*/

	}
}
