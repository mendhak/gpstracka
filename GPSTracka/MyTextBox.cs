using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using OpenNETCF.Windows.Forms;

namespace GPSTracka {
    /// <summary>Text box with context menu</summary>
    class MyTextBox : TextBox2 {
        private MenuItem mniUndo;
        private MenuItem mniSep1;
        private MenuItem mniCut;
        private MenuItem mniCopy;
        private MenuItem mniPaste;
        private MenuItem mniSep2;
        private MenuItem mniClear;
        private MenuItem mniSelectAll;
        private ContextMenu cmnContext;

        public MyTextBox() :base(){
            this.InitializeComponent();
            this.ContextMenu = cmnContext;
        }

        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MyTextBox));
            this.cmnContext = new System.Windows.Forms.ContextMenu();
            this.mniUndo = new System.Windows.Forms.MenuItem();
            this.mniSep1 = new System.Windows.Forms.MenuItem();
            this.mniCut = new System.Windows.Forms.MenuItem();
            this.mniCopy = new System.Windows.Forms.MenuItem();
            this.mniPaste = new System.Windows.Forms.MenuItem();
            this.mniSep2 = new System.Windows.Forms.MenuItem();
            this.mniClear = new System.Windows.Forms.MenuItem();
            this.mniSelectAll = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // cmnContext
            // 
            this.cmnContext.MenuItems.Add(this.mniUndo);
            this.cmnContext.MenuItems.Add(this.mniSep1);
            this.cmnContext.MenuItems.Add(this.mniCut);
            this.cmnContext.MenuItems.Add(this.mniCopy);
            this.cmnContext.MenuItems.Add(this.mniPaste);
            this.cmnContext.MenuItems.Add(this.mniSep2);
            this.cmnContext.MenuItems.Add(this.mniClear);
            this.cmnContext.MenuItems.Add(this.mniSelectAll);
            this.cmnContext.Popup += new System.EventHandler(this.cmnContext_Popup);
            // 
            // mniUndo
            // 
            resources.ApplyResources(this.mniUndo, "mniUndo");
            this.mniUndo.Click += new System.EventHandler(this.mniUndo_Click);
            // 
            // mniSep1
            // 
            resources.ApplyResources(this.mniSep1, "mniSep1");
            // 
            // mniCut
            // 
            resources.ApplyResources(this.mniCut, "mniCut");
            this.mniCut.Click += new System.EventHandler(this.mniCut_Click);
            // 
            // mniCopy
            // 
            resources.ApplyResources(this.mniCopy, "mniCopy");
            this.mniCopy.Click += new System.EventHandler(this.mniCopy_Click);
            // 
            // mniPaste
            // 
            resources.ApplyResources(this.mniPaste, "mniPaste");
            this.mniPaste.Click += new System.EventHandler(this.mniPaste_Click);
            // 
            // mniSep2
            // 
            resources.ApplyResources(this.mniSep2, "mniSep2");
            // 
            // mniClear
            // 
            resources.ApplyResources(this.mniClear, "mniClear");
            this.mniClear.Click += new System.EventHandler(this.mniClear_Click);
            // 
            // mniSelectAll
            // 
            resources.ApplyResources(this.mniSelectAll, "mniSelectAll");
            this.mniSelectAll.Click += new System.EventHandler(this.mniSelectAll_Click);
            // 
            // MyTextBox
            // 
            resources.ApplyResources(this, "$this");
            this.ResumeLayout(false);

        }

        private void mniUndo_Click(object sender, EventArgs e) {
            this.Undo();
        }


        private void mniCut_Click(object sender, EventArgs e) {
            this.Cut();
        }

        private void mniCopy_Click(object sender, EventArgs e) {
            this.Copy();
        }

        private void mniPaste_Click(object sender, EventArgs e) {
            this.Paste();
        }


        private void mniClear_Click(object sender, EventArgs e) {
            this.Text = "";
        }

        private void mniSelectAll_Click(object sender, EventArgs e) {
            this.SelectAll();
        }

        private void cmnContext_Popup(object sender, EventArgs e) {
            mniClear.Enabled = !this.ReadOnly;
            mniUndo.Enabled = !this.ReadOnly;
            mniCut.Enabled = !this.ReadOnly && !String.IsNullOrEmpty(this.SelectedText);
            mniCopy.Enabled = !String.IsNullOrEmpty(this.SelectedText);
            mniSelectAll.Enabled = !string.IsNullOrEmpty(this.Text) && this.Text != this.SelectedText;
            mniPaste.Enabled = !this.ReadOnly && Clipboard2.ContainsText() ;
        }
    }
}
