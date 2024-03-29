﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static SumatraPDF.SumatraPDFControl;

namespace SumatraPDFControlTest
{
    public partial class Form1 : Form
    {
        public string Filename = string.Empty;
        public Boolean NewSumatraPDFProcess = false;
        public Boolean UseLocalSumatraPDF = true;

        public Form1()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                LoadFile(openFileDialog.FileName, int.Parse(toolText.Text == "" ? "1" : toolText.Text), false);
            }

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            SumatraPDFControl.Page = int.Parse(toolText.Text);
        }

        private void toolStripCopySelection_Click(object sender, EventArgs e)
        {
            SumatraPDFControl.CopySelection();
        }

        private void toolStripToogleToolbar_Click(object sender, EventArgs e)
        {
            SumatraPDFControl.ToolBarVisible = !SumatraPDFControl.ToolBarVisible;
        }

        private void SumatraPDFControl_SumatraMessage(object sender, SumatraMessageEventArgs e)
        {
            AddText(e.Msg + " - " + e.CallBackReturn);
        }

        private void SumatraPDFControl_PageChangedMessage(object sender, PageChangedEventArgs e)
        {
            toolPage.Text = e.Page.ToString() + " / " + e.NamedDest;
            lblCurrPage.Text = SumatraPDFControl.Page.ToString();
        }

        private void SumatraPDFControl_ContextMenuMessage(object sender, ContextMenuOpeningEventArgs e)
        {
            AddText("ContextMenu - X: " + e.X.ToString() + " - Y: " + e.Y.ToString());
            e.Handled = (cbContextMenuType.SelectedIndex == 2);
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

        private void toolStripToogleToc_Click(object sender, EventArgs e)
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

        System.IO.FileSystemWatcher fw;
        private void LoadFile(string Filename, int Page, bool NewSumatraInstance)
        {
            if (fw != null) 
            { 
                fw.EnableRaisingEvents = false;
                fw.Dispose();
            }
            SumatraPDFControl.LoadFile(Filename, Page, NewSumatraInstance);
            fw = new System.IO.FileSystemWatcher(System.IO.Path.GetDirectoryName(Filename))
            {
                NotifyFilter = System.IO.NotifyFilters.LastWrite,
                Filter = System.IO.Path.GetFileName(Filename),
                IncludeSubdirectories = true
            };
            fw.Changed += Fw_Changed;
            fw.EnableRaisingEvents = true;

        }

        private void Form1_Shown(object sender, EventArgs e)
        {            
            LoadFile(Filename, 1, NewSumatraInstance: NewSumatraPDFProcess);
            cbContextMenuType.SelectedIndex = 1;
            Filename = string.Empty;
        }

        private void Fw_Changed(object sender, System.IO.FileSystemEventArgs e)
        {
            SumatraPDFControl.ReloadCurrentFile();
        }

        private void SumatraPDFControl_ScrollStateChanged(object sender, ScrollStateEventArgs e)
        {
            if (cbShowScrollState.Checked)
            {
                toolScrollPosX.Text = e.ScrollState.X.ToString();
                toolScrollPosY.Text = e.ScrollState.Y.ToString();
                toolScrollPosPage.Text = e.ScrollState.Page.ToString();
            }
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

        private void toolOpenPrintDialog_Click(object sender, EventArgs e)
        {
            SumatraPDFControl.OpenPrintDialog();
        }

        private void FormTest_Load(object sender, EventArgs e)
        {
            // If SumatraPDFPath was not filled try to use SumatraPDF.exe from SumatraPDF compiled source (if available in same SumatraPDFControl solution dir level).
            if (UseLocalSumatraPDF && (SumatraPDFControl.SumatraPDFPath == null || SumatraPDFControl.SumatraPDFPath == string.Empty)) {
                string arch = System.Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE", EnvironmentVariableTarget.Machine);
                string SumatraDir = string.Empty, SumatraDir2 = string.Empty, SumatraDir3 = string.Empty, SumatraPDFSubdir = string.Empty;

                SumatraPDFSubdir = (arch == "AMD64") ? @"\dbg64\" : @"\dbg32\"; 
                SumatraDir = @"..\..\..\..\..\sumatrapdf\out" + SumatraPDFSubdir; 
                if (System.IO.File.Exists(SumatraDir + SumatraPDFControl.SumatraPDFExe)) SumatraPDFControl.SumatraPDFPath = SumatraDir;
            }
            
        }

        private void toolStripSelectAllText_Click(object sender, EventArgs e)
        {
            SumatraPDFControl.SelectAll();
        }

        public void SelectAll()
        {
            SumatraPDFControl.SelectAll();
        }

        public void CopySelection()
        {
            SumatraPDFControl.CopySelection();
        }

        public void OpenPrintDialog()
        {
            SumatraPDFControl.OpenPrintDialog();
        }

        private void buttonGotoFirst_Click(object sender, EventArgs e)
        {
            SumatraPDFControl.GoToFirstPage();
        }

        private void buttonGotoPrev_Click(object sender, EventArgs e)
        {
            SumatraPDFControl.GoToPrevPage();
        }

        private void buttonGotoNext_Click(object sender, EventArgs e)
        {
            SumatraPDFControl.GoToNextPage();
        }

        private void buttonGotoLast_Click(object sender, EventArgs e)
        {
            SumatraPDFControl.GoToLastPage();
        }

        private void SumatraPDFControl_FileOpened(object sender, EventArgs e)
        {
            toolPage.Text = SumatraPDFControl.Page.ToString() + " - " + SumatraPDFControl.NamedDest;
            lblCurrPage.Text = SumatraPDFControl.Page.ToString();
            lblPageCount.Text = SumatraPDFControl.PageCount.ToString();
            cbKeyAccel.Checked = SumatraPDFControl.KeyAccelerators;
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            SumatraPDFControl.ReloadCurrentFile();
        }

        private void cbKeyAccel_CheckedChanged(object sender, EventArgs e)
        {
            SumatraPDFControl.KeyAccelerators = cbKeyAccel.Checked;
        }

        private void cbContextMenuType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbContextMenuType.SelectedIndex) {
                case 1:
                    SumatraPDFControl.ContextMenuStrip = contextMenuSumatraPDF;
                    break;
                default:
                    SumatraPDFControl.ContextMenuStrip = null;
                    break;                
            }

        }
    }
}
