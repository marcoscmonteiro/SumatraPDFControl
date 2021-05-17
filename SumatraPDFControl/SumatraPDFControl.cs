using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text.RegularExpressions;

namespace SumatraPDFControl
{
	public partial class SumatraPDFControl : UserControl
	{
		public struct COPYDATASTRUCT
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
		private const int WM_RBUTTONDOWN = 0x0204;
		private const int WM_COMMAND = 0x0111;
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

		static List<IntPtr> pSumatraWindowHandleList = new List<IntPtr> { };

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

		public enum ZoomVirtuamEnum
        {
			None = 0,
			FitPage, // ZoomVirtual = -1
			FitWidth, // ZoomVirtual = -2
			FitContent // ZoomVirtual = -3
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

		private ZoomVirtuamEnum fZoomVirtual;
		public ZoomVirtuamEnum ZoomVirtual
		{
			get
			{
				GetZoom();
				return fZoomVirtual;
			}
			set
            {
				float fZoomVirtual;
				switch (value)
				{
					case ZoomVirtuamEnum.FitPage:
						fZoomVirtual = -1;
						break;
					case ZoomVirtuamEnum.FitWidth:
						fZoomVirtual = -2;
						break;
					case ZoomVirtuamEnum.FitContent:
						fZoomVirtual = -3;
						break;
					default:
						return;
				}
				SetZoom(fZoomVirtual);
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

		private void SetPage(int page)
		{
			SendSumatraCommand("GotoPage", page);
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
		}

		public class PageChangedEventArgs : EventArgs
        {
			public int Page { get; set; }
			public string NamedDest { get; set; }
		}
		public class ContextMenuEventArgs : EventArgs
        {
			public int X { get; set; }
			public int Y { get; set; }
			public Boolean OpenSumatraContextMenu { get; set; }
		}
		public class SumatraMessageEventArgs : EventArgs
		{
			public int CallBackReturn { get; set; }
			public string Msg { get; set; }
			public IntPtr Data { get; set; }
		}
		public class KeyPressedEventArgs : EventArgs
		{
			public char Key { get; set; }
			public Boolean DisallowKeyPress { get; set; }
		}
		public class ZoomChangedEventArgs : EventArgs
		{
			public float Zoom { get; set; }
			public ZoomVirtuamEnum ZoomVirtual { get; set; }
			public Boolean MouseWheel { get; set; }
		}
		public class LinkClickedEventArgs : EventArgs
        {
			public string LinkText { get; set; }
        }

		public class DisplayModeChangedEventArgs : EventArgs
        {
			public DisplayModeEnum DisplayMode { get; set; }
        }

		public event EventHandler<SumatraMessageEventArgs> SumatraMessage;
		public event EventHandler<PageChangedEventArgs> PageChangedMessage;
		public event EventHandler<ContextMenuEventArgs> ContextMenuMessage;
		public event EventHandler<KeyPressedEventArgs> KeyPressedMessage;
		public event EventHandler<ZoomChangedEventArgs> ZoomChangedMessage;
		public event EventHandler<LinkClickedEventArgs> LinkClickedMessage;
		public event EventHandler<DisplayModeChangedEventArgs> DisplayModeChangedMessage;

		private IntPtr ParseSumatraMessage(string sMsg, IntPtr dwData)
		{
			string sMsg0 = sMsg.Substring(0, sMsg.Length - 1);
			int CallBackReturn = 0;

			// dwData = (IntPtr)0x4C5255 it's a message to internet browser when SumatraPDF is operating in plugin mode, so raise LinkClickedMessage event			
			if (dwData == (IntPtr)0x4C5255) { 
				LinkClickedMessage?.Invoke(this, new LinkClickedEventArgs { LinkText = sMsg0 });
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
							PageChangedMessage?.Invoke(this, new PageChangedEventArgs { Page = Page, NamedDest = NamedDest });
						break;

					case "KeyPressed":
						var kpe = new KeyPressedEventArgs { Key = (char)int.Parse(m.Result("${args}")), DisallowKeyPress = false };
                        KeyPressedMessage?.Invoke(this, kpe);
						kpe.DisallowKeyPress = (kpe.Key == 'q');
						CallBackReturn = kpe.DisallowKeyPress ? 1 : 0;
						break;

					case "ContextMenuOpened":
						Match m2 = Regex.Match(m.Result("${args}"), @"(?<x>.+)\,\s*(?<y>.+)");
						var cmoe = new ContextMenuEventArgs {
							X = int.Parse(m2.Result("${x}")),
							Y = int.Parse(m2.Result("${y}")),
							OpenSumatraContextMenu = true
						};
						ContextMenuMessage?.Invoke(this, cmoe);
						CallBackReturn = cmoe.OpenSumatraContextMenu ? 0 : 1;
						break;

					case "ZoomChanged":
					case "ZoomChangedMouseWeel":
					case "Zoom":

						Match mZoom = Regex.Match(m.Result("${args}"), @"(?<x>.+)\,\s*(?<y>.+)");
						fZoom = float.Parse(mZoom.Result("${x}"), new System.Globalization.CultureInfo("en-US"));
						float fZoomVirtual0 = float.Parse(mZoom.Result("${y}"), new System.Globalization.CultureInfo("en-US"));
						switch (fZoomVirtual0)
                        {
							case -1:
								fZoomVirtual = ZoomVirtuamEnum.FitPage;
								break;
							case -2:
								fZoomVirtual = ZoomVirtuamEnum.FitWidth;
								break;
							case -3:
								fZoomVirtual = ZoomVirtuamEnum.FitContent;
								break;
							default:
								fZoomVirtual = ZoomVirtuamEnum.None;
								break;
						}
						if (mmsg.Contains("Changed"))
							ZoomChangedMessage?.Invoke(this, 
								new ZoomChangedEventArgs { 
									Zoom = fZoom, 
									ZoomVirtual = fZoomVirtual,
									MouseWheel = (mmsg!="ZoomChanged")
								});
						break;

					case "DisplayModeChanged":
					case "DisplayMode":
						eDisplayMode = (DisplayModeEnum)int.Parse(m.Result("${args}"));
						if (mmsg.Contains("Changed"))
							DisplayModeChangedMessage?.Invoke(this,
								new DisplayModeChangedEventArgs
								{
									DisplayMode = eDisplayMode,
								});
							break;
					case "StartupFinished":
					case "FileOpen":
					case "FileOpenPluginWindow":
						GetPage();
						CallBackReturn = RaiseDefaultSumatraEvent(sMsg0, dwData);
						break;

					case "ToolbarVisible":
						bToolbarVisible = (int.Parse(m.Result("${args}")) == 1);
						break;

					case "TocVisible":
						bTocVisible = (int.Parse(m.Result("${args}")) == 1);
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
				FileName = SumatraPDFPath + "SumatraPDF.exe",
				Arguments =
					"-plugin " + base.Handle.ToString() +
					//" -invert-colors" +
					//" -appdata \"c:\\users\\marco\\Downloads\"" +
					" -page " + page.ToString() +										
					" \"" + sFile + "\""
					
			};
            SumatraProcess = Process.Start(PSInfo);			
		}

		public void LoadFile(string PDFFile, int Page = 1, Boolean NewSumatraInstance = false)
		{
			sCurrentFile = PDFFile;
			this.Page = Page;
			NamedDest = string.Empty;
			if (SumatraWindowHandle != (IntPtr)0)
			{
				SendSumatraCommand("OpenPluginWindow", sCurrentFile, base.Handle.ToString());
				pSumatraWindowHandleList.Remove(SumatraWindowHandle);
				pSumatraWindowHandle = (IntPtr)0;				
			}
			else
			{					
				if (NewSumatraInstance || pSumatraWindowHandleList.Count==0) RestartSumatra(PDFFile, Page); 
				else SendSumatraCommand("OpenPluginWindow", sCurrentFile, base.Handle.ToString());				
			}
			SetPage(Page);

			// Force refresh of sumatracontrol (solution to bypass a bug in sumatrapdf redering window in plugin mode)
			Width -= 1;
			Width += 1;
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
		}

        /* TODO: 
		 * Bug (bypassed): When open new window using the same sumatrapdf process do not resize correctly
		 * Bug: Toc window title is not being repainted after another window pass over it
		 * ScrollPosition - Set and Get properties / event
		 * Special keys - events		 		 
		 * Page Rotation - Set and Get properties /event		 
		 * LastPage - Get property
		 * Commands call
		 * - Page first, previous, next, last 
		 * - Close Document
		 * - Copy Selection
		 * - Select All
		 * - Print - Show Dialog and direct print
		 * Global Config - auto update,  etc
		 * Bookmarks - Context Menu event
		 * Dont call Get property after raising changing events (optimization)
		 * Do a revision in GEDVISA PDFXChange used properties
		*/

    }
}
