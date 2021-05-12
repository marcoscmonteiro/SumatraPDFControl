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
            this.Name = "SumatraPDFControl";
            this.Size = new System.Drawing.Size(460, 334);
            this.Resize += new System.EventHandler(this.SumatraPDFControl_Resize);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
