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
    public partial class SumatraPDFControl: UserControl
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

		private const int WM_COPYDATA = 74;
        private Process SumatraProcess;
        private string sCurrentFile;
        private string pSumatraPDFPath;
		private IntPtr pSumatraWindowHandle;

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
			if (m.Msg == 7)
			{
				OnEnter(new EventArgs());
			}
			else if (m.Msg == 528 && (m.WParam.ToInt32() == 513 || m.WParam.ToInt32() == 516))
			{
				if (!base.ContainsFocus)
				{
					OnEnter(new EventArgs());
				}
			}
			else if (m.Msg == 2 && !base.IsDisposed && !base.Disposing)
			{
				Dispose();
			}
			else if (m.Msg == 74)
			{
				object lParam = m.GetLParam(typeof(COPYDATASTRUCT));
				COPYDATASTRUCT x = (lParam != null) ? ((COPYDATASTRUCT)lParam) : default(COPYDATASTRUCT);
				byte[] strb = new byte[checked(x.cbData)];
				Marshal.Copy(x.lpData, strb, 0, x.cbData);
				string sMsg = Encoding.Default.GetString(strb);
				m.Result = ParseSumatraMessage(sMsg, x.dwData);
			}
		}

		public class PageChangedEventArgs : EventArgs
        {
			public int Page { get; set; }
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

		public event EventHandler<SumatraMessageEventArgs> SumatraMessage;
		public event EventHandler<PageChangedEventArgs> PageChangedMessage;
		public event EventHandler<ContextMenuEventArgs> ContextMenuMessage;
		public event EventHandler<KeyPressedEventArgs> KeyPressedMessage;

		private IntPtr ParseSumatraMessage(string sMsg, IntPtr dwData)
		{
			var e = new SumatraMessageEventArgs { CallBackReturn = 0, Msg=sMsg.Substring(0, sMsg.Length - 1), Data = dwData};

			// dwData = (IntPtr)0x4C5255 é a mensagem esperada pelo navegador de internet quando o SumatraPDF está operando em modo de plugin
			// Sendo assim, no caso de recebimento deste tipo de mensagem, ela é formatada para ficar dentro do padrão das mensagens do tipo 0x1			
			if (dwData == (IntPtr)0x4C5255) e.Msg = string.Format("[LinkClicked(\"{0}\")]", e.Msg);
			else
				if (dwData != (IntPtr)0x1) e.Msg = string.Format("[UnknownMessage(\"{0}\")]", e.Msg);

			Match m = Regex.Match(e.Msg, @"\[(?<message>\w+)\((?<args>.+)\)\]");
			if (m.Success)
			{
				switch (m.Result("${message}"))
                {
					case "PageChanged":
						PageChangedMessage?.Invoke(this, new PageChangedEventArgs { Page = int.Parse(m.Result("${args}")) });
						break;
					case "KeyPressed":
						var kpe = new KeyPressedEventArgs { Key = (char)int.Parse(m.Result("${args}")), DisallowKeyPress = false };
                        KeyPressedMessage?.Invoke(this, kpe);
                        if (kpe.Key == 'q') kpe.DisallowKeyPress = true;
						if (kpe.DisallowKeyPress) e.CallBackReturn = 1; else e.CallBackReturn = 0;
						break;
					case "ContextMenuOpened":
						Match m2 = Regex.Match(m.Result("${args}"), @"(?<x>.+)\,\s+(?<y>.+)");
						var cmoe = new ContextMenuEventArgs {
							X = int.Parse(m2.Result("${x}")),
							Y = int.Parse(m2.Result("${y}")),
							OpenSumatraContextMenu = true
						};
						ContextMenuMessage?.Invoke(this, cmoe); 
						if (cmoe.OpenSumatraContextMenu) e.CallBackReturn = 0; else e.CallBackReturn = 1;

						break;
					default:
						SumatraMessage(this, e);
						break;
				}
			}

			return (IntPtr)e.CallBackReturn;
		}

		private void SendDDECommand(string strMessage)
		{
			COPYDATASTRUCT DataStruct = default(COPYDATASTRUCT);
			strMessage += "\0";
			DataStruct.dwData = (IntPtr)1147430231;
			DataStruct.cbData = checked(strMessage.Length * Marshal.SystemDefaultCharSize);
			DataStruct.lpData = Marshal.StringToHGlobalAuto(strMessage);
			IntPtr pDataStruct = Marshal.AllocHGlobal(Marshal.SizeOf(DataStruct));
			Marshal.StructureToPtr(DataStruct, pDataStruct, fDeleteOld: false);
			if (SumatraWindowHandle != (IntPtr)0)
			{
				SendMessage(SumatraWindowHandle, 74u, (IntPtr)0, pDataStruct);
			}
			Marshal.FreeHGlobal(DataStruct.lpData);
			Marshal.FreeHGlobal(pDataStruct);
		}

		private void RestartSumatra(string sFile)
        {
			if ((SumatraWindowHandle != (IntPtr)0) & (SumatraProcess != null))
			{
				// Ver qual o código do comando no arquivo Commands.h: CmdClose
				SendMessage(SumatraWindowHandle, 0x111, (IntPtr)204, (IntPtr)0);
				SumatraProcess.Kill();
				pSumatraWindowHandle = (IntPtr)0;
			}
            var PSInfo = new ProcessStartInfo
            {
                FileName = SumatraPDFPath + "SumatraPDF.exe",
                Arguments = "-invert-colors -plugin " + base.Handle + " \"" + sFile + "\""
            };
            SumatraProcess = Process.Start(PSInfo);			
		}

		public void LoadFile(string sFile)
		{
			if (SumatraWindowHandle != (IntPtr)0)
			{
				// Ver qual o código do comando no arquivo Commands.h: CmdClose
				SendMessage(SumatraWindowHandle, 0x111, (IntPtr)204, (IntPtr)0);
				SendDDECommand("[Open(\"" + sFile + "\")]");				
			} else RestartSumatra(sFile);
			SumatraMessage(this, new SumatraMessageEventArgs { CallBackReturn = 0, Msg = "[FileOpen(\"" + sFile + "\")]" });
			sCurrentFile = sFile;
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
				SendMessage(SumatraWindowHandle, 0x111, (IntPtr)225, (IntPtr)0);
			}
		}

		public void CopySelection()
		{
			if (SumatraWindowHandle != (IntPtr)0)
			{
				// Ver qual o código do comando no arquivo Commands.h: CmdCopySelection
				SendMessage(SumatraWindowHandle, 0x111, (IntPtr)228, (IntPtr)0);
			}
		}

		public void GotoPage(int nPage)
		{
			SendDDECommand("[GotoPage(\"" + sCurrentFile + "\", " + nPage + ")]");
		}

	}
}
