using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml;
using System.IO.Ports;
using OpenNETCF.IO.Serial.GPS;
using OpenNETCF.IO.Serial;

namespace GPSTracka {


    public partial class GPSTracka : Form {
        public enum DevicePowerState {
            Unspecified = -1,
            D0 = 0, // Full On: full power, full functionality
            D1 = 1, // Low Power On: fully functional at low power/performance
            D2 = 2, // Standby: partially powered with automatic wake
            D3 = 3, // Sleep: partially powered with device initiated wake
            D4 = 4, // Off: unpowered
        }

        private IntPtr powerHandle;

        [DllImport("CoreDll.DLL", EntryPoint = "SetPowerRequirement", SetLastError = true)]
        public static extern IntPtr SetPowerRequirement(String pvDevice, int DeviceState, int DeviceFlags, IntPtr pvSystemState, int StateFlags);

        [DllImport("CoreDll.DLL", EntryPoint = "ReleasePowerRequirement", SetLastError = true)]
        public static extern uint ReleasePowerRequirement(IntPtr hPowerReq);

        [DllImport("CoreDll.dll")]
        private static extern void SystemIdleTimerReset();

        private static System.Threading.Timer preventSleepTimer = null;

        public static GPS gps = new GPS();
        public static bool LogEverything = false;

        private string latestLatitude;
        private string latestLongitude;
        private string latestAltitude;//Added by Đonny

        public delegate void UpdateTextboxDelegate(string theText);
        public delegate void UpdateFileDelegate(double latitude, double longitude, double altitude);//Altered by Đonny
        bool positionLogged = true;
        string previousSentence = String.Empty;

        private string currentFileName;//Added by Đonny

