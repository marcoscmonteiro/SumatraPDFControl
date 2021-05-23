using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using SumatraPDF;
using static SumatraPDF.SumatraPDFControl;

namespace SumatraPDFControlTest
{
    public partial class Form1 : Form
    {
        public string Filename = string.Empty;
        public Boolean NewSumatraPDFProcess = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                SumatraPDFControl.LoadFile(openFileDialog1.FileName, int.Parse(toolText.Text == "" ? "1" : toolText.Text));
            }

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            SumatraPDFControl.Page = int.Parse(toolText.Text);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            SumatraPDFControl.CopySelection();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            //SumatraPDFControl.ToogleToolBar();
            SumatraPDFControl.ToolBarVisible = !SumatraPDFControl.ToolBarVisible;
        }

        private void SumatraPDFControl_SumatraMessage(object sender, SumatraMessageEventArgs e)
        {
            AddText(e.Msg + " - " + e.CallBackReturn);
            if (e.Msg == "[StartupFinished()]" || e.Msg == "[FileOpened()]")
                toolPage.Text = SumatraPDFControl.Page.ToString() + " - " + SumatraPDFControl.NamedDest;
        }

        private void SumatraPDFControl_PageChangedMessage(object sender, PageChangedEventArgs e)
        {
            toolPage.Text = e.Page.ToString() + " / " + e.NamedDest;
        }

        private void SumatraPDFControl_ContextMenuMessage(object sender, ContextMenuOpenEventArgs e)
        {
            // To do: alterar para retorno false e criar o próprio menu de contexto customizado
            AddText("ContextMenu - X: " + e.X.ToString() + " - Y: " + e.Y.ToString());
            if (SumatraPDFControl.ContextMenuStrip != null)
            {
                SumatraPDFControl.ContextMenuStrip.Show(SumatraPDFControl, e.X, e.Y);
                e.Handled = true;
            }
        }

        private void SumatraPDFControl_ZoomChangedMessage(object sender, ZoomChangedEventArgs e)
        {
            toolZoom.Text = e.Zoom.ToString();
            toolZoomVirtual.Text = e.ZoomVirtual.ToString();
            // Example limiting zoom in 300%
            if (e.Zoom > 300) SumatraPDFControl.Zoom = 300;
        }

        private void SumatraPDFControl_LinkClickedMessage(object sender, SumatraPDF.SumatraPDFControl.LinkClickedEventArgs e)
        {
            AddText("LinkClicked: " + e.LinkText);
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            SumatraPDFControl.TextSearch(toolText.Text, true);
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            SumatraPDFControl.TextSearchNext(false);
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            SumatraPDFControl.TextSearchNext(true);
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            SumatraPDFControl.NamedDest = toolText.Text;
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            toolText.Text = SumatraPDFControl.Page.ToString();
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            SumatraPDFControl.TocVisible = !SumatraPDFControl.TocVisible;
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (toolPageMode.Text != String.Empty)
                SumatraPDFControl.DisplayMode = (DisplayModeEnum)toolPageMode.SelectedIndex;

        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            toolPageMode.Text = SumatraPDFControl.DisplayMode.ToString();
        }

        private void toolSetZoom_Click(object sender, EventArgs e)
        {
            SumatraPDFControl.Zoom = float.Parse(toolText.Text);
        }

        private void toolZoomVirtualSet_SelectedIndexChanged(object sender, EventArgs e)
        {
            SumatraPDFControl.ZoomVirtual = (ZoomVirtualEnum)(toolZoomVirtualSet.SelectedIndex * -1);
        }

        private void toolGetZoomVirtual_Click(object sender, EventArgs e)
        {
            toolZoomVirtualSet.Text = SumatraPDFControl.ZoomVirtual.ToString();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            if (Filename != string.Empty)
            {
                SumatraPDFControl.LoadFile(Filename, NewSumatraInstance: NewSumatraPDFProcess);
                Filename = string.Empty;
            }
        }

        private void SumatraPDFControl_ScrollStateChanged(object sender, ScrollStateEventArgs e)
        {
            toolScrollPosX.Text = e.ScrollState.X.ToString();
            toolScrollPosY.Text = e.ScrollState.Y.ToString();
            toolScrollPosPage.Text = e.ScrollState.Page.ToString();
        }

        private void toolSetScrollPos_Click(object sender, EventArgs e)
        {
            Match mSP = Regex.Match(toolText.Text, @"(?<page>.+)\,(?<x>.+)\,\s*(?<y>.+)");
            var pScrollPosition = new ScrollStateStruct(
                int.Parse(mSP.Result("${page}")),
                Double.Parse(mSP.Result("${x}")),
                Double.Parse(mSP.Result("${y}"))
            );
            SumatraPDFControl.ScrollState = pScrollPosition;
        }

        private void SumatraPDFControl_DisplayModeChangedMessage(object sender, DisplayModeChangedEventArgs e)
        {
            toolDisplayMode.Text = e.DisplayMode.ToString();
        }


        private void toolGetRotation_Click(object sender, EventArgs e)
        {
            toolText.Text = SumatraPDFControl.Rotation.ToString();
        }

        private void toolSetRotation_Click(object sender, EventArgs e)
        {
            SumatraPDFControl.RotateBy((RotationEnum)int.Parse(toolText.Text));
        }

        private void SumatraPDFControl_KeyDown(object sender, KeyEventArgs e)
        {
            AddText("KeyDown: " + e.KeyData.ToString());
            e.Handled = false;
            e.SuppressKeyPress = false;
        }
        private void SumatraPDFControl_KeyPressedMessage(object sender, KeyPressEventArgs e)
        {
            AddText("KeyPressed: " + e.KeyChar);
        }

        private void SumatraPDFControl_KeyUp(object sender, KeyEventArgs e)
        {
            AddText("KeyUp: " + e.KeyData.ToString());
        }

        private void AddText(string text)
        {
            textBox1.Text = DateTime.Now.ToLongTimeString() + ": " + text + System.Environment.NewLine + textBox1.Text;
            textBox1.SelectionStart = 0;
        }

        private void SumatraPDFControl_MouseClick(object sender, MouseEventArgs e)
        {
            AddText("MouseClick: " + e.ToString());
        }

        private void SumatraPDFControl_MouseDown(object sender, MouseEventArgs e)
        {
            AddText("MouseDown: " + e.Button.ToString());
        }

        private void SumatraPDFControl_MouseUp(object sender, MouseEventArgs e)
        {
            AddText("MouseUp: " + e.Button.ToString());
        }

        private void SumatraPDFControl_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            AddText("MouseDoubleClick: " + e.Button.ToString() + string.Format(" - ({0},{1})", e.X, e.Y));
        }

        private void SumatraPDFControl_Scroll(object sender, ScrollEventArgs e)
        {
            AddText("Scroll: " + e.OldValue.ToString() + " - " + e.NewValue.ToString() + " - " + e.Type.ToString() + " - " + e.ScrollOrientation.ToString());
        }
    
    }
}
