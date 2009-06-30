//Added by Đonny (whole file)
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using GPSTracka.Properties;

namespace GPSTracka
{
    /// <summary>Form to edit advanced settings</summary>
    internal partial class AdvancedConfigForm : Form
    {
        /// <summary>CTor</summary>
        public AdvancedConfigForm()
        {
            InitializeComponent();
            {
                lstCSVAvailableFields.Items.Add(new CSVItem(0, Resources.Latitude));
                lstCSVAvailableFields.Items.Add(new CSVItem(1, Resources.Longitude));
                lstCSVAvailableFields.Items.Add(new CSVItem(2, Resources.Altitude));
                lstCSVAvailableFields.Items.Add(new CSVItem(3, Resources.Date));
                lstCSVAvailableFields.DisplayMember = "Desc";
                lstCSVFields.DisplayMember = "Desc";
                cmbLanguage.DisplayMember = "NativeName";
                //Cultures
                cmbLanguage.Items.Add(Resources.Default);
                object[] attrs = typeof(TrackerForm).Assembly.GetCustomAttributes(typeof(System.Resources.NeutralResourcesLanguageAttribute), false);
                if (string.IsNullOrEmpty(AdvancedConfig.Language)) cmbLanguage.SelectedIndex = 0;
                if (attrs.Length > 0)
                    try
                    {
                        cmbLanguage.Items.Add(System.Globalization.CultureInfo.GetCultureInfo(((System.Resources.NeutralResourcesLanguageAttribute)attrs[0]).CultureName));
                    }
                    catch { }
                foreach (string dir in System.IO.Directory.GetDirectories(System.IO.Path.GetDirectoryName(typeof(TrackerForm).Assembly.GetModules()[0].FullyQualifiedName)))
                    try
                    {
                        cmbLanguage.Items.Add(System.Globalization.CultureInfo.GetCultureInfo(System.IO.Path.GetFileName(dir)));
                    }
                    catch { }
            }
            copKMLColor.Text = Properties.Resources.LineColor;
            chkStatusBar.Checked = AdvancedConfig.StatusBar;
            optTimeGPS.Checked = AdvancedConfig.UseGpsTime;
            optTimeSystem.Checked = !AdvancedConfig.UseGpsTime;
            nudMaxLogLen.Value = AdvancedConfig.MaxLogLength;
            nudAltitudeCorrection.Value = AdvancedConfig.AltitudeCorrection;
            chkStartImmediatelly.Checked = AdvancedConfig.StartImmediatelly;
            txtKMLDescFormat.Text = AdvancedConfig.KMLDescFormat;
            txtKMLNameFormat.Text = AdvancedConfig.KMLNameFormat;
            copKMLColor.Color = AdvancedConfig.KMLLineColor;
            nudMinimalDistance.Value = AdvancedConfig.MinimalDistance;
            txtLogFormat.Text = AdvancedConfig.TextLogFormat;
            nudInvalidMax.Value = AdvancedConfig.InvalidPositionsMax;
            chkNMEA.Checked = AdvancedConfig.NmeaLog;
            chkSeaLevelAltitude.Checked = AdvancedConfig.SeaLevelAltitude;
            chkInfoPane.Checked = AdvancedConfig.InfoPane;
            nudBeepEvery.Value = AdvancedConfig.BeepTimer;
            txtKeepAwake.Text = String.Join(",", AdvancedConfig.KeepAwakeList);
            cmbSpeedUnit.SelectedIndex = (int)AdvancedConfig.SpeedUnit;
            cmbDistanceUnit.SelectedIndex = (int)AdvancedConfig.DistanceUnit;
            cmbElevationUnit.SelectedIndex = (int)AdvancedConfig.ElevationUnit;
            txtCSVSeparator.Text = AdvancedConfig.CSVSeparator.ToString();
            txtCSVQualifier.Text = AdvancedConfig.CSVTextQualifier.ToString();
            cmbCSVQualifierUsage.SelectedIndex = (int)AdvancedConfig.CSVQualifierUsage;
            cmbCSVNewLine.SelectedIndex = AdvancedConfig.CSVNewLine == "\r\n" ? 0 : AdvancedConfig.CSVNewLine == "\n" ? 1 : 2;
            txtCSVHeader.Text = AdvancedConfig.CSVHeader;
            if (AdvancedConfig.CSVFields != null)
                foreach (char chr in AdvancedConfig.CSVFields)
                    foreach (CSVItem itm in lstCSVAvailableFields.Items)
                        if ((int)char.GetNumericValue(chr) == itm.Code)
                        {
                            lstCSVFields.Items.Add(itm);
                            lstCSVAvailableFields.Items.Remove(itm);
                            break;
                        }
            txtCSVDateFormat.Text = AdvancedConfig.CSVDateFormat;
            chkCSVUTC.Checked = AdvancedConfig.CSVUTC;
            if (!string.IsNullOrEmpty(AdvancedConfig.Language))
                foreach (System.Globalization.CultureInfo ci in cmbLanguage.Items)
                    if (ci != null && ci.Name == AdvancedConfig.Language)
                    {
                        cmbLanguage.SelectedItem = ci;
                        break;
                    }
        }

