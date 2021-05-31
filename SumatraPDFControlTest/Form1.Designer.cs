namespace SumatraPDFControlTest
{
    partial class FormTest
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTest));
            this.toolSumatraControl = new System.Windows.Forms.ToolStrip();
            this.toolOpen = new System.Windows.Forms.ToolStripButton();
            this.toolText = new System.Windows.Forms.ToolStripTextBox();
            this.toolGotoPage = new System.Windows.Forms.ToolStripButton();
            this.toolGotoNamedDest = new System.Windows.Forms.ToolStripButton();
            this.toolSearchText = new System.Windows.Forms.ToolStripButton();
            this.toolSearchForward = new System.Windows.Forms.ToolStripButton();
            this.toolSearchBackward = new System.Windows.Forms.ToolStripButton();
            this.toolSetScrollPos = new System.Windows.Forms.ToolStripButton();
            this.toolSetZoom = new System.Windows.Forms.ToolStripButton();
            this.toolSetRotation = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolGetRotation = new System.Windows.Forms.ToolStripButton();
            this.toolGetPage = new System.Windows.Forms.ToolStripButton();
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
            this.toolDisplayModeLbl = new System.Windows.Forms.ToolStripLabel();
            this.toolDisplayMode = new System.Windows.Forms.ToolStripLabel();
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
            this.toolOpenPrintDialog = new System.Windows.Forms.ToolStripButton();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.contextMenuSumatraPDF = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tootStripCopySelection = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripToogleToolbar = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripToogleToc = new System.Windows.Forms.ToolStripMenuItem();
            this.cbShowScrollState = new System.Windows.Forms.CheckBox();
            this.SumatraPDFControl = new SumatraPDF.SumatraPDFControl();
            this.toolSumatraControl.SuspendLayout();
            this.contextMenuSumatraPDF.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolSumatraControl
            // 
            this.toolSumatraControl.Dock = System.Windows.Forms.DockStyle.Left;
            this.toolSumatraControl.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolOpen,
            this.toolText,
            this.toolGotoPage,
            this.toolGotoNamedDest,
            this.toolSearchText,
            this.toolSearchForward,
            this.toolSearchBackward,
            this.toolSetScrollPos,
            this.toolSetZoom,
            this.toolSetRotation,
            this.toolStripSeparator4,
            this.toolGetRotation,
            this.toolGetPage,
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
            this.toolDisplayModeLbl,
            this.toolDisplayMode,
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
            this.toolScrollPosPage,
            this.toolOpenPrintDialog});
            this.toolSumatraControl.Location = new System.Drawing.Point(0, 0);
            this.toolSumatraControl.Name = "toolSumatraControl";
            this.toolSumatraControl.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolSumatraControl.Size = new System.Drawing.Size(126, 741);
            this.toolSumatraControl.TabIndex = 1;
            this.toolSumatraControl.Text = "toolStrip1";
            // 
            // toolOpen
            // 
            this.toolOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolOpen.Image = ((System.Drawing.Image)(resources.GetObject("toolOpen.Image")));
            this.toolOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolOpen.Name = "toolOpen";
            this.toolOpen.Size = new System.Drawing.Size(123, 19);
            this.toolOpen.Text = "Open";
            this.toolOpen.ToolTipText = "Open";
            this.toolOpen.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolText
            // 
            this.toolText.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolText.Name = "toolText";
            this.toolText.Size = new System.Drawing.Size(121, 23);
            // 
            // toolGotoPage
            // 
            this.toolGotoPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolGotoPage.Image = ((System.Drawing.Image)(resources.GetObject("toolGotoPage.Image")));
            this.toolGotoPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolGotoPage.Name = "toolGotoPage";
            this.toolGotoPage.Size = new System.Drawing.Size(123, 19);
            this.toolGotoPage.Text = "GotoPage";
            this.toolGotoPage.ToolTipText = "Goto Page";
            this.toolGotoPage.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolGotoNamedDest
            // 
            this.toolGotoNamedDest.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolGotoNamedDest.Image = ((System.Drawing.Image)(resources.GetObject("toolGotoNamedDest.Image")));
            this.toolGotoNamedDest.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolGotoNamedDest.Name = "toolGotoNamedDest";
            this.toolGotoNamedDest.Size = new System.Drawing.Size(123, 19);
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
            this.toolSearchText.Size = new System.Drawing.Size(123, 19);
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
            this.toolSearchForward.Size = new System.Drawing.Size(123, 19);
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
            this.toolSearchBackward.Size = new System.Drawing.Size(123, 19);
            this.toolSearchBackward.Text = "SearchBackward";
            this.toolSearchBackward.ToolTipText = "Search Backward";
            this.toolSearchBackward.Click += new System.EventHandler(this.toolStripButton6_Click);
            // 
            // toolSetScrollPos
            // 
            this.toolSetScrollPos.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolSetScrollPos.Image = ((System.Drawing.Image)(resources.GetObject("toolSetScrollPos.Image")));
            this.toolSetScrollPos.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolSetScrollPos.Name = "toolSetScrollPos";
            this.toolSetScrollPos.Size = new System.Drawing.Size(123, 19);
            this.toolSetScrollPos.Text = "Set ScrollPos";
            this.toolSetScrollPos.Click += new System.EventHandler(this.toolSetScrollPos_Click);
            // 
            // toolSetZoom
            // 
            this.toolSetZoom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolSetZoom.Image = ((System.Drawing.Image)(resources.GetObject("toolSetZoom.Image")));
            this.toolSetZoom.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolSetZoom.Name = "toolSetZoom";
            this.toolSetZoom.Size = new System.Drawing.Size(123, 19);
            this.toolSetZoom.Text = "Set Zoom";
            this.toolSetZoom.ToolTipText = "Set Zoom";
            this.toolSetZoom.Click += new System.EventHandler(this.toolSetZoom_Click);
            // 
            // toolSetRotation
            // 
            this.toolSetRotation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolSetRotation.ForeColor = System.Drawing.SystemColors.ControlText;
            this.toolSetRotation.Image = ((System.Drawing.Image)(resources.GetObject("toolSetRotation.Image")));
            this.toolSetRotation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolSetRotation.Name = "toolSetRotation";
            this.toolSetRotation.Size = new System.Drawing.Size(123, 19);
            this.toolSetRotation.Text = "Rotate By";
            this.toolSetRotation.Click += new System.EventHandler(this.toolSetRotation_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(123, 6);
            // 
            // toolGetRotation
            // 
            this.toolGetRotation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolGetRotation.Image = ((System.Drawing.Image)(resources.GetObject("toolGetRotation.Image")));
            this.toolGetRotation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolGetRotation.Name = "toolGetRotation";
            this.toolGetRotation.Size = new System.Drawing.Size(123, 19);
            this.toolGetRotation.Text = "Get Rotation";
            this.toolGetRotation.ToolTipText = "Get Rotation";
            this.toolGetRotation.Click += new System.EventHandler(this.toolGetRotation_Click);
            // 
            // toolGetPage
            // 
            this.toolGetPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolGetPage.Image = ((System.Drawing.Image)(resources.GetObject("toolGetPage.Image")));
            this.toolGetPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolGetPage.Name = "toolGetPage";
            this.toolGetPage.Size = new System.Drawing.Size(123, 19);
            this.toolGetPage.Text = "Get Page";
            this.toolGetPage.Click += new System.EventHandler(this.toolStripButton9_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(123, 6);
            // 
            // toolCopySelection
            // 
            this.toolCopySelection.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolCopySelection.Image = ((System.Drawing.Image)(resources.GetObject("toolCopySelection.Image")));
            this.toolCopySelection.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolCopySelection.Name = "toolCopySelection";
            this.toolCopySelection.Size = new System.Drawing.Size(123, 19);
            this.toolCopySelection.Text = "CopySelection";
            this.toolCopySelection.ToolTipText = "Copy Selection";
            this.toolCopySelection.Click += new System.EventHandler(this.toolStripCopySelection_Click);
            // 
            // toolToogleToolbar
            // 
            this.toolToogleToolbar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolToogleToolbar.Image = ((System.Drawing.Image)(resources.GetObject("toolToogleToolbar.Image")));
            this.toolToogleToolbar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolToogleToolbar.Name = "toolToogleToolbar";
            this.toolToogleToolbar.Size = new System.Drawing.Size(123, 19);
            this.toolToogleToolbar.Text = "ToogleToolbar";
            this.toolToogleToolbar.ToolTipText = "Toogle Toolbar";
            this.toolToogleToolbar.Click += new System.EventHandler(this.toolStripToogleToolbar_Click);
            // 
            // toolToogleToc
            // 
            this.toolToogleToc.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolToogleToc.Image = ((System.Drawing.Image)(resources.GetObject("toolToogleToc.Image")));
            this.toolToogleToc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolToogleToc.Name = "toolToogleToc";
            this.toolToogleToc.Size = new System.Drawing.Size(123, 19);
            this.toolToogleToc.Text = "ToogleToc";
            this.toolToogleToc.ToolTipText = "Toogle Toc";
            this.toolToogleToc.Click += new System.EventHandler(this.toolStripToogleToc_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(123, 6);
            // 
            // toolPageLbl
            // 
            this.toolPageLbl.Name = "toolPageLbl";
            this.toolPageLbl.Size = new System.Drawing.Size(123, 15);
            this.toolPageLbl.Text = "Pág./Named Dest:";
            // 
            // toolPage
            // 
            this.toolPage.Name = "toolPage";
            this.toolPage.Size = new System.Drawing.Size(123, 15);
            this.toolPage.Text = "---";
            // 
            // toolZoomLbl
            // 
            this.toolZoomLbl.Name = "toolZoomLbl";
            this.toolZoomLbl.Size = new System.Drawing.Size(123, 15);
            this.toolZoomLbl.Text = "Zoom:";
            // 
            // toolZoom
            // 
            this.toolZoom.Name = "toolZoom";
            this.toolZoom.Size = new System.Drawing.Size(123, 15);
            this.toolZoom.Text = "---";
            // 
            // toolZoomVirtualLbl
            // 
            this.toolZoomVirtualLbl.Name = "toolZoomVirtualLbl";
            this.toolZoomVirtualLbl.Size = new System.Drawing.Size(123, 15);
            this.toolZoomVirtualLbl.Text = "Zoom Virtual:";
            // 
            // toolZoomVirtual
            // 
            this.toolZoomVirtual.Name = "toolZoomVirtual";
            this.toolZoomVirtual.Size = new System.Drawing.Size(123, 15);
            this.toolZoomVirtual.Text = "---";
            // 
            // toolDisplayModeLbl
            // 
            this.toolDisplayModeLbl.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolDisplayModeLbl.Name = "toolDisplayModeLbl";
            this.toolDisplayModeLbl.Size = new System.Drawing.Size(123, 15);
            this.toolDisplayModeLbl.Text = "Display Mode:";
            // 
            // toolDisplayMode
            // 
            this.toolDisplayMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolDisplayMode.Name = "toolDisplayMode";
            this.toolDisplayMode.Size = new System.Drawing.Size(123, 15);
            this.toolDisplayMode.Text = "---";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(123, 6);
            // 
            // toolPageModeLbl
            // 
            this.toolPageModeLbl.Name = "toolPageModeLbl";
            this.toolPageModeLbl.Size = new System.Drawing.Size(123, 15);
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
            this.toolPageMode.Size = new System.Drawing.Size(121, 23);
            this.toolPageMode.ToolTipText = "Page Mode";
            this.toolPageMode.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox1_SelectedIndexChanged);
            // 
            // toolGetDisplayMode
            // 
            this.toolGetDisplayMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolGetDisplayMode.Image = ((System.Drawing.Image)(resources.GetObject("toolGetDisplayMode.Image")));
            this.toolGetDisplayMode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolGetDisplayMode.Name = "toolGetDisplayMode";
            this.toolGetDisplayMode.Size = new System.Drawing.Size(123, 19);
            this.toolGetDisplayMode.Text = "Get Display Mode";
            this.toolGetDisplayMode.Click += new System.EventHandler(this.toolStripButton11_Click);
            // 
            // toolZoomVirtualSetLbl
            // 
            this.toolZoomVirtualSetLbl.Name = "toolZoomVirtualSetLbl";
            this.toolZoomVirtualSetLbl.Size = new System.Drawing.Size(123, 15);
            this.toolZoomVirtualSetLbl.Text = "Zoom Virtual:";
            // 
            // toolZoomVirtualSet
            // 
            this.toolZoomVirtualSet.Items.AddRange(new object[] {
            "0 - None",
            "-1 - FitPage",
            "-2 - FitWidth",
            "-3 - FitContent"});
            this.toolZoomVirtualSet.Name = "toolZoomVirtualSet";
            this.toolZoomVirtualSet.Size = new System.Drawing.Size(121, 23);
            this.toolZoomVirtualSet.SelectedIndexChanged += new System.EventHandler(this.toolZoomVirtualSet_SelectedIndexChanged);
            // 
            // toolGetZoomVirtual
            // 
            this.toolGetZoomVirtual.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolGetZoomVirtual.Image = ((System.Drawing.Image)(resources.GetObject("toolGetZoomVirtual.Image")));
            this.toolGetZoomVirtual.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolGetZoomVirtual.Name = "toolGetZoomVirtual";
            this.toolGetZoomVirtual.Size = new System.Drawing.Size(123, 19);
            this.toolGetZoomVirtual.Text = "Get Zoom Virtual";
            this.toolGetZoomVirtual.Click += new System.EventHandler(this.toolGetZoomVirtual_Click);
            // 
            // toolScrollPosLbl
            // 
            this.toolScrollPosLbl.Name = "toolScrollPosLbl";
            this.toolScrollPosLbl.Size = new System.Drawing.Size(123, 15);
            this.toolScrollPosLbl.Text = "Scroll State (X,Y,Page):";
            // 
            // toolScrollPosX
            // 
            this.toolScrollPosX.Name = "toolScrollPosX";
            this.toolScrollPosX.Size = new System.Drawing.Size(123, 15);
            this.toolScrollPosX.Text = "---";
            // 
            // toolScrollPosY
            // 
            this.toolScrollPosY.Name = "toolScrollPosY";
            this.toolScrollPosY.Size = new System.Drawing.Size(123, 15);
            this.toolScrollPosY.Text = "---";
            // 
            // toolScrollPosPage
            // 
            this.toolScrollPosPage.Name = "toolScrollPosPage";
            this.toolScrollPosPage.Size = new System.Drawing.Size(123, 15);
            this.toolScrollPosPage.Text = "---";
            // 
            // toolOpenPrintDialog
            // 
            this.toolOpenPrintDialog.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolOpenPrintDialog.Image = ((System.Drawing.Image)(resources.GetObject("toolOpenPrintDialog.Image")));
            this.toolOpenPrintDialog.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolOpenPrintDialog.Name = "toolOpenPrintDialog";
            this.toolOpenPrintDialog.Size = new System.Drawing.Size(123, 19);
            this.toolOpenPrintDialog.Text = "Open Print Dialog";
            this.toolOpenPrintDialog.Click += new System.EventHandler(this.toolOpenPrintDialog_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(127, 23);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(1291, 59);
            this.textBox1.TabIndex = 2;
            // 
            // contextMenuSumatraPDF
            // 
            this.contextMenuSumatraPDF.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tootStripCopySelection,
            this.toolStripToogleToolbar,
            this.toolStripToogleToc});
            this.contextMenuSumatraPDF.Name = "contextMenuStrip1";
            this.contextMenuSumatraPDF.Size = new System.Drawing.Size(154, 70);
            // 
            // tootStripCopySelection
            // 
            this.tootStripCopySelection.Name = "tootStripCopySelection";
            this.tootStripCopySelection.Size = new System.Drawing.Size(153, 22);
            this.tootStripCopySelection.Text = "Copy Selection";
            this.tootStripCopySelection.Click += new System.EventHandler(this.toolStripCopySelection_Click);
            // 
            // toolStripToogleToolbar
            // 
            this.toolStripToogleToolbar.Name = "toolStripToogleToolbar";
            this.toolStripToogleToolbar.Size = new System.Drawing.Size(153, 22);
            this.toolStripToogleToolbar.Text = "Toogle Toolbar";
            this.toolStripToogleToolbar.Click += new System.EventHandler(this.toolStripToogleToolbar_Click);
            // 
            // toolStripToogleToc
            // 
            this.toolStripToogleToc.Name = "toolStripToogleToc";
            this.toolStripToogleToc.Size = new System.Drawing.Size(153, 22);
            this.toolStripToogleToc.Text = "Toogle Toc";
            this.toolStripToogleToc.Click += new System.EventHandler(this.toolStripToogleToc_Click);
            // 
            // cbShowScrollState
            // 
            this.cbShowScrollState.AutoSize = true;
            this.cbShowScrollState.Location = new System.Drawing.Point(130, 6);
            this.cbShowScrollState.Name = "cbShowScrollState";
            this.cbShowScrollState.Size = new System.Drawing.Size(110, 17);
            this.cbShowScrollState.TabIndex = 3;
            this.cbShowScrollState.Text = "Show Scroll State";
            this.cbShowScrollState.UseVisualStyleBackColor = true;
            // 
            // SumatraPDFControl
            // 
            this.SumatraPDFControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SumatraPDFControl.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.SumatraPDFControl.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.SumatraPDFControl.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("SumatraPDFControl.BackgroundImage")));
            this.SumatraPDFControl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.SumatraPDFControl.ContextMenuStrip = this.contextMenuSumatraPDF;
            this.SumatraPDFControl.Cursor = System.Windows.Forms.Cursors.Default;
            this.SumatraPDFControl.DisplayMode = SumatraPDF.SumatraPDFControl.DisplayModeEnum.Automatic;
            this.SumatraPDFControl.Location = new System.Drawing.Point(127, 88);
            this.SumatraPDFControl.Name = "SumatraPDFControl";
            this.SumatraPDFControl.NamedDest = null;
            this.SumatraPDFControl.Page = 0;
            this.SumatraPDFControl.Size = new System.Drawing.Size(1291, 653);
            this.SumatraPDFControl.SumatraPDFExe = "SumatraPDF.exe";
            this.SumatraPDFControl.SumatraPDFPath = "";
            this.SumatraPDFControl.TabIndex = 0;
            this.SumatraPDFControl.Tag = "";
            this.SumatraPDFControl.TocVisible = false;
            this.SumatraPDFControl.ToolBarVisible = false;
            this.SumatraPDFControl.Zoom = 0F;
            this.SumatraPDFControl.ZoomVirtual = SumatraPDF.SumatraPDFControl.ZoomVirtualEnum.None;
            this.SumatraPDFControl.SumatraMessage += new System.EventHandler<SumatraPDF.SumatraPDFControl.SumatraMessageEventArgs>(this.SumatraPDFControl_SumatraMessage);
            this.SumatraPDFControl.PageChanged += new System.EventHandler<SumatraPDF.SumatraPDFControl.PageChangedEventArgs>(this.SumatraPDFControl_PageChangedMessage);
            this.SumatraPDFControl.ContextMenuOpen += new System.EventHandler<SumatraPDF.SumatraPDFControl.ContextMenuOpenEventArgs>(this.SumatraPDFControl_ContextMenuMessage);
            this.SumatraPDFControl.ZoomChanged += new System.EventHandler<SumatraPDF.SumatraPDFControl.ZoomChangedEventArgs>(this.SumatraPDFControl_ZoomChangedMessage);
            this.SumatraPDFControl.LinkClick += new System.EventHandler<SumatraPDF.SumatraPDFControl.LinkClickedEventArgs>(this.SumatraPDFControl_LinkClickedMessage);
            this.SumatraPDFControl.DisplayModeChanged += new System.EventHandler<SumatraPDF.SumatraPDFControl.DisplayModeChangedEventArgs>(this.SumatraPDFControl_DisplayModeChangedMessage);
            this.SumatraPDFControl.ScrollStateChanged += new System.EventHandler<SumatraPDF.SumatraPDFControl.ScrollStateEventArgs>(this.SumatraPDFControl_ScrollStateChanged);
            this.SumatraPDFControl.KeyPress += new System.EventHandler<System.Windows.Forms.KeyPressEventArgs>(this.SumatraPDFControl_KeyPressedMessage);
            this.SumatraPDFControl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SumatraPDFControl_KeyDown);
            this.SumatraPDFControl.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SumatraPDFControl_KeyUp);
            this.SumatraPDFControl.Scroll += new System.Windows.Forms.ScrollEventHandler(this.SumatraPDFControl_Scroll);
            this.SumatraPDFControl.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.SumatraPDFControl_MouseDoubleClick);
            this.SumatraPDFControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SumatraPDFControl_MouseDown);
            this.SumatraPDFControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SumatraPDFControl_MouseUp);
            // 
            // FormTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1418, 741);
            this.Controls.Add(this.cbShowScrollState);
            this.Controls.Add(this.SumatraPDFControl);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.toolSumatraControl);
            this.Name = "FormTest";
            this.Text = "SumatraPDFControl Test Window";
            this.Load += new System.EventHandler(this.FormTest_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.toolSumatraControl.ResumeLayout(false);
            this.toolSumatraControl.PerformLayout();
            this.contextMenuSumatraPDF.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SumatraPDF.SumatraPDFControl SumatraPDFControl;
        private System.Windows.Forms.ToolStrip toolSumatraControl;
        private System.Windows.Forms.ToolStripButton toolOpen;
        private System.Windows.Forms.ToolStripTextBox toolText;
        private System.Windows.Forms.ToolStripButton toolGotoPage;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
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
        private System.Windows.Forms.ToolStripLabel toolDisplayModeLbl;
        private System.Windows.Forms.ToolStripLabel toolDisplayMode;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton toolGetRotation;
        private System.Windows.Forms.ToolStripButton toolSetRotation;
        private System.Windows.Forms.ContextMenuStrip contextMenuSumatraPDF;
        private System.Windows.Forms.ToolStripMenuItem tootStripCopySelection;
        private System.Windows.Forms.ToolStripMenuItem toolStripToogleToolbar;
        private System.Windows.Forms.ToolStripMenuItem toolStripToogleToc;
        private System.Windows.Forms.CheckBox cbShowScrollState;
        private System.Windows.Forms.ToolStripButton toolOpenPrintDialog;
    }
}