        public GPSTracka() {
            InitializeComponent();

            string[] portNames = SerialPort.GetPortNames();
            foreach (string port in portNames) {
                this.ComboBoxCOMPorts.Items.Add(port);
            }

            gps.Position += new GPS.PositionEventHandler(gps_Position);
            gps.GpsSentence += new GPS.GpsSentenceEventHandler(gps_GpsSentence);
            gps.GpsCommState += new GPS.GpsCommStateEventHandler(gps_GpsCommState);
            gps.Error += new GPS.ErrorEventHandler(gps_Error);

            //set up about Label
            aboutLabel.Text = "GPSTracka Version " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        void gps_Error(object sender, Exception exception, string message, string gps_data) {
            if (LogEverything) {
                WriteToTextbox(message + ". The GPS data is: " + gps_data);
                writeExceptionToTextBox(exception);
            }
        }

        void gps_GpsCommState(object sender, GpsCommStateEventArgs e) {
            if (LogEverything) {
                switch (e.State) {
                    case States.AutoDiscovery:
                        WriteToTextbox("AutoDiscovery");
                        break;
                    case States.Opening:
                        WriteToTextbox("Opening...");
                        break;
                    case States.Running:
                        WriteToTextbox("Running...");
                        break;
                    case States.Stopped:
                        WriteToTextbox("Stopped.");
                        break;
                    case States.Stopping:
                        WriteToTextbox("Stopping.");
                        break;
                }
            }

        }

        void gps_GpsSentence(object sender, GpsSentenceEventArgs e) {
            if (LogEverything) {
                System.Threading.Thread.Sleep(200); //It might go too fast.

                if (e.Sentence != previousSentence) {
                    this.WriteToTextbox(e.Sentence);
                    previousSentence = e.Sentence;
                }
            }
        }

        public void WriteToTextbox(string theText) {
            if (this.TextBoxRawLog.InvokeRequired) {
                UpdateTextboxDelegate theDelegate = new UpdateTextboxDelegate(WriteToTextbox);
                this.Invoke(theDelegate, new object[] { theText });
            } else {
                if (LogEverything || CheckBoxToTextBox.Checked) {
                    this.TextBoxRawLog.Text = this.TextBoxRawLog.Text + theText + "\r\n";

                    if (TextBoxRawLog.Text.Length > 1) {
                        TextBoxRawLog.SelectionStart = TextBoxRawLog.Text.Length - 1;
                        TextBoxRawLog.SelectionLength = 0;
                        TextBoxRawLog.ScrollToCaret();
                    }
                }
            }
        }

        void gps_Position(object sender, Position pos) {
            if (pos.Longitude_Decimal != 0 && pos.Latitude_Decimal != 0) {
                int latMultiplier = 1;
                int longMultiplier = 1;

                if (pos.DirectionLatitude == CardinalDirection.South) {
                    latMultiplier = -1;
                }

                if (pos.DirectionLongitude == CardinalDirection.West) {
                    longMultiplier = -1;
                }

                string gpsData = string.Format(System.Globalization.CultureInfo.InvariantCulture,
                    "{0:HHmmss},{1},{2},{3}", DateTime.Now,
                    Math.Round(pos.Latitude_Decimal * latMultiplier, 7),
                    Math.Round(pos.Longitude_Decimal * longMultiplier, 7),
                    Math.Round(pos.Altitude * longMultiplier, 7)); //Changed by Đonny 

                if (!positionLogged) {


                    latestLatitude = Math.Round(pos.Latitude_Decimal * latMultiplier, 7).ToString();
                    latestLongitude = Math.Round(pos.Longitude_Decimal * longMultiplier, 7).ToString();
                    latestAltitude = Math.Round(pos.Altitude, 7).ToString();//Added by Đonny

                    WriteToTextbox(gpsData);
                    WriteToFile(
                        Convert.ToDouble(Math.Round(pos.Latitude_Decimal * latMultiplier, 7)),
                        Convert.ToDouble(Math.Round(pos.Longitude_Decimal * longMultiplier, 7)),
                        Convert.ToDouble(Math.Round(pos.Altitude * longMultiplier, 7)));//Altered by Đonny
                    StopGps();
                }
                positionLogged = true;
            }
        }


        private void WriteToFile(double latitude, double longitude, double altitude)//Altered by Đonny
        {
            if (this.CheckBoxToFile.InvokeRequired || radioButtonGPX.InvokeRequired || optTrack.InvokeRequired)//Altered by Đonny
            {
                UpdateFileDelegate theDelegate = new UpdateFileDelegate(WriteToFile);
                this.Invoke(theDelegate, new object[] { latitude, longitude, altitude });//Altered by Đonny
            } else {
                try {
                    if (CheckBoxToFile.Checked && !LogEverything) {

                        if (radioButtonGPX.Checked) {
                            WriteToGPXFile(latitude, longitude, altitude);//Altered by Đonny
                        }

                        if (radioButtonKML.Checked) {
                            WriteToKMLFile(latitude, longitude, altitude);//Altered by Đonny
                        }
                    }
                } catch (Exception ex)//Commented out by Đonny
                {
                    WriteToTextbox("Couldn't write location to file. Try changing locations. " + ex.Message);
                    //writeExceptionToTextBox(ex);
                }
            }
        }
        private const string KmlNs = "http://www.opengis.net/kml/2.2";//Added by Đonny

        private void WriteToKMLFile(double latitude, double longitude, double altitude)//Altered by Đonny
        {
            XmlDocument doc = new XmlDocument();
            if (currentFileName == null)//Added by Đonny
                currentFileName = Path.Combine(logLocationTextBox.Text, DateTime.Now.ToString("yyyyMMdd_HHmmss", System.Globalization.CultureInfo.InvariantCulture) + ".kml");//Altered by Đonny

            if (!File.Exists(currentFileName)) {

                if (!Directory.Exists(logLocationTextBox.Text)) {
                    try {
                        Directory.CreateDirectory(logLocationTextBox.Text);
                    } catch {
                        WriteToTextbox("Cannot create the directory specified.  Try another location.");
                    }

                }

                StreamWriter stream = File.CreateText(currentFileName);
                stream.WriteLine(@"<?xml version=""1.0"" encoding=""UTF-8""?>
                                <kml xmlns=""" + KmlNs /*Altered by Đonny*/ + @""">
                                <Document>
                                    <name>GPSTracka KML Output</name>
                                    <Folder id='Points'>
                                        <name>Points</name>
                                    </Folder>
                                    " + (optTrack.Checked ? //Added by Đonny (<Folder id='Points'> and <Placemark id='track'>)
                                    @"<Placemark id='track'>
                                        <name>18.4.09 15:00:57</name>
                                        <styleUrl>#yellowLineGreenPoly</styleUrl>
                                        <Style id='yellowLineGreenPoly'>
                                            <LineStyle>
                                                <color>ffff0000</color>
                                                <width>4</width>
                                            </LineStyle>
                                            <PolyStyle><color>7f00ff00</color></PolyStyle>
                                        </Style>
                                        <LineString>" +
                                                     (chkAltitude.Checked ? @"<altitudeMode>absolute</altitudeMode>" : "")
                                            + @"<coordinates/>
                                        </LineString>
                                      </Placemark>"
                                : "") + @"</Document></kml>");
                //stream.WriteLine("<time>" + DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ") + "</time><bounds />");
                //stream.WriteLine("</gpx>");
                stream.Close();
            }

            doc.Load(currentFileName);

            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("def", KmlNs);//Altered by Đonny

            //XmlNode kml = doc.ChildNodes[1].SelectSingleNode("Document");
            XmlNode kml = doc.SelectSingleNode("/def:kml/def:Document/def:Folder[@id='Points']", nsmgr);

            XmlNode placemark = doc.CreateElement("Placemark", KmlNs);

            XmlNode plName = doc.CreateElement("name", KmlNs);//Altered by Đonny
            plName.InnerText = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ", System.Globalization.CultureInfo.InvariantCulture);//Altered by Đonny
            placemark.AppendChild(plName);

            XmlNode plDescription = doc.CreateElement("description", KmlNs);//Altered by Đonny
            plDescription.InnerText = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ", System.Globalization.CultureInfo.InvariantCulture);//Altered by Đonny
            placemark.AppendChild(plDescription);

            XmlNode plPoint = doc.CreateElement("Point", KmlNs);//Altered by Đonny
            XmlNode plPointCoord = doc.CreateElement("coordinates", KmlNs);//Altered by Đonny
            plPointCoord.InnerText = longitude.ToString(System.Globalization.CultureInfo.InvariantCulture) + "," +
                                     latitude.ToString(System.Globalization.CultureInfo.InvariantCulture) + "," +
                                     (chkAltitude.Checked ? altitude.ToString(System.Globalization.CultureInfo.InvariantCulture) : "0");//Altered by Đonny
            if (chkAltitude.Checked) {//Added by Đonny (whole if)
                XmlNode coMode = doc.CreateElement("altitudeMode", KmlNs);
                coMode.InnerText = "absolute";
                plPoint.AppendChild(coMode);
            }
            plPoint.AppendChild(plPointCoord);

            placemark.AppendChild(plPoint);

            kml.AppendChild(placemark);
            if (optTrack.Checked) {//Added by Đonny (whole if)
                XmlNode coordinates = doc.SelectSingleNode("/def:kml/def:Document/def:Placemark[@id='track']/def:LineString/def:coordinates", nsmgr);
                coordinates.InnerText += string.Format(System.Globalization.CultureInfo.InvariantCulture,
                    "\r\n{0},{1},{2}", longitude, latitude, (chkAltitude.Checked ? altitude.ToString(System.Globalization.CultureInfo.InvariantCulture) : "0"));
            }
            doc.Save(currentFileName);
        }

