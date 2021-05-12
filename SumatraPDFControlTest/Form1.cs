using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SumatraPDFControlTest
{
    public partial class Form1 : Form
    {
        public string sFilename;

        public Form1()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                sumatraPDFControl1.LoadFile(openFileDialog1.FileName, int.Parse(toolText.Text=="" ? "1" : toolText.Text));
            }

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            sumatraPDFControl1.Page = int.Parse(toolText.Text);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            sumatraPDFControl1.CopySelection();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            //sumatraPDFControl1.ToogleToolBar();
            sumatraPDFControl1.ToolBarVisible = !sumatraPDFControl1.ToolBarVisible;
        }

        private void sumatraPDFControl1_SumatraMessage(object sender, SumatraPDFControl.SumatraPDFControl.SumatraMessageEventArgs e)
        {
            textBox1.Text += e.Msg + " - " + e.CallBackReturn + System.Environment.NewLine;
            if (e.Msg == "[StartupFinished()]" || e.Msg == "[FileOpen()]") 
                toolPage.Text = sumatraPDFControl1.Page.ToString() + " - " + sumatraPDFControl1.NamedDest;
        }

        private void sumatraPDFControl1_PageChangedMessage(object sender, SumatraPDFControl.SumatraPDFControl.PageChangedEventArgs e)
        {
            toolPage.Text = e.Page.ToString() + " / " + e.NamedDest;
        }

        private void sumatraPDFControl1_ContextMenuMessage(object sender, SumatraPDFControl.SumatraPDFControl.ContextMenuEventArgs e)
        {
            e.OpenSumatraContextMenu = true;
            // To do: alterar para retorno false e criar o próprio menu de contexto customizado
            textBox1.Text += "ContextMenu - X: " + e.X.ToString() + " - Y: " + e.Y.ToString() + System.Environment.NewLine;
        }

        private void sumatraPDFControl1_KeyPressedMessage(object sender, SumatraPDFControl.SumatraPDFControl.KeyPressedEventArgs e)
        {
            textBox1.Text += "KeyPressed: " + e.Key + System.Environment.NewLine;
        }

        private void sumatraPDFControl1_ZoomChangedMessage(object sender, SumatraPDFControl.SumatraPDFControl.ZoomChangedEventArgs e)
        {
            toolZoom.Text = e.Zoom.ToString();
            toolZoomVirtual.Text = e.ZoomVirtual.ToString();
        }

        private void sumatraPDFControl1_LinkClickedMessage(object sender, SumatraPDFControl.SumatraPDFControl.LinkClickedEventArgs e)
        {
            textBox1.Text += "LinkClicked: " + e.LinkText + System.Environment.NewLine;
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            sumatraPDFControl1.TextSearch(toolText.Text, true);
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            sumatraPDFControl1.TextSearchNext(false);
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            sumatraPDFControl1.TextSearchNext(true);
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            sumatraPDFControl1.NamedDest = toolText.Text;
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            toolText.Text = sumatraPDFControl1.Page.ToString();
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            sumatraPDFControl1.TocVisible = !sumatraPDFControl1.TocVisible;
        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (toolPageMode.Text != String.Empty)
                sumatraPDFControl1.DisplayMode = (SumatraPDFControl.SumatraPDFControl.DisplayModeEnum)toolPageMode.SelectedIndex; // int.Parse(toolPageMode.Text.Split('-')[0]);

        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            toolPageMode.Text = sumatraPDFControl1.DisplayMode.ToString();
        }

        private void toolSetZoom_Click(object sender, EventArgs e)
        {
            sumatraPDFControl1.Zoom = float.Parse(toolText.Text);
        }

        private void toolZoomVirtualSet_SelectedIndexChanged(object sender, EventArgs e)
        {
            sumatraPDFControl1.ZoomVirtual = (SumatraPDFControl.SumatraPDFControl.ZoomVirtuamEnum)toolZoomVirtualSet.SelectedIndex;
        }

        private void toolGetZoomVirtual_Click(object sender, EventArgs e)
        {
            toolZoomVirtualSet.Text = sumatraPDFControl1.ZoomVirtual.ToString();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            if (sFilename!=string.Empty)
            {
                sumatraPDFControl1.LoadFile(sFilename);
                sFilename = string.Empty;
            }
        }
    }
}
