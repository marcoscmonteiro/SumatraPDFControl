namespace SumatraPDFControlTest
{
    partial class Form1
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
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.toolSumatraControl = new System.Windows.Forms.ToolStrip();
            this.toolOpen = new System.Windows.Forms.ToolStripButton();
            this.toolText = new System.Windows.Forms.ToolStripTextBox();
            this.toolGotoPage = new System.Windows.Forms.ToolStripButton();
            this.toolGetPage = new System.Windows.Forms.ToolStripButton();
            this.toolGotoNamedDest = new System.Windows.Forms.ToolStripButton();
            this.toolSearchText = new System.Windows.Forms.ToolStripButton();
            this.toolSearchForward = new System.Windows.Forms.ToolStripButton();
            this.toolSearchBackward = new System.Windows.Forms.ToolStripButton();
            this.toolSetZoom = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolCopySelection = new System.Windows.Forms.ToolStripButton();
            this.toolToogleToolbar = new System.Windows.Forms.ToolStripButton();
            this.toolToogleToc = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolPageLbl = new System.Windows.Forms.ToolStripLabel();
            this.toolPage = new System.Windows.Forms.ToolStripLabel();
            this.toolZoomLbl = new System.Windows.Forms.ToolStripLabel();
            this.toolZoom = new System.Windows.Forms.ToolStripLabel();
            this.toolZoomVirtualLbl = new System.Windows.Forms.ToolStripLabel();
            this.toolZoomVirtual = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolPageModeLbl = new System.Windows.Forms.ToolStripLabel();
            this.toolPageMode = new System.Windows.Forms.ToolStripComboBox();
            this.toolGetDisplayMode = new System.Windows.Forms.ToolStripButton();
            this.toolZoomVirtualSetLbl = new System.Windows.Forms.ToolStripLabel();
            this.toolZoomVirtualSet = new System.Windows.Forms.ToolStripComboBox();
            this.toolGetZoomVirtual = new System.Windows.Forms.ToolStripButton();
            this.toolScrollPosLbl = new System.Windows.Forms.ToolStripLabel();
            this.toolScrollPosX = new System.Windows.Forms.ToolStripLabel();
            this.toolScrollPosY = new System.Windows.Forms.ToolStripLabel();
            this.toolScrollPosPage = new System.Windows.Forms.ToolStripLabel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.sumatraPDFControl1 = new SumatraPDFControl.SumatraPDFControl();
            this.toolSetScrollPos = new System.Windows.Forms.ToolStripButton();
            this.toolSumatraControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolSumatraControl
            // 
            this.toolSumatraControl.Dock = System.Windows.Forms.DockStyle.Left;
            this.toolSumatraControl.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolOpen,
            this.toolText,
            this.toolGotoPage,
            this.toolGetPage,
            this.toolGotoNamedDest,
            this.toolSearchText,
            this.toolSearchForward,
            this.toolSearchBackward,
            this.toolSetScrollPos,
            this.toolSetZoom,
            this.toolStripSeparator1,
            this.toolCopySelection,
            this.toolToogleToolbar,
            this.toolToogleToc,
            this.toolStripSeparator2,
            this.toolPageLbl,
            this.toolPage,
            this.toolZoomLbl,
            this.toolZoom,
            this.toolZoomVirtualLbl,
            this.toolZoomVirtual,
            this.toolStripSeparator3,
            this.toolPageModeLbl,
            this.toolPageMode,
            this.toolGetDisplayMode,
            this.toolZoomVirtualSetLbl,
            this.toolZoomVirtualSet,
            this.toolGetZoomVirtual,
            this.toolScrollPosLbl,
            this.toolScrollPosX,
            this.toolScrollPosY,
            this.toolScrollPosPage});
            this.toolSumatraControl.Location = new System.Drawing.Point(0, 0);
            this.toolSumatraControl.Name = "toolSumatraControl";
            this.toolSumatraControl.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolSumatraControl.Size = new System.Drawing.Size(108, 692);
            this.toolSumatraControl.TabIndex = 1;
            this.toolSumatraControl.Text = "toolStrip1";
            this.toolSumatraControl.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip1_ItemClicked);
            // 
            // toolOpen
            // 
            this.toolOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolOpen.Image = ((System.Drawing.Image)(resources.GetObject("toolOpen.Image")));
            this.toolOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolOpen.Name = "toolOpen";
            this.toolOpen.Size = new System.Drawing.Size(105, 19);
            this.toolOpen.Text = "Open";
            this.toolOpen.ToolTipText = "Open";
            this.toolOpen.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolText
            // 
            this.toolText.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolText.Name = "toolText";
            this.toolText.Size = new System.Drawing.Size(103, 23);
            // 
            // toolGotoPage
            // 
            this.toolGotoPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolGotoPage.Image = ((System.Drawing.Image)(resources.GetObject("toolGotoPage.Image")));
            this.toolGotoPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolGotoPage.Name = "toolGotoPage";
            this.toolGotoPage.Size = new System.Drawing.Size(105, 19);
            this.toolGotoPage.Text = "GotoPage";
            this.toolGotoPage.ToolTipText = "Goto Page";
            this.toolGotoPage.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolGetPage
            // 
            this.toolGetPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolGetPage.Image = ((System.Drawing.Image)(resources.GetObject("toolGetPage.Image")));
            this.toolGetPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolGetPage.Name = "toolGetPage";
            this.toolGetPage.Size = new System.Drawing.Size(105, 19);
            this.toolGetPage.Text = "Get Page";
            this.toolGetPage.Click += new System.EventHandler(this.toolStripButton9_Click);
            // 
            // toolGotoNamedDest
            // 
            this.toolGotoNamedDest.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolGotoNamedDest.Image = ((System.Drawing.Image)(resources.GetObject("toolGotoNamedDest.Image")));
            this.toolGotoNamedDest.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolGotoNamedDest.Name = "toolGotoNamedDest";
            this.toolGotoNamedDest.Size = new System.Drawing.Size(105, 19);
            this.toolGotoNamedDest.Tag = "";
            this.toolGotoNamedDest.Text = "GotoNamedDest";
            this.toolGotoNamedDest.ToolTipText = "Goto Named Destination";
            this.toolGotoNamedDest.Click += new System.EventHandler(this.toolStripButton8_Click);
            // 
            // toolSearchText
            // 
            this.toolSearchText.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolSearchText.Image = ((System.Drawing.Image)(resources.GetObject("toolSearchText.Image")));
            this.toolSearchText.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolSearchText.Name = "toolSearchText";
            this.toolSearchText.Size = new System.Drawing.Size(105, 19);
            this.toolSearchText.Text = "SearchText";
            this.toolSearchText.ToolTipText = "Search Text";
            this.toolSearchText.Click += new System.EventHandler(this.toolStripButton5_Click);
            // 
            // toolSearchForward
            // 
            this.toolSearchForward.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolSearchForward.Image = ((System.Drawing.Image)(resources.GetObject("toolSearchForward.Image")));
            this.toolSearchForward.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolSearchForward.Name = "toolSearchForward";
            this.toolSearchForward.Size = new System.Drawing.Size(105, 19);
            this.toolSearchForward.Text = "SearchForward";
            this.toolSearchForward.ToolTipText = "Search Forward";
            this.toolSearchForward.Click += new System.EventHandler(this.toolStripButton7_Click);
            // 
            // toolSearchBackward
            // 
            this.toolSearchBackward.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolSearchBackward.Image = ((System.Drawing.Image)(resources.GetObject("toolSearchBackward.Image")));
            this.toolSearchBackward.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolSearchBackward.Name = "toolSearchBackward";
            this.toolSearchBackward.Size = new System.Drawing.Size(105, 19);
            this.toolSearchBackward.Text = "SearchBackward";
            this.toolSearchBackward.ToolTipText = "Search Backward";
            this.toolSearchBackward.Click += new System.EventHandler(this.toolStripButton6_Click);
            // 
            // toolSetZoom
            // 
            this.toolSetZoom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolSetZoom.Image = ((System.Drawing.Image)(resources.GetObject("toolSetZoom.Image")));
            this.toolSetZoom.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolSetZoom.Name = "toolSetZoom";
            this.toolSetZoom.Size = new System.Drawing.Size(105, 19);
            this.toolSetZoom.Text = "Set Zoom";
            this.toolSetZoom.ToolTipText = "Set Zoom";
            this.toolSetZoom.Click += new System.EventHandler(this.toolSetZoom_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(105, 6);
            // 
            // toolCopySelection
            // 
            this.toolCopySelection.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolCopySelection.Image = ((System.Drawing.Image)(resources.GetObject("toolCopySelection.Image")));
            this.toolCopySelection.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolCopySelection.Name = "toolCopySelection";
            this.toolCopySelection.Size = new System.Drawing.Size(105, 19);
            this.toolCopySelection.Text = "CopySelection";
            this.toolCopySelection.ToolTipText = "Copy Selection";
            this.toolCopySelection.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // toolToogleToolbar
            // 
            this.toolToogleToolbar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolToogleToolbar.Image = ((System.Drawing.Image)(resources.GetObject("toolToogleToolbar.Image")));
            this.toolToogleToolbar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolToogleToolbar.Name = "toolToogleToolbar";
            this.toolToogleToolbar.Size = new System.Drawing.Size(105, 19);
            this.toolToogleToolbar.Text = "ToogleToolbar";
            this.toolToogleToolbar.ToolTipText = "Toogle Toolbar";
            this.toolToogleToolbar.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // toolToogleToc
            // 
            this.toolToogleToc.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolToogleToc.Image = ((System.Drawing.Image)(resources.GetObject("toolToogleToc.Image")));
            this.toolToogleToc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolToogleToc.Name = "toolToogleToc";
            this.toolToogleToc.Size = new System.Drawing.Size(105, 19);
            this.toolToogleToc.Text = "ToogleToc";
            this.toolToogleToc.ToolTipText = "Toogle Toc";
            this.toolToogleToc.Click += new System.EventHandler(this.toolStripButton10_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(105, 6);
            // 
            // toolPageLbl
            // 
            this.toolPageLbl.Name = "toolPageLbl";
            this.toolPageLbl.Size = new System.Drawing.Size(105, 15);
            this.toolPageLbl.Text = "Pág./Named Dest:";
            // 
            // toolPage
            // 
            this.toolPage.Name = "toolPage";
            this.toolPage.Size = new System.Drawing.Size(105, 15);
            this.toolPage.Text = "---";
            // 
            // toolZoomLbl
            // 
            this.toolZoomLbl.Name = "toolZoomLbl";
            this.toolZoomLbl.Size = new System.Drawing.Size(105, 15);
            this.toolZoomLbl.Text = "Zoom:";
            // 
            // toolZoom
            // 
            this.toolZoom.Name = "toolZoom";
            this.toolZoom.Size = new System.Drawing.Size(105, 15);
            this.toolZoom.Text = "---";
            // 
            // toolZoomVirtualLbl
            // 
            this.toolZoomVirtualLbl.Name = "toolZoomVirtualLbl";
            this.toolZoomVirtualLbl.Size = new System.Drawing.Size(105, 15);
            this.toolZoomVirtualLbl.Text = "Zoom Virtual:";
            // 
            // toolZoomVirtual
            // 
            this.toolZoomVirtual.Name = "toolZoomVirtual";
            this.toolZoomVirtual.Size = new System.Drawing.Size(105, 15);
            this.toolZoomVirtual.Text = "---";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(105, 6);
            // 
            // toolPageModeLbl
            // 
            this.toolPageModeLbl.Name = "toolPageModeLbl";
            this.toolPageModeLbl.Size = new System.Drawing.Size(105, 15);
            this.toolPageModeLbl.Text = "Display Mode:";
            // 
            // toolPageMode
            // 
            this.toolPageMode.Items.AddRange(new object[] {
            "0 - Automatic",
            "1 - SinglePage",
            "2 - Facing",
            "3 - BookView",
            "4 - Continuous",
            "5 - ContinuousFacing",
            "6 - ContinuousBookView"});
            this.toolPageMode.Name = "toolPageMode";
            this.toolPageMode.Size = new System.Drawing.Size(103, 23);
            this.toolPageMode.ToolTipText = "Page Mode";
            this.toolPageMode.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox1_SelectedIndexChanged);
            this.toolPageMode.Click += new System.EventHandler(this.toolStripComboBox1_Click);
            // 
            // toolGetDisplayMode
            // 
            this.toolGetDisplayMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolGetDisplayMode.Image = ((System.Drawing.Image)(resources.GetObject("toolGetDisplayMode.Image")));
            this.toolGetDisplayMode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolGetDisplayMode.Name = "toolGetDisplayMode";
            this.toolGetDisplayMode.Size = new System.Drawing.Size(105, 19);
            this.toolGetDisplayMode.Text = "Get Display Mode";
            this.toolGetDisplayMode.Click += new System.EventHandler(this.toolStripButton11_Click);
            // 
            // toolZoomVirtualSetLbl
            // 
            this.toolZoomVirtualSetLbl.Name = "toolZoomVirtualSetLbl";
            this.toolZoomVirtualSetLbl.Size = new System.Drawing.Size(105, 15);
            this.toolZoomVirtualSetLbl.Text = "Zoom Virtual:";
            // 
            // toolZoomVirtualSet
            // 
            this.toolZoomVirtualSet.Items.AddRange(new object[] {
            "0 - None",
            "1 - FitPage",
            "2 - FitWidth",
            "3 - FitContent"});
            this.toolZoomVirtualSet.Name = "toolZoomVirtualSet";
            this.toolZoomVirtualSet.Size = new System.Drawing.Size(103, 23);
            this.toolZoomVirtualSet.SelectedIndexChanged += new System.EventHandler(this.toolZoomVirtualSet_SelectedIndexChanged);
            // 
            // toolGetZoomVirtual
            // 
            this.toolGetZoomVirtual.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolGetZoomVirtual.Image = ((System.Drawing.Image)(resources.GetObject("toolGetZoomVirtual.Image")));
            this.toolGetZoomVirtual.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolGetZoomVirtual.Name = "toolGetZoomVirtual";
            this.toolGetZoomVirtual.Size = new System.Drawing.Size(105, 19);
            this.toolGetZoomVirtual.Text = "Get Zoom Virtual";
            this.toolGetZoomVirtual.Click += new System.EventHandler(this.toolGetZoomVirtual_Click);
            // 
            // toolScrollPosLbl
            // 
            this.toolScrollPosLbl.Name = "toolScrollPosLbl";
            this.toolScrollPosLbl.Size = new System.Drawing.Size(105, 15);
            this.toolScrollPosLbl.Text = "Scroll Pos:";
            // 
            // toolScrollPosX
            // 
            this.toolScrollPosX.Name = "toolScrollPosX";
            this.toolScrollPosX.Size = new System.Drawing.Size(105, 15);
            this.toolScrollPosX.Text = "---";
            // 
            // toolScrollPosY
            // 
            this.toolScrollPosY.Name = "toolScrollPosY";
            this.toolScrollPosY.Size = new System.Drawing.Size(105, 15);
            this.toolScrollPosY.Text = "---";
            // 
            // toolScrollPosPage
            // 
            this.toolScrollPosPage.Name = "toolScrollPosPage";
            this.toolScrollPosPage.Size = new System.Drawing.Size(105, 15);
            this.toolScrollPosPage.Text = "---";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(127, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(1291, 58);
            this.textBox1.TabIndex = 2;
            // 
            // sumatraPDFControl1
            // 
            this.sumatraPDFControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sumatraPDFControl1.DisplayMode = SumatraPDFControl.SumatraPDFControl.DisplayModeEnum.Automatic;
            this.sumatraPDFControl1.Location = new System.Drawing.Point(127, 64);
            this.sumatraPDFControl1.Name = "sumatraPDFControl1";
            this.sumatraPDFControl1.NamedDest = null;
            this.sumatraPDFControl1.Page = 0;
            this.sumatraPDFControl1.Size = new System.Drawing.Size(1291, 628);
            this.sumatraPDFControl1.SumatraPDFPath = "C:\\Users\\marco\\source\\repos\\sumatrapdf\\out\\dbg64\\";
            this.sumatraPDFControl1.TabIndex = 0;
            this.sumatraPDFControl1.TocVisible = false;
            this.sumatraPDFControl1.ToolBarVisible = false;
            this.sumatraPDFControl1.Zoom = 0F;
            this.sumatraPDFControl1.ZoomVirtual = SumatraPDFControl.SumatraPDFControl.ZoomVirtuamEnum.None;
            this.sumatraPDFControl1.SumatraMessage += new System.EventHandler<SumatraPDFControl.SumatraPDFControl.SumatraMessageEventArgs>(this.sumatraPDFControl1_SumatraMessage);
            this.sumatraPDFControl1.PageChangedMessage += new System.EventHandler<SumatraPDFControl.SumatraPDFControl.PageChangedEventArgs>(this.sumatraPDFControl1_PageChangedMessage);
            this.sumatraPDFControl1.ContextMenuMessage += new System.EventHandler<SumatraPDFControl.SumatraPDFControl.ContextMenuEventArgs>(this.sumatraPDFControl1_ContextMenuMessage);
            this.sumatraPDFControl1.KeyPressedMessage += new System.EventHandler<SumatraPDFControl.SumatraPDFControl.KeyPressedEventArgs>(this.sumatraPDFControl1_KeyPressedMessage);
            this.sumatraPDFControl1.ZoomChangedMessage += new System.EventHandler<SumatraPDFControl.SumatraPDFControl.ZoomChangedEventArgs>(this.sumatraPDFControl1_ZoomChangedMessage);
            this.sumatraPDFControl1.LinkClickedMessage += new System.EventHandler<SumatraPDFControl.SumatraPDFControl.LinkClickedEventArgs>(this.sumatraPDFControl1_LinkClickedMessage);
            this.sumatraPDFControl1.ScrollPositionMessage += new System.EventHandler<System.EventArgs>(this.sumatraPDFControl1_ScrollPositionMessage);
            // 
            // toolSetScrollPos
            // 
            this.toolSetScrollPos.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolSetScrollPos.Image = ((System.Drawing.Image)(resources.GetObject("toolSetScrollPos.Image")));
            this.toolSetScrollPos.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolSetScrollPos.Name = "toolSetScrollPos";
            this.toolSetScrollPos.Size = new System.Drawing.Size(105, 19);
            this.toolSetScrollPos.Text = "Set ScrollPos";
            this.toolSetScrollPos.Click += new System.EventHandler(this.toolSetScrollPos_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1418, 692);
            this.Controls.Add(this.sumatraPDFControl1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.toolSumatraControl);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.toolSumatraControl.ResumeLayout(false);
            this.toolSumatraControl.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SumatraPDFControl.SumatraPDFControl sumatraPDFControl1;
        private System.Windows.Forms.ToolStrip toolSumatraControl;
        private System.Windows.Forms.ToolStripButton toolOpen;
        private System.Windows.Forms.ToolStripTextBox toolText;
        private System.Windows.Forms.ToolStripButton toolGotoPage;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripButton toolCopySelection;
        private System.Windows.Forms.ToolStripButton toolToogleToolbar;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ToolStripLabel toolPageLbl;
        private System.Windows.Forms.ToolStripLabel toolPage;
        private System.Windows.Forms.ToolStripLabel toolZoomLbl;
        private System.Windows.Forms.ToolStripLabel toolZoom;
        private System.Windows.Forms.ToolStripButton toolSearchText;
        private System.Windows.Forms.ToolStripButton toolSearchBackward;
        private System.Windows.Forms.ToolStripButton toolSearchForward;
        private System.Windows.Forms.ToolStripButton toolGotoNamedDest;
        private System.Windows.Forms.ToolStripButton toolGetPage;
        private System.Windows.Forms.ToolStripButton toolToogleToc;
        private System.Windows.Forms.ToolStripComboBox toolPageMode;
        private System.Windows.Forms.ToolStripButton toolGetDisplayMode;
        private System.Windows.Forms.ToolStripLabel toolZoomVirtualLbl;
        private System.Windows.Forms.ToolStripLabel toolZoomVirtual;
        private System.Windows.Forms.ToolStripButton toolSetZoom;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripLabel toolPageModeLbl;
        private System.Windows.Forms.ToolStripLabel toolZoomVirtualSetLbl;
        private System.Windows.Forms.ToolStripComboBox toolZoomVirtualSet;
        private System.Windows.Forms.ToolStripButton toolGetZoomVirtual;
        private System.Windows.Forms.ToolStripLabel toolScrollPosLbl;
        private System.Windows.Forms.ToolStripLabel toolScrollPosX;
        private System.Windows.Forms.ToolStripLabel toolScrollPosY;
        private System.Windows.Forms.ToolStripLabel toolScrollPosPage;
        private System.Windows.Forms.ToolStripButton toolSetScrollPos;
    }
}

