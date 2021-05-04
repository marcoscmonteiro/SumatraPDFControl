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

		private const int WM_COPYDATA = 0x004A;
		private const int WM_DESTROY = 0x0002;
		private const int WM_SETFOCUS = 0x0007;
		private const int WM_PARENTNOTIFY = 0x0210;
		private const int WM_LBUTTONDOWN = 0x0201;
		private const int WM_RBUTTONDOWN = 0x0204;
		private const int WM_COMMAND = 0x0111;
		private readonly IntPtr DDEW = (IntPtr)0x44646557;

		private Process SumatraProcess;
		private string sCurrentFile;
		private string pSumatraPDFPath;
		private IntPtr pSumatraWindowHandle;


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

		private float fZoom;
		public float Zoom
        {
			get
            {
				GetZoom();
				return fZoom;
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
		}

		private DisplayModeEnum eDisplayMode;
		public DisplayModeEnum DisplayMode {
			get {
				GetDisplayMode();
				return eDisplayMode;
			}
		}

		private Boolean bContinuousDisplayMode;
		public Boolean ContinuousDisplayMode
        {
			get
            {
				GetDisplayMode();
				return bContinuousDisplayMode;
			}
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
				SetNamedDest(value);
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
			public Boolean Continuous { get; set; }
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
				if (dwData != DDEW) sMsg0 = string.Format("[UnknownMessage(\"{0}\")]", sMsg0);

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
						Match mDM = Regex.Match(m.Result("${args}"), @"(?<dm>.+)\,\s*(?<kc>.+)");
						eDisplayMode = (DisplayModeEnum)int.Parse(mDM.Result("${dm}"));
						bContinuousDisplayMode = (int.Parse(mDM.Result("${kc}"))!=0);
						if (mmsg.Contains("Changed"))
							DisplayModeChangedMessage?.Invoke(this,
								new DisplayModeChangedEventArgs
								{
									DisplayMode = eDisplayMode,
									Continuous = bContinuousDisplayMode
								});
							break;
					case "StartupFinished":
					case "FileOpen":
						GetPage();
						CallBackReturn = RaiseDefaultSumatraEvent(sMsg0, dwData);
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

		private void SendDDECommand(string strMessage, params Object[] parr)
		{
			string DDEMessage = string.Empty;
			if (strMessage.StartsWith("[")) DDEMessage = String.Format(strMessage, parr); else
            {
				DDEMessage = "[" + strMessage + "(";
				int count = 0;
				foreach (Object p in parr)
				{
					if (count > 0) DDEMessage += ",";
					switch (p.GetType().FullName)
					{
						case "System.String":
							DDEMessage += "\"" + p.ToString() + "\"";
							break;
						default:
							DDEMessage += p.ToString();
							break;
					}
					count++;
				}
				DDEMessage += ")]";
			}

			COPYDATASTRUCT DataStruct = default;
			DDEMessage += "\0";
			DataStruct.dwData = DDEW;
			DataStruct.cbData = checked(DDEMessage.Length * Marshal.SystemDefaultCharSize);
			DataStruct.lpData = Marshal.StringToHGlobalAuto(DDEMessage);
			IntPtr pDataStruct = Marshal.AllocHGlobal(Marshal.SizeOf(DataStruct));
			Marshal.StructureToPtr(DataStruct, pDataStruct, fDeleteOld: false);
			if (SumatraWindowHandle != (IntPtr)0)
			{
				SendMessage(SumatraWindowHandle, WM_COPYDATA, (IntPtr)0, pDataStruct);
			}
			Marshal.FreeHGlobal(DataStruct.lpData);
			Marshal.FreeHGlobal(pDataStruct);
		}

		private void RestartSumatra(string sFile, int page = 1)
        {
			if ((SumatraWindowHandle != (IntPtr)0) & (SumatraProcess != null))
			{
				// Ver qual o código do comando no arquivo Commands.h: CmdClose
				SendMessage(SumatraWindowHandle, WM_COMMAND, (IntPtr)204, (IntPtr)0);
				SumatraProcess.Kill();
				pSumatraWindowHandle = (IntPtr)0;
			}
			var PSInfo = new ProcessStartInfo
			{
				FileName = SumatraPDFPath + "SumatraPDF.exe",
				Arguments =
					"-plugin " + base.Handle +
					//" -invert-colors" +
					//" -appdata \"c:\\users\\marco\\Downloads\"" +
					" -page " + page.ToString() +										
					" \"" + sFile + "\""
					
			};
            SumatraProcess = Process.Start(PSInfo);			
		}

		public void LoadFile(string sFile, int page = 1)
		{
			sCurrentFile = sFile;
			Page = page;
			NamedDest = string.Empty;
			if (SumatraWindowHandle != (IntPtr)0)
			{
				// Ver qual o código do comando no arquivo Commands.h: CmdClose
				SendMessage(SumatraWindowHandle, WM_COMMAND, (IntPtr)204, (IntPtr)0);
				SendDDECommand("Open", sFile);
				SetPage(page);
			}
			else
			{				
				RestartSumatra(sFile, page);
			}
		}

		private void SumatraPDFControl_Resize(object sender, EventArgs e)
		{
			if (SumatraWindowHandle != (IntPtr)0)
			{
				MoveWindow(SumatraWindowHandle, 0, 0, base.Width, base.Height, 0);
			}
		}

		public void ToogleToolBar()
		{
			if (SumatraWindowHandle != (IntPtr)0)
			{
				// Ver qual o código do comando no arquivo Commands.h: CmdViewShowHideToolbar
				SendMessage(SumatraWindowHandle, WM_COMMAND, (IntPtr)225, (IntPtr)0);
			}
		}

		public void CopySelection()
		{
			if (SumatraWindowHandle != (IntPtr)0)
			{
				// Ver qual o código do comando no arquivo Commands.h: CmdCopySelection
				SendMessage(SumatraWindowHandle, WM_COMMAND, (IntPtr)228, (IntPtr)0);
			}
		}

		private void SetPage(int nPage)
		{
			SendDDECommand("GotoPage", sCurrentFile, nPage);
		}

		private void GetPage()
		{
			SendDDECommand("GetProperty", sCurrentFile, "Page");
		}

		private void SetNamedDest(string namedDest)
        {
			SendDDECommand("GotoNamedDest", sCurrentFile, namedDest);
		}

		private void GetDisplayMode()
        {
			SendDDECommand("GetProperty", sCurrentFile, "DisplayMode");
		}

		private void GetZoom()
        {
			SendDDECommand("GetProperty", sCurrentFile, "Zoom");
		}

		public void TextSearch(string searchText, Boolean matchCase)
		{
			SendDDECommand("TextSearch", sCurrentFile, searchText, (matchCase ? 1 : 0));
		}

		public void TextSearchNext(Boolean forward)
        {
			SendDDECommand("TextSearchNext", sCurrentFile, (forward ? 1 : 0));
		}

		/* TODO: 
		 * ScrollPosition - Set and Get properties
		 * Zoom - Set and Get properties
		 * DisplayMode - Set and Get properties
		 * Show TOC - Set and Get properties
		 * Show Toolbar - Set and Get properties
		 * Special kys - events
		 * Global Config - auto update,  etc
		 * Print - Show Dialog and direct print
		 * Page Rotation - Set and Get properties
		 * Page: first, previous, next, last 
		 * Dont call Get property after raising changing events (optimization)
		 * Do a revision in GEDVISA PDFXChange used properties
		*/

	}
}
