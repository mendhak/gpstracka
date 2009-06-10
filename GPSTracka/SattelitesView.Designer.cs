namespace GPSTracka {
    partial class SattelitesView {
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SattelitesView));
            this.mmnMain = new System.Windows.Forms.MainMenu();
            this.mniClose = new System.Windows.Forms.MenuItem();
            this.mniView = new System.Windows.Forms.MenuItem();
            this.lblI = new System.Windows.Forms.Label();
            this.panSattelites = new System.Windows.Forms.Panel();
            this.bsSattelites = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.dataGridTextBoxColumn2 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn3 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.tabSattelites = new System.Windows.Forms.TabControl();
            this.tapStrength = new System.Windows.Forms.TabPage();
            this.tapDetails = new System.Windows.Forms.TabPage();
            this.lvwSattelites = new System.Windows.Forms.ListView();
            this.cohID = new System.Windows.Forms.ColumnHeader();
            this.cohSNR = new System.Windows.Forms.ColumnHeader();
            this.cohActive = new System.Windows.Forms.ColumnHeader();
            this.cohAzimuth = new System.Windows.Forms.ColumnHeader();
            this.cohElevation = new System.Windows.Forms.ColumnHeader();
            this.tapSattelites = new System.Windows.Forms.TabPage();
            this.panPosition = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.bsSattelites)).BeginInit();
            this.tabSattelites.SuspendLayout();
            this.tapStrength.SuspendLayout();
            this.tapDetails.SuspendLayout();
            this.tapSattelites.SuspendLayout();
            this.SuspendLayout();
            // 
            // mmnMain
            // 
            this.mmnMain.MenuItems.Add(this.mniClose);
            this.mmnMain.MenuItems.Add(this.mniView);
            // 
            // mniClose
            // 
            resources.ApplyResources(this.mniClose, "mniClose");
            this.mniClose.Click += new System.EventHandler(this.mniClose_Click);
            // 
            // mniView
            // 
            resources.ApplyResources(this.mniView, "mniView");
            this.mniView.Click += new System.EventHandler(this.mniView_Click);
            // 
            // lblI
            // 
            resources.ApplyResources(this.lblI, "lblI");
            this.lblI.Name = "lblI";
            // 
            // panSattelites
            // 
            resources.ApplyResources(this.panSattelites, "panSattelites");
            this.panSattelites.Name = "panSattelites";
            this.panSattelites.Resize += new System.EventHandler(this.panSattelites_Resize);
            // 
            // bsSattelites
            // 
            this.bsSattelites.AllowNew = false;
            this.bsSattelites.Filter = "ID <> 0";
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn2);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn3);
            this.dataGridTableStyle1.MappingName = "bsSattelites";
            // 
            // dataGridTextBoxColumn2
            // 
            this.dataGridTextBoxColumn2.Format = "";
            this.dataGridTextBoxColumn2.FormatInfo = null;
            resources.ApplyResources(this.dataGridTextBoxColumn2, "dataGridTextBoxColumn2");
            // 
            // dataGridTextBoxColumn3
            // 
            this.dataGridTextBoxColumn3.Format = "";
            this.dataGridTextBoxColumn3.FormatInfo = null;
            resources.ApplyResources(this.dataGridTextBoxColumn3, "dataGridTextBoxColumn3");
            // 
            // tabSattelites
            // 
            resources.ApplyResources(this.tabSattelites, "tabSattelites");
            this.tabSattelites.Controls.Add(this.tapStrength);
            this.tabSattelites.Controls.Add(this.tapDetails);
            this.tabSattelites.Controls.Add(this.tapSattelites);
            this.tabSattelites.Name = "tabSattelites";
            this.tabSattelites.SelectedIndex = 0;
            // 
            // tapStrength
            // 
            resources.ApplyResources(this.tapStrength, "tapStrength");
            this.tapStrength.Controls.Add(this.panSattelites);
            this.tapStrength.Name = "tapStrength";
            // 
            // tapDetails
            // 
            resources.ApplyResources(this.tapDetails, "tapDetails");
            this.tapDetails.Controls.Add(this.lvwSattelites);
            this.tapDetails.Name = "tapDetails";
            // 
            // lvwSattelites
            // 
            resources.ApplyResources(this.lvwSattelites, "lvwSattelites");
            this.lvwSattelites.Columns.Add(this.cohID);
            this.lvwSattelites.Columns.Add(this.cohSNR);
            this.lvwSattelites.Columns.Add(this.cohActive);
            this.lvwSattelites.Columns.Add(this.cohAzimuth);
            this.lvwSattelites.Columns.Add(this.cohElevation);
            this.lvwSattelites.FullRowSelect = true;
            this.lvwSattelites.Name = "lvwSattelites";
            this.lvwSattelites.View = System.Windows.Forms.View.Details;
            // 
            // cohID
            // 
            resources.ApplyResources(this.cohID, "cohID");
            // 
            // cohSNR
            // 
            resources.ApplyResources(this.cohSNR, "cohSNR");
            // 
            // cohActive
            // 
            resources.ApplyResources(this.cohActive, "cohActive");
            // 
            // cohAzimuth
            // 
            resources.ApplyResources(this.cohAzimuth, "cohAzimuth");
            // 
            // cohElevation
            // 
            resources.ApplyResources(this.cohElevation, "cohElevation");
            // 
            // tapSattelites
            // 
            resources.ApplyResources(this.tapSattelites, "tapSattelites");
            this.tapSattelites.Controls.Add(this.panPosition);
            this.tapSattelites.Name = "tapSattelites";
            // 
            // panPosition
            // 
            resources.ApplyResources(this.panPosition, "panPosition");
            this.panPosition.Name = "panPosition";
            this.panPosition.Paint += new System.Windows.Forms.PaintEventHandler(this.panPosition_Paint);
            // 
            // SattelitesView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.tabSattelites);
            this.Controls.Add(this.lblI);
            this.Menu = this.mmnMain;
            this.Name = "SattelitesView";
            this.Closed += new System.EventHandler(this.SattelitesView_Closed);
            ((System.ComponentModel.ISupportInitialize)(this.bsSattelites)).EndInit();
            this.tabSattelites.ResumeLayout(false);
            this.tapStrength.ResumeLayout(false);
            this.tapDetails.ResumeLayout(false);
            this.tapSattelites.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblI;
        private System.Windows.Forms.Panel panSattelites;
        private System.Windows.Forms.MenuItem mniClose;
        private System.Windows.Forms.BindingSource bsSattelites;
        private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn2;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn3;
        private System.Windows.Forms.MenuItem mniView;
        private System.Windows.Forms.TabControl tabSattelites;
        private System.Windows.Forms.TabPage tapStrength;
        private System.Windows.Forms.TabPage tapDetails;
        private System.Windows.Forms.TabPage tapSattelites;
        private System.Windows.Forms.Panel panPosition;
        private System.Windows.Forms.ListView lvwSattelites;
        private System.Windows.Forms.ColumnHeader cohID;
        private System.Windows.Forms.ColumnHeader cohSNR;
        private System.Windows.Forms.ColumnHeader cohActive;
        private System.Windows.Forms.ColumnHeader cohAzimuth;
        private System.Windows.Forms.ColumnHeader cohElevation;
    }
}