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
				m.Result = ParseSumatraMessage(sMsg);
			}
		}

		private IntPtr ParseSumatraMessage(string sMsg)
		{
			if (MessageBox.Show("Mensagem: " + sMsg + " - Sim=1, Não=0", "Responda", MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				return (IntPtr)1;
			}
			return (IntPtr)0;
		}

		private object[] ParseString(string Format, string FStr)
		{
			List<object> lObject = new List<object>();
			int startIndex = 0;
			while (Format.IndexOf("%", startIndex) != 0)
			{
			}
			return lObject.ToArray();
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

		public void LoadFile(string sFile)
		{
			if ((pSumatraWindowHandle != (IntPtr)0) & (SumatraProcess != null))
			{
				SumatraProcess.Close();
				pSumatraWindowHandle = (IntPtr)0;
			}
			ProcessStartInfo PSInfo = new ProcessStartInfo();
			PSInfo.FileName = SumatraPDFPath + "SumatraPDF.exe";
			PSInfo.Arguments = "-plugin " + base.Handle + " \"" + sFile + "\"";
			SumatraProcess = Process.Start(PSInfo);
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
				SendMessage(SumatraWindowHandle, 273u, (IntPtr)440, (IntPtr)0);
			}
		}

		public void GotoPage(int nPage)
		{
			SendDDECommand("[GotoPage(\"" + sCurrentFile + "\", " + nPage + ")]");
		}

	}
}
