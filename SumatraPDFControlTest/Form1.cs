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
            // Ao pressionar 'q' não permite o fechamento da visualização
            if (e.Msg == "[KeyPressed(113)]") e.CallBackReturn = 1;

            textBox1.Text = e.Msg + " - " + e.CallBackReturn + System.Environment.NewLine + textBox1.Text;
        }

        private void sumatraPDFControl1_PageChangedMessage(object sender, SumatraPDFControl.SumatraPDFControl.PageChangedEventArgs e)
        {
            toolStripLabel2.Text = e.Page.ToString();
        }

        private void sumatraPDFControl1_ContextMenuMessage(object sender, SumatraPDFControl.SumatraPDFControl.ContextMenuEventArgs e)
        {
            e.OpenSumatraContextMenu = true;
            // To do: alterar para retorno false e criar o próprio menu de contexto customizado
        }
    }
}