        private void tmiOK_Click(object sender, EventArgs e)
        {
            try
            {
                string.Format(txtKMLNameFormat.Text, DateTime.UtcNow, decimal.Zero, decimal.Zero, decimal.Zero, DateTime.Now);
            }
            catch (Exception ex)
            {
                tabMain.SelectedIndex = tabMain.TabPages.IndexOf(tapKML);
                txtKMLNameFormat.Focus();
                MessageBox.Show(Resources.err_InvalidKmlPointNameFormat + "\r\n" + ex.Message);
                return;
            }
            try
            {
                string.Format(txtKMLDescFormat.Text, DateTime.UtcNow, decimal.Zero, decimal.Zero, decimal.Zero, DateTime.Now);
            }
            catch (Exception ex)
            {
                tabMain.SelectedIndex = tabMain.TabPages.IndexOf(tapKML);
                txtKMLDescFormat.Focus();
                MessageBox.Show(Resources.err_InvalidKmlPointDescFormat + "\r\n" + ex.Message);
                return;
            }
            try
            {
                string.Format(txtLogFormat.Text, DateTime.UtcNow, decimal.Zero, decimal.Zero, decimal.Zero, DateTime.Now);
            }
            catch (Exception ex)
            {
                tabMain.SelectedIndex = tabMain.TabPages.IndexOf(tapDisplay);
                txtLogFormat.Focus();
                MessageBox.Show(Resources.err_InvalidLogFormat + "\r\n" + ex.Message);
                return;
            }
            if (string.IsNullOrEmpty(txtCSVSeparator.Text))
            {
                tabMain.SelectedIndex = tabMain.TabPages.IndexOf(tapCSV);
                txtCSVSeparator.Focus();
                MessageBox.Show(Resources.err_CSVSeparatorEmpty);
                return;
            }
            if (string.IsNullOrEmpty(txtCSVQualifier.Text))
            {
                tabMain.SelectedIndex = tabMain.TabPages.IndexOf(tapCSV);
                txtCSVQualifier.Focus();
                MessageBox.Show(Resources.err_CSVQualifierEmpty);
                return;
            }
            if (lstCSVFields.Items.Count == 0)
            {
                tabMain.SelectedIndex = tabMain.TabPages.IndexOf(tapCSV);
                lstCSVAvailableFields.Focus();
                MessageBox.Show(Resources.err_NoCSVFields);
                return;
            }
            try
            {
                DateTime.Now.ToString(txtCSVDateFormat.Text, System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                tabMain.SelectedIndex = tabMain.TabPages.IndexOf(tapCSV);
                txtCSVDateFormat.Focus();
                MessageBox.Show(Resources.err_InvalidCSVDateFormat + " " + ex.Message);
                return;
            }

            AdvancedConfig.StatusBar = chkStatusBar.Checked;
            AdvancedConfig.UseGpsTime = optTimeGPS.Checked;
            AdvancedConfig.MaxLogLength = (int)nudMaxLogLen.Value;
            AdvancedConfig.AltitudeCorrection = (int)nudAltitudeCorrection.Value;
            AdvancedConfig.StartImmediatelly = chkStartImmediatelly.Checked;
            AdvancedConfig.KMLDescFormat = txtKMLDescFormat.Text;
            AdvancedConfig.KMLNameFormat = txtKMLNameFormat.Text;
            AdvancedConfig.KMLLineColor = copKMLColor.Color;
            AdvancedConfig.MinimalDistance = (int)nudMinimalDistance.Value;
            AdvancedConfig.TextLogFormat = txtLogFormat.Text;
            AdvancedConfig.InvalidPositionsMax = (int)nudInvalidMax.Value;
            AdvancedConfig.NmeaLog = chkNMEA.Checked;
            AdvancedConfig.SeaLevelAltitude = chkSeaLevelAltitude.Checked;
            AdvancedConfig.InfoPane = chkInfoPane.Checked;
            AdvancedConfig.BeepTimer = (int)nudBeepEvery.Value;
            AdvancedConfig.KeepAwakeList = txtKeepAwake.Text.Split(',');
            AdvancedConfig.SpeedUnit = (SpeedUnit)cmbSpeedUnit.SelectedIndex;
            AdvancedConfig.DistanceUnit = (DistanceUnit)cmbDistanceUnit.SelectedIndex;
            AdvancedConfig.ElevationUnit = (ElevationUnit)cmbElevationUnit.SelectedIndex;
            AdvancedConfig.CSVSeparator = txtCSVSeparator.Text[0];
            AdvancedConfig.CSVTextQualifier = txtCSVQualifier.Text[0];
            AdvancedConfig.CSVQualifierUsage = (CSVQualifierUsage)cmbCSVQualifierUsage.SelectedIndex;
            AdvancedConfig.CSVNewLine = cmbCSVNewLine.SelectedIndex == 0 ? "\r\n" : cmbCSVQualifierUsage.SelectedIndex == 1 ? "\n" : "\r";
            AdvancedConfig.CSVHeader = txtCSVHeader.Text;

            ArrayList alFields = new ArrayList();
            foreach (CSVItem fld in lstCSVFields.Items)
            {
                alFields.Add(fld.Code.ToString(System.Globalization.CultureInfo.InvariantCulture));
            }
            AdvancedConfig.CSVFields = String.Join("", (string[])alFields.ToArray(typeof(string)));
            //AdvancedConfig.CSVFields = String.Join("", (from CSVItem fld in lstCSVFields.Items select fld.Code.ToString(System.Globalization.CultureInfo.InvariantCulture)).ToArray());
            AdvancedConfig.CSVDateFormat = txtCSVDateFormat.Text;
            AdvancedConfig.CSVUTC = chkCSVUTC.Checked;
            string newlang;
            
            if (cmbLanguage.SelectedItem is string)
            {
                newlang = AdvancedConfig.Language == "" ? "" : null;
            }
            else
            {
                newlang = ((System.Globalization.CultureInfo) cmbLanguage.SelectedItem).Name;
            }

            if (AdvancedConfig.Language != newlang)
                MessageBox.Show(Resources.msg_LngChange);
            AdvancedConfig.Language = newlang;
            this.Close();
            this.DialogResult = DialogResult.OK;
        }
        #region Interactivity
        private void tmiCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void llbStringFormat_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://msdn.microsoft.com/en-us/library/fht0f5be.aspx", null);
            }
            catch { }
        }