        private const string GpxNs = "http://www.topografix.com/GPX/1/0";//Added by Đonny

        private void WriteToGPXFile(double latitude, double longitude, double altitude)//Altered by Đonny
        {
            XmlDocument doc = new XmlDocument();
            if (currentFileName == null)//Added by Đonny
                currentFileName = Path.Combine(logLocationTextBox.Text, DateTime.Now.ToString("yyyyMMdd_HHmmss", System.Globalization.CultureInfo.InvariantCulture) + ".gpx");//Altered by Đonny

            if (!File.Exists(currentFileName)) {

                if (!Directory.Exists(logLocationTextBox.Text)) {
                    try {
                        Directory.CreateDirectory(logLocationTextBox.Text);
                    } catch {
                        WriteToTextbox("Cannot create the directory specified.  Try another location.");
                    }

                }

                StreamWriter stream = File.CreateText(currentFileName);
                stream.WriteLine(@"<?xml version=""1.0""?>");
                stream.WriteLine(@"<gpx
                             version=""1.0""
                             creator=""GPSTracka - http://www.mendhak.com/""
                             xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
                             xmlns=""" + GpxNs /*Altered by Đonny*/+ @"""
                             xsi:schemaLocation=""http://www.topografix.com/GPX/1/0 http://www.topografix.com/GPX/1/0/gpx.xsd"">");
                stream.WriteLine("<time>" + DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ", System.Globalization.CultureInfo.InvariantCulture) + "</time>");//Altered by Đonny (culture, removed <bounds/>)
                if (optTrack.Checked) {//Aded by Đonny (whole if)
                    stream.WriteLine("<trk>");
                    stream.WriteLine("<trkseg/>");
                    stream.WriteLine("</trk>");
                }
                stream.WriteLine("</gpx>");
                stream.Close();
            }

            doc.Load(currentFileName);

            XmlNode parent;//Altered by Đonny (gpx renamed to parent)
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);//Added by Đonny
            nsmgr.AddNamespace("def", GpxNs);//Added by Đonny
            if (optTrack.Checked)//Added by Đonny (whole if)
                parent = doc.SelectSingleNode("/def:gpx/def:trk/def:trkseg", nsmgr);
            else
                parent = doc.SelectSingleNode("/def:gpx", nsmgr);

            XmlNode wpt = doc.CreateElement(optTrack.Checked ? "trkpt" : "wpt", GpxNs);//Altered by Đonny

            XmlAttribute latAttrib = doc.CreateAttribute("lat");
            latAttrib.Value = latitude.ToString(System.Globalization.CultureInfo.InvariantCulture);//Altered by Đonny
            wpt.Attributes.Append(latAttrib);

            XmlAttribute longAttrib = doc.CreateAttribute("lon");
            longAttrib.Value = longitude.ToString(System.Globalization.CultureInfo.InvariantCulture);//Altered by Đonny
            wpt.Attributes.Append(longAttrib);

            if (chkAltitude.Checked) {//Added by Đonny (whole if and its body)
                XmlNode altNode = doc.CreateElement("ele", GpxNs);
                altNode.InnerText = altitude.ToString(System.Globalization.CultureInfo.InvariantCulture);
                wpt.AppendChild(altNode);
            }

            XmlNode timeNode = doc.CreateElement("time", GpxNs); ;//Altered by Đonny
            timeNode.InnerText = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ", System.Globalization.CultureInfo.InvariantCulture);//Altered by Đonny

            wpt.AppendChild(timeNode);

            parent.AppendChild(wpt);//Altered by Đonny

            doc.Save(currentFileName);
        }

        private void writeExceptionToTextBox(Exception ex) {
            string errorMessage = String.Concat("\r\n\r\n",
                "****ERROR****", "\r\n",
                ex.Message, "\r\n",
                "*************", "\r\n",
                ex.StackTrace, "\r\n",
                "*************", "\r\n");

            if (ex.InnerException != null) {
                errorMessage = errorMessage + ex.InnerException.Message + "\r\n" + ex.InnerException.StackTrace;
            }

            WriteToTextbox(errorMessage);
        }


        private void timer1_Tick(object sender, EventArgs e) {
            positionLogged = false;
            StartupGps();
            timer1.Enabled = true;
        }

        private void StopGps() {
            gps.Stop();
            timer1.Enabled = true;
        }

        private void StartupGps() {
            string comPort = ComboBoxCOMPorts.Text;
            int baudRate = Convert.ToInt32(ComboBaudRate.Text);

            if (String.IsNullOrEmpty(comPort)) {
                comPort = "COM1";
            }

            gps.ComPort = comPort;
            gps.BaudRate = (BaudRates)baudRate;

            gps.Start();

        }

        private void GPSTracka_Closing(object sender, CancelEventArgs e) {
            try {
                gps.Stop();

                ReleasePowerRequirement(powerHandle);
                powerHandle = IntPtr.Zero;

            } catch {
                //Nothing
            }

        }

        private static void KeepDeviceAwake(object o) {
            try {
                SystemIdleTimerReset();
            } catch (Exception) {
                // Nothing
            }
        }

        private void GPSTracka_Load(object sender, EventArgs e) {
            //prepare Panels
            HidePanel(aboutPanel);
            HidePanel(settingsPanel);
            ShowPanel(mainPanel);

            //prepare form
            this.Size = new Size(240, 294);

            //Default values
            TextBoxRawLog.ScrollBars = ScrollBars.Vertical;
            ComboBoxCOMPorts.SelectedIndex = 0;
            ComboBaudRate.SelectedIndex = 0;
            chkAltitude.Checked = false;//Added by Đonny
            optDistinct.Checked = true;//Added by Đonny


            //Read from config file
            string currentDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);


            string settingsCOMPort = String.Empty;
            string settingsBaudRate = String.Empty;
            int settingsSeconds = 60;
            bool settingsToTextBox = true;
            bool settingsToLogFile = false;
            string settingsLogFileLocation = String.Empty;
            string logFormat = "GPX";
            bool LogAltitude = false;//Added by Đonny

            settingsCOMPort = ConfigurationManager.AppSettings["COMPort"];
            settingsBaudRate = ConfigurationManager.AppSettings["BaudRate"];
            TryParse(ConfigurationManager.AppSettings["PollingInterval"], out settingsSeconds);
            TryParse(ConfigurationManager.AppSettings["LogToTextBox"], out settingsToTextBox);
            TryParse(ConfigurationManager.AppSettings["LogToLogFile"], out settingsToLogFile);
            settingsLogFileLocation = ConfigurationManager.AppSettings["LogFileLocation"];
            logFormat = ConfigurationManager.AppSettings["LogFormat"];
            TryParse(ConfigurationManager.AppSettings["LogAltitude"], out LogAltitude);//Added by Đonny

            if (!String.IsNullOrEmpty(settingsCOMPort)) {
                //ComboBoxCOMPorts.SelectedItem = settingsCOMPort;
                ComboBoxCOMPorts.Text = settingsCOMPort;
            }

            if (!String.IsNullOrEmpty(settingsBaudRate)) {
                ComboBaudRate.Text = settingsBaudRate;
            }

            if (!String.IsNullOrEmpty(settingsLogFileLocation)) {
                logLocationTextBox.Text = settingsLogFileLocation;
            }
            if (ConfigurationManager.AppSettings["TrackType"] == "Track") optTrack.Checked = true;//Added by Đonny

            if (settingsSeconds >= 1)//Altered by Đonny (change 60 to 1)
            {
                NumericUpDownInterval.Value = settingsSeconds;
            }


            CheckBoxToTextBox.Checked = settingsToTextBox;
            CheckBoxToFile.Checked = settingsToLogFile;
            chkAltitude.Checked = LogAltitude;//Added by Đonny

            switch (logFormat) {
                case "GPX":
                    radioButtonGPX.Checked = true;
                    break;
                case "KML":
                    radioButtonKML.Checked = true;
                    break;
                default:
                    radioButtonGPX.Checked = true;
                    break;
            }



            //Call keepdeviceawake every 30 seconds in its own timer
            //Cannot use existing timer because it may have 5 minute intervals.
            preventSleepTimer = new System.Threading.Timer(new System.Threading.TimerCallback(KeepDeviceAwake), null, 0, 30000);
        }

