using System;

namespace SumatraPDFControl
{
    partial class SumatraPDFControl
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
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
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.BackgroundImage = global::SumatraPDFControl.Properties.Resources.SumatraPDF_48x48x32;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.DoubleBuffered = true;
            this.Name = "SumatraPDFControl";
            this.Size = new System.Drawing.Size(460, 334);
            this.Load += new System.EventHandler(this.SumatraPDFControl_Load);
            this.Resize += new System.EventHandler(this.SumatraPDFControl_Resize);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
