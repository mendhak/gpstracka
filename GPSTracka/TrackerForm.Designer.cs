namespace GPSTracka {
    partial class TrackerForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;


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
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrackerForm));
            this.timer1 = new System.Windows.Forms.Timer();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.TextBoxRawLog = new GPSTracka.MyTextBox();
            this.panInfoPane = new System.Windows.Forms.Panel();
            this.lblSpeedI = new System.Windows.Forms.Label();
            this.lblSpeed = new System.Windows.Forms.Label();
            this.lblAverageI = new System.Windows.Forms.Label();
            this.lblAverage = new System.Windows.Forms.Label();
            this.lblDistanceI = new System.Windows.Forms.Label();
            this.lblDistance = new System.Windows.Forms.Label();
            this.lblAerialI = new System.Windows.Forms.Label();
            this.lblAerial = new System.Windows.Forms.Label();
            this.lblElevationI = new System.Windows.Forms.Label();
            this.lblElevation = new System.Windows.Forms.Label();
            this.lblElevationMinusI = new System.Windows.Forms.Label();
            this.lblElevationMinus = new System.Windows.Forms.Label();
            this.lblTimeI = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblStopTimeI = new System.Windows.Forms.Label();
            this.lblStopTime = new System.Windows.Forms.Label();
            this.lblPointsI = new System.Windows.Forms.Label();
            this.lblPoints = new System.Windows.Forms.Label();
            this.lblSatellitesI = new System.Windows.Forms.Label();
            this.lblSatellites = new System.Windows.Forms.Label();
            this.stsStatus = new GPSTracka.ItemsStatusBar();
            this.mainPanelMenu = new System.Windows.Forms.MainMenu();
            this.startMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.clearMenuItem = new System.Windows.Forms.MenuItem();
            this.verboseMenuItem = new System.Windows.Forms.MenuItem();
            this.settingsMenuItem = new System.Windows.Forms.MenuItem();
            this.mniSatellites = new System.Windows.Forms.MenuItem();
            this.mniSep3 = new System.Windows.Forms.MenuItem();
            this.mniSaveStatistic = new System.Windows.Forms.MenuItem();
            this.mniSep1 = new System.Windows.Forms.MenuItem();
            this.aboutMenuItem = new System.Windows.Forms.MenuItem();
            this.mniHelp = new System.Windows.Forms.MenuItem();
            this.mniSep2 = new System.Windows.Forms.MenuItem();
            this.menuExit = new System.Windows.Forms.MenuItem();
            this.aboutPanel = new System.Windows.Forms.Panel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.aboutLabel = new System.Windows.Forms.Label();
            this.settingsPanel = new System.Windows.Forms.Panel();
            this.fraFormat = new OpenNETCF.Windows.Forms.GroupBox();
            this.panTRackLogType = new System.Windows.Forms.Panel();
            this.optTrack = new System.Windows.Forms.RadioButton();
            this.optDistinct = new System.Windows.Forms.RadioButton();
            this.panFileFormat = new System.Windows.Forms.Panel();
            this.optCSV = new System.Windows.Forms.RadioButton();
            this.radioButtonKML = new System.Windows.Forms.RadioButton();
            this.radioButtonGPX = new System.Windows.Forms.RadioButton();
            this.chkAltitude = new System.Windows.Forms.CheckBox();
            this.fraOptions = new OpenNETCF.Windows.Forms.GroupBox();
            this.CheckBoxToTextBox = new System.Windows.Forms.CheckBox();
            this.CheckBoxToFile = new System.Windows.Forms.CheckBox();
            this.logLocationTextBox = new GPSTracka.MyTextBox();
            this.saveDialogButton = new System.Windows.Forms.Button();
            this.fraSpeed = new OpenNETCF.Windows.Forms.GroupBox();
            this.NumericUpDownInterval = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.fraCOMPort = new OpenNETCF.Windows.Forms.GroupBox();
            this.chkUseWindowsDriver = new System.Windows.Forms.CheckBox();
            this.lblRecommendedPort = new System.Windows.Forms.Label();
            this.ComboBoxCOMPorts = new System.Windows.Forms.ComboBox();
            this.ComboBaudRate = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.aboutPanelMenu = new System.Windows.Forms.MainMenu();
            this.cancelMenuItem = new System.Windows.Forms.MenuItem();
            this.settingsPanelMenu = new System.Windows.Forms.MainMenu();
            this.saveMenuItem = new System.Windows.Forms.MenuItem();
            this.mniAdvanced = new System.Windows.Forms.MenuItem();
            this.backMenuItem = new System.Windows.Forms.MenuItem();
            this.tmrCountDown = new System.Windows.Forms.Timer();
            this.tmrBeep = new System.Windows.Forms.Timer();
            this.panHelper = new System.Windows.Forms.Panel();
            this.mainPanel.SuspendLayout();
            this.panInfoPane.SuspendLayout();
            this.aboutPanel.SuspendLayout();
            this.settingsPanel.SuspendLayout();
            this.fraFormat.SuspendLayout();
            this.panTRackLogType.SuspendLayout();
            this.panFileFormat.SuspendLayout();
            this.fraOptions.SuspendLayout();
            this.fraSpeed.SuspendLayout();
            this.fraCOMPort.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.TextBoxRawLog);
            this.mainPanel.Controls.Add(this.panInfoPane);
            this.mainPanel.Controls.Add(this.stsStatus);
            this.mainPanel.Controls.Add(this.panHelper);
                    resources.ApplyResources(this.mainPanel, "mainPanel");
            this.mainPanel.Name = "mainPanel";
            // 
            // TextBoxRawLog
            // 
            resources.ApplyResources(this.TextBoxRawLog, "TextBoxRawLog");
            this.TextBoxRawLog.Name = "TextBoxRawLog";
            this.TextBoxRawLog.ReadOnly = true;
            // 
            // panInfoPane
            // 
            this.panInfoPane.BackColor = System.Drawing.SystemColors.Control;
            this.panInfoPane.Controls.Add(this.lblSpeedI);
            this.panInfoPane.Controls.Add(this.lblSpeed);
            this.panInfoPane.Controls.Add(this.lblAverageI);
            this.panInfoPane.Controls.Add(this.lblAverage);
            this.panInfoPane.Controls.Add(this.lblDistanceI);
            this.panInfoPane.Controls.Add(this.lblDistance);
            this.panInfoPane.Controls.Add(this.lblAerialI);
            this.panInfoPane.Controls.Add(this.lblAerial);
            this.panInfoPane.Controls.Add(this.lblElevationI);
            this.panInfoPane.Controls.Add(this.lblElevation);
            this.panInfoPane.Controls.Add(this.lblElevationMinusI);
            this.panInfoPane.Controls.Add(this.lblElevationMinus);
            this.panInfoPane.Controls.Add(this.lblTimeI);
            this.panInfoPane.Controls.Add(this.lblTime);
            this.panInfoPane.Controls.Add(this.lblStopTimeI);
            this.panInfoPane.Controls.Add(this.lblStopTime);
            this.panInfoPane.Controls.Add(this.lblPointsI);
            this.panInfoPane.Controls.Add(this.lblPoints);
            this.panInfoPane.Controls.Add(this.lblSatellitesI);
            this.panInfoPane.Controls.Add(this.lblSatellites);
            resources.ApplyResources(this.panInfoPane, "panInfoPane");
            this.panInfoPane.Name = "panInfoPane";
            this.panInfoPane.Resize += new System.EventHandler(this.panInfoPane_Resize);
            // 
            // lblSpeedI
            // 
            resources.ApplyResources(this.lblSpeedI, "lblSpeedI");
            this.lblSpeedI.Name = "lblSpeedI";
            // 
            // lblSpeed
            // 
            resources.ApplyResources(this.lblSpeed, "lblSpeed");
            this.lblSpeed.Name = "lblSpeed";
            // 
            // lblAverageI
            // 
            resources.ApplyResources(this.lblAverageI, "lblAverageI");
            this.lblAverageI.Name = "lblAverageI";
            // 
            // lblAverage
            // 
            resources.ApplyResources(this.lblAverage, "lblAverage");
            this.lblAverage.Name = "lblAverage";
            // 
            // lblDistanceI
            // 
            resources.ApplyResources(this.lblDistanceI, "lblDistanceI");
            this.lblDistanceI.Name = "lblDistanceI";
            // 
            // lblDistance
            // 
            resources.ApplyResources(this.lblDistance, "lblDistance");
            this.lblDistance.Name = "lblDistance";
            // 
            // lblAerialI
            // 
            resources.ApplyResources(this.lblAerialI, "lblAerialI");
            this.lblAerialI.Name = "lblAerialI";
            // 
            // lblAerial
            // 
            resources.ApplyResources(this.lblAerial, "lblAerial");
            this.lblAerial.Name = "lblAerial";
            // 
            // lblElevationI
            // 
            resources.ApplyResources(this.lblElevationI, "lblElevationI");
            this.lblElevationI.Name = "lblElevationI";
            // 
            // lblElevation
            // 
            resources.ApplyResources(this.lblElevation, "lblElevation");
            this.lblElevation.Name = "lblElevation";
            // 
            // lblElevationMinusI
            // 
            resources.ApplyResources(this.lblElevationMinusI, "lblElevationMinusI");
            this.lblElevationMinusI.Name = "lblElevationMinusI";
            // 
            // lblElevationMinus
            // 
            resources.ApplyResources(this.lblElevationMinus, "lblElevationMinus");
            this.lblElevationMinus.Name = "lblElevationMinus";
            // 
            // lblTimeI
            // 
            resources.ApplyResources(this.lblTimeI, "lblTimeI");
            this.lblTimeI.Name = "lblTimeI";
            // 
            // lblTime
            // 
            resources.ApplyResources(this.lblTime, "lblTime");
            this.lblTime.Name = "lblTime";
            // 
            // lblStopTimeI
            // 
            resources.ApplyResources(this.lblStopTimeI, "lblStopTimeI");
            this.lblStopTimeI.Name = "lblStopTimeI";
            // 
            // lblStopTime
            // 
            resources.ApplyResources(this.lblStopTime, "lblStopTime");
            this.lblStopTime.Name = "lblStopTime";
            // 
            // lblPointsI
            // 
            resources.ApplyResources(this.lblPointsI, "lblPointsI");
            this.lblPointsI.Name = "lblPointsI";
            // 
            // lblPoints
            // 
            resources.ApplyResources(this.lblPoints, "lblPoints");
            this.lblPoints.Name = "lblPoints";
            // 
            // lblSatellitesI
            // 
            resources.ApplyResources(this.lblSatellitesI, "lblSatellitesI");
            this.lblSatellitesI.Name = "lblSatellitesI";
            // 
            // lblSatellites
            // 
            resources.ApplyResources(this.lblSatellites, "lblSatellites");
            this.lblSatellites.Name = "lblSatellites";
            // 
            // stsStatus
            // 
            this.stsStatus.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.stsStatus, "stsStatus");
            this.stsStatus.Items = new string[] {
        "Press Start",
        ""};
            this.stsStatus.Name = "stsStatus";
            // 
            // mainPanelMenu
            // 
            this.mainPanelMenu.MenuItems.Add(this.startMenuItem);
            this.mainPanelMenu.MenuItems.Add(this.menuItem2);
            // 
            // startMenuItem
            // 
            resources.ApplyResources(this.startMenuItem, "startMenuItem");
            this.startMenuItem.Click += new System.EventHandler(this.startMenuItem_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.MenuItems.Add(this.clearMenuItem);
            this.menuItem2.MenuItems.Add(this.verboseMenuItem);
            this.menuItem2.MenuItems.Add(this.settingsMenuItem);
            this.menuItem2.MenuItems.Add(this.mniSatellites);
            this.menuItem2.MenuItems.Add(this.mniSep3);
            this.menuItem2.MenuItems.Add(this.mniSaveStatistic);
            this.menuItem2.MenuItems.Add(this.mniSep1);
            this.menuItem2.MenuItems.Add(this.aboutMenuItem);
            this.menuItem2.MenuItems.Add(this.mniHelp);
            this.menuItem2.MenuItems.Add(this.mniSep2);
            this.menuItem2.MenuItems.Add(this.menuExit);
            resources.ApplyResources(this.menuItem2, "menuItem2");
            // 
            // clearMenuItem
            // 
            resources.ApplyResources(this.clearMenuItem, "clearMenuItem");
            this.clearMenuItem.Click += new System.EventHandler(this.clearMenuItem_Click);
            // 
            // verboseMenuItem
            // 
            resources.ApplyResources(this.verboseMenuItem, "verboseMenuItem");
            this.verboseMenuItem.Click += new System.EventHandler(this.verboseMenuItem_Click);
            // 
            // settingsMenuItem
            // 
            resources.ApplyResources(this.settingsMenuItem, "settingsMenuItem");
            this.settingsMenuItem.Click += new System.EventHandler(this.settingsMenuItem_Click);
            // 
            // mniSatellites
            // 
            resources.ApplyResources(this.mniSatellites, "mniSatellites");
            this.mniSatellites.Click += new System.EventHandler(this.mniSatellites_Click);
            // 
            // mniSep3
            // 
            resources.ApplyResources(this.mniSep3, "mniSep3");
            // 
            // mniSaveStatistic
            // 
            resources.ApplyResources(this.mniSaveStatistic, "mniSaveStatistic");
            this.mniSaveStatistic.Click += new System.EventHandler(this.mniSaveStatistic_Click);
            // 
            // mniSep1
            // 
            resources.ApplyResources(this.mniSep1, "mniSep1");
            // 
            // aboutMenuItem
            // 
            resources.ApplyResources(this.aboutMenuItem, "aboutMenuItem");
            this.aboutMenuItem.Click += new System.EventHandler(this.aboutMenuItem_Click);
            // 
            // mniHelp
            // 
            resources.ApplyResources(this.mniHelp, "mniHelp");
            this.mniHelp.Click += new System.EventHandler(this.mniHelp_Click);
            // 
            // mniSep2
            // 
            resources.ApplyResources(this.mniSep2, "mniSep2");
            // 
            // menuExit
            // 
            resources.ApplyResources(this.menuExit, "menuExit");
            this.menuExit.Click += new System.EventHandler(this.menuExit_Click);
            // 
            // aboutPanel
            // 
            this.aboutPanel.Controls.Add(this.linkLabel1);
            this.aboutPanel.Controls.Add(this.pictureBox1);
            this.aboutPanel.Controls.Add(this.aboutLabel);
            resources.ApplyResources(this.aboutPanel, "aboutPanel");
            this.aboutPanel.Name = "aboutPanel";
            // 
            // linkLabel1
            // 
            resources.ApplyResources(this.linkLabel1, "linkLabel1");
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Click += new System.EventHandler(this.linkLabel1_Click);
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            // 
            // aboutLabel
            // 
            resources.ApplyResources(this.aboutLabel, "aboutLabel");
            this.aboutLabel.Name = "aboutLabel";
            // 
            // settingsPanel
            // 
            resources.ApplyResources(this.settingsPanel, "settingsPanel");
            this.settingsPanel.Controls.Add(this.fraFormat);
            this.settingsPanel.Controls.Add(this.fraOptions);
            this.settingsPanel.Controls.Add(this.fraSpeed);
            this.settingsPanel.Controls.Add(this.fraCOMPort);
            this.settingsPanel.Name = "settingsPanel";
            // 
            // fraFormat
            // 
            this.fraFormat.Controls.Add(this.panTRackLogType);
            this.fraFormat.Controls.Add(this.panFileFormat);
            this.fraFormat.Controls.Add(this.chkAltitude);
            resources.ApplyResources(this.fraFormat, "fraFormat");
            this.fraFormat.Name = "fraFormat";
            // 
            // panTRackLogType
            // 
            resources.ApplyResources(this.panTRackLogType, "panTRackLogType");
            this.panTRackLogType.Controls.Add(this.optTrack);
            this.panTRackLogType.Controls.Add(this.optDistinct);
            this.panTRackLogType.Name = "panTRackLogType";
            // 
            // optTrack
            // 
            resources.ApplyResources(this.optTrack, "optTrack");
            this.optTrack.Name = "optTrack";
            // 
            // optDistinct
            // 
            resources.ApplyResources(this.optDistinct, "optDistinct");
            this.optDistinct.Name = "optDistinct";
            // 
            // panFileFormat
            // 
            resources.ApplyResources(this.panFileFormat, "panFileFormat");
            this.panFileFormat.Controls.Add(this.optCSV);
            this.panFileFormat.Controls.Add(this.radioButtonKML);
            this.panFileFormat.Controls.Add(this.radioButtonGPX);
            this.panFileFormat.Name = "panFileFormat";
            // 
            // optCSV
            // 
            resources.ApplyResources(this.optCSV, "optCSV");
            this.optCSV.Name = "optCSV";
            // 
            // radioButtonKML
            // 
            resources.ApplyResources(this.radioButtonKML, "radioButtonKML");
            this.radioButtonKML.Name = "radioButtonKML";
            // 
            // radioButtonGPX
            // 
            resources.ApplyResources(this.radioButtonGPX, "radioButtonGPX");
            this.radioButtonGPX.Name = "radioButtonGPX";
            // 
            // chkAltitude
            // 
            resources.ApplyResources(this.chkAltitude, "chkAltitude");
            this.chkAltitude.Name = "chkAltitude";
            // 
            // fraOptions
            // 
            this.fraOptions.Controls.Add(this.CheckBoxToTextBox);
            this.fraOptions.Controls.Add(this.CheckBoxToFile);
            this.fraOptions.Controls.Add(this.logLocationTextBox);
            this.fraOptions.Controls.Add(this.saveDialogButton);
            resources.ApplyResources(this.fraOptions, "fraOptions");
            this.fraOptions.Name = "fraOptions";
            // 
            // CheckBoxToTextBox
            // 
            this.CheckBoxToTextBox.Checked = true;
            this.CheckBoxToTextBox.CheckState = System.Windows.Forms.CheckState.Checked;
            resources.ApplyResources(this.CheckBoxToTextBox, "CheckBoxToTextBox");
            this.CheckBoxToTextBox.Name = "CheckBoxToTextBox";
            // 
            // CheckBoxToFile
            // 
            resources.ApplyResources(this.CheckBoxToFile, "CheckBoxToFile");
            this.CheckBoxToFile.Name = "CheckBoxToFile";
            // 
            // logLocationTextBox
            // 
            resources.ApplyResources(this.logLocationTextBox, "logLocationTextBox");
            this.logLocationTextBox.Name = "logLocationTextBox";
            // 
            // saveDialogButton
            // 
            resources.ApplyResources(this.saveDialogButton, "saveDialogButton");
            this.saveDialogButton.Name = "saveDialogButton";
            this.saveDialogButton.Click += new System.EventHandler(this.saveDialogButton_Click);
            // 
            // fraSpeed
            // 
            this.fraSpeed.Controls.Add(this.NumericUpDownInterval);
            this.fraSpeed.Controls.Add(this.label1);
            resources.ApplyResources(this.fraSpeed, "fraSpeed");
            this.fraSpeed.Name = "fraSpeed";
            // 
            // NumericUpDownInterval
            // 
            resources.ApplyResources(this.NumericUpDownInterval, "NumericUpDownInterval");
            this.NumericUpDownInterval.Maximum = new decimal(new int[] {
            50000,
            0,
            0,
            0});
            this.NumericUpDownInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NumericUpDownInterval.Name = "NumericUpDownInterval";
            this.NumericUpDownInterval.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // fraCOMPort
            // 
            this.fraCOMPort.Controls.Add(this.chkUseWindowsDriver);
            this.fraCOMPort.Controls.Add(this.lblRecommendedPort);
            this.fraCOMPort.Controls.Add(this.ComboBoxCOMPorts);
            this.fraCOMPort.Controls.Add(this.ComboBaudRate);
            this.fraCOMPort.Controls.Add(this.label4);
            this.fraCOMPort.Controls.Add(this.label5);
            resources.ApplyResources(this.fraCOMPort, "fraCOMPort");
            this.fraCOMPort.Name = "fraCOMPort";
            // 
            // chkUseWindowsDriver
            // 
            resources.ApplyResources(this.chkUseWindowsDriver, "chkUseWindowsDriver");
            this.chkUseWindowsDriver.Name = "chkUseWindowsDriver";
            this.chkUseWindowsDriver.CheckStateChanged += new System.EventHandler(this.chkUseWindowsDriver_CheckStateChanged);
            // 
            // lblRecommendedPort
            // 
            resources.ApplyResources(this.lblRecommendedPort, "lblRecommendedPort");
            this.lblRecommendedPort.Name = "lblRecommendedPort";
            // 
            // ComboBoxCOMPorts
            // 
            this.ComboBoxCOMPorts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            resources.ApplyResources(this.ComboBoxCOMPorts, "ComboBoxCOMPorts");
            this.ComboBoxCOMPorts.Name = "ComboBoxCOMPorts";
            // 
            // ComboBaudRate
            // 
            resources.ApplyResources(this.ComboBaudRate, "ComboBaudRate");
            this.ComboBaudRate.Items.Add(resources.GetString("ComboBaudRate.Items"));
            this.ComboBaudRate.Items.Add(resources.GetString("ComboBaudRate.Items1"));
            this.ComboBaudRate.Items.Add(resources.GetString("ComboBaudRate.Items2"));
            this.ComboBaudRate.Items.Add(resources.GetString("ComboBaudRate.Items3"));
            this.ComboBaudRate.Items.Add(resources.GetString("ComboBaudRate.Items4"));
            this.ComboBaudRate.Items.Add(resources.GetString("ComboBaudRate.Items5"));
            this.ComboBaudRate.Name = "ComboBaudRate";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // aboutPanelMenu
            // 
            this.aboutPanelMenu.MenuItems.Add(this.cancelMenuItem);
            // 
            // cancelMenuItem
            // 
            resources.ApplyResources(this.cancelMenuItem, "cancelMenuItem");
            this.cancelMenuItem.Click += new System.EventHandler(this.cancelMenuItem_Click);
            // 
            // settingsPanelMenu
            // 
            this.settingsPanelMenu.MenuItems.Add(this.saveMenuItem);
            this.settingsPanelMenu.MenuItems.Add(this.mniAdvanced);
            this.settingsPanelMenu.MenuItems.Add(this.backMenuItem);
            // 
            // saveMenuItem
            // 
            resources.ApplyResources(this.saveMenuItem, "saveMenuItem");
            this.saveMenuItem.Click += new System.EventHandler(this.saveMenuItem_Click);
            // 
            // mniAdvanced
            // 
            resources.ApplyResources(this.mniAdvanced, "mniAdvanced");
            this.mniAdvanced.Click += new System.EventHandler(this.mniAdvanced_Click);
            // 
            // backMenuItem
            // 
            resources.ApplyResources(this.backMenuItem, "backMenuItem");
            this.backMenuItem.Click += new System.EventHandler(this.backMenuItem_Click);
            // 
            // tmrCountDown
            // 
            this.tmrCountDown.Interval = 1000;
            this.tmrCountDown.Tick += new System.EventHandler(this.tmrCountDown_Tick);
            // 
            // tmrBeep
            // 
            this.tmrBeep.Tick += new System.EventHandler(this.tmrBeep_Tick);
            // 
            // panHelper
            // 
            resources.ApplyResources(this.panHelper, "panHelper");
            this.panHelper.Name = "panHelper";
            // 
            // TrackerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.settingsPanel);
            this.Controls.Add(this.aboutPanel);
            this.Controls.Add(this.mainPanel);
            this.Menu = this.mainPanelMenu;
            this.Name = "TrackerForm";
            this.Load += new System.EventHandler(this.GPSTracka_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.GPSTracka_Closing);
            this.mainPanel.ResumeLayout(false);
            this.panInfoPane.ResumeLayout(false);
            this.aboutPanel.ResumeLayout(false);
            this.settingsPanel.ResumeLayout(false);
            this.fraFormat.ResumeLayout(false);
            this.panTRackLogType.ResumeLayout(false);
            this.panFileFormat.ResumeLayout(false);
            this.fraOptions.ResumeLayout(false);
            this.fraSpeed.ResumeLayout(false);
            this.fraCOMPort.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel mainPanel;
        private MyTextBox TextBoxRawLog;
        private System.Windows.Forms.MainMenu mainPanelMenu;
        private System.Windows.Forms.MenuItem startMenuItem;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem clearMenuItem;
        private System.Windows.Forms.MenuItem verboseMenuItem;
        private System.Windows.Forms.MenuItem settingsMenuItem;
        private System.Windows.Forms.MenuItem mniSep1;
        private System.Windows.Forms.MenuItem aboutMenuItem;
        private System.Windows.Forms.Panel aboutPanel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label aboutLabel;
        private System.Windows.Forms.Panel settingsPanel;
        private System.Windows.Forms.CheckBox CheckBoxToFile;
        private System.Windows.Forms.CheckBox CheckBoxToTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox ComboBaudRate;
        private System.Windows.Forms.MainMenu aboutPanelMenu;
        private System.Windows.Forms.MainMenu settingsPanelMenu;
        private System.Windows.Forms.MenuItem cancelMenuItem;
        private System.Windows.Forms.MenuItem saveMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown NumericUpDownInterval;
        private System.Windows.Forms.ComboBox ComboBoxCOMPorts;
        private System.Windows.Forms.Label label4;
        private OpenNETCF.Windows.Forms.GroupBox  fraCOMPort;
        private OpenNETCF.Windows.Forms.GroupBox fraSpeed;
        private OpenNETCF.Windows.Forms.GroupBox fraOptions;
        private System.Windows.Forms.Button saveDialogButton;
        private MyTextBox logLocationTextBox;
        private System.Windows.Forms.MenuItem backMenuItem;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.MenuItem menuExit;
        private System.Windows.Forms.RadioButton radioButtonKML;
        private System.Windows.Forms.RadioButton radioButtonGPX;
        private OpenNETCF.Windows.Forms.GroupBox fraFormat;
        private System.Windows.Forms.CheckBox chkAltitude;
        private System.Windows.Forms.Panel panFileFormat;
        private System.Windows.Forms.Panel panTRackLogType;
        private System.Windows.Forms.RadioButton optTrack;
        private System.Windows.Forms.RadioButton optDistinct;
        private System.Windows.Forms.MenuItem mniAdvanced;
        private ItemsStatusBar stsStatus;
        private System.Windows.Forms.Timer tmrCountDown;
        private System.Windows.Forms.MenuItem mniHelp;
        private System.Windows.Forms.Label lblRecommendedPort;
        private System.Windows.Forms.MenuItem mniSatellites;
        private System.Windows.Forms.MenuItem mniSep2;
        private System.Windows.Forms.CheckBox chkUseWindowsDriver;
        private System.Windows.Forms.Panel panInfoPane;
        private System.Windows.Forms.Label lblDistance;
        private System.Windows.Forms.Label lblDistanceI;
        private System.Windows.Forms.Label lblSpeed;
        private System.Windows.Forms.Label lblSpeedI;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label lblTimeI;
        private System.Windows.Forms.Label lblStopTime;
        private System.Windows.Forms.Label lblStopTimeI;
        private System.Windows.Forms.Label lblElevationI;
        private System.Windows.Forms.Label lblAverageI;
        private System.Windows.Forms.Label lblSatellitesI;
        private System.Windows.Forms.Label lblPointsI;
        private System.Windows.Forms.Label lblElevationMinusI;
        private System.Windows.Forms.Label lblSatellites;
        private System.Windows.Forms.Label lblPoints;
        private System.Windows.Forms.Label lblElevationMinus;
        private System.Windows.Forms.Label lblElevation;
        private System.Windows.Forms.Label lblAerial;
        private System.Windows.Forms.Label lblAverage;
        private System.Windows.Forms.Label lblAerialI;
        private System.Windows.Forms.Timer tmrBeep;
        private System.Windows.Forms.RadioButton optCSV;
        private System.Windows.Forms.MenuItem mniSep3;
        private System.Windows.Forms.MenuItem mniSaveStatistic;
        private System.Windows.Forms.Panel panHelper;
    }
}

