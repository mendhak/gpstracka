namespace GPSTracka {
    partial class AdvancedConfigForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdvancedConfigForm));
            this.mmnMain = new System.Windows.Forms.MainMenu();
            this.tmiOK = new System.Windows.Forms.MenuItem();
            this.tmiCancel = new System.Windows.Forms.MenuItem();
            this.lblTime = new System.Windows.Forms.Label();
            this.panTime = new System.Windows.Forms.Panel();
            this.optTimeSystem = new System.Windows.Forms.RadioButton();
            this.optTimeGPS = new System.Windows.Forms.RadioButton();
            this.lblAltitudeCorrection = new System.Windows.Forms.Label();
            this.nudAltitudeCorrection = new System.Windows.Forms.NumericUpDown();
            this.chkStartImmediatelly = new System.Windows.Forms.CheckBox();
            this.lblKMLDescFormat = new System.Windows.Forms.Label();
            this.lblKMLNameFormat = new System.Windows.Forms.Label();
            this.lblPointPlacemark = new System.Windows.Forms.Label();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tapGeneral = new System.Windows.Forms.TabPage();
            this.panGeneral = new System.Windows.Forms.Panel();
            this.chkDoNotPause = new System.Windows.Forms.CheckBox();
            this.txtKeepAwake = new GPSTracka.MyTextBox();
            this.lblInvalidMaxPositions = new System.Windows.Forms.Label();
            this.lblAwakeDevices = new System.Windows.Forms.Label();
            this.nudInvalidMax = new System.Windows.Forms.NumericUpDown();
            this.lblInvalidMaxAfter = new System.Windows.Forms.Label();
            this.lblMinimalDistanceM = new System.Windows.Forms.Label();
            this.nudMinimalDistance = new System.Windows.Forms.NumericUpDown();
            this.lblAltCorrM = new System.Windows.Forms.Label();
            this.lblMinimalDistance = new System.Windows.Forms.Label();
            this.tapDisplay = new System.Windows.Forms.TabPage();
            this.panDisplay = new System.Windows.Forms.Panel();
            this.panUnits = new System.Windows.Forms.Panel();
            this.lblSpeedUnit = new System.Windows.Forms.Label();
            this.cmbElevationUnit = new System.Windows.Forms.ComboBox();
            this.lblElevationUnit = new System.Windows.Forms.Label();
            this.cmbDistanceUnit = new System.Windows.Forms.ComboBox();
            this.lblDistanceUnit = new System.Windows.Forms.Label();
            this.cmbSpeedUnit = new System.Windows.Forms.ComboBox();
            this.chkInfoPane = new System.Windows.Forms.CheckBox();
            this.panLogFormat = new System.Windows.Forms.Panel();
            this.cmdDisplayFormatInfo = new System.Windows.Forms.Button();
            this.lblLogFormat = new System.Windows.Forms.Label();
            this.txtLogFormat = new GPSTracka.MyTextBox();
            this.cmdLogFormatTest = new System.Windows.Forms.Button();
            this.panClearEveryXLInes = new System.Windows.Forms.Panel();
            this.lblClearLog = new System.Windows.Forms.Label();
            this.lblLines = new System.Windows.Forms.Label();
            this.lblClearInfo = new System.Windows.Forms.Label();
            this.nudMaxLogLen = new System.Windows.Forms.NumericUpDown();
            this.chkStatusBar = new System.Windows.Forms.CheckBox();
            this.panLanguage = new System.Windows.Forms.Panel();
            this.cmbLanguage = new System.Windows.Forms.ComboBox();
            this.lblLanguage = new System.Windows.Forms.Label();
            this.tapKML = new System.Windows.Forms.TabPage();
            this.panKML = new System.Windows.Forms.Panel();
            this.copKMLColor = new GPSTracka.ColorPicker();
            this.panKmlFormatInfo = new System.Windows.Forms.Panel();
            this.lblKmlAsterisk = new System.Windows.Forms.Label();
            this.btnKmlFormatInfo = new System.Windows.Forms.Button();
            this.panKMLDescFormat = new System.Windows.Forms.Panel();
            this.txtKMLDescFormat = new GPSTracka.MyTextBox();
            this.cmdTestDesc = new System.Windows.Forms.Button();
            this.panKMLNameFormat = new System.Windows.Forms.Panel();
            this.txtKMLNameFormat = new GPSTracka.MyTextBox();
            this.cmdTestName = new System.Windows.Forms.Button();
            this.tabWindowsDriver = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtDemoFile = new GPSTracka.MyTextBox();
            this.lblDemoFile = new System.Windows.Forms.Label();
            this.cmdDemoFile = new System.Windows.Forms.Button();
            this.chkDemoMode = new System.Windows.Forms.CheckBox();
            this.lblCOMPort = new System.Windows.Forms.Label();
            this.chkSeaLevelAltitude = new System.Windows.Forms.CheckBox();
            this.lblWindowsDriverI = new System.Windows.Forms.Label();
            this.tapDiagnostic = new System.Windows.Forms.TabPage();
            this.lblEmptyI = new System.Windows.Forms.Label();
            this.panEmpty = new System.Windows.Forms.Panel();
            this.nudEmpty = new System.Windows.Forms.NumericUpDown();
            this.lblEmpty = new System.Windows.Forms.Label();
            this.lblEmptyPx = new System.Windows.Forms.Label();
            this.panBeepEvery = new System.Windows.Forms.Panel();
            this.nudBeepEvery = new System.Windows.Forms.NumericUpDown();
            this.lblBeepEvery = new System.Windows.Forms.Label();
            this.lblBeepEveryS = new System.Windows.Forms.Label();
            this.chkNMEA = new System.Windows.Forms.CheckBox();
            this.tapCSV = new System.Windows.Forms.TabPage();
            this.panCSVFields = new System.Windows.Forms.Panel();
            this.lstCSVFields = new System.Windows.Forms.ListBox();
            this.cmdCSVAdd = new System.Windows.Forms.Button();
            this.cmdCSVRemove = new System.Windows.Forms.Button();
            this.lstCSVAvailableFields = new System.Windows.Forms.ListBox();
            this.cmdCSVDown = new System.Windows.Forms.Button();
            this.lblCSVAvailableFields = new System.Windows.Forms.Label();
            this.cmdCSVUp = new System.Windows.Forms.Button();
            this.lblCSVFields = new System.Windows.Forms.Label();
            this.panCSVBottom = new System.Windows.Forms.Panel();
            this.chkCSVUTC = new System.Windows.Forms.CheckBox();
            this.cmbDateFormat = new System.Windows.Forms.Button();
            this.txtCSVDateFormat = new System.Windows.Forms.TextBox();
            this.lblCSVDateFormat = new System.Windows.Forms.Label();
            this.panCSV = new System.Windows.Forms.Panel();
            this.lblCSVSeparator = new System.Windows.Forms.Label();
            this.lblCSVQualifier = new System.Windows.Forms.Label();
            this.cmdCSVHeaderTab = new System.Windows.Forms.Button();
            this.lblCSVNewLine = new System.Windows.Forms.Label();
            this.cmdCSVTab = new System.Windows.Forms.Button();
            this.lblCSVHeader = new System.Windows.Forms.Label();
            this.txtCSVHeader = new GPSTracka.MyTextBox();
            this.txtCSVSeparator = new GPSTracka.MyTextBox();
            this.cmbCSVNewLine = new System.Windows.Forms.ComboBox();
            this.txtCSVQualifier = new GPSTracka.MyTextBox();
            this.cmbCSVQualifierUsage = new System.Windows.Forms.ComboBox();
            this.notFormatInfo = new Microsoft.WindowsCE.Forms.Notification();
            this.panTime.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tapGeneral.SuspendLayout();
            this.panGeneral.SuspendLayout();
            this.tapDisplay.SuspendLayout();
            this.panDisplay.SuspendLayout();
            this.panUnits.SuspendLayout();
            this.panLogFormat.SuspendLayout();
            this.panClearEveryXLInes.SuspendLayout();
            this.panLanguage.SuspendLayout();
            this.tapKML.SuspendLayout();
            this.panKML.SuspendLayout();
            this.panKmlFormatInfo.SuspendLayout();
            this.panKMLDescFormat.SuspendLayout();
            this.panKMLNameFormat.SuspendLayout();
            this.tabWindowsDriver.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tapDiagnostic.SuspendLayout();
            this.panEmpty.SuspendLayout();
            this.panBeepEvery.SuspendLayout();
            this.tapCSV.SuspendLayout();
            this.panCSVFields.SuspendLayout();
            this.panCSVBottom.SuspendLayout();
            this.panCSV.SuspendLayout();
            this.SuspendLayout();
            // 
            // mmnMain
            // 
            this.mmnMain.MenuItems.Add(this.tmiOK);
            this.mmnMain.MenuItems.Add(this.tmiCancel);
            // 
            // tmiOK
            // 
            resources.ApplyResources(this.tmiOK, "tmiOK");
            this.tmiOK.Click += new System.EventHandler(this.tmiOK_Click);
            // 
            // tmiCancel
            // 
            resources.ApplyResources(this.tmiCancel, "tmiCancel");
            this.tmiCancel.Click += new System.EventHandler(this.tmiCancel_Click);
            // 
            // lblTime
            // 
            resources.ApplyResources(this.lblTime, "lblTime");
            this.lblTime.Name = "lblTime";
            // 
            // panTime
            // 
            resources.ApplyResources(this.panTime, "panTime");
            this.panTime.Controls.Add(this.lblTime);
            this.panTime.Controls.Add(this.optTimeSystem);
            this.panTime.Controls.Add(this.optTimeGPS);
            this.panTime.Name = "panTime";
            // 
            // optTimeSystem
            // 
            this.optTimeSystem.Checked = true;
            resources.ApplyResources(this.optTimeSystem, "optTimeSystem");
            this.optTimeSystem.Name = "optTimeSystem";
            // 
            // optTimeGPS
            // 
            resources.ApplyResources(this.optTimeGPS, "optTimeGPS");
            this.optTimeGPS.Name = "optTimeGPS";
            // 
            // lblAltitudeCorrection
            // 
            resources.ApplyResources(this.lblAltitudeCorrection, "lblAltitudeCorrection");
            this.lblAltitudeCorrection.Name = "lblAltitudeCorrection";
            // 
            // nudAltitudeCorrection
            // 
            resources.ApplyResources(this.nudAltitudeCorrection, "nudAltitudeCorrection");
            this.nudAltitudeCorrection.Maximum = new decimal(new int[] {
            8850,
            0,
            0,
            0});
            this.nudAltitudeCorrection.Minimum = new decimal(new int[] {
            11034,
            0,
            0,
            -2147483648});
            this.nudAltitudeCorrection.Name = "nudAltitudeCorrection";
            // 
            // chkStartImmediatelly
            // 
            resources.ApplyResources(this.chkStartImmediatelly, "chkStartImmediatelly");
            this.chkStartImmediatelly.Name = "chkStartImmediatelly";
            // 
            // lblKMLDescFormat
            // 
            resources.ApplyResources(this.lblKMLDescFormat, "lblKMLDescFormat");
            this.lblKMLDescFormat.Name = "lblKMLDescFormat";
            // 
            // lblKMLNameFormat
            // 
            resources.ApplyResources(this.lblKMLNameFormat, "lblKMLNameFormat");
            this.lblKMLNameFormat.Name = "lblKMLNameFormat";
            // 
            // lblPointPlacemark
            // 
            resources.ApplyResources(this.lblPointPlacemark, "lblPointPlacemark");
            this.lblPointPlacemark.Name = "lblPointPlacemark";
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tapGeneral);
            this.tabMain.Controls.Add(this.tapDisplay);
            this.tabMain.Controls.Add(this.tapKML);
            this.tabMain.Controls.Add(this.tabWindowsDriver);
            this.tabMain.Controls.Add(this.tapDiagnostic);
            this.tabMain.Controls.Add(this.tapCSV);
            resources.ApplyResources(this.tabMain, "tabMain");
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            // 
            // tapGeneral
            // 
            this.tapGeneral.Controls.Add(this.panGeneral);
            resources.ApplyResources(this.tapGeneral, "tapGeneral");
            this.tapGeneral.Name = "tapGeneral";
            // 
            // panGeneral
            // 
            resources.ApplyResources(this.panGeneral, "panGeneral");
            this.panGeneral.Controls.Add(this.chkDoNotPause);
            this.panGeneral.Controls.Add(this.panTime);
            this.panGeneral.Controls.Add(this.txtKeepAwake);
            this.panGeneral.Controls.Add(this.lblInvalidMaxPositions);
            this.panGeneral.Controls.Add(this.lblAwakeDevices);
            this.panGeneral.Controls.Add(this.nudInvalidMax);
            this.panGeneral.Controls.Add(this.lblInvalidMaxAfter);
            this.panGeneral.Controls.Add(this.lblAltitudeCorrection);
            this.panGeneral.Controls.Add(this.lblMinimalDistanceM);
            this.panGeneral.Controls.Add(this.nudAltitudeCorrection);
            this.panGeneral.Controls.Add(this.nudMinimalDistance);
            this.panGeneral.Controls.Add(this.lblAltCorrM);
            this.panGeneral.Controls.Add(this.lblMinimalDistance);
            this.panGeneral.Controls.Add(this.chkStartImmediatelly);
            this.panGeneral.Name = "panGeneral";
            // 
            // chkDoNotPause
            // 
            resources.ApplyResources(this.chkDoNotPause, "chkDoNotPause");
            this.chkDoNotPause.Name = "chkDoNotPause";
            // 
            // txtKeepAwake
            // 
            resources.ApplyResources(this.txtKeepAwake, "txtKeepAwake");
            this.txtKeepAwake.Name = "txtKeepAwake";
            // 
            // lblInvalidMaxPositions
            // 
            resources.ApplyResources(this.lblInvalidMaxPositions, "lblInvalidMaxPositions");
            this.lblInvalidMaxPositions.Name = "lblInvalidMaxPositions";
            // 
            // lblAwakeDevices
            // 
            resources.ApplyResources(this.lblAwakeDevices, "lblAwakeDevices");
            this.lblAwakeDevices.Name = "lblAwakeDevices";
            // 
            // nudInvalidMax
            // 
            resources.ApplyResources(this.nudInvalidMax, "nudInvalidMax");
            this.nudInvalidMax.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudInvalidMax.Name = "nudInvalidMax";
            // 
            // lblInvalidMaxAfter
            // 
            resources.ApplyResources(this.lblInvalidMaxAfter, "lblInvalidMaxAfter");
            this.lblInvalidMaxAfter.Name = "lblInvalidMaxAfter";
            // 
            // lblMinimalDistanceM
            // 
            resources.ApplyResources(this.lblMinimalDistanceM, "lblMinimalDistanceM");
            this.lblMinimalDistanceM.Name = "lblMinimalDistanceM";
            // 
            // nudMinimalDistance
            // 
            resources.ApplyResources(this.nudMinimalDistance, "nudMinimalDistance");
            this.nudMinimalDistance.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudMinimalDistance.Name = "nudMinimalDistance";
            // 
            // lblAltCorrM
            // 
            resources.ApplyResources(this.lblAltCorrM, "lblAltCorrM");
            this.lblAltCorrM.Name = "lblAltCorrM";
            // 
            // lblMinimalDistance
            // 
            resources.ApplyResources(this.lblMinimalDistance, "lblMinimalDistance");
            this.lblMinimalDistance.Name = "lblMinimalDistance";
            // 
            // tapDisplay
            // 
            this.tapDisplay.Controls.Add(this.panDisplay);
            resources.ApplyResources(this.tapDisplay, "tapDisplay");
            this.tapDisplay.Name = "tapDisplay";
            // 
            // panDisplay
            // 
            resources.ApplyResources(this.panDisplay, "panDisplay");
            this.panDisplay.Controls.Add(this.panUnits);
            this.panDisplay.Controls.Add(this.chkInfoPane);
            this.panDisplay.Controls.Add(this.panLogFormat);
            this.panDisplay.Controls.Add(this.panClearEveryXLInes);
            this.panDisplay.Controls.Add(this.chkStatusBar);
            this.panDisplay.Controls.Add(this.panLanguage);
            this.panDisplay.Name = "panDisplay";
            // 
            // panUnits
            // 
            this.panUnits.Controls.Add(this.lblSpeedUnit);
            this.panUnits.Controls.Add(this.cmbElevationUnit);
            this.panUnits.Controls.Add(this.lblElevationUnit);
            this.panUnits.Controls.Add(this.cmbDistanceUnit);
            this.panUnits.Controls.Add(this.lblDistanceUnit);
            this.panUnits.Controls.Add(this.cmbSpeedUnit);
            resources.ApplyResources(this.panUnits, "panUnits");
            this.panUnits.Name = "panUnits";
            // 
            // lblSpeedUnit
            // 
            resources.ApplyResources(this.lblSpeedUnit, "lblSpeedUnit");
            this.lblSpeedUnit.Name = "lblSpeedUnit";
            // 
            // cmbElevationUnit
            // 
            this.cmbElevationUnit.Items.Add(resources.GetString("cmbElevationUnit.Items"));
            this.cmbElevationUnit.Items.Add(resources.GetString("cmbElevationUnit.Items1"));
            this.cmbElevationUnit.Items.Add(resources.GetString("cmbElevationUnit.Items2"));
            this.cmbElevationUnit.Items.Add(resources.GetString("cmbElevationUnit.Items3"));
            resources.ApplyResources(this.cmbElevationUnit, "cmbElevationUnit");
            this.cmbElevationUnit.Name = "cmbElevationUnit";
            // 
            // lblElevationUnit
            // 
            resources.ApplyResources(this.lblElevationUnit, "lblElevationUnit");
            this.lblElevationUnit.Name = "lblElevationUnit";
            // 
            // cmbDistanceUnit
            // 
            this.cmbDistanceUnit.Items.Add(resources.GetString("cmbDistanceUnit.Items"));
            this.cmbDistanceUnit.Items.Add(resources.GetString("cmbDistanceUnit.Items1"));
            this.cmbDistanceUnit.Items.Add(resources.GetString("cmbDistanceUnit.Items2"));
            this.cmbDistanceUnit.Items.Add(resources.GetString("cmbDistanceUnit.Items3"));
            this.cmbDistanceUnit.Items.Add(resources.GetString("cmbDistanceUnit.Items4"));
            resources.ApplyResources(this.cmbDistanceUnit, "cmbDistanceUnit");
            this.cmbDistanceUnit.Name = "cmbDistanceUnit";
            // 
            // lblDistanceUnit
            // 
            resources.ApplyResources(this.lblDistanceUnit, "lblDistanceUnit");
            this.lblDistanceUnit.Name = "lblDistanceUnit";
            // 
            // cmbSpeedUnit
            // 
            this.cmbSpeedUnit.Items.Add(resources.GetString("cmbSpeedUnit.Items"));
            this.cmbSpeedUnit.Items.Add(resources.GetString("cmbSpeedUnit.Items1"));
            this.cmbSpeedUnit.Items.Add(resources.GetString("cmbSpeedUnit.Items2"));
            this.cmbSpeedUnit.Items.Add(resources.GetString("cmbSpeedUnit.Items3"));
            resources.ApplyResources(this.cmbSpeedUnit, "cmbSpeedUnit");
            this.cmbSpeedUnit.Name = "cmbSpeedUnit";
            // 
            // chkInfoPane
            // 
            resources.ApplyResources(this.chkInfoPane, "chkInfoPane");
            this.chkInfoPane.Name = "chkInfoPane";
            // 
            // panLogFormat
            // 
            this.panLogFormat.Controls.Add(this.cmdDisplayFormatInfo);
            this.panLogFormat.Controls.Add(this.lblLogFormat);
            this.panLogFormat.Controls.Add(this.txtLogFormat);
            this.panLogFormat.Controls.Add(this.cmdLogFormatTest);
            resources.ApplyResources(this.panLogFormat, "panLogFormat");
            this.panLogFormat.Name = "panLogFormat";
            // 
            // cmdDisplayFormatInfo
            // 
            resources.ApplyResources(this.cmdDisplayFormatInfo, "cmdDisplayFormatInfo");
            this.cmdDisplayFormatInfo.Name = "cmdDisplayFormatInfo";
            this.cmdDisplayFormatInfo.Click += new System.EventHandler(this.btnFormatInfo_Click);
            // 
            // lblLogFormat
            // 
            resources.ApplyResources(this.lblLogFormat, "lblLogFormat");
            this.lblLogFormat.Name = "lblLogFormat";
            // 
            // txtLogFormat
            // 
            resources.ApplyResources(this.txtLogFormat, "txtLogFormat");
            this.txtLogFormat.Name = "txtLogFormat";
            // 
            // cmdLogFormatTest
            // 
            resources.ApplyResources(this.cmdLogFormatTest, "cmdLogFormatTest");
            this.cmdLogFormatTest.Name = "cmdLogFormatTest";
            // 
            // panClearEveryXLInes
            // 
            this.panClearEveryXLInes.Controls.Add(this.lblClearLog);
            this.panClearEveryXLInes.Controls.Add(this.lblLines);
            this.panClearEveryXLInes.Controls.Add(this.lblClearInfo);
            this.panClearEveryXLInes.Controls.Add(this.nudMaxLogLen);
            resources.ApplyResources(this.panClearEveryXLInes, "panClearEveryXLInes");
            this.panClearEveryXLInes.Name = "panClearEveryXLInes";
            // 
            // lblClearLog
            // 
            resources.ApplyResources(this.lblClearLog, "lblClearLog");
            this.lblClearLog.Name = "lblClearLog";
            // 
            // lblLines
            // 
            resources.ApplyResources(this.lblLines, "lblLines");
            this.lblLines.Name = "lblLines";
            // 
            // lblClearInfo
            // 
            resources.ApplyResources(this.lblClearInfo, "lblClearInfo");
            this.lblClearInfo.BackColor = System.Drawing.SystemColors.Info;
            this.lblClearInfo.ForeColor = System.Drawing.SystemColors.InfoText;
            this.lblClearInfo.Name = "lblClearInfo";
            // 
            // nudMaxLogLen
            // 
            resources.ApplyResources(this.nudMaxLogLen, "nudMaxLogLen");
            this.nudMaxLogLen.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudMaxLogLen.Name = "nudMaxLogLen";
            // 
            // chkStatusBar
            // 
            resources.ApplyResources(this.chkStatusBar, "chkStatusBar");
            this.chkStatusBar.Name = "chkStatusBar";
            // 
            // panLanguage
            // 
            this.panLanguage.Controls.Add(this.cmbLanguage);
            this.panLanguage.Controls.Add(this.lblLanguage);
            resources.ApplyResources(this.panLanguage, "panLanguage");
            this.panLanguage.Name = "panLanguage";
            // 
            // cmbLanguage
            // 
            resources.ApplyResources(this.cmbLanguage, "cmbLanguage");
            this.cmbLanguage.Name = "cmbLanguage";
            // 
            // lblLanguage
            // 
            resources.ApplyResources(this.lblLanguage, "lblLanguage");
            this.lblLanguage.Name = "lblLanguage";
            // 
            // tapKML
            // 
            this.tapKML.Controls.Add(this.panKML);
            resources.ApplyResources(this.tapKML, "tapKML");
            this.tapKML.Name = "tapKML";
            // 
            // panKML
            // 
            resources.ApplyResources(this.panKML, "panKML");
            this.panKML.Controls.Add(this.copKMLColor);
            this.panKML.Controls.Add(this.panKmlFormatInfo);
            this.panKML.Controls.Add(this.panKMLDescFormat);
            this.panKML.Controls.Add(this.panKMLNameFormat);
            this.panKML.Controls.Add(this.lblPointPlacemark);
            this.panKML.Name = "panKML";
            // 
            // copKMLColor
            // 
            resources.ApplyResources(this.copKMLColor, "copKMLColor");
            this.copKMLColor.Name = "copKMLColor";
            // 
            // panKmlFormatInfo
            // 
            this.panKmlFormatInfo.Controls.Add(this.lblKmlAsterisk);
            this.panKmlFormatInfo.Controls.Add(this.btnKmlFormatInfo);
            resources.ApplyResources(this.panKmlFormatInfo, "panKmlFormatInfo");
            this.panKmlFormatInfo.Name = "panKmlFormatInfo";
            // 
            // lblKmlAsterisk
            // 
            this.lblKmlAsterisk.BackColor = System.Drawing.SystemColors.Info;
            resources.ApplyResources(this.lblKmlAsterisk, "lblKmlAsterisk");
            this.lblKmlAsterisk.Name = "lblKmlAsterisk";
            // 
            // btnKmlFormatInfo
            // 
            resources.ApplyResources(this.btnKmlFormatInfo, "btnKmlFormatInfo");
            this.btnKmlFormatInfo.Name = "btnKmlFormatInfo";
            this.btnKmlFormatInfo.Click += new System.EventHandler(this.btnFormatInfo_Click);
            // 
            // panKMLDescFormat
            // 
            this.panKMLDescFormat.Controls.Add(this.txtKMLDescFormat);
            this.panKMLDescFormat.Controls.Add(this.lblKMLDescFormat);
            this.panKMLDescFormat.Controls.Add(this.cmdTestDesc);
            resources.ApplyResources(this.panKMLDescFormat, "panKMLDescFormat");
            this.panKMLDescFormat.Name = "panKMLDescFormat";
            // 
            // txtKMLDescFormat
            // 
            resources.ApplyResources(this.txtKMLDescFormat, "txtKMLDescFormat");
            this.txtKMLDescFormat.Name = "txtKMLDescFormat";
            // 
            // cmdTestDesc
            // 
            resources.ApplyResources(this.cmdTestDesc, "cmdTestDesc");
            this.cmdTestDesc.Name = "cmdTestDesc";
            this.cmdTestDesc.Click += new System.EventHandler(this.cmdTestFormat_Click);
            // 
            // panKMLNameFormat
            // 
            this.panKMLNameFormat.Controls.Add(this.txtKMLNameFormat);
            this.panKMLNameFormat.Controls.Add(this.lblKMLNameFormat);
            this.panKMLNameFormat.Controls.Add(this.cmdTestName);
            resources.ApplyResources(this.panKMLNameFormat, "panKMLNameFormat");
            this.panKMLNameFormat.Name = "panKMLNameFormat";
            // 
            // txtKMLNameFormat
            // 
            resources.ApplyResources(this.txtKMLNameFormat, "txtKMLNameFormat");
            this.txtKMLNameFormat.Name = "txtKMLNameFormat";
            // 
            // cmdTestName
            // 
            resources.ApplyResources(this.cmdTestName, "cmdTestName");
            this.cmdTestName.Name = "cmdTestName";
            this.cmdTestName.Click += new System.EventHandler(this.cmdTestFormat_Click);
            // 
            // tabWindowsDriver
            // 
            resources.ApplyResources(this.tabWindowsDriver, "tabWindowsDriver");
            this.tabWindowsDriver.Controls.Add(this.panel1);
            this.tabWindowsDriver.Controls.Add(this.chkDemoMode);
            this.tabWindowsDriver.Controls.Add(this.lblCOMPort);
            this.tabWindowsDriver.Controls.Add(this.chkSeaLevelAltitude);
            this.tabWindowsDriver.Controls.Add(this.lblWindowsDriverI);
            this.tabWindowsDriver.Name = "tabWindowsDriver";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtDemoFile);
            this.panel1.Controls.Add(this.lblDemoFile);
            this.panel1.Controls.Add(this.cmdDemoFile);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // txtDemoFile
            // 
            resources.ApplyResources(this.txtDemoFile, "txtDemoFile");
            this.txtDemoFile.Name = "txtDemoFile";
            // 
            // lblDemoFile
            // 
            resources.ApplyResources(this.lblDemoFile, "lblDemoFile");
            this.lblDemoFile.Name = "lblDemoFile";
            // 
            // cmdDemoFile
            // 
            resources.ApplyResources(this.cmdDemoFile, "cmdDemoFile");
            this.cmdDemoFile.Name = "cmdDemoFile";
            this.cmdDemoFile.Click += new System.EventHandler(this.cmdDemoFile_Click);
            // 
            // chkDemoMode
            // 
            resources.ApplyResources(this.chkDemoMode, "chkDemoMode");
            this.chkDemoMode.Name = "chkDemoMode";
            // 
            // lblCOMPort
            // 
            resources.ApplyResources(this.lblCOMPort, "lblCOMPort");
            this.lblCOMPort.Name = "lblCOMPort";
            // 
            // chkSeaLevelAltitude
            // 
            resources.ApplyResources(this.chkSeaLevelAltitude, "chkSeaLevelAltitude");
            this.chkSeaLevelAltitude.Name = "chkSeaLevelAltitude";
            // 
            // lblWindowsDriverI
            // 
            resources.ApplyResources(this.lblWindowsDriverI, "lblWindowsDriverI");
            this.lblWindowsDriverI.Name = "lblWindowsDriverI";
            // 
            // tapDiagnostic
            // 
            resources.ApplyResources(this.tapDiagnostic, "tapDiagnostic");
            this.tapDiagnostic.Controls.Add(this.lblEmptyI);
            this.tapDiagnostic.Controls.Add(this.panEmpty);
            this.tapDiagnostic.Controls.Add(this.panBeepEvery);
            this.tapDiagnostic.Controls.Add(this.chkNMEA);
            this.tapDiagnostic.Name = "tapDiagnostic";
            // 
            // lblEmptyI
            // 
            this.lblEmptyI.BackColor = System.Drawing.SystemColors.Info;
            resources.ApplyResources(this.lblEmptyI, "lblEmptyI");
            this.lblEmptyI.Name = "lblEmptyI";
            // 
            // panEmpty
            // 
            this.panEmpty.Controls.Add(this.nudEmpty);
            this.panEmpty.Controls.Add(this.lblEmpty);
            this.panEmpty.Controls.Add(this.lblEmptyPx);
            resources.ApplyResources(this.panEmpty, "panEmpty");
            this.panEmpty.Name = "panEmpty";
            // 
            // nudEmpty
            // 
            resources.ApplyResources(this.nudEmpty, "nudEmpty");
            this.nudEmpty.Maximum = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this.nudEmpty.Name = "nudEmpty";
            // 
            // lblEmpty
            // 
            resources.ApplyResources(this.lblEmpty, "lblEmpty");
            this.lblEmpty.Name = "lblEmpty";
            // 
            // lblEmptyPx
            // 
            resources.ApplyResources(this.lblEmptyPx, "lblEmptyPx");
            this.lblEmptyPx.Name = "lblEmptyPx";
            // 
            // panBeepEvery
            // 
            this.panBeepEvery.Controls.Add(this.nudBeepEvery);
            this.panBeepEvery.Controls.Add(this.lblBeepEvery);
            this.panBeepEvery.Controls.Add(this.lblBeepEveryS);
            resources.ApplyResources(this.panBeepEvery, "panBeepEvery");
            this.panBeepEvery.Name = "panBeepEvery";
            // 
            // nudBeepEvery
            // 
            resources.ApplyResources(this.nudBeepEvery, "nudBeepEvery");
            this.nudBeepEvery.Maximum = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this.nudBeepEvery.Name = "nudBeepEvery";
            // 
            // lblBeepEvery
            // 
            resources.ApplyResources(this.lblBeepEvery, "lblBeepEvery");
            this.lblBeepEvery.Name = "lblBeepEvery";
            // 
            // lblBeepEveryS
            // 
            resources.ApplyResources(this.lblBeepEveryS, "lblBeepEveryS");
            this.lblBeepEveryS.Name = "lblBeepEveryS";
            // 
            // chkNMEA
            // 
            resources.ApplyResources(this.chkNMEA, "chkNMEA");
            this.chkNMEA.Name = "chkNMEA";
            // 
            // tapCSV
            // 
            this.tapCSV.Controls.Add(this.panCSVFields);
            this.tapCSV.Controls.Add(this.panCSVBottom);
            this.tapCSV.Controls.Add(this.panCSV);
            resources.ApplyResources(this.tapCSV, "tapCSV");
            this.tapCSV.Name = "tapCSV";
            // 
            // panCSVFields
            // 
            resources.ApplyResources(this.panCSVFields, "panCSVFields");
            this.panCSVFields.Controls.Add(this.lstCSVFields);
            this.panCSVFields.Controls.Add(this.cmdCSVAdd);
            this.panCSVFields.Controls.Add(this.cmdCSVRemove);
            this.panCSVFields.Controls.Add(this.lstCSVAvailableFields);
            this.panCSVFields.Controls.Add(this.cmdCSVDown);
            this.panCSVFields.Controls.Add(this.lblCSVAvailableFields);
            this.panCSVFields.Controls.Add(this.cmdCSVUp);
            this.panCSVFields.Controls.Add(this.lblCSVFields);
            this.panCSVFields.Name = "panCSVFields";
            this.panCSVFields.Resize += new System.EventHandler(this.panCSVFields_Resize);
            // 
            // lstCSVFields
            // 
            resources.ApplyResources(this.lstCSVFields, "lstCSVFields");
            this.lstCSVFields.Name = "lstCSVFields";
            this.lstCSVFields.SelectedIndexChanged += new System.EventHandler(this.lstCSVFields_SelectedIndexChanged);
            // 
            // cmdCSVAdd
            // 
            resources.ApplyResources(this.cmdCSVAdd, "cmdCSVAdd");
            this.cmdCSVAdd.Name = "cmdCSVAdd";
            this.cmdCSVAdd.Click += new System.EventHandler(this.cmdCSVAdd_Click);
            // 
            // cmdCSVRemove
            // 
            resources.ApplyResources(this.cmdCSVRemove, "cmdCSVRemove");
            this.cmdCSVRemove.Name = "cmdCSVRemove";
            this.cmdCSVRemove.Click += new System.EventHandler(this.cmdCSVRemove_Click);
            // 
            // lstCSVAvailableFields
            // 
            resources.ApplyResources(this.lstCSVAvailableFields, "lstCSVAvailableFields");
            this.lstCSVAvailableFields.Name = "lstCSVAvailableFields";
            this.lstCSVAvailableFields.SelectedIndexChanged += new System.EventHandler(this.lstCSVAvailableFields_SelectedIndexChanged);
            // 
            // cmdCSVDown
            // 
            resources.ApplyResources(this.cmdCSVDown, "cmdCSVDown");
            this.cmdCSVDown.Name = "cmdCSVDown";
            this.cmdCSVDown.Click += new System.EventHandler(this.cmdCSVDown_Click);
            // 
            // lblCSVAvailableFields
            // 
            resources.ApplyResources(this.lblCSVAvailableFields, "lblCSVAvailableFields");
            this.lblCSVAvailableFields.Name = "lblCSVAvailableFields";
            // 
            // cmdCSVUp
            // 
            resources.ApplyResources(this.cmdCSVUp, "cmdCSVUp");
            this.cmdCSVUp.Name = "cmdCSVUp";
            this.cmdCSVUp.Click += new System.EventHandler(this.cmdCSVUp_Click);
            // 
            // lblCSVFields
            // 
            resources.ApplyResources(this.lblCSVFields, "lblCSVFields");
            this.lblCSVFields.Name = "lblCSVFields";
            // 
            // panCSVBottom
            // 
            this.panCSVBottom.Controls.Add(this.chkCSVUTC);
            this.panCSVBottom.Controls.Add(this.cmbDateFormat);
            this.panCSVBottom.Controls.Add(this.txtCSVDateFormat);
            this.panCSVBottom.Controls.Add(this.lblCSVDateFormat);
            resources.ApplyResources(this.panCSVBottom, "panCSVBottom");
            this.panCSVBottom.Name = "panCSVBottom";
            // 
            // chkCSVUTC
            // 
            resources.ApplyResources(this.chkCSVUTC, "chkCSVUTC");
            this.chkCSVUTC.Name = "chkCSVUTC";
            // 
            // cmbDateFormat
            // 
            resources.ApplyResources(this.cmbDateFormat, "cmbDateFormat");
            this.cmbDateFormat.Name = "cmbDateFormat";
            this.cmbDateFormat.Click += new System.EventHandler(this.cmbDateFormat_Click);
            // 
            // txtCSVDateFormat
            // 
            resources.ApplyResources(this.txtCSVDateFormat, "txtCSVDateFormat");
            this.txtCSVDateFormat.Name = "txtCSVDateFormat";
            // 
            // lblCSVDateFormat
            // 
            resources.ApplyResources(this.lblCSVDateFormat, "lblCSVDateFormat");
            this.lblCSVDateFormat.Name = "lblCSVDateFormat";
            // 
            // panCSV
            // 
            this.panCSV.Controls.Add(this.lblCSVSeparator);
            this.panCSV.Controls.Add(this.lblCSVQualifier);
            this.panCSV.Controls.Add(this.cmdCSVHeaderTab);
            this.panCSV.Controls.Add(this.lblCSVNewLine);
            this.panCSV.Controls.Add(this.cmdCSVTab);
            this.panCSV.Controls.Add(this.lblCSVHeader);
            this.panCSV.Controls.Add(this.txtCSVHeader);
            this.panCSV.Controls.Add(this.txtCSVSeparator);
            this.panCSV.Controls.Add(this.cmbCSVNewLine);
            this.panCSV.Controls.Add(this.txtCSVQualifier);
            this.panCSV.Controls.Add(this.cmbCSVQualifierUsage);
            resources.ApplyResources(this.panCSV, "panCSV");
            this.panCSV.Name = "panCSV";
            // 
            // lblCSVSeparator
            // 
            resources.ApplyResources(this.lblCSVSeparator, "lblCSVSeparator");
            this.lblCSVSeparator.Name = "lblCSVSeparator";
            // 
            // lblCSVQualifier
            // 
            resources.ApplyResources(this.lblCSVQualifier, "lblCSVQualifier");
            this.lblCSVQualifier.Name = "lblCSVQualifier";
            // 
            // cmdCSVHeaderTab
            // 
            resources.ApplyResources(this.cmdCSVHeaderTab, "cmdCSVHeaderTab");
            this.cmdCSVHeaderTab.Name = "cmdCSVHeaderTab";
            this.cmdCSVHeaderTab.Click += new System.EventHandler(this.cmdCSVHeaderTab_Click);
            // 
            // lblCSVNewLine
            // 
            resources.ApplyResources(this.lblCSVNewLine, "lblCSVNewLine");
            this.lblCSVNewLine.Name = "lblCSVNewLine";
            // 
            // cmdCSVTab
            // 
            resources.ApplyResources(this.cmdCSVTab, "cmdCSVTab");
            this.cmdCSVTab.Name = "cmdCSVTab";
            this.cmdCSVTab.Click += new System.EventHandler(this.cmdCSVTab_Click);
            // 
            // lblCSVHeader
            // 
            resources.ApplyResources(this.lblCSVHeader, "lblCSVHeader");
            this.lblCSVHeader.Name = "lblCSVHeader";
            // 
            // txtCSVHeader
            // 
            resources.ApplyResources(this.txtCSVHeader, "txtCSVHeader");
            this.txtCSVHeader.Name = "txtCSVHeader";
            // 
            // txtCSVSeparator
            // 
            resources.ApplyResources(this.txtCSVSeparator, "txtCSVSeparator");
            this.txtCSVSeparator.Name = "txtCSVSeparator";
            // 
            // cmbCSVNewLine
            // 
            this.cmbCSVNewLine.Items.Add(resources.GetString("cmbCSVNewLine.Items"));
            this.cmbCSVNewLine.Items.Add(resources.GetString("cmbCSVNewLine.Items1"));
            this.cmbCSVNewLine.Items.Add(resources.GetString("cmbCSVNewLine.Items2"));
            resources.ApplyResources(this.cmbCSVNewLine, "cmbCSVNewLine");
            this.cmbCSVNewLine.Name = "cmbCSVNewLine";
            // 
            // txtCSVQualifier
            // 
            resources.ApplyResources(this.txtCSVQualifier, "txtCSVQualifier");
            this.txtCSVQualifier.Name = "txtCSVQualifier";
            // 
            // cmbCSVQualifierUsage
            // 
            this.cmbCSVQualifierUsage.Items.Add(resources.GetString("cmbCSVQualifierUsage.Items"));
            this.cmbCSVQualifierUsage.Items.Add(resources.GetString("cmbCSVQualifierUsage.Items1"));
            this.cmbCSVQualifierUsage.Items.Add(resources.GetString("cmbCSVQualifierUsage.Items2"));
            resources.ApplyResources(this.cmbCSVQualifierUsage, "cmbCSVQualifierUsage");
            this.cmbCSVQualifierUsage.Name = "cmbCSVQualifierUsage";
            // 
            // notFormatInfo
            // 
            resources.ApplyResources(this.notFormatInfo, "notFormatInfo");
            this.notFormatInfo.BalloonChanged += new Microsoft.WindowsCE.Forms.BalloonChangedEventHandler(this.notFormatInfo_BalloonChanged);
            // 
            // AdvancedConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.tabMain);
            this.Menu = this.mmnMain;
            this.MinimizeBox = false;
            this.Name = "AdvancedConfigForm";
            this.Closed += new System.EventHandler(this.AdvancedConfigForm_Closed);
            this.panTime.ResumeLayout(false);
            this.tabMain.ResumeLayout(false);
            this.tapGeneral.ResumeLayout(false);
            this.panGeneral.ResumeLayout(false);
            this.tapDisplay.ResumeLayout(false);
            this.panDisplay.ResumeLayout(false);
            this.panUnits.ResumeLayout(false);
            this.panLogFormat.ResumeLayout(false);
            this.panClearEveryXLInes.ResumeLayout(false);
            this.panLanguage.ResumeLayout(false);
            this.tapKML.ResumeLayout(false);
            this.panKML.ResumeLayout(false);
            this.panKmlFormatInfo.ResumeLayout(false);
            this.panKMLDescFormat.ResumeLayout(false);
            this.panKMLNameFormat.ResumeLayout(false);
            this.tabWindowsDriver.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tapDiagnostic.ResumeLayout(false);
            this.panEmpty.ResumeLayout(false);
            this.panBeepEvery.ResumeLayout(false);
            this.tapCSV.ResumeLayout(false);
            this.panCSVFields.ResumeLayout(false);
            this.panCSVBottom.ResumeLayout(false);
            this.panCSV.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem tmiOK;
        private System.Windows.Forms.MenuItem tmiCancel;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Panel panTime;
        private System.Windows.Forms.RadioButton optTimeGPS;
        private System.Windows.Forms.RadioButton optTimeSystem;
        private System.Windows.Forms.Label lblAltitudeCorrection;
        private System.Windows.Forms.NumericUpDown nudAltitudeCorrection;
        private System.Windows.Forms.CheckBox chkStartImmediatelly;
        private System.Windows.Forms.Label lblKMLDescFormat;
        private MyTextBox txtKMLDescFormat;
        private MyTextBox txtKMLNameFormat;
        private System.Windows.Forms.Label lblKMLNameFormat;
        private System.Windows.Forms.Label lblPointPlacemark;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tapGeneral;
        private System.Windows.Forms.TabPage tapKML;
        private global::GPSTracka.ColorPicker copKMLColor;
        private System.Windows.Forms.Button cmdTestDesc;
        private System.Windows.Forms.Button cmdTestName;
        private System.Windows.Forms.Label lblMinimalDistance;
        private System.Windows.Forms.NumericUpDown nudMinimalDistance;
        private System.Windows.Forms.Label lblMinimalDistanceM;
        private System.Windows.Forms.Label lblAltCorrM;
        private System.Windows.Forms.Label lblInvalidMaxAfter;
        private System.Windows.Forms.NumericUpDown nudInvalidMax;
        private System.Windows.Forms.Label lblInvalidMaxPositions;
        private System.Windows.Forms.CheckBox chkNMEA;
        private System.Windows.Forms.TabPage tabWindowsDriver;
        private System.Windows.Forms.Label lblWindowsDriverI;
        private System.Windows.Forms.CheckBox chkSeaLevelAltitude;
        private System.Windows.Forms.TabPage tapDiagnostic;
        private System.Windows.Forms.Label lblBeepEveryS;
        private System.Windows.Forms.NumericUpDown nudBeepEvery;
        private System.Windows.Forms.Label lblBeepEvery;
        private MyTextBox txtKeepAwake;
        private System.Windows.Forms.Label lblAwakeDevices;
        private System.Windows.Forms.TabPage tapDisplay;
        private System.Windows.Forms.CheckBox chkStatusBar;
        private System.Windows.Forms.Label lblClearLog;
        private System.Windows.Forms.NumericUpDown nudMaxLogLen;
        private System.Windows.Forms.Label lblLines;
        private System.Windows.Forms.Label lblClearInfo;
        private System.Windows.Forms.Label lblLogFormat;
        private MyTextBox txtLogFormat;
        private System.Windows.Forms.Button cmdLogFormatTest;
        private System.Windows.Forms.CheckBox chkInfoPane;
        private System.Windows.Forms.Label lblSpeedUnit;
        private System.Windows.Forms.ComboBox cmbSpeedUnit;
        private System.Windows.Forms.Label lblDistanceUnit;
        private System.Windows.Forms.ComboBox cmbDistanceUnit;
        private System.Windows.Forms.Label lblElevationUnit;
        private System.Windows.Forms.ComboBox cmbElevationUnit;
        private System.Windows.Forms.TabPage tapCSV;
        private MyTextBox txtCSVSeparator;
        private System.Windows.Forms.Label lblCSVQualifier;
        private System.Windows.Forms.Label lblCSVSeparator;
        private System.Windows.Forms.Button cmdCSVRemove;
        private System.Windows.Forms.Button cmdCSVAdd;
        private System.Windows.Forms.ListBox lstCSVFields;
        private System.Windows.Forms.ListBox lstCSVAvailableFields;
        private System.Windows.Forms.Label lblCSVFields;
        private MyTextBox txtCSVHeader;
        private System.Windows.Forms.ComboBox cmbCSVNewLine;
        private System.Windows.Forms.ComboBox cmbCSVQualifierUsage;
        private MyTextBox txtCSVQualifier;
        private System.Windows.Forms.Label lblCSVAvailableFields;
        private System.Windows.Forms.Label lblCSVHeader;
        private System.Windows.Forms.Label lblCSVNewLine;
        private System.Windows.Forms.Button cmdCSVUp;
        private System.Windows.Forms.Button cmdCSVDown;
        private System.Windows.Forms.Button cmdCSVTab;
        private System.Windows.Forms.Button cmdCSVHeaderTab;
        private System.Windows.Forms.Panel panKMLDescFormat;
        private System.Windows.Forms.Panel panKMLNameFormat;
        private System.Windows.Forms.Panel panCSVFields;
        private System.Windows.Forms.Panel panCSV;
        private System.Windows.Forms.Panel panLogFormat;
        private System.Windows.Forms.Panel panClearEveryXLInes;
        private System.Windows.Forms.Panel panUnits;
        private System.Windows.Forms.Panel panDisplay;
        private System.Windows.Forms.Panel panGeneral;
        private System.Windows.Forms.Panel panKML;
        private System.Windows.Forms.Panel panCSVBottom;
        private System.Windows.Forms.Button cmbDateFormat;
        private System.Windows.Forms.TextBox txtCSVDateFormat;
        private System.Windows.Forms.Label lblCSVDateFormat;
        private System.Windows.Forms.CheckBox chkCSVUTC;
        private System.Windows.Forms.Panel panLanguage;
        private System.Windows.Forms.ComboBox cmbLanguage;
        private System.Windows.Forms.Label lblLanguage;
        private System.Windows.Forms.Label lblKmlAsterisk;
        private System.Windows.Forms.Panel panBeepEvery;
        private System.Windows.Forms.Button btnKmlFormatInfo;
        private Microsoft.WindowsCE.Forms.Notification notFormatInfo;
        private System.Windows.Forms.Panel panKmlFormatInfo;
        private System.Windows.Forms.Button cmdDisplayFormatInfo;
        private System.Windows.Forms.Panel panEmpty;
        private System.Windows.Forms.NumericUpDown nudEmpty;
        private System.Windows.Forms.Label lblEmpty;
        private System.Windows.Forms.Label lblEmptyPx;
        private System.Windows.Forms.Label lblEmptyI;
        private System.Windows.Forms.CheckBox chkDoNotPause;
        private System.Windows.Forms.CheckBox chkDemoMode;
        private System.Windows.Forms.Label lblCOMPort;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button cmdDemoFile;
        private System.Windows.Forms.Label lblDemoFile;
        private MyTextBox txtDemoFile;
    }
}