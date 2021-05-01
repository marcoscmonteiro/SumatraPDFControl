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
        public Form1()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                sumatraPDFControl1.LoadFile(openFileDialog1.FileName);
            }

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            sumatraPDFControl1.GotoPage(int.Parse(toolStripTextBox1.Text));
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            sumatraPDFControl1.CopySelection();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            sumatraPDFControl1.ToogleToolBar();
        }

        private void sumatraPDFControl1_SumatraMessage(object sender, SumatraPDFControl.SumatraPDFControl.SumatraMessageEventArgs e)
        {
            textBox1.Text += e.Msg + " - " + e.CallBackReturn + System.Environment.NewLine;
        }

        private void sumatraPDFControl1_PageChangedMessage(object sender, SumatraPDFControl.SumatraPDFControl.PageChangedEventArgs e)
        {
            toolStripLabel2.Text = e.Page.ToString() + " - " + e.NamedDest;
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
            toolStripLabel4.Text = e.ZoomLevel.ToString() + " - Fit Widht: " + e.FitWidth.ToString() + " - Fit Page: " + e.FitPage.ToString() + " - Fit Content:" + e.FitContent.ToString();
        }

        private void sumatraPDFControl1_LinkClickedMessage(object sender, SumatraPDFControl.SumatraPDFControl.LinkClickedEventArgs e)
        {
            textBox1.Text += "LinkClicked: " + e.LinkText + System.Environment.NewLine;
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            sumatraPDFControl1.TextSearch(toolStripTextBox1.Text, true);
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
            sumatraPDFControl1.GotoNamedDest(toolStripTextBox1.Text);
        }
    }
}
