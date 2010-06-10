namespace GPSTracka
{
    partial class OpenFileDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mmnMain;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OpenFileDialog));
            this.mmnMain = new System.Windows.Forms.MainMenu();
            this.mniOK = new System.Windows.Forms.MenuItem();
            this.mniCancel = new System.Windows.Forms.MenuItem();
            this.lvwFiles = new System.Windows.Forms.ListView();
            this.cohName = new System.Windows.Forms.ColumnHeader();
            this.cohDate = new System.Windows.Forms.ColumnHeader();
            this.cohSize = new System.Windows.Forms.ColumnHeader();
            this.cohAttributes = new System.Windows.Forms.ColumnHeader();
            this.imlLargeIcons = new System.Windows.Forms.ImageList();
            this.panSelection = new System.Windows.Forms.Panel();
            this.lblFileName = new System.Windows.Forms.Label();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.lblFilter = new System.Windows.Forms.Label();
            this.cmbFilter = new System.Windows.Forms.ComboBox();
            this.imlSmallIcons = new System.Windows.Forms.ImageList();
            this.panTop = new System.Windows.Forms.Panel();
            this.cmdUp = new OpenNETCF.Windows.Forms.Button2();
            this.lblPath = new System.Windows.Forms.Label();
            this.cmdViewMode = new OpenNETCF.Windows.Forms.Button2();
            this.cmnView = new System.Windows.Forms.ContextMenu();
            this.mniLargeIcons = new System.Windows.Forms.MenuItem();
            this.mniSmallIcons = new System.Windows.Forms.MenuItem();
            this.mniDetails = new System.Windows.Forms.MenuItem();
            this.mniList = new System.Windows.Forms.MenuItem();
            this.panSelection.SuspendLayout();
            this.panTop.SuspendLayout();
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
            // lvwFiles
            // 
            this.lvwFiles.Columns.Add(this.cohName);
            this.lvwFiles.Columns.Add(this.cohDate);
            this.lvwFiles.Columns.Add(this.cohSize);
            this.lvwFiles.Columns.Add(this.cohAttributes);
            resources.ApplyResources(this.lvwFiles, "lvwFiles");
            this.lvwFiles.LargeImageList = this.imlLargeIcons;
            this.lvwFiles.Name = "lvwFiles";
            this.lvwFiles.SmallImageList = this.imlSmallIcons;
            this.lvwFiles.ItemActivate += new System.EventHandler(this.lvwFiles_ItemActivate);
            this.lvwFiles.SelectedIndexChanged += new System.EventHandler(this.lvwFiles_SelectedIndexChanged);
            this.lvwFiles.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvwFiles_ColumnClick);
            // 
            // cohName
            // 
            resources.ApplyResources(this.cohName, "cohName");
            // 
            // cohDate
            // 
            resources.ApplyResources(this.cohDate, "cohDate");
            // 
            // cohSize
            // 
            resources.ApplyResources(this.cohSize, "cohSize");
            // 
            // cohAttributes
            // 
            resources.ApplyResources(this.cohAttributes, "cohAttributes");
            // 
            // imlLargeIcons
            // 
            resources.ApplyResources(this.imlLargeIcons, "imlLargeIcons");
            this.imlLargeIcons.Images.Clear();
            this.imlLargeIcons.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
            this.imlLargeIcons.Images.Add(((System.Drawing.Icon)(resources.GetObject("resource1"))));
            // 
            // panSelection
            // 
            this.panSelection.Controls.Add(this.lblFileName);
            this.panSelection.Controls.Add(this.txtFileName);
            this.panSelection.Controls.Add(this.lblFilter);
            this.panSelection.Controls.Add(this.cmbFilter);
            resources.ApplyResources(this.panSelection, "panSelection");
            this.panSelection.Name = "panSelection";
            // 
            // lblFileName
            // 
            resources.ApplyResources(this.lblFileName, "lblFileName");
            this.lblFileName.Name = "lblFileName";
            // 
            // txtFileName
            // 
            resources.ApplyResources(this.txtFileName, "txtFileName");
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFileName_KeyDown);
            // 
            // lblFilter
            // 
            resources.ApplyResources(this.lblFilter, "lblFilter");
            this.lblFilter.Name = "lblFilter";
            // 
            // cmbFilter
            // 
            resources.ApplyResources(this.cmbFilter, "cmbFilter");
            this.cmbFilter.Name = "cmbFilter";
            this.cmbFilter.SelectedIndexChanged += new System.EventHandler(this.cmbFilter_SelectedIndexChanged);
            this.imlSmallIcons.Images.Clear();
            this.imlSmallIcons.Images.Add(((System.Drawing.Image)(resources.GetObject("resource2"))));
            this.imlSmallIcons.Images.Add(((System.Drawing.Icon)(resources.GetObject("resource3"))));
            // 
            // panTop
            // 
            this.panTop.Controls.Add(this.cmdUp);
            this.panTop.Controls.Add(this.lblPath);
            this.panTop.Controls.Add(this.cmdViewMode);
            resources.ApplyResources(this.panTop, "panTop");
            this.panTop.Name = "panTop";
            // 
            // cmdUp
            // 
            this.cmdUp.BackgroundImage = null;
            resources.ApplyResources(this.cmdUp, "cmdUp");
            this.cmdUp.Image = ((System.Drawing.Image)(resources.GetObject("cmdUp.Image")));
            this.cmdUp.Name = "cmdUp";
            this.cmdUp.Click += new System.EventHandler(this.cmdUp_Click);
            // 
            // lblPath
            // 
            resources.ApplyResources(this.lblPath, "lblPath");
            this.lblPath.Name = "lblPath";
            // 
            // cmdViewMode
            // 
            this.cmdViewMode.BackgroundImage = null;
            resources.ApplyResources(this.cmdViewMode, "cmdViewMode");
            this.cmdViewMode.Image = ((System.Drawing.Image)(resources.GetObject("cmdViewMode.Image")));
            this.cmdViewMode.Name = "cmdViewMode";
            this.cmdViewMode.Click += new System.EventHandler(this.cmdViewMode_Click);
            // 
            // cmnView
            // 
            this.cmnView.MenuItems.Add(this.mniLargeIcons);
            this.cmnView.MenuItems.Add(this.mniSmallIcons);
            this.cmnView.MenuItems.Add(this.mniDetails);
            this.cmnView.MenuItems.Add(this.mniList);
            this.cmnView.Popup += new System.EventHandler(this.cmnView_Popup);
            // 
            // mniLargeIcons
            // 
            resources.ApplyResources(this.mniLargeIcons, "mniLargeIcons");
            this.mniLargeIcons.Click += new System.EventHandler(this.mniView_Click);
            // 
            // mniSmallIcons
            // 
            resources.ApplyResources(this.mniSmallIcons, "mniSmallIcons");
            this.mniSmallIcons.Click += new System.EventHandler(this.mniView_Click);
            // 
            // mniDetails
            // 
            resources.ApplyResources(this.mniDetails, "mniDetails");
            this.mniDetails.Click += new System.EventHandler(this.mniView_Click);
            // 
            // mniList
            // 
            resources.ApplyResources(this.mniList, "mniList");
            this.mniList.Click += new System.EventHandler(this.mniView_Click);
            // 
            // OpenFileDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.panSelection);
            this.Controls.Add(this.lvwFiles);
            this.Controls.Add(this.panTop);
            this.KeyPreview = true;
            this.Menu = this.mmnMain;
            this.MinimizeBox = false;
            this.Name = "OpenFileDialog";
            this.panSelection.ResumeLayout(false);
            this.panTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem mniOK;
        private System.Windows.Forms.MenuItem mniCancel;
        private System.Windows.Forms.ListView lvwFiles;
        private System.Windows.Forms.ImageList imlLargeIcons;
        private System.Windows.Forms.Panel panSelection;
        private System.Windows.Forms.ComboBox cmbFilter;
        private System.Windows.Forms.Label lblFilter;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Label lblFileName;
        private System.Windows.Forms.ImageList imlSmallIcons;
        private System.Windows.Forms.ColumnHeader cohName;
        private System.Windows.Forms.ColumnHeader cohDate;
        private System.Windows.Forms.ColumnHeader cohSize;
        private System.Windows.Forms.ColumnHeader cohAttributes;
        private System.Windows.Forms.Panel panTop;
        private OpenNETCF.Windows.Forms.Button2 cmdViewMode;
        private System.Windows.Forms.ContextMenu cmnView;
        private System.Windows.Forms.MenuItem mniLargeIcons;
        private System.Windows.Forms.MenuItem mniSmallIcons;
        private System.Windows.Forms.MenuItem mniDetails;
        private System.Windows.Forms.MenuItem mniList;
        private System.Windows.Forms.Label lblPath;
        private OpenNETCF.Windows.Forms.Button2 cmdUp;
    }
}