using System;

namespace SumatraPDF
{
    partial class SumatraPDFControl
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Cleaning resources being used
        /// </summary>
        /// <param name="disposing">true if necessary to dispose managed resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (pSumatraWindowHandle != (IntPtr)0)
            {
                CloseDocument();
                pSumatraWindowHandleList.Remove(pSumatraWindowHandle);
            }
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Designer de Componentes

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // SumatraPDFControl
            // 
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Size = new System.Drawing.Size(460, 334);
            this.Resize += new System.EventHandler(this.SumatraPDFControl_Resize);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
