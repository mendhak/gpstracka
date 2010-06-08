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
                lstCSVAvailableFields.Items.Add(new CsvItem(0, Resources.Latitude));
                lstCSVAvailableFields.Items.Add(new CsvItem(1, Resources.Longitude));
                lstCSVAvailableFields.Items.Add(new CsvItem(2, Resources.Altitude));
                lstCSVAvailableFields.Items.Add(new CsvItem(3, Resources.Date));
                lstCSVAvailableFields.Items.Add(new CsvItem(4, Resources.Distance));
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
            txtKMLDescFormat.Text = AdvancedConfig.KmlDescFormat;
            txtKMLNameFormat.Text = AdvancedConfig.KmlNameFormat;
            copKMLColor.Color = AdvancedConfig.KmlLineColor;
            nudMinimalDistance.Value = AdvancedConfig.MinimalDistance;
            txtLogFormat.Text = AdvancedConfig.TextLogFormat;
            nudInvalidMax.Value = AdvancedConfig.InvalidPositionsMax;
            chkNMEA.Checked = AdvancedConfig.NmeaLog;
            chkSeaLevelAltitude.Checked = AdvancedConfig.SeaLevelAltitude;
            chkInfoPane.Checked = AdvancedConfig.InfoPane;
            nudBeepEvery.Value = AdvancedConfig.BeepTimer;
            nudEmpty.Value = AdvancedConfig.MainFormBottomEmptySpace;
            txtKeepAwake.Text = String.Join(",", AdvancedConfig.KeepAwakeList);
            cmbSpeedUnit.SelectedIndex = (int)AdvancedConfig.SpeedUnit;
            cmbDistanceUnit.SelectedIndex = (int)AdvancedConfig.DistanceUnit;
            cmbElevationUnit.SelectedIndex = (int)AdvancedConfig.ElevationUnit;
            txtCSVSeparator.Text = AdvancedConfig.CsvSeparator.ToString();
            txtCSVQualifier.Text = AdvancedConfig.CsvTextQualifier.ToString();
            cmbCSVQualifierUsage.SelectedIndex = (int)AdvancedConfig.CsvQualifierUsage;
            cmbCSVNewLine.SelectedIndex = AdvancedConfig.CsvNewLine == "\r\n" ? 0 : AdvancedConfig.CsvNewLine == "\n" ? 1 : 2;
            txtCSVHeader.Text = AdvancedConfig.CsvHeader;
            if (AdvancedConfig.CsvFields != null)
                foreach (char chr in AdvancedConfig.CsvFields)
                    foreach (CsvItem itm in lstCSVAvailableFields.Items)
                        if ((int)char.GetNumericValue(chr) == itm.Code)
                        {
                            lstCSVFields.Items.Add(itm);
                            lstCSVAvailableFields.Items.Remove(itm);
                            break;
                        }
            txtCSVDateFormat.Text = AdvancedConfig.CsvDateFormat;
            chkCSVUTC.Checked = AdvancedConfig.CsvUtc;
            if (!string.IsNullOrEmpty(AdvancedConfig.Language))
            {
                foreach (object item in cmbLanguage.Items)
                {
                    var ci = item as System.Globalization.CultureInfo;
                    if (ci != null && ci.Name == AdvancedConfig.Language)
                    {
                        cmbLanguage.SelectedItem = ci;
                        break;
                    }
                }
            }
        }

        private void tmiOK_Click(object sender, EventArgs e)
        {
            object[] testFormat = new object[] { DateTime.UtcNow, decimal.Zero, decimal.Zero, decimal.Zero, DateTime.Now, string.Empty, decimal.Zero, string.Empty };
            try
            {
                string.Format(txtKMLNameFormat.Text, testFormat);
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
                string.Format(txtKMLDescFormat.Text, testFormat);
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
                string.Format(txtLogFormat.Text, testFormat);
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
            AdvancedConfig.KmlDescFormat = txtKMLDescFormat.Text;
            AdvancedConfig.KmlNameFormat = txtKMLNameFormat.Text;
            AdvancedConfig.KmlLineColor = copKMLColor.Color;
            AdvancedConfig.MinimalDistance = (int)nudMinimalDistance.Value;
            AdvancedConfig.TextLogFormat = txtLogFormat.Text;
            AdvancedConfig.InvalidPositionsMax = (int)nudInvalidMax.Value;
            AdvancedConfig.NmeaLog = chkNMEA.Checked;
            AdvancedConfig.SeaLevelAltitude = chkSeaLevelAltitude.Checked;
            AdvancedConfig.InfoPane = chkInfoPane.Checked;
            AdvancedConfig.BeepTimer = (int)nudBeepEvery.Value;
            AdvancedConfig.MainFormBottomEmptySpace = (int)nudEmpty.Value;
            AdvancedConfig.KeepAwakeList = txtKeepAwake.Text.Split(',');
            AdvancedConfig.SpeedUnit = (SpeedUnit)cmbSpeedUnit.SelectedIndex;
            AdvancedConfig.DistanceUnit = (DistanceUnit)cmbDistanceUnit.SelectedIndex;
            AdvancedConfig.ElevationUnit = (ElevationUnit)cmbElevationUnit.SelectedIndex;
            AdvancedConfig.CsvSeparator = txtCSVSeparator.Text[0];
            AdvancedConfig.CsvTextQualifier = txtCSVQualifier.Text[0];
            AdvancedConfig.CsvQualifierUsage = (CSVQualifierUsage)cmbCSVQualifierUsage.SelectedIndex;
            AdvancedConfig.CsvNewLine = cmbCSVNewLine.SelectedIndex == 0 ? "\r\n" : cmbCSVQualifierUsage.SelectedIndex == 1 ? "\n" : "\r";
            AdvancedConfig.CsvHeader = txtCSVHeader.Text;

            ArrayList alFields = new ArrayList();
            foreach (CsvItem fld in lstCSVFields.Items)
            {
                alFields.Add(fld.Code.ToString(System.Globalization.CultureInfo.InvariantCulture));
            }
            AdvancedConfig.CsvFields = String.Join("", (string[])alFields.ToArray(typeof(string)));
            //AdvancedConfig.CSVFields = String.Join("", (from CSVItem fld in lstCSVFields.Items select fld.Code.ToString(System.Globalization.CultureInfo.InvariantCulture)).ToArray());
            AdvancedConfig.CsvDateFormat = txtCSVDateFormat.Text;
            AdvancedConfig.CsvUtc = chkCSVUTC.Checked;
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
            Close();
            DialogResult = DialogResult.OK;
        }

        private void tmiCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
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

        /// <summary>Represents CSV field displayedn in <see cref="ListBox"/></summary>
        private class CsvItem
        {
            /// <summary>Contains value of the <see cref="Code"/> property</summary>
            private readonly int code;
            /// <summary>Contains value of the <see cref="Desc"/> property</summary>
            private readonly string desc;
            /// <summary>CTor</summary>
            /// <param name="code">Field code</param>
            /// <param name="desc">Field description</param>
            public CsvItem(int code, string desc)
            {
                this.code = code;
                this.desc = desc;
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

        private void btnFormatInfo_Click(object sender, EventArgs e)
        {
            notFormatInfo.Visible = true;
        }

        private void notFormatInfo_BalloonChanged(object sender, Microsoft.WindowsCE.Forms.BalloonChangedEventArgs e)
        {
            if (e.Visible == false) notFormatInfo.Visible = false;
        }

        private void AdvancedConfigForm_Closed(object sender, EventArgs e)
        {
            notFormatInfo.Visible = false;
        }
    }
}