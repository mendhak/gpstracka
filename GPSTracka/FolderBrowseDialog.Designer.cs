namespace GPSTracka {
    partial class FolderBrowseDialog {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mmnMain;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FolderBrowseDialog));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode(resources.GetString("tvwTree.Nodes"));
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("please wait ...");
            this.mmnMain = new System.Windows.Forms.MainMenu();
            this.mniOK = new System.Windows.Forms.MenuItem();
            this.mniCancel = new System.Windows.Forms.MenuItem();
            this.tvwTree = new System.Windows.Forms.TreeView();
            this.imlImages = new System.Windows.Forms.ImageList();
            this.SuspendLayout();
            // 
            // mmnMain
            // 
            this.mmnMain.MenuItems.Add(this.mniOK);
            this.mmnMain.MenuItems.Add(this.mniCancel);
            // 
            // mniOK
            // 
            resources.ApplyResources(this.mniOK, "mniOK");
            this.mniOK.Click += new System.EventHandler(this.mniOK_Click);
            // 
            // mniCancel
            // 
            resources.ApplyResources(this.mniCancel, "mniCancel");
            this.mniCancel.Click += new System.EventHandler(this.mniCancel_Click);
            // 
            // tvwTree
            // 
            resources.ApplyResources(this.tvwTree, "tvwTree");
            this.tvwTree.ImageList = this.imlImages;
            this.tvwTree.Name = "tvwTree";
            treeNode2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            resources.ApplyResources(treeNode2, "treeNode2");
            treeNode1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2});
            resources.ApplyResources(treeNode1, "treeNode1");
            this.tvwTree.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.tvwTree.ShowRootLines = false;
            //this.tvwTree.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.tvwTree_AfterCollapse);
            this.tvwTree.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvwTree_BeforeExpand);
            //this.tvwTree.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.tvwTree_AfterExpand);
            this.imlImages.Images.Clear();
            this.imlImages.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
            this.imlImages.Images.Add(((System.Drawing.Image)(resources.GetObject("resource1"))));
            // 
            // FolderBrowseDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.tvwTree);
            this.Menu = this.mmnMain;
            this.MinimizeBox = false;
            this.Name = "FolderBrowseDialog";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.FolderBrowseDialog_Closing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem mniOK;
        private System.Windows.Forms.MenuItem mniCancel;
        private System.Windows.Forms.TreeView tvwTree;
        private System.Windows.Forms.ImageList imlImages;
    }
}