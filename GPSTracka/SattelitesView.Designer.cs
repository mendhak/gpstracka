namespace GPSTracka {
    partial class SatellitesView {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SatellitesView));
            this.mmnMain = new System.Windows.Forms.MainMenu();
            this.mniClose = new System.Windows.Forms.MenuItem();
            this.mniView = new System.Windows.Forms.MenuItem();
            this.panSatellites = new System.Windows.Forms.Panel();
            this.bsSatellites = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.dataGridTextBoxColumn2 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn3 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.tabSatellites = new System.Windows.Forms.TabControl();
            this.tapStrength = new System.Windows.Forms.TabPage();
            this.tapDetails = new System.Windows.Forms.TabPage();
            this.lvwSatellites = new System.Windows.Forms.ListView();
            this.cohID = new System.Windows.Forms.ColumnHeader();
            this.cohSNR = new System.Windows.Forms.ColumnHeader();
            this.cohActive = new System.Windows.Forms.ColumnHeader();
            this.cohAzimuth = new System.Windows.Forms.ColumnHeader();
            this.cohElevation = new System.Windows.Forms.ColumnHeader();
            this.tapSatellites = new System.Windows.Forms.TabPage();
            this.panPosition = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.bsSatellites)).BeginInit();
            this.tabSatellites.SuspendLayout();
            this.tapStrength.SuspendLayout();
            this.tapDetails.SuspendLayout();
            this.tapSatellites.SuspendLayout();
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
            // panSatellites
            // 
            resources.ApplyResources(this.panSatellites, "panSatellites");
            this.panSatellites.Name = "panSatellites";
            this.panSatellites.Resize += new System.EventHandler(this.panSatellites_Resize);
            // 
            // bsSatellites
            // 
            this.bsSatellites.AllowNew = false;
            this.bsSatellites.Filter = "ID <> 0";
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn2);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn3);
            this.dataGridTableStyle1.MappingName = "bsSatellites";
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
            // tabSatellites
            // 
            this.tabSatellites.Controls.Add(this.tapStrength);
            this.tabSatellites.Controls.Add(this.tapDetails);
            this.tabSatellites.Controls.Add(this.tapSatellites);
            resources.ApplyResources(this.tabSatellites, "tabSatellites");
            this.tabSatellites.Name = "tabSatellites";
            this.tabSatellites.SelectedIndex = 0;
            // 
            // tapStrength
            // 
            this.tapStrength.Controls.Add(this.panSatellites);
            resources.ApplyResources(this.tapStrength, "tapStrength");
            this.tapStrength.Name = "tapStrength";
            // 
            // tapDetails
            // 
            this.tapDetails.Controls.Add(this.lvwSatellites);
            resources.ApplyResources(this.tapDetails, "tapDetails");
            this.tapDetails.Name = "tapDetails";
            // 
            // lvwSatellites
            // 
            this.lvwSatellites.Columns.Add(this.cohID);
            this.lvwSatellites.Columns.Add(this.cohSNR);
            this.lvwSatellites.Columns.Add(this.cohActive);
            this.lvwSatellites.Columns.Add(this.cohAzimuth);
            this.lvwSatellites.Columns.Add(this.cohElevation);
            resources.ApplyResources(this.lvwSatellites, "lvwSatellites");
            this.lvwSatellites.FullRowSelect = true;
            this.lvwSatellites.Name = "lvwSatellites";
            this.lvwSatellites.View = System.Windows.Forms.View.Details;
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
            // tapSatellites
            // 
            this.tapSatellites.Controls.Add(this.panPosition);
            resources.ApplyResources(this.tapSatellites, "tapSatellites");
            this.tapSatellites.Name = "tapSatellites";
            // 
            // panPosition
            // 
            resources.ApplyResources(this.panPosition, "panPosition");
            this.panPosition.Name = "panPosition";
            this.panPosition.Paint += new System.Windows.Forms.PaintEventHandler(this.panPosition_Paint);
            // 
            // SatellitesView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.tabSatellites);
            this.Menu = this.mmnMain;
            this.Name = "SatellitesView";
            this.Closed += new System.EventHandler(this.SatellitesView_Closed);
            ((System.ComponentModel.ISupportInitialize)(this.bsSatellites)).EndInit();
            this.tabSatellites.ResumeLayout(false);
            this.tapStrength.ResumeLayout(false);
            this.tapDetails.ResumeLayout(false);
            this.tapSatellites.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panSatellites;
        private System.Windows.Forms.MenuItem mniClose;
        private System.Windows.Forms.BindingSource bsSatellites;
        private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn2;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn3;
        private System.Windows.Forms.MenuItem mniView;
        private System.Windows.Forms.TabControl tabSatellites;
        private System.Windows.Forms.TabPage tapStrength;
        private System.Windows.Forms.TabPage tapDetails;
        private System.Windows.Forms.TabPage tapSatellites;
        private System.Windows.Forms.Panel panPosition;
        private System.Windows.Forms.ListView lvwSatellites;
        private System.Windows.Forms.ColumnHeader cohID;
        private System.Windows.Forms.ColumnHeader cohSNR;
        private System.Windows.Forms.ColumnHeader cohActive;
        private System.Windows.Forms.ColumnHeader cohAzimuth;
        private System.Windows.Forms.ColumnHeader cohElevation;
    }
}