        private void cmdTestFormat_Click(object sender, EventArgs e)
        {
            //Test string format
            string text;
            if (sender == cmdTestDesc) text = txtKMLDescFormat.Text;
            else if (sender == cmdTestName) text = txtKMLNameFormat.Text;
            else if (sender == cmdLogFormatTest) text = txtLogFormat.Text;
            else return;
            object[] objarr = new object[] { DateTime.UtcNow, (decimal)14.451075, (decimal)50.1256567, (decimal)299.9, DateTime.Now, AdvancedConfig.ElevationUnitName };
            try
            {
                MessageBox.Show(
                    string.Format(Resources.FormatTestInfo, objarr) +
                    string.Format(text, objarr),
                    Resources.ExapleOfFormatting, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    Resources.err_InvalidFormatString + ex.Message,
                    Resources.err_ErrorTitleMsg, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion

        #region "CSV"
        /// <summary>Represents CSV field displayedn in <see cref="ListBox"/></summary>
        private class CSVItem
        {
            /// <summary>Contains value of the <see cref="Code"/> property</summary>
            private readonly int code;
            /// <summary>Contains value of the <see cref="Desc"/> property</summary>
            private readonly string desc;
            /// <summary>CTor</summary>
            /// <param name="Code">Field code</param>
            /// <param name="Desc">Field description</param>
            public CSVItem(int Code, string Desc)
            {
                this.code = Code;
                this.desc = Desc;
            }
            /// <summary>Gets field code as used in <see cref="String.Format(string,object)"/></summary>
            public int Code { get { return code; } }
            /// <summary>Gets field description displayed to the user</summary>
            public string Desc { get { return desc; } }
            public override string ToString()
            {
                return desc;
            }
        }
        private void cmdCSVTab_Click(object sender, EventArgs e)
        {
            txtCSVSeparator.Text = "\t";
        }
        private void lstCSVAvailableFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmdCSVAdd.Enabled = lstCSVAvailableFields.SelectedIndex >= 0;
        }
        private void lstCSVFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmdCSVRemove.Enabled = lstCSVFields.SelectedIndex >= 0;
            cmdCSVDown.Enabled = lstCSVFields.SelectedIndex >= 0 && lstCSVFields.SelectedIndex < lstCSVFields.Items.Count - 1;
            cmdCSVUp.Enabled = lstCSVFields.SelectedIndex >= 1;
        }
        private void cmdCSVAdd_Click(object sender, EventArgs e)
        {
            object item = lstCSVAvailableFields.SelectedItem;
            lstCSVAvailableFields.Items.RemoveAt(lstCSVAvailableFields.SelectedIndex);
            lstCSVFields.Items.Add(item);
        }

        private void cmdCSVRemove_Click(object sender, EventArgs e)
        {
            object item = lstCSVFields.SelectedItem;
            lstCSVFields.Items.RemoveAt(lstCSVFields.SelectedIndex);
            lstCSVAvailableFields.Items.Add(item);
        }

        private void cmdCSVUp_Click(object sender, EventArgs e)
        {
            object item = lstCSVFields.SelectedItem;
            int index = lstCSVFields.SelectedIndex;
            lstCSVFields.Items.RemoveAt(index);
            lstCSVFields.Items.Insert(index - 1, item);
            lstCSVFields.SelectedIndex = index - 1;
        }

        private void cmdCSVDown_Click(object sender, EventArgs e)
        {
            object item = lstCSVFields.SelectedItem;
            int index = lstCSVFields.SelectedIndex;
            lstCSVFields.Items.RemoveAt(index);
            lstCSVFields.Items.Insert(index + 1, item);
            lstCSVFields.SelectedIndex = index + 1;
        }

        private void cmdCSVHeaderTab_Click(object sender, EventArgs e)
        {
            txtCSVHeader.SelectedText = "\t";
        }
        private void panCSVFields_Resize(object sender, EventArgs e)
        {
            cmdCSVAdd.Left = cmdCSVRemove.Left = cmdCSVUp.Left = cmdCSVDown.Left =
                panCSV.Width / 2 - cmdCSVAdd.Width / 2;
            lblCSVAvailableFields.Left = lstCSVAvailableFields.Left = 0;
            lblCSVAvailableFields.Top = 0;
            lstCSVAvailableFields.Top = lstCSVFields.Top = lblCSVAvailableFields.Bottom;
            lblCSVAvailableFields.Width = lstCSVAvailableFields.Width = lblCSVFields.Width = lstCSVFields.Width =
                panCSVFields.Width / 2 - cmdCSVAdd.Width / 2;
            lblCSVFields.Top = 0;
            lblCSVFields.Left = lstCSVFields.Left = panCSVFields.Width - lblCSVFields.Width;
            cmdCSVAdd.Top = lstCSVFields.Top;
            cmdCSVRemove.Top = cmdCSVAdd.Bottom;
            cmdCSVUp.Top = cmdCSVRemove.Bottom;
            cmdCSVDown.Top = cmdCSVUp.Bottom;
            lstCSVFields.Height = lstCSVAvailableFields.Height =
                panCSVFields.Height - lstCSVAvailableFields.Top;
        }
        private void cmbDateFormat_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show(string.Format(Resources.DateTestMessage, DateTime.Now, DateTime.Now.ToString(txtCSVDateFormat.Text, System.Globalization.CultureInfo.InvariantCulture)), Resources.DateFormatTest);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Resources.err_InvalidFormat + "\r\n" + ex.Message);
            }
        }
        #endregion


    }

    /// <summary>Stores, loads and saves advanced settings</summary>
    internal static class AdvancedConfig
    {
        static AdvancedConfig()
        {
            //Set defaults.

            KMLNameFormat = KMLDescFormat = Resources.format_KmlDescDefault;
            TextLogFormat = Resources.format_TextLogDefault;
            COMPort = "COM1";
            BaudRate = OpenNETCF.IO.Serial.BaudRates.CBR_4800;
            TrackType = TrackType.Points;
            LogFormat = LogFormat.GPX;
            LogToLogFile = false;
            LogToTextBox = true;
            PollingInterval = 60;
            UseWindowsDriver = false;
            InfoPane = true;
            StatusBar = true;
            StartImmediatelly = true;
            KMLLineColor = Color.FromArgb(0, 0, 0xff);
            InvalidPositionsMax = 500;
            NmeaLog = false;
            KeepAwakeList = new string[] { "GPD0", "GPS0" };
            SpeedUnit = SpeedUnit.kmh;
            DistanceUnit = DistanceUnit.km;
            ElevationUnit = ElevationUnit.m;
            CSVQualifierUsage = CSVQualifierUsage.AsNeeded;
        }
        /// <summary>Loads advanced settings from <see cref="ConfigurationManager"/></summary>
        public static void Load()
        {
            //Read from config file

            COMPort = ConfigurationManager.AppSettings["COMPort"];

            if (String.IsNullOrEmpty(COMPort))
            {
                COMPort = "COM1";
            }

            try
            {
                BaudRate = (OpenNETCF.IO.Serial.BaudRates)Enum.Parse(typeof(OpenNETCF.IO.Serial.BaudRates), ConfigurationManager.AppSettings["BaudRate"], true);
            }
            catch
            {
                BaudRate = OpenNETCF.IO.Serial.BaudRates.CBR_4800;
            }

            if(!String.IsNullOrEmpty(ConfigurationManager.AppSettings["UseWindowsDriver"]))
            {
                UseWindowsDriver = Convert.ToBoolean(ConfigurationManager.AppSettings["UseWindowsDriver"]);
            }

            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["PollingInterval"]))
            {
                PollingInterval = Int32.Parse(ConfigurationManager.AppSettings["PollingInterval"], System.Globalization.CultureInfo.InvariantCulture);
            }

            if(!String.IsNullOrEmpty(ConfigurationManager.AppSettings["LogToTextBox"]))
            {
                LogToTextBox = Convert.ToBoolean(ConfigurationManager.AppSettings["LogToTextBox"]);
            }

            if(!String.IsNullOrEmpty(ConfigurationManager.AppSettings["LogToFile"]))
            {
                LogToLogFile = Convert.ToBoolean(ConfigurationManager.AppSettings["LogToFile"]);
            }
            
            if(!String.IsNullOrEmpty(ConfigurationManager.AppSettings["LogFileLocation"]))
            {
                LogFileLocation = ConfigurationManager.AppSettings["LogFileLocation"];
            }

            if(!String.IsNullOrEmpty(ConfigurationManager.AppSettings["LogAltitude"]))
            {
                LogAltitude = Convert.ToBoolean(ConfigurationManager.AppSettings["LogAltitude"]);
            }
            

            
            switch (ConfigurationManager.AppSettings["LogFormat"])
            {
                case "KML": 
                    LogFormat = LogFormat.KML; 
                    break;
                case "CSV": 
                    LogFormat = LogFormat.CSV; 
                    break;
                default: 
                    LogFormat = LogFormat.GPX; 
                    break;
            }

            switch (ConfigurationManager.AppSettings["TrackType"])
            {
                case "Track": 
                    TrackType = TrackType.Track; 
                    break;
                default: 
                    TrackType = TrackType.Points; 
                    break;
            }

            if(!String.IsNullOrEmpty(ConfigurationManager.AppSettings["UseGPSTime"]))
            {
                UseGpsTime = Convert.ToBoolean(ConfigurationManager.AppSettings["UseGPSTime"]);
            }
            
            if(!String.IsNullOrEmpty(ConfigurationManager.AppSettings["StatusBar"]))
            {
                StatusBar = Convert.ToBoolean(ConfigurationManager.AppSettings["StatusBar"]);
            }

            if(!String.IsNullOrEmpty(ConfigurationManager.AppSettings["MaxLogLength"]))
            {
                MaxLogLength = Int32.Parse(ConfigurationManager.AppSettings["MaxLogLength"], System.Globalization.CultureInfo.InvariantCulture);
            }


            if(!String.IsNullOrEmpty(ConfigurationManager.AppSettings["AltitudeCorrection"]))
            {
                AltitudeCorrection = Int32.Parse(ConfigurationManager.AppSettings["AltitudeCorrection"], System.Globalization.CultureInfo.InvariantCulture);
            }

            if(!String.IsNullOrEmpty(ConfigurationManager.AppSettings["StartImmediatelly"]))
            {
                StartImmediatelly = Convert.ToBoolean(ConfigurationManager.AppSettings["StartImmediatelly"]);
            }
            
            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["KMLNameFormat"]))
            {
                KMLNameFormat = ConfigurationManager.AppSettings["KMLNameFormat"];
            }

            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["KMLDescFormat"]))
            {
                KMLDescFormat = ConfigurationManager.AppSettings["KMLDescFormat"];
            }

            if(!String.IsNullOrEmpty(ConfigurationManager.AppSettings["KMLLineColor"]))
            {
                KMLLineColor = Color.FromArgb(Int32.Parse(ConfigurationManager.AppSettings["KMLLineColor"], System.Globalization.CultureInfo.InvariantCulture));
            }

            if(!String.IsNullOrEmpty(ConfigurationManager.AppSettings["MinimalDistance"]))
            {

                MinimalDistance = Int32.Parse(ConfigurationManager.AppSettings["MinimalDistance"], System.Globalization.CultureInfo.InvariantCulture);
            }


            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["TextLogFormat"]))
            {
                TextLogFormat = ConfigurationManager.AppSettings["TextLogFormat"];
            }

            if(!String.IsNullOrEmpty(ConfigurationManager.AppSettings["InvalidPositionsMax"]))
            {
                InvalidPositionsMax = Int32.Parse(ConfigurationManager.AppSettings["InvalidPositionsMax"], System.Globalization.CultureInfo.InvariantCulture);
            }

            if(!String.IsNullOrEmpty(ConfigurationManager.AppSettings["NMEALog"]))
            {
                NmeaLog = Convert.ToBoolean(ConfigurationManager.AppSettings["NMEALog"]);
            }
            
            if(!String.IsNullOrEmpty(ConfigurationManager.AppSettings["SeaLevelAltitude"]))
            {
                SeaLevelAltitude = Convert.ToBoolean(ConfigurationManager.AppSettings["SeaLevelAltitude"]);
            }

            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["InfoPane"]))
            {
                InfoPane = Convert.ToBoolean(ConfigurationManager.AppSettings["InfoPane"]);
            }

            if(!String.IsNullOrEmpty(ConfigurationManager.AppSettings["BeepTimer"]))
            {
                BeepTimer = Int32.Parse(ConfigurationManager.AppSettings["BeepTimer"], System.Globalization.CultureInfo.InvariantCulture); 
            }
            
            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["KeepAwake"]))
            {
                KeepAwakeList = ConfigurationManager.AppSettings["KeepAwake"].Split(' ');
            }

            if(!String.IsNullOrEmpty(ConfigurationManager.AppSettings["SpeedUnit"]))
            {
                SpeedUnit = (SpeedUnit)Enum.Parse(typeof(SpeedUnit), ConfigurationManager.AppSettings["SpeedUnit"], true);
            }

            if(!String.IsNullOrEmpty(ConfigurationManager.AppSettings["DistanceUnit"]))
            {
                DistanceUnit = (DistanceUnit)Enum.Parse(typeof(DistanceUnit), ConfigurationManager.AppSettings["DistanceUnit"], true);
            }
            
            if(!String.IsNullOrEmpty(ConfigurationManager.AppSettings["ElevationUnit"]))
            {
                ElevationUnit = (ElevationUnit)Enum.Parse(typeof(ElevationUnit), ConfigurationManager.AppSettings["ElevationUnit"], true);
            }
            


            CSVSeparator = String.IsNullOrEmpty(ConfigurationManager.AppSettings["CSVSeparator"]) ? ',' : ConfigurationManager.AppSettings["CSVSeparator"][0];
            CSVTextQualifier = String.IsNullOrEmpty(ConfigurationManager.AppSettings["CSVTextQualifier"]) ? '"' : ConfigurationManager.AppSettings["CSVTextQualifier"][0];

            if(!String.IsNullOrEmpty(ConfigurationManager.AppSettings["CSVQualifierUsage"]))
            {
                CSVQualifierUsage = (CSVQualifierUsage)int.Parse(ConfigurationManager.AppSettings["CSVQualifierUsage"], System.Globalization.CultureInfo.InvariantCulture);
            }

            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["CSVHeader"]))
            {
                CSVHeader = ConfigurationManager.AppSettings["CSVHeader"];
            }

            CSVFields = String.IsNullOrEmpty(ConfigurationManager.AppSettings["CSVFields"]) ? "3012" : ConfigurationManager.AppSettings["CSVFields"];
            CSVNewLine = String.IsNullOrEmpty(ConfigurationManager.AppSettings["CSVNewLine"]) ? "\r\n" : ConfigurationManager.AppSettings["CSVNewLine"];
            CSVDateFormat = String.IsNullOrEmpty(ConfigurationManager.AppSettings["CSVDateFormat"]) ? "yyyy-MM-dd HH:mm:ss" : ConfigurationManager.AppSettings["CSVDateFormat"];

            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["CSVUTC"]))
            {
                CSVUTC = Convert.ToBoolean(ConfigurationManager.AppSettings["CSVUTC"]);
            }
            
            if(!String.IsNullOrEmpty(ConfigurationManager.AppSettings["Language"]))
            {
                Language = ConfigurationManager.AppSettings["Language"];
            }
        }
        /// <summary>Stores advanced settings to <see cref="ConfigurationManager"/></summary>
        public static void Store()
        {
            ConfigurationManager.AppSettings["COMPort"] = COMPort;
            ConfigurationManager.AppSettings["BaudRate"] = BaudRate.ToString();
            ConfigurationManager.AppSettings["UseWindowsDriver"] = UseWindowsDriver ? "true" : "false";
            ConfigurationManager.AppSettings["PollingInterval"] = PollingInterval.ToString(System.Globalization.CultureInfo.InvariantCulture);
            ConfigurationManager.AppSettings["LogToTextBox"] = LogToTextBox ? "true" : "false";
            ConfigurationManager.AppSettings["LogToFile"] = LogToLogFile ? "true" : "false";
            switch (LogFormat)
            {
                case LogFormat.KML: 
                    ConfigurationManager.AppSettings["LogFormat"] = "KML"; 
                    break;
                case LogFormat.CSV: 
                    ConfigurationManager.AppSettings["LogFormat"] = "CSV"; 
                    break;
                default: 
                    ConfigurationManager.AppSettings["LogFormat"] = "GPX"; 
                    break;
            }

            switch (TrackType)
            {
                case TrackType.Track: ConfigurationManager.AppSettings["TrackType"] = "Track"; break;
                default: ConfigurationManager.AppSettings["TrackType"] = "Points"; break;
            }
            ConfigurationManager.AppSettings["LogAltitude"] = LogAltitude ? "true" : "false";
            ConfigurationManager.AppSettings["LogFileLocation"] = LogFileLocation;

            ConfigurationManager.AppSettings["UseGPSTime"] = UseGpsTime ? "true" : "false";
            ConfigurationManager.AppSettings["StatusBar"] = StatusBar ? "true" : "false";
            ConfigurationManager.AppSettings["MaxLogLength"] = MaxLogLength.ToString(System.Globalization.CultureInfo.InvariantCulture);
            ConfigurationManager.AppSettings["AltitudeCorrection"] = AltitudeCorrection.ToString(System.Globalization.CultureInfo.InvariantCulture);
            ConfigurationManager.AppSettings["StartImmediatelly"] = StartImmediatelly ? "true" : "false";
            ConfigurationManager.AppSettings["KMLNameFormat"] = KMLNameFormat;
            ConfigurationManager.AppSettings["KMLDescFormat"] = KMLDescFormat;
            ConfigurationManager.AppSettings["KMLLineColor"] = KMLLineColor.ToArgb().ToString(System.Globalization.CultureInfo.InvariantCulture);
            ConfigurationManager.AppSettings["MinimalDistance"] = MinimalDistance.ToString(System.Globalization.CultureInfo.InvariantCulture);
            ConfigurationManager.AppSettings["TextLogFormat"] = TextLogFormat;
            ConfigurationManager.AppSettings["InvalidPositionsMax"] = InvalidPositionsMax.ToString(System.Globalization.CultureInfo.InvariantCulture);
            ConfigurationManager.AppSettings["NMEALog"] = NmeaLog ? "true" : "false";
            ConfigurationManager.AppSettings["SeaLevelAltitude"] = SeaLevelAltitude ? "true" : "false";
            ConfigurationManager.AppSettings["InfoPane"] = InfoPane ? "true" : "false";
            ConfigurationManager.AppSettings["BeepTimer"] = BeepTimer.ToString(System.Globalization.CultureInfo.InvariantCulture);
            ConfigurationManager.AppSettings["KeepAwake"] = KeepAwakeList == null ? "" : string.Join(" ", KeepAwakeList);
            ConfigurationManager.AppSettings["SpeedUnit"] = SpeedUnit.ToString();
            ConfigurationManager.AppSettings["DistanceUnit"] = DistanceUnit.ToString();
            ConfigurationManager.AppSettings["ElevationUnit"] = ElevationUnit.ToString();
            ConfigurationManager.AppSettings["CSVSeparator"] = CSVSeparator.ToString();
            ConfigurationManager.AppSettings["CSVTextQualifier"] = CSVTextQualifier.ToString();
            ConfigurationManager.AppSettings["CSVNewLine"] = CSVNewLine;
            ConfigurationManager.AppSettings["CSVQualifierUsage"] = ((int)CSVQualifierUsage).ToString(System.Globalization.CultureInfo.InvariantCulture);
            ConfigurationManager.AppSettings["CSVHeader"] = CSVHeader;
            ConfigurationManager.AppSettings["CSVFields"] = CSVFields;
            ConfigurationManager.AppSettings["CSVDateFormat"] = CSVDateFormat;
            ConfigurationManager.AppSettings["CSVUTC"] = CSVUTC ? "true" : "false";
            ConfigurationManager.AppSettings["Language"] = Language;
        }

        /// <summary>Gets or sets COM port name to get GPS data from</summary>
        /// <remarks>Ignored when <see cref="UseWindowsDriver"/> is true</remarks>
        public static string COMPort { get; set; }
        /// <summary>Gets or sets baude rate of COM port</summary>
        /// <remarks>Ignored when <see cref="UseWindowsDriver"/> is true</remarks>
        public static OpenNETCF.IO.Serial.BaudRates BaudRate { get; set; }
        /// <summary>True to use Windows driver false to get data from COM port</summary>
        public static bool UseWindowsDriver { get; set; }
        /// <summary>Polling interval, in seconds</summary>
        public static int PollingInterval { get; set; }
        /// <summary>True to write gps info to text box</summary>
        public static bool LogToTextBox { get; set; }
        /// <summary>True to write GPS info to file</summary>
        public static bool LogToLogFile { get; set; }
        /// <summary>Directory where to write GPS files to</summary>
        public static string LogFileLocation { get; set; }
        /// <summary>True to log altitude</summary>
        public static bool LogAltitude { get; set; }
        /// <summary>Gets or sets format of logi file</summary>
        public static LogFormat LogFormat { get; set; }
        /// <summary>Gets or sets format of logged track</summary>
        public static TrackType TrackType { get; set; }

        /// <summary>Gets or sets value indicationg if GPS time shoudl be used instead of system</summary>
        public static bool UseGpsTime { get; set; }
        /// <summary>Gets or sets value indicationg if status bar is shown</summary>
        public static bool StatusBar { get; set; }
        /// <summary>Gets or sets maximum number of lines in logging text box (0=infinite)</summary>
        public static int MaxLogLength { get; set; }
        /// <summary>Gets or sets altitude correction</summary>
        public static int AltitudeCorrection { get; set; }
        /// <summary>Gets or sets value indicationg if gps processing starts immediately or after a polling interval</summary>
        public static bool StartImmediatelly { get; set; }
        
        /// <summary>Gets or sets format used for KML point name</summary>
        /// <value>String used by <see cref="String.Format(string,object)"/>. Parameters are:
        /// <list type="table"><listheader><term>Number</term><description>Value</description></listheader>
        /// <item><term>0</term><description>Date and time UTC</description></item>
        /// <item><term>1</term><description>Longitude</description></item>
        /// <item><term>2</term><description>Latitude</description></item>
        /// <item><term>3</term><description>Altitude</description></item>
        /// <item><term>4</term><description>Date and time local</description></item>
        /// <item><term>5</term><description>Altitude unit</description></item>
        /// </list></value>
        public static string KMLNameFormat { get; set; }
        
        /// <summary>Gets or sets format used for KML point description</summary>
        /// <value>String used by <see cref="String.Format(string,object)"/>. Parameters are:
        /// <list type="table"><listheader><term>Number</term><description>Value</description></listheader>
        /// <item><term>0</term><description>Date and time UTC</description></item>
        /// <item><term>1</term><description>Longitude</description></item>
        /// <item><term>2</term><description>Latitude</description></item>
        /// <item><term>3</term><description>Altitude</description></item>
        /// <item><term>4</term><description>Date and time local</description></item>
        /// <item><term>5</term><description>Altitude unit</description></item>
        /// </list></value>
        public static string KMLDescFormat { get; set; }
        
        /// <summary>Gets or sets color used for KML line</summary>
        public static Color KMLLineColor { get; set; }
        
        /// <summary>Gets minimal distance of two logged points (in meters)</summary>
        /// <remarks>When point A is logged, its location is remembered.
        /// If point B is about to be logged and it's neared than given distance form A, its ignored, but remembered as well.
        /// When point C is about to be logged and it's still neared that distance from A, its ignored again, and remembered instead of B.
        /// When point D is about to be logged and it's farer from A then distance:
        /// a) It's farer than distance from both A and C: C and D are logged. D is remembered.
        /// b) It's nearer than distance from C (but farer than distance from A): Only D is logged. D is remembered.</remarks>
        public static int MinimalDistance { get; set; }
        
        /// <summary>Gets or sets format of log</summary>
        public static string TextLogFormat { get; set; }
        
        /// <summary>Gets or sets maximal number of invalid positions in a row.</summary>
        /// <remarks>After this number of invalid positions received, GPS is closed and waits for polling interval.</remarks>
        public static int InvalidPositionsMax { get; set; }
        
        /// <summary>Write an extra raw NMEA log file</summary>
        public static bool NmeaLog { get; set; }
        
        /// <summary>Gets or sets value indicating if Windows GPS Intermediate driver wrapper returns GPS elipsoid altitude (false) or sea level altitude (true)</summary>
        public static bool SeaLevelAltitude { get; set; }
        
        /// <summary>Gets or sets value indicating if main window shows information panel</summary>
        public static bool InfoPane { get; set; }
        
        /// <summary>Gets or sets number of seconds to beep in; zero - never</summary>
        public static int BeepTimer { get; set; }
        
        /// <summary>Names of devices to keep awake when GPS is running</summary>
        public static string[] KeepAwakeList { get; set; }
        
        /// <summary>Gets or sets unit used to measure speed</summary>
        public static SpeedUnit SpeedUnit { get; set; }
        
        /// <summary>Gets or sets unit used to measure distance</summary>
        public static DistanceUnit DistanceUnit { get; set; }
        
        /// <summary>Gets or sets unit used to measure elevation</summary>
        public static ElevationUnit ElevationUnit { get; set; }

        /// <summary>CSV field separator</summary>
        public static char CSVSeparator { get; set; }
        
        /// <summary>CSV text qualifier</summary>
        public static char CSVTextQualifier { get; set; }
        
        /// <summary>When to use CSV text qualifier</summary>
        public static CSVQualifierUsage CSVQualifierUsage { get; set; }
        
        /// <summary>CSV new linestring</summary>
        public static string CSVNewLine { get; set; }
        
        /// <summary>CSV file header</summary>
        public static string CSVHeader { get; set; }
        
        /// <summary>Order of SCV fileds - single-digits from 0 to 3.</summary>
        /// <remarks>
        /// <list type="table"><listheader><term>Number</term><description>Value</description></listheader>
        /// <item><term>0</term><description>Latitude</description></item>
        /// <item><term>1</term><description>Longitude</description></item>
        /// <item><term>2</term><description>Altitude</description></item>
        /// <item><term>3</term><description>Date</description></item>
        /// </list></remarks>
        public static string CSVFields { get; set; }
        
        /// <summary>Format of date in CSV file</summary>
        public static string CSVDateFormat { get; set; }
        
        /// <summary>Use UTC time in CSV file</summary>
        public static bool CSVUTC { get; set; }

        /// <summary>Langauge (culture name)</summary>
        public static string Language { get; set; }

        /// <summary>Gets multiplier for <see cref="SpeedUnit"/>. Multiply speed in km/h by returned value to get speed in actual unit.</summary>
        public static decimal SpeedMultiplier
        {
            get
            {
                switch (SpeedUnit)
                {
                    case SpeedUnit.kmh: return 1m;
                    case SpeedUnit.kn: return 1m / 1.852m;
                    case SpeedUnit.mih: return 1m / 1.609344m;
                    case SpeedUnit.ms: return 1m / 3.6m;
                    default: return 0m;
                }
            }
        }

        /// <summary>Gets multiplier for <see cref="DistanceUnit"/>. Multiply distance in km by returned value to get distance in actual unit.</summary>
        public static decimal DistanceMultiplier
        {
            get
            {
                switch (DistanceUnit)
                {
                    case DistanceUnit.km: return 1m;
                    case DistanceUnit.m: return 1000m;
                    case DistanceUnit.mi: return 1m / 1.609344m;
                    case DistanceUnit.nmi: return 1m / 1.852m;
                    case DistanceUnit.yd: return 1m / 0.0009144m;
                    default: return 0m;
                }
            }
        }

        /// <summary>Gets multiplier for <see cref="ElevationUnit"/>. Multiply elevation in m by returned value to get elevation in actual unit.</summary>
        public static decimal ElevationMultiplier
        {
            get
            {
                switch (ElevationUnit)
                {
                    case ElevationUnit.ft: return 1m / 0.3048m;
                    case ElevationUnit.km: return 1m / 1000m;
                    case ElevationUnit.m: return 1m;
                    case ElevationUnit.yd: return 1m / 0.9144m;
                    default: return 0m;
                }
            }
        }

        /// <summary>Gets short display name of actual <see cref="SpeedUnit"/>.</summary>
        public static string SpeedUnitName
        {
            get
            {
                switch (SpeedUnit)
                {
                    case SpeedUnit.kmh: return "km/h";
                    case SpeedUnit.kn: return "kn";
                    case SpeedUnit.mih: return "mi/h";
                    case SpeedUnit.ms: return "m/s";
                    default: return null;
                }
            }
        }

        /// <summary>Gets short display name of actual <see cref="DistanceUnit"/>.</summary>
        public static string DistanceUnitName
        {
            get
            {
                switch (DistanceUnit)
                {
                    case DistanceUnit.km: return "km";
                    case DistanceUnit.m: return "m";
                    case DistanceUnit.mi: return "mi";
                    case DistanceUnit.nmi: return "nmi";
                    case DistanceUnit.yd: return "yd";
                    default: return null;
                }
            }
        }

        /// <summary>Gets short display name of actual <see cref="ElevationUnit"/>.</summary>
        public static string ElevationUnitName
        {
            get
            {
                switch (ElevationUnit)
                {
                    case ElevationUnit.ft: return "ft";
                    case ElevationUnit.km: return "km";
                    case ElevationUnit.m: return "m";
                    case ElevationUnit.yd: return "yd";
                    default: return null;
                }
            }
        }
    }

    /// <summary>Supported file formats</summary>
    public enum LogFormat
    {
        /// <summary>GPS XML format</summary>
        GPX,
        /// <summary>Google Earth KML (XML) format</summary>
        KML,
        /// <summary>Somethins-separated values</summary>
        CSV
    }
    
    /// <summary>Supported track types</summary>
    public enum TrackType
    {
        /// <summary>Distinct points</summary>
        Points,
        /// <summary>Connected paths</summary>
        Track
    }

    /// <summary>Speed units</summary>
    public enum SpeedUnit
    {
        /// <summary>Kilometers per hour</summary>
        kmh,
        /// <summary>Meters per second (3.6 km/h)</summary>
        ms,
        /// <summary>Miles per hour (1.609344 km/h)</summary>
        mih,
        /// <summary>Knots (1.85200 km/h)</summary>
        kn
    }

    /// <summary>Distance units</summary>
    public enum DistanceUnit
    {
        /// <summary>Kilometers</summary>
        km,
        /// <summary>Meters (1e-3 km)</summary>
        m,
        /// <summary>Miles (1.609344 km)</summary>
        mi,
        /// <summary>Nautical miles (1.852 km)</summary>
        nmi,
        /// <summary>Yards (0.0009144 km)</summary>
        yd
    }

    /// <summary>Elevation units</summary>
    public enum ElevationUnit
    {
        /// <summary>Meters</summary>
        m,
        /// <summary>Kimlometers (1000 m)</summary>
        km,
        /// <summary>Feet (0.3048 m)</summary>
        ft,
        /// <summary>Yards (0.9144 m)</summary>
        yd
    }


    /// <summary>When is CSV text qualifier used</summary>
    public enum CSVQualifierUsage
    {
        /// <summary>Used when text contains separator</summary>
        AsNeeded = 0,
        /// <summary>Never used</summary>
        Never = 1,
        /// <summary>Used always</summary>
        Always = 2
    }
}