        private void clearMenuItem_Click(object sender, EventArgs e) {
            TextBoxRawLog.Text = string.Empty;
        }

        private void verboseMenuItem_Click(object sender, EventArgs e) {
            verboseMenuItem.Checked = !verboseMenuItem.Checked;
        }

        private void startMenuItem_Click(object sender, EventArgs e) {


            //First, check if it's verbose and we've just clicked start

            if (verboseMenuItem.Checked && !LogEverything) {
                LogEverything = true;
                StartupGps();
                startMenuItem.Text = "Stop!";
                return;
            }



            //Check if it's verbose and we're trying to stop it.
            if (verboseMenuItem.Checked && LogEverything) {
                gps.Stop();
                LogEverything = false;
                timer1.Enabled = false;
                startMenuItem.Text = "Start";
                return;
            }

            //Else, work normally.
            timer1.Interval = Convert.ToInt16(NumericUpDownInterval.Value) * 1000;

            timer1.Enabled = !timer1.Enabled;

            if (timer1.Enabled) {
                powerHandle = SetPowerRequirement(ComboBoxCOMPorts.Text + ":", (int)DevicePowerState.D0, 1, IntPtr.Zero, 0);
                WriteToTextbox("Will begin to read GPS data after " + NumericUpDownInterval.Value.ToString() + " seconds.");
                startMenuItem.Text = "Stop";
                verboseMenuItem.Enabled = false;
                logLocationTextBox.Enabled = false;
                CheckBoxToFile.Enabled = false;
                ComboBoxCOMPorts.Enabled = false;
                ComboBaudRate.Enabled = false;
                NumericUpDownInterval.Enabled = false;
                radioButtonGPX.Enabled = false; //Added by Đonny
                radioButtonKML.Enabled = false; //Added by Đonny
                optTrack.Enabled = false;//Added by Đonny
                optDistinct.Enabled = false;//Added by Đonny
            } else {
                gps.Stop();

                ReleasePowerRequirement(powerHandle);
                powerHandle = IntPtr.Zero;

                verboseMenuItem.Enabled = true;
                CheckBoxToFile.Enabled = true;
                logLocationTextBox.Enabled = true;
                ComboBoxCOMPorts.Enabled = true;
                ComboBaudRate.Enabled = true;
                NumericUpDownInterval.Enabled = true;
                radioButtonGPX.Enabled = true; //Added by Đonny
                radioButtonKML.Enabled = true; //Added by Đonny
                optTrack.Enabled = true;//Added by Đonny
                optDistinct.Enabled = true;//Added by Đonny
                startMenuItem.Text = "Start";
                currentFileName = null;//Added by Đonny
            }
        }

