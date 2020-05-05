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
    }
}