        private void ShowPanel(Panel panel) {
            panel.Location = new Point(0, 0);
            panel.Dock = DockStyle.Fill;

            switch (panel.Name) {
                case "settingsPanel": {
                        this.Menu = settingsPanelMenu;
                        break;
                    }
                case "aboutPanel": {
                        this.Menu = aboutPanelMenu;
                        break;
                    }
                case "mainPanel": {
                        this.Menu = mainPanelMenu;
                        break;
                    }
                default: {
                        this.Menu = mainPanelMenu;
                        break;
                    }
            }
        }

        private void HidePanel(Panel panel) {
            panel.Dock = DockStyle.None;
            panel.Location = new Point(800, 800);
        }

        private void aboutMenuItem_Click(object sender, EventArgs e) {
            HidePanel(mainPanel);
            HidePanel(settingsPanel);
            ShowPanel(aboutPanel);
        }

        private void settingsMenuItem_Click(object sender, EventArgs e) {
            HidePanel(mainPanel);
            HidePanel(aboutPanel);
            ShowPanel(settingsPanel);
        }

        private void cancelMenuItem_Click(object sender, EventArgs e) {
            HidePanel(settingsPanel);
            HidePanel(aboutPanel);
            ShowPanel(mainPanel);
        }

        private void saveMenuItem_Click(object sender, EventArgs e) {

            if (CheckBoxToFile.Checked && String.IsNullOrEmpty(logLocationTextBox.Text)) {
                MessageBox.Show("Please select a proper file path");
                return;
            }

            ConfigurationManager.AppSettings["COMPort"] = ComboBoxCOMPorts.Text;
            ConfigurationManager.AppSettings["BaudRate"] = ComboBaudRate.Text;
            ConfigurationManager.AppSettings["PollingInterval"] = NumericUpDownInterval.Value.ToString(System.Globalization.CultureInfo.InvariantCulture);//Altered by Đonny
            ConfigurationManager.AppSettings["LogToTextBox"] = CheckBoxToTextBox.Checked.ToString(System.Globalization.CultureInfo.InvariantCulture);//Altered by Đonny
            ConfigurationManager.AppSettings["LogToLogFile"] = CheckBoxToFile.Checked.ToString(System.Globalization.CultureInfo.InvariantCulture);//Altered by Đonny
            ConfigurationManager.AppSettings["LogFileLocation"] = logLocationTextBox.Text;
            ConfigurationManager.AppSettings["LogAltitude"] = chkAltitude.Checked.ToString(System.Globalization.CultureInfo.InvariantCulture);//Added by Đonny

            if (radioButtonKML.Checked) {
                ConfigurationManager.AppSettings["LogFormat"] = "KML";
            } else {
                ConfigurationManager.AppSettings["LogFormat"] = "GPX";
            }
            ConfigurationManager.AppSettings["TrackType"] = optTrack.Checked ? "Track" : "Points";//Added by Đonny

            ConfigurationManager.Save();

            HidePanel(settingsPanel);
            HidePanel(aboutPanel);
            ShowPanel(mainPanel);
        }

        private void saveDialogButton_Click(object sender, EventArgs e) {
            DialogResult result = saveFileDialog1.ShowDialog();
            if (result == DialogResult.OK) {
                FileInfo fi = new FileInfo(saveFileDialog1.FileName);
                //Parse and get the folder path
                logLocationTextBox.Text = fi.DirectoryName;
            } else {
                logLocationTextBox.Text = string.Empty;
            }
        }

        /// <summary>
        /// Attempts to parse a number from a string
        /// </summary>
        /// <param name="strNumber"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        private static bool TryParse(string strNumber, out int number) {
            try {
                number = int.Parse(strNumber, System.Globalization.CultureInfo.InvariantCulture);//Altered by Đonny
                return true;

            } catch (Exception) {
                number = 0;
                return false;
            }
        }

        /*/// <summary>
        /// Attempts to parse a DateTime from a string
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private static bool TryParse(string inDateTime, out DateTime outDateTime)
        {
            try
            {
                if (inDateTime.EndsWith("-00:00"))
                {
                    inDateTime = inDateTime.Replace("-00:00", "");
                }
                outDateTime = DateTime.Parse(inDateTime);
                return true;
            }
            catch
            {
                outDateTime = DateTime.MinValue;
                return false;
            }
        }*/
        //Commented out by Đonny (not used)

        private static bool TryParse(string inBoolean, out Boolean outBoolean) {
            try {
                outBoolean = Boolean.Parse(inBoolean);
                return true;
            } catch {
                outBoolean = false;
                return false;
            }
        }


        private void backMenuItem_Click(object sender, EventArgs e) {

            if (CheckBoxToFile.Checked && String.IsNullOrEmpty(logLocationTextBox.Text)) {
                CheckBoxToFile.Checked = false;
            }

            HidePanel(settingsPanel);
            HidePanel(aboutPanel);
            ShowPanel(mainPanel);
        }

        private void linkLabel1_Click(object sender, EventArgs e) {
            System.Diagnostics.Process.Start(linkLabel1.Text, "");
        }

        private void menuExit_Click(object sender, EventArgs e) {

            this.Close();
        }

        //private void menuItemSend_Click(object sender, EventArgs e)
        //{
        //    DescInput di = new DescInput();
        //    di.Latitude = latestLatitude;
        //    di.Longitude = latestLongitude;
        //    if (!String.IsNullOrEmpty(latestLatitude) && !String.IsNullOrEmpty(latestLongitude))
        //    {
        //        di.ShowDialog();
        //    }
        //    else
        //    {
        //        MessageBox.Show("Nothing to send yet.");
        //    }
        //}
    }
}