using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml;
using System.IO.Ports;
using OpenNETCF.IO.Serial;

namespace GPSTracka
{

    /// <summary>Main form</summary>
    public partial class TrackerForm : Form
    { //renamed from GPSTracka to TrackerForm - old name puzzled the designer
        #region API
        /// <summary>Power states</summary>
        public enum DevicePowerState : int
        {
            /// <summary>Power state not specified</summary>
            Unspecified = -1,
            /// <summary>Full On: full power, full functionality</summary>
            D0 = 0,
            /// <summary>Low Power On: fully functional at low power/performance</summary>
            D1 = 1,
            /// <summary>Standby: partially powered with automatic wake</summary>
            D2 = 2,
            /// <summary>Sleep: partially powered with device initiated wake</summary>
            D3 = 3,
            /// <summary>Off: unpowered</summary>
            D4 = 4,
        }
        /// <summary>Power requirement handle</summary>
        private IntPtr powerHandle;
        /// <summary>This function notifies Power Manager that an application has a specified device power requirement.</summary>
        /// <param name="pvDevice">[in] Specifies the device. Must be a valid LPWSTR device name, for example, "COM1:". The meaning is determined by the <paramref name="deviceFlags"/> parameter.</param>
        /// <param name="deviceState">[in] Specifies the minimum device power state from the CEDEVICE_POWER_STATE enumeration at which to maintain the device.</param>
        /// <param name="deviceFlags">[in] Bitwise or of POWER_FORCE and POWER_NAME flags</param>
        /// <param name="pvSystemState">[in] If not set to null, indicates that the requirement should only be enforced for the named system power state.</param>
        /// <param name="stateFlags">[in] Unused. Set to zero. 
        /// <returns>Nonzero indicates success. Zero indicates failure. </returns></param>
        [DllImport("CoreDll.DLL", EntryPoint = "SetPowerRequirement", SetLastError = true)]
        private static extern IntPtr SetPowerRequirement(String pvDevice, DevicePowerState deviceState, int deviceFlags, IntPtr pvSystemState, int stateFlags);
        /// <summary>This function requests that Power Manager release a power requirement previously set with <see cref="SetPowerRequirement"/>. Applications and drivers should always explicitly release their power requirements as soon as they cease to be necessary. </summary>
        /// <param name="hPowerReq">[in] Specifies a handle returned by a previous successful call to <see cref="SetPowerRequirement"/>.</param>
        /// <returns>Win32 error code or ERROR_SUCCESS or ERROR_INVALID_HANDLE</returns>
        [DllImport("CoreDll.DLL", EntryPoint = "ReleasePowerRequirement", SetLastError = true)]
        private static extern uint ReleasePowerRequirement(IntPtr hPowerReq);
        /// <summary>This function resets a system timer that controls whether or not the device will automatically go into a suspended state. </summary>
        [DllImport("CoreDll.dll")]
        private static extern void SystemIdleTimerReset();

        private static System.Threading.Timer preventSleepTimer = null;
        ///// <summary>This function notifies Power Manager of the events required for implementing a power policy created by an OEM.</summary>
        ///// <param name="dwMessage">Set to one of the predefined PPN_* values, or a custom value.</param>
        ///// <param name="onOrOff">32-bit value that varies depending on the <paramref name="dwMessage"/> value.</param>
        ///// <returns>TRUE indicates success. FALSE indicates failure.</returns>
        //[DllImport("CoreDll.dll")]
        //static extern bool PowerPolicyNotify(int dwMessage, int onOrOff);
        ///// <summary>Unattended mode</summary>
        //const int PPN_UNATTENDEDMODE = 0x00000003;
        #endregion

        #region Initialization
        /// <summary>CTor</summary>
        public TrackerForm()
        {
            InitializeComponent();
            initMeasure = lblAerial.Size;
            stsStatus[0] = Properties.Resources.PressStart;//Status 
            stsStatus[1] = "";//Countdown 
            stsStatus[2] = "";//Speed 

            string[] portNames = SerialPort.GetPortNames();
            foreach (string port in portNames)
            {
                this.ComboBoxCOMPorts.Items.Add(port);
            }

            //set up about Label
            aboutLabel.Text = Properties.Resources.GPSTrackaVersion + " " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
            //stsStatus.Dock = DockStyle.Bottom;
            stsStatus.Visible = false;
            panInfoPane_Resize(this, EventArgs.Empty);
            panInfoPane.Visible = false;

            fraCOMPort.Text = Properties.Resources.COMPort;
            fraFormat.Text = Properties.Resources.Format;
            fraOptions.Text = Properties.Resources.LoggingOptions;
            fraSpeed.Text = Properties.Resources.PollingRate;
        }
        private void GPSTracka_Load(object sender, EventArgs e)
        {
            TextBoxRawLog.BackColor = SystemColors.Window;
            //PowerPolicyNotify(PPN_UNATTENDEDMODE, 1);
            //prepare Panels
            HidePanel(aboutPanel);
            HidePanel(settingsPanel);
            ShowPanel(mainPanel);

            //prepare form
            this.Size = new Size(240, 294);
            TextBoxRawLog.ScrollBars = ScrollBars.Vertical;
            //Apply settings
            ShowSettings();

            //Call keepdeviceawake every 30 seconds in its own timer
            //Cannot use existing timer because it may have 5 minute intervals.
            preventSleepTimer = new System.Threading.Timer(new System.Threading.TimerCallback(KeepDeviceAwake), null, 0, 30000);
        }
        /// <summary>Shows basig settings to user using controls</summary>
        private void ShowSettings()
        {
            ComboBoxCOMPorts.Text = AdvancedConfig.COMPort;
            ComboBaudRate.Text = ((int)AdvancedConfig.BaudRate).ToString();
            chkUseWindowsDriver.Checked = AdvancedConfig.UseWindowsDriver;
            NumericUpDownInterval.Value = AdvancedConfig.PollingInterval;
            CheckBoxToTextBox.Checked = AdvancedConfig.LogToTextBox;
            CheckBoxToFile.Checked = AdvancedConfig.LogToLogFile;
            logLocationTextBox.Text = AdvancedConfig.LogFileLocation;
            
            switch (AdvancedConfig.LogFormat)
            {
                case LogFormat.KML: radioButtonKML.Checked = true; break;
                case LogFormat.CSV: optCSV.Checked = true; break;
                default: radioButtonGPX.Checked = true; break;
            }
            chkAltitude.Checked = AdvancedConfig.LogAltitude;
            switch (AdvancedConfig.TrackType)
            {
                case TrackType.Track: optTrack.Checked = true; break;
                default: optDistinct.Checked = true; break;
            }
            stsStatus.Visible = AdvancedConfig.StatusBar;
            panInfoPane.Visible = AdvancedConfig.InfoPane;
        }
        /// <summary>Attaches or detaches GPS events</summary>
        /// <param name="hook">True to atache false to detach</param>
        private void HookGps(bool hook)
        {
            if (hook)
            {
                GPS.Position += gps_Position;
                GPS.GpsSentence += gps_GpsSentence;
                GPS.StateChanged += gps_GpsCommState;
                GPS.Error += gps_Error;
                GPS.Movement += GPS_Movement;
                GPS.Satellite += GPS_Satellite;
            }
            else
            {
                GPS.Position -= gps_Position;
                GPS.GpsSentence -= gps_GpsSentence;
                GPS.StateChanged -= gps_GpsCommState;
                GPS.Error -= gps_Error;
                GPS.Movement -= GPS_Movement;
                GPS.Satellite -= GPS_Satellite;
            }
        }

        #endregion

        #region Status
        /// <summary>Sets mssage to status bar</summary>
        /// <param name="message">Message to be shown</param>
        void Status(string message)
        {
            if (stsStatus.InvokeRequired)
                try
                {
                    stsStatus.BeginInvoke(new Action<string>(Status), message);
                }
                catch (ObjectDisposedException) { }
            else
            {
                stsStatus[0] = message;
            }
        }

        private delegate void StatusDelegate(string message, int slot);

        void Status(string message, int slot)
        {
            if (stsStatus.InvokeRequired)
            {
                try
                {

                    StatusDelegate theDelegate = Status;
                    BeginInvoke(theDelegate, new object[] { message, slot });

                }
                catch (ObjectDisposedException) { }
            }
            else
            {
                stsStatus[slot] = message;
            }
        }
        /// <summary>Shows speed in status bar to the user</summary>
        private void ShowSpeed(decimal speed)
        {
            if (stsStatus.InvokeRequired)
            {
                stsStatus.BeginInvoke(new Action<decimal>(ShowSpeed), speed);
                return;
            }
            if (!AdvancedConfig.StatusBar) return;
            stsStatus[2] = speed.ToString("0") + AdvancedConfig.SpeedUnitName;
        }
        int CountDown = 0;
        private void tmrCountDown_Tick(object sender, EventArgs e)
        {
            if (CountDown < 0)
            {
                stsStatus[1] = "";
                tmrCountDown.Enabled = false;
            }
            else stsStatus[1] = CountDown--.ToString();
        }
        #endregion

        #region GPS events
        #region Sentence
        DateTime lastSentenceTime = DateTime.MinValue;
        /// <summary>Number of currently reported sentence</summary>
        /// <remarks>Synchronize access using <see cref="sentenceCounterSyncObj"/></remarks>
        int sentenceCounter = -1;
        /// <summary>Synchronize access to <see cref="sentenceCounter"/> using this</summary>
        object sentenceCounterSyncObj = new object();
        private bool logSentenceOnStack = false;
        void LogSentence(Gps.GpsSentenceEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action<Gps.GpsSentenceEventArgs>(LogSentence), e);
                return;
            }
            lock (sentenceCounterSyncObj)
            {
                if (sentenceCounter - e.Counter > 10) return;//Too much sentences in queue for logging
            }
            this.WriteToTextbox(e.Sentence);
            if (!logSentenceOnStack)
            {
                logSentenceOnStack = true;
                try { Application.DoEvents(); }
                finally { logSentenceOnStack = false; }
            }
        }
        void gps_GpsSentence(Gps.GpsProvider sender, Gps.GpsSentenceEventArgs e)
        {
            //if ((LogEverything || AdvancedConfig.NmeaLog) && (DateTime.Now - lastSentenceTime).TotalMilliseconds < 150) System.Threading.Thread.Sleep(200);

            lastSentenceTime = DateTime.Now;
            if (LogEverything)
            {//Verbose (to textbox)

                System.Threading.Thread.Sleep(200); //It might go too fast.

                if (e.Sentence != previousSentence)
                {
                    lock (sentenceCounterSyncObj) sentenceCounter = e.Counter;
                    LogSentence(e);
                    previousSentence = e.Sentence;
                }
            }
            if (AdvancedConfig.NmeaLog)
            {//NMEA file logging
                if (NMEAFileName == null)
                {
                    string nmeaFileLocation = String.Empty;
                    if(String.IsNullOrEmpty(AdvancedConfig.LogFileLocation))
                    {
                        nmeaFileLocation = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase.ToString());
                    }
                    else
                    {
                        nmeaFileLocation = AdvancedConfig.LogFileLocation;
                    }

                    NMEAFileName = System.IO.Path.Combine(nmeaFileLocation,
                                                          DateTime.Now.ToString("yyyyMMdd_HHmmss",
                                                                                System.Globalization.CultureInfo.
                                                                                    InvariantCulture) + ".nmea");
                }
                if (!System.IO.File.Exists(NMEAFileName))
                {
                    if (!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(NMEAFileName)))
                    {
                        try
                        {
                            System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(NMEAFileName));
                        }
                        catch (Exception ex)
                        {
                            WriteToTextbox(string.Format(Properties.Resources.err_NmeaCreateDir, NMEAFileName, ex.Message));
                            return;
                        }
                    }
                }
                try
                {
                    using (var file = System.IO.File.Open(NMEAFileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write))
                    {
                        file.Seek(file.Length, SeekOrigin.Begin);
                        var w = new System.IO.StreamWriter(file);
                        w.WriteLine(e.Sentence);
                        w.Flush();
                    }
                }
                catch (Exception ex)
                {
                    WriteToTextbox(string.Format(Properties.Resources.err_NmeaOpenFile, NMEAFileName, ex.Message));
                }
            }
        }
        /// <summary>Delegate fo parameterless function returning string</summary>
        private delegate string getString();
        #endregion
        /// <summary>Last date &amp; time when possition was logged (to prevent duplicates)</summary>
        DateTime LastGpsLoggedAt = DateTime.MinValue;
        /// <summary>Counts invalid positions in order to stop GPS after 10</summary>
        int InvalidPositionCount = 0;
        /// <summary>Indicates that GPS logging just started and no valid position has been got yet.</summary>
        bool AfterStart = true;
        /// <summary>Device local time when invalid position was last reported to user</summary>
        DateTime LastInvPosUserTime = DateTime.MinValue;
        /// <summary>Device local time when invalid position was last received</summary>
        DateTime LastInvPosTime = DateTime.MinValue;
        void gps_Position(Gps.GpsProvider sender, Gps.GpsPositionEventArgs e)
        {
            Gps.GpsPosition pos = e.Position;
            if (WaitForStop) return;
            if ((DateTime.Now - LastGpsLoggedAt).TotalMilliseconds < 10) return; //I've experianced multiple logs at same place in same time
            if (pos.Longitude.HasValue && pos.Latitude.HasValue)
            {
                AfterStart = false;
                InvalidPositionCount = 0;
                LastGpsLoggedAt = DateTime.Now;
                DateTime time = AdvancedConfig.UseGpsTime ? pos.GpsTime.ToLocalTime() : DateTime.Now;
                int latMultiplier = 1;
                int longMultiplier = 1;
                decimal? Altitude = pos.Altitude.HasValue ? pos.Altitude + (decimal)AdvancedConfig.AltitudeCorrection : null;

                //if (pos.DirectionLatitude == CardinalDirection.South) {
                //    latMultiplier = -1;
                //}

                //if (pos.DirectionLongitude == CardinalDirection.West) {
                //    longMultiplier = -1;
                //}
                string gpsData = string.Format(
                    AdvancedConfig.TextLogFormat,
                    time.ToUniversalTime(),
                    Math.Round(pos.Longitude.Value  /** longMultiplier*/, 7),
                    Math.Round(pos.Latitude.Value  /** latMultiplier*/, 7),
                    Altitude.HasValue ? Math.Round(Altitude.Value, 7) : 0,
                    time,
                    AdvancedConfig.ElevationUnitName
                    );

                if (!positionLogged)
                {
                    bool LogThisPoint = false; //Check if the point is not too neer to stop-start point 
                    decimal? distance = null;
                    if (LastPositionA == null)
                    {//Stop-start point unknown (immediately after start)  
                        LogThisPoint = true;
                    }
                    else
                    {
                        distance = Math.Abs(Gps.GpsPosition.CalculateDistance(LastPositionA.Value, pos, OpenNETCF.IO.Serial.GPS.Units.Kilometers) * 1000);
                        if (distance < AdvancedConfig.MinimalDistance)
                        { //Point is to near
                            LastPositionC = pos;//Remember it
                            LastTimeC = time; //And remember the time
                        }
                        else
                        {//Points are far enough
                            LogThisPoint = true;//Log this point
                            /*//If distance of this point from latest not logged point is long enough log that point as well
                            decimal distAC = Math.Abs(Gps.GpsPosition.CalculateDistance(LastPositionC.Value, pos, OpenNETCF.IO.Serial.GPS.Units.Kilometers) * 1000);*/
                            if (LastPositionC != null && time != LastTimeA && LastTimeA != LastTimeC /*&& distAC >= AdvancedConfig.MinimalDistance*/)
                            { //Commented-out - always log last pause point
                                WriteToFile(
                                    LastTimeC,
                                    Convert.ToDouble(Math.Round(LastPositionC.Value.Latitude.Value /* * latMultiplier*/, 7)),
                                    Convert.ToDouble(Math.Round(LastPositionC.Value.Longitude.Value /* * longMultiplier*/, 7)),
                                    Convert.ToDouble(Math.Round(LastPositionC.Value.Altitude.HasValue ? LastPositionC.Value.Altitude.Value + AdvancedConfig.AltitudeCorrection : 0, 7))
                                );
                            }
                        }
                    }
                    WriteToTextbox(gpsData + (LogThisPoint ? "" : " X"));
                    OnPoint(time, new GpsPoint(pos.Latitude.Value, pos.Longitude.Value, pos.Altitude), LogThisPoint);
                    if (LogThisPoint)
                    {
                        if (distance.HasValue)
                            ShowSpeed(distance.Value / 1000 / (decimal)(time - LastTimeA).TotalHours);
                        Status(Properties.Resources.status_PositionReceived);
                        LastPositionA = pos;//Remember position
                        LastTimeA = time;//Remember time
                        LastPositionC = pos;
                        LastTimeC = time;
                        WriteToFile(
                            time,
                            Convert.ToDouble(Math.Round(pos.Latitude.Value * latMultiplier, 7)),
                            Convert.ToDouble(Math.Round(pos.Longitude.Value * longMultiplier, 7)),
                            Convert.ToDouble(Altitude.HasValue ? Math.Round(Altitude.Value, 7) : 0));

                    }
                    else
                    {
                        Status(string.Format(Properties.Resources.PointsTooNear, distance));
                    }
                    StopGps();
                }
                positionLogged = true;
            }
            else
            {
                if (++InvalidPositionCount == int.MaxValue) InvalidPositionCount = 0;
                if (!AfterStart && AdvancedConfig.InvalidPositionsMax != 0 && InvalidPositionCount >= AdvancedConfig.InvalidPositionsMax)
                {
                    InvalidPositionCount = 0;
                    Status(string.Format(Properties.Resources.status_InvalidPosWait, InvalidPositionCount));
                    StopGps();
                }
                else if (AfterStart || AdvancedConfig.InvalidPositionsMax == 0)
                {
                    if ((DateTime.Now - LastInvPosUserTime).TotalMilliseconds >= 900)
                    {
                        Status(string.Format("inv. pos. {0}", InvalidPositionCount));
                        LastInvPosUserTime = DateTime.Now;
                    }
                }
                else
                {
                    if ((DateTime.Now - LastInvPosUserTime).TotalMilliseconds >= 900)
                    {
                        Status(string.Format(Properties.Resources.status_InvalidPos, InvalidPositionCount));
                        LastInvPosUserTime = DateTime.Now;
                    }
                }
                //if ((DateTime.Now - LastInvPosTime).TotalMilliseconds < 150) System.Threading.Thread.Sleep(200);
                LastInvPosTime = DateTime.Now;
            }
        }

        void gps_GpsCommState(Gps.GpsProvider sender, Gps.GpsState state)
        {
            switch (state)
            {
                /*case States.AutoDiscovery:
                    if (LogEverything)  WriteToTextbox("AutoDiscovery");
                    Status("Auto discovery");
                    break;*/
                case GPSTracka.Gps.GpsState.Opening:
                    if (LogEverything) WriteToTextbox(Properties.Resources.Opening);
                    Status(Properties.Resources.status_Opening);
                    break;
                case GPSTracka.Gps.GpsState.Open:
                    if (LogEverything) WriteToTextbox(Properties.Resources.Running);
                    Status(Properties.Resources.status_Running);
                    break;
                case GPSTracka.Gps.GpsState.Closed:
                    if (LogEverything) WriteToTextbox(Properties.Resources.Stopped);
                    Status(Properties.Resources.Status_Stopped);
                    break;
                case GPSTracka.Gps.GpsState.Closing:
                    if (LogEverything) WriteToTextbox(Properties.Resources.Stopping);
                    Status(Properties.Resources.Status_Stopping);
                    break;
            }

        }
        void gps_Error(Gps.GpsProvider sender, string message, Exception exception)
        {
            if (LogEverything)
            {
                WriteToTextbox(message/* + ". The GPS data is: " + gps_data*/);
                if (exception != null)
                    writeExceptionToTextBox(exception);
            }
            Status(Properties.Resources.err_ErrorTitle + " " + (exception == null ? "" : exception.Message));
        }
        private void GPS_Movement(GPSTracka.Gps.GpsProvider sender, GPSTracka.Gps.GpsMovementEventArgs e)
        {
            if (statistic != null) statistic.ActualSpeed = e.Speed;
        }
        #region Satellites
        private void GPS_Satellite(GPSTracka.Gps.GpsProvider sender, GPSTracka.Gps.GpsSatelliteEventArgs e)
        {
            if (statistic == null && this.InvokeRequired) { this.BeginInvoke(new Gps.GpsSatelliteEventHandler(GPS_Satellite), sender, e); return; }
            int cnt = 0;
            foreach (var sat in e.Satellites)
                if (sat.SignalToNoiseRatio != 0) ++cnt;
            if (statistic != null)
                statistic.CurrentSats = cnt;
            else
                lblSatellites.Text = cnt.ToString();
        }
        /// <summary>Instance of window showing GPS satellites</summary>
        SatellitesView satellitesWindow = null;
        private void mniSatellites_Click(object sender, EventArgs e)
        {
            if (satellitesWindow == null)
            {
                satellitesWindow = new SatellitesView(GPS);
                satellitesWindow.Closed += new EventHandler(satellitesWindow_Closed);
            }
            satellitesWindow.Show();
        }
        private void satellitesWindow_Closed(object sender, EventArgs e)
        {
            satellitesWindow.Closed -= satellitesWindow_Closed;
            satellitesWindow = null;
        }
        #endregion
        #endregion

        #region Write to file
        /// <summary>Path of file data are currently written to</summary>
        /// <remarks>Whenever this is set to null <see cref="NMEAFileName"/> must be set to null as well.</remarks>
        private string currentFileName;
        /// <summary>Path of current NMEA log file</summary>
        private string NMEAFileName;
        private void WriteToFile(DateTime time, double latitude, double longitude, double altitude)
        {
            try
            {
                if (AdvancedConfig.LogToLogFile)
                {
                    switch (AdvancedConfig.LogFormat)
                    {
                        case LogFormat.KML: WriteToKMLFile(time, latitude, longitude, altitude); break;
                        case LogFormat.CSV: WriteToCSVFile(time, latitude, longitude, altitude); break;
                        default: WriteToGPXFile(time, latitude, longitude, altitude); break;
                    }
                }
            }
            catch (Exception ex)
            {
                WriteToTextbox(Properties.Resources.err_WriteToFile + " " + ex.Message);
                //writeExceptionToTextBox(ex);
            }
        }
        private const string KmlNs = "http://www.opengis.net/kml/2.2";

        private void WriteToKMLFile(DateTime time, double latitude, double longitude, double altitude)
        {
            XmlDocument doc = new XmlDocument();
            if (currentFileName == null)
                currentFileName = Path.Combine(AdvancedConfig.LogFileLocation, time.ToLocalTime().ToString("yyyyMMdd_HHmmss", System.Globalization.CultureInfo.InvariantCulture) + ".kml");

            if (!File.Exists(currentFileName))
            {

                if (!Directory.Exists(AdvancedConfig.LogFileLocation))
                {
                    try
                    {
                        Directory.CreateDirectory(AdvancedConfig.LogFileLocation);
                    }
                    catch
                    {
                        WriteToTextbox(Properties.Resources.err_CreateDir);
                    }

                }

                StreamWriter stream = File.CreateText(currentFileName);
                stream.WriteLine(@"<?xml version=""1.0"" encoding=""UTF-8""?>
                                <kml xmlns=""" + KmlNs + @""">
                                <Document>
                                    <name>" + System.IO.Path.GetFileNameWithoutExtension(currentFileName) + @"</name>
                                    <Folder id='Points'>
                                        <name>Points</name>
                                    </Folder>
                                    " + (AdvancedConfig.TrackType == TrackType.Track ?
                                    @"<Placemark id='track'>
                                        <name>" + System.IO.Path.GetFileNameWithoutExtension(currentFileName) + @"</name>
                                        <styleUrl>#yellowLineGreenPoly</styleUrl>
                                        <Style id='yellowLineGreenPoly'>
                                            <LineStyle>
                                                <color>" +
                                                    string.Format(System.Globalization.CultureInfo.InvariantCulture,
                                                        "{0:x2}{1:x2}{2:x2}{3:x2}",
                                                        AdvancedConfig.KMLLineColor.A, AdvancedConfig.KMLLineColor.B, AdvancedConfig.KMLLineColor.G, AdvancedConfig.KMLLineColor.R)
                                                + @"</color>
                                                <width>4</width>
                                            </LineStyle>
                                            <PolyStyle><color>7f00ff00</color></PolyStyle>
                                        </Style>
                                        <LineString>" +
                                                     (AdvancedConfig.LogAltitude ? @"<altitudeMode>absolute</altitudeMode>" : "")
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
            nsmgr.AddNamespace("def", KmlNs);

            //XmlNode kml = doc.ChildNodes[1].SelectSingleNode("Document");
            XmlNode kml = doc.SelectSingleNode("/def:kml/def:Document/def:Folder[@id='Points']", nsmgr);

            XmlNode placemark = doc.CreateElement("Placemark", KmlNs);

            XmlNode plName = doc.CreateElement("name", KmlNs);
            plName.InnerText = string.Format(AdvancedConfig.KMLNameFormat,
                time.ToUniversalTime(), longitude, latitude, altitude, time, AdvancedConfig.ElevationUnitName);
            placemark.AppendChild(plName);

            XmlNode plDescription = doc.CreateElement("description", KmlNs);
            plDescription.InnerText = string.Format(AdvancedConfig.KMLDescFormat,
                time.ToUniversalTime(), longitude, latitude, altitude, time, AdvancedConfig.ElevationUnitName);
            placemark.AppendChild(plDescription);

            XmlNode plPoint = doc.CreateElement("Point", KmlNs);
            XmlNode plPointCoord = doc.CreateElement("coordinates", KmlNs);
            plPointCoord.InnerText = longitude.ToString(System.Globalization.CultureInfo.InvariantCulture) + "," +
                                     latitude.ToString(System.Globalization.CultureInfo.InvariantCulture) + "," +
                                     (AdvancedConfig.LogAltitude ? altitude.ToString(System.Globalization.CultureInfo.InvariantCulture) : "0");
            if (AdvancedConfig.LogAltitude)
            {
                XmlNode coMode = doc.CreateElement("altitudeMode", KmlNs);
                coMode.InnerText = "absolute";
                plPoint.AppendChild(coMode);
            }
            plPoint.AppendChild(plPointCoord);

            placemark.AppendChild(plPoint);

            kml.AppendChild(placemark);
            if (AdvancedConfig.TrackType == TrackType.Track)
            {
                XmlNode coordinates = doc.SelectSingleNode("/def:kml/def:Document/def:Placemark[@id='track']/def:LineString/def:coordinates", nsmgr);
                coordinates.InnerText += string.Format(System.Globalization.CultureInfo.InvariantCulture,
                    "\r\n{0},{1},{2}", longitude, latitude, (AdvancedConfig.LogAltitude ? altitude.ToString(System.Globalization.CultureInfo.InvariantCulture) : "0"));
            }
            doc.Save(currentFileName);
        }

        private const string GpxNs = "http://www.topografix.com/GPX/1/0";

        private void WriteToGPXFile(DateTime time, double latitude, double longitude, double altitude)
        {
            XmlDocument doc = new XmlDocument();
            if (currentFileName == null)
                currentFileName = Path.Combine(AdvancedConfig.LogFileLocation, time.ToLocalTime().ToString("yyyyMMdd_HHmmss", System.Globalization.CultureInfo.InvariantCulture) + ".gpx");

            if (!File.Exists(currentFileName))
            {

                if (!Directory.Exists(AdvancedConfig.LogFileLocation))
                {
                    try
                    {
                        Directory.CreateDirectory(AdvancedConfig.LogFileLocation);
                    }
                    catch
                    {
                        WriteToTextbox(Properties.Resources.err_CreateDir);
                    }

                }

                StreamWriter stream = File.CreateText(currentFileName);
                stream.WriteLine(@"<?xml version=""1.0""?>");
                stream.WriteLine(@"<gpx
                             version=""1.0""
                             creator=""GPSTracka - http://www.mendhak.com/""
                             xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
                             xmlns=""" + GpxNs + @"""
                             xsi:schemaLocation=""http://www.topografix.com/GPX/1/0 http://www.topografix.com/GPX/1/0/gpx.xsd"">");
                stream.WriteLine("<time>" + time.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ", System.Globalization.CultureInfo.InvariantCulture) + "</time>");
                if (AdvancedConfig.TrackType == TrackType.Track)
                {
                    stream.WriteLine("<trk>");
                    stream.WriteLine("<trkseg/>");
                    stream.WriteLine("</trk>");
                }
                stream.WriteLine("</gpx>");
                stream.Close();
            }

            doc.Load(currentFileName);

            XmlNode parent;
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("def", GpxNs);
            if (AdvancedConfig.TrackType == TrackType.Track)
                parent = doc.SelectSingleNode("/def:gpx/def:trk/def:trkseg", nsmgr);
            else
                parent = doc.SelectSingleNode("/def:gpx", nsmgr);

            XmlNode wpt = doc.CreateElement(AdvancedConfig.TrackType == TrackType.Track ? "trkpt" : "wpt", GpxNs);

            XmlAttribute latAttrib = doc.CreateAttribute("lat");
            latAttrib.Value = latitude.ToString(System.Globalization.CultureInfo.InvariantCulture);
            wpt.Attributes.Append(latAttrib);

            XmlAttribute longAttrib = doc.CreateAttribute("lon");
            longAttrib.Value = longitude.ToString(System.Globalization.CultureInfo.InvariantCulture);
            wpt.Attributes.Append(longAttrib);

            if (AdvancedConfig.LogAltitude)
            {
                XmlNode altNode = doc.CreateElement("ele", GpxNs);
                altNode.InnerText = altitude.ToString(System.Globalization.CultureInfo.InvariantCulture);
                wpt.AppendChild(altNode);
            }

            XmlNode timeNode = doc.CreateElement("time", GpxNs); ;
            timeNode.InnerText = time.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ", System.Globalization.CultureInfo.InvariantCulture);

            wpt.AppendChild(timeNode);

            parent.AppendChild(wpt);

            doc.Save(currentFileName);
        }

        /// <summary>Writes data to CSV file</summary>
        /// <param name="time">Current time</param>
        /// <param name="latitude">Latitude</param>
        /// <param name="longitude">Longitude</param>
        /// <param name="altitude">Altitude</param>
        private void WriteToCSVFile(DateTime time, double latitude, double longitude, double altitude)
        {
            if (currentFileName == null)
                currentFileName = Path.Combine(AdvancedConfig.LogFileLocation, time.ToLocalTime().ToString("yyyyMMdd_HHmmss", System.Globalization.CultureInfo.InvariantCulture) + ".csv");
            if (!File.Exists(currentFileName))
            {
                if (!Directory.Exists(AdvancedConfig.LogFileLocation))
                {
                    try
                    {
                        Directory.CreateDirectory(AdvancedConfig.LogFileLocation);
                    }
                    catch
                    {
                        WriteToTextbox(Properties.Resources.err_CreateDir);
                    }
                }
                using (StreamWriter stream = File.CreateText(currentFileName))
                {
                    if (!string.IsNullOrEmpty(AdvancedConfig.CSVHeader)) stream.Write(AdvancedConfig.CSVHeader + AdvancedConfig.CSVNewLine);
                    stream.Flush();
                }
            }
            using (IDisposable stream = System.IO.File.Open(currentFileName, FileMode.Open, FileAccess.ReadWrite, FileShare.Read),
                               xxx = new StreamWriter((FileStream)stream))
            {
                StreamWriter stream2 = (StreamWriter)xxx;
                stream2.BaseStream.Seek(0, SeekOrigin.End);
                System.Text.StringBuilder fb = new StringBuilder();
                foreach (char ch in AdvancedConfig.CSVFields)
                {
                    if (fb.Length > 0) fb.Append(AdvancedConfig.CSVSeparator);
                    string fieldval;
                    switch ((int)char.GetNumericValue(ch))
                    {
                        case 0: fieldval = latitude.ToString(System.Globalization.CultureInfo.InvariantCulture); break;
                        case 1: fieldval = longitude.ToString(System.Globalization.CultureInfo.InvariantCulture); break;
                        case 2: fieldval = altitude.ToString(System.Globalization.CultureInfo.InvariantCulture); break;
                        case 3: fieldval = (AdvancedConfig.CSVUTC ? time.ToLocalTime() : time.ToUniversalTime()).ToString(AdvancedConfig.CSVDateFormat, System.Globalization.CultureInfo.InvariantCulture); break;
                        default: fieldval = ""; break;
                    }
                    if (AdvancedConfig.CSVQualifierUsage == CSVQualifierUsage.Always || (fieldval.IndexOf(AdvancedConfig.CSVSeparator.ToString()) > 0 && AdvancedConfig.CSVQualifierUsage == CSVQualifierUsage.AsNeeded))
                    {
                        fb.Append(AdvancedConfig.CSVTextQualifier);
                        fb.Append(fieldval.Replace(AdvancedConfig.CSVTextQualifier.ToString(), AdvancedConfig.CSVTextQualifier.ToString() + AdvancedConfig.CSVTextQualifier.ToString()));
                        fb.Append(AdvancedConfig.CSVTextQualifier);
                    }
                    else
                        fb.Append(fieldval);
                }
                stream2.Write(fb.ToString() + AdvancedConfig.CSVNewLine);
                stream2.Flush();
            }
        }
        #endregion
        #region Write to TextBox
        /// <summary>Indicates if verbose mode is enabled</summary>
        public static bool LogEverything = false;
        /// <summary>Delegate to update text box with text</summary>
        public delegate void UpdateTextboxDelegate(string theText);
        /// <summary>Delegate to update etxt vox with GPS info</summary>
        public delegate void UpdateFileDelegate(DateTime time, double latitude, double longitude, double altitude);
        private void writeExceptionToTextBox(Exception ex)
        {
            if (ex == null) throw new ArgumentNullException("ex");
            string errorMessage = String.Concat("\r\n\r\n",
                "****" + Properties.Resources.err_ERROR + "****", "\r\n",
                ex.Message, "\r\n",
                "*************", "\r\n",
                ex.StackTrace, "\r\n",
                "*************", "\r\n");

            if (ex.InnerException != null)
            {
                errorMessage = errorMessage + ex.InnerException.Message + "\r\n" + ex.InnerException.StackTrace;
            }

            WriteToTextbox(errorMessage);
        }
        private void WriteToTextbox(string theText)
        {
            if (this.TextBoxRawLog.InvokeRequired)
            {
                UpdateTextboxDelegate theDelegate = new UpdateTextboxDelegate(WriteToTextbox);
                this.BeginInvoke(theDelegate, new object[] { theText });
            }
            else
            {
                if (!string.IsNullOrEmpty(this.TextBoxRawLog.Text) && !this.TextBoxRawLog.Text.EndsWith("\r\n")) this.TextBoxRawLog.Text += "\r\n";
                if (LogEverything || AdvancedConfig.LogToTextBox)
                {
                    this.TextBoxRawLog.Text = this.TextBoxRawLog.Text + theText + "\r\n";

                    if (TextBoxRawLog.Text.Length > 1)
                    {
                        TextBoxRawLog.SelectionStart = TextBoxRawLog.Text.Length - 1;
                        TextBoxRawLog.SelectionLength = 0;
                        TextBoxRawLog.ScrollToCaret();
                    }
                    if (AdvancedConfig.MaxLogLength > 0)
                    {
                        int nlc = 0;
                        foreach (char ch in TextBoxRawLog.Text)
                        {
                            if (ch == '\n') nlc++;
                        }
                        if (nlc > AdvancedConfig.MaxLogLength) TextBoxRawLog.Text = "";
                    }
                }
            }
        }
        private void verboseMenuItem_Click(object sender, EventArgs e)
        {
            //currentFileName = null;
            verboseMenuItem.Checked = !verboseMenuItem.Checked;
            LogEverything = verboseMenuItem.Checked;
        }
        #endregion

        #region Control GPS running
        /// <summary>COntains value of the <see cref="GPS"/> property</summary>
        private Gps.GpsProvider gps_instance;
        /// <summary>Gets instance of GPS provider</summary>
        private Gps.GpsProvider GPS
        {
            get
            {
                if (gps_instance == null)
                {
                    if (AdvancedConfig.UseWindowsDriver)
                        gps_instance = new Gps.MSGpsWrapper();
                    else
                        gps_instance = new Gps.OpenNetGpsWrapper();
                    HookGps(true);
                }
                return gps_instance;
            }
        }
        /// <summary>True when we are currently logging (or waiting); false otherwise</summary>
        bool GPSRunning = false;
        bool positionLogged = true;
        string previousSentence = String.Empty;
        //See description of AdvancedConfig.MinimalDistance
        /// <summary>Position where minimal distance rule started to be violated</summary>
        Gps.GpsPosition? LastPositionA;
        /// <summary>Time associated with <see cref="LastPositionA"/></summary>
        DateTime LastTimeA;
        /// <summary>Last position not logged due to vilolation of minimal distance rule</summary>
        Gps.GpsPosition? LastPositionC;
        /// <summary>Time associated with <see cref="LastPositionC"/>.</summary>
        DateTime LastTimeC;

        private Dictionary<string, IntPtr> PowerHandles = new Dictionary<string, IntPtr>();

        private void startMenuItem_Click(object sender, EventArgs e)
        {
            LastGpsLoggedAt = DateTime.MinValue;
            try
            {
                if (!GPSRunning)
                {//Start
                    if (currentFileName == null || MessageBox.Show(string.Format(Properties.Resources.ContinueInCurrentFile, currentFileName), "GPSTracka", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) != DialogResult.Yes)
                    {
                        currentFileName = null;
                        NMEAFileName = null;
                        statistic = null;
                        tickCount = 0;
                    }
                    if (currentFileName != null) Continuing = true;
                    InvalidPositionCount = 0;
                    GPSRunning = true;
                    timer1.Interval = Convert.ToInt16(AdvancedConfig.PollingInterval) * 1000;
                    AfterStart = true;
                    //Power requirements
                    if (!AdvancedConfig.UseWindowsDriver) powerHandle = SetPowerRequirement(AdvancedConfig.COMPort + ":", (int)DevicePowerState.D0, 1, IntPtr.Zero, 0);
                    foreach (string device in AdvancedConfig.KeepAwakeList)
                    {
                        if (!device.EndsWith(":"))
                        {
                            IntPtr devicePowerHandle = SetPowerRequirement(device + ":", DevicePowerState.D0, 1, IntPtr.Zero, 0);
                            if (PowerHandles.ContainsKey(device))
                                PowerHandles[device] = devicePowerHandle;
                            else
                                PowerHandles.Add(device, devicePowerHandle);
                        }
                    }
                    //if (!PowerPolicyNotify(PPN_UNATTENDEDMODE, 1)) {
                    //    WriteToTextbox(Properties.Resources.err_PowerPolicyNotifiyFailed);
                    //}
                    if (!AdvancedConfig.StartImmediatelly)
                        WriteToTextbox(string.Format(Properties.Resources.WillBeginReading, AdvancedConfig.PollingInterval));
                    Status(Properties.Resources.Starting);
                    startMenuItem.Text = Properties.Resources.Stop;
                    if (!AdvancedConfig.StartImmediatelly)
                    {
                        timer1.Enabled = true;
                        tmrCountDown.Enabled = AdvancedConfig.StatusBar;
                        CountDown = timer1.Interval / tmrCountDown.Interval;
                    }
                    tmrBeep.Enabled = AdvancedConfig.BeepTimer != 0;
                    tmrBeep.Interval = AdvancedConfig.BeepTimer * 1000;
                    if (statistic == null)
                        GpsStatistics.ClearAll(this);
                    else
                        statistic.ShowValues(GpsStatistics.ValueKind.All);
                }
                else
                {//Stop
                    startMenuItem.Enabled = false;
                    startMenuItem.Text = Properties.Resources.PleaseWait;
                    try
                    {
                        GpsStopAndWait();
                    }
                    finally
                    {
                        GPSRunning = false;
                        startMenuItem.Enabled = true;
                        stsStatus[1] = "";
                        stsStatus[2] = "";
                        Status(Properties.Resources.Status_Stopped_simple);
                        //Release power requirements
                        if (powerHandle != IntPtr.Zero) ReleasePowerRequirement(powerHandle);
                        powerHandle = IntPtr.Zero;
                        //PowerPolicyNotify(PPN_UNATTENDEDMODE, 0);
                        foreach (string device in AdvancedConfig.KeepAwakeList)
                        {
                            if (!device.EndsWith(":") && PowerHandles.ContainsKey(device) && PowerHandles[device] != IntPtr.Zero)
                            {
                                ReleasePowerRequirement(PowerHandles[device]);
                                PowerHandles[device] = IntPtr.Zero;
                            }
                        }

                        startMenuItem.Text = Properties.Resources.Start;
                        timer1.Enabled = false;
                        tmrCountDown.Enabled = false;
                    }
                }
            }
            finally
            {
                settingsMenuItem.Enabled = !GPSRunning;
                if (!GPSRunning)
                {
                    LastPositionA = null;
                    LastPositionC = null;
                    tmrBeep.Enabled = false;
                }
            }
            if (GPSRunning && AdvancedConfig.StartImmediatelly) timer1_Tick(timer1, e);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            positionLogged = false;
            stsStatus[1] = null;
            StartupGps();
            timer1.Enabled = false;
            CountDown = timer1.Interval / tmrCountDown.Interval;
            tmrCountDown.Enabled = false;
        }

        private delegate void StopGpsExtractedDelegate();

        /// <summary>Stops GPS and sets timer to count down for next GPS run</summary>
        private void StopGps()
        {
            if (this.InvokeRequired)
            {
                StopGpsExtractedDelegate theDelegate = new StopGpsExtractedDelegate(StopGps_Extracted);
                this.BeginInvoke(theDelegate);
                ////Note: With MS driver managed wrapper we shall NOT stop device from within GPS event handler - it causes unpredicable behavior (deadlocks, device no longer receiving events after start ...)
                //this.BeginInvoke(new Action(StopGps_Extracted));
            }
            else
            {
                try
                {
                    GpsStopAndWait();
                }
                finally
                {
                    StopGps_Extracted();
                }
            }
        }
        /// <summary>Extracted part of <see cref="StopGps"/> - sets timer</summary>
        private void StopGps_Extracted()
        {
            foreach (string device in AdvancedConfig.KeepAwakeList)
            {
                if (device.EndsWith(":"))
                {
                    if (PowerHandles.ContainsKey(device) && PowerHandles[device] != IntPtr.Zero)
                    {
                        ReleasePowerRequirement(PowerHandles[device]);
                        PowerHandles[device] = IntPtr.Zero;
                    }
                }
            }
            SetTimerEnabled(timer1, true);
            CountDown = timer1.Interval / tmrCountDown.Interval;
            SetTimerEnabled(tmrCountDown, AdvancedConfig.StatusBar);
        }
        /// <summary>Delegate to the <see cref="SetTimerEnabled"/> method</summary>
        /// <param name="timer"><see cref="Timer"/> to set <see cref="Timer.Enabled">Enabled</see> of</param>
        /// <param name="enabled">New value of the <see cref="Timer.Enabled"/> property</param>
        /// <exception cref="ArgumentNullException"><paramref name="timer"/> is null</exception>
        private delegate void dSetTimerEnabled(Timer timer, bool enabled);
        /// <summary>Sets value of the <see cref="Timer.Enabled"/> property of given timer in thread safe way</summary>
        /// <param name="timer"><see cref="Timer"/> to set <see cref="Timer.Enabled">Enabled</see> of</param>
        /// <param name="enabled">New value of the <see cref="Timer.Enabled"/> property</param>
        /// <exception cref="ArgumentNullException"><paramref name="timer"/> is null</exception>
        private void SetTimerEnabled(Timer timer, bool enabled)
        {
            if (timer == null) throw new ArgumentNullException("timer");
            if (this.InvokeRequired)
            {//Required as timer started in bad thread may not work
                this.BeginInvoke(new dSetTimerEnabled(SetTimerEnabled), timer, enabled);
            }
            else
            {
                timer.Enabled = enabled;
            }
        }
        /// <summary>When true, <see cref="GpsStopAndWait"/> is curently being called</summary>
        private bool WaitForStop = false;
        /// <summary>Stops GPS and waits for it to stop</summary>
        private void GpsStopAndWait()
        {
            try
            {
                while (GPS.State == GPSTracka.Gps.GpsState.Opening)
                {
                    System.Threading.Thread.Sleep(100);
                    Application.DoEvents();
                }
                GPS.Stop();
                WaitForStop = true;
                while (GPS.State != GPSTracka.Gps.GpsState.Closed)
                {
                    System.Threading.Thread.Sleep(100);
                    Application.DoEvents();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.Message);
                throw;
            }
            finally
            {
                WaitForStop = false;
            }
        }

        private delegate void StartupGpsDelegate();

        private void StartupGps()
        {
            if (this.InvokeRequired)
            {
                StartupGpsDelegate theDelegate = new StartupGpsDelegate(StartupGps);
                this.BeginInvoke(theDelegate);

                //this.BeginInvoke(new Action(StartupGps));
                return;
            }
            if (GPS is Gps.OpenNetGpsWrapper)
            {
                string comPort = AdvancedConfig.COMPort;
                int baudRate = (int)AdvancedConfig.BaudRate;
                if (String.IsNullOrEmpty(comPort)) comPort = "COM1";
                ((Gps.OpenNetGpsWrapper)GPS).Port = comPort;
                ((Gps.OpenNetGpsWrapper)GPS).BaudRate = (BaudRates)baudRate;
            }
            foreach (string device in AdvancedConfig.KeepAwakeList)
            {
                if (device.EndsWith(":"))
                {
                    IntPtr devicePowerHandle = SetPowerRequirement(device, DevicePowerState.D0, 1, IntPtr.Zero, 0);
                    if (PowerHandles.ContainsKey(device))
                        PowerHandles[device] = devicePowerHandle;
                    else
                        PowerHandles.Add(device, devicePowerHandle);
                }
            }
            GPS.Start();
        }
        #endregion

        #region Helpers
        private void GPSTracka_Closing(object sender, CancelEventArgs e)
        {
            try
            {
                try
                {
                    GpsStopAndWait();
                }
                finally
                {
                    if (powerHandle != IntPtr.Zero) ReleasePowerRequirement(powerHandle);
                    powerHandle = IntPtr.Zero;
                    //if (GPSRunning) PowerPolicyNotify(PPN_UNATTENDEDMODE, 0);
                }
            }
            catch
            {
                //Nothing
            }
        }

        private static void KeepDeviceAwake(object o)
        {
            try
            {
                SystemIdleTimerReset();
            }
            catch (Exception)
            {
                // Nothing
            }
        }

        private void clearMenuItem_Click(object sender, EventArgs e)
        {
            TextBoxRawLog.Text = string.Empty;
        }
        int tickCount = 0;
        private void tmrBeep_Tick(object sender, EventArgs e)
        {
            BeepWrapper.Beep(BeepWrapper.BeepAlert.Exclamation);
            //System.Media.SystemSounds.Beep.Play();
            Status((++tickCount).ToString(), 3);
        }
        private void menuExit_Click(object sender, EventArgs e)
        {
            if (GPSRunning)
            {
                if (MessageBox.Show(Properties.Resources.ConfirmExit + (AdvancedConfig.LogToLogFile ? Properties.Resources.TrackSaved : ""), "GPSTRacka", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                    return;
                startMenuItem_Click(sender, e);
            }
            this.Close();
        }

        private void mniHelp_Click(object sender, EventArgs e)
        {
            string filename = null;
            string HelpDir = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(typeof(TrackerForm).Assembly.GetModules()[0].FullyQualifiedName), "Help");
            var CurrentUICulture = System.Globalization.CultureInfo.CurrentUICulture;
            while (CurrentUICulture != null && !string.IsNullOrEmpty(CurrentUICulture.Name))
            {
                if (System.IO.File.Exists(filename = System.IO.Path.Combine(HelpDir, "GPSTracka." + CurrentUICulture.Name + ".htm")))
                    break;
                else filename = null;
                CurrentUICulture = CurrentUICulture.Parent;
            }
            if (filename == null) filename = System.IO.Path.Combine(HelpDir, "GPSTracka.htm");
            try
            {
                System.Diagnostics.Process.Start(filename, null);
            }
            catch (Exception)
            {
                try
                {
                    System.Diagnostics.Process.Start(filename = System.IO.Path.Combine(HelpDir, "GPSTracka.htm"), null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format(Properties.Resources.err_HelpNotAvailable, filename, ex.Message), "GPSTracka", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                }
            }
        }
        #endregion

        #region About
        private void aboutMenuItem_Click(object sender, EventArgs e)
        {
            HidePanel(mainPanel);
            HidePanel(settingsPanel);
            ShowPanel(aboutPanel);
        }
        private void cancelMenuItem_Click(object sender, EventArgs e)
        {
            HidePanel(settingsPanel);
            HidePanel(aboutPanel);
            ShowPanel(mainPanel);
        }

        private void linkLabel1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(linkLabel1.Text, "");
        }
        #endregion

        #region Settings
        private void settingsMenuItem_Click(object sender, EventArgs e)
        {
            currentFileName = null;
            NMEAFileName = null;
            HidePanel(mainPanel);
            HidePanel(aboutPanel);
            ShowPanel(settingsPanel);
            try
            {
                using (var key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"System\CurrentControlSet\GPS Intermediate Driver\Multiplexer"))
                {
                    lblRecommendedPort.Text = Properties.Resources.Recommended + " " + (string)key.GetValue("DriverInterface");
                }
            }
            catch
            {
                lblRecommendedPort.Text = "";
            }
        }
        private void saveMenuItem_Click(object sender, EventArgs e)
        {

            if (CheckBoxToFile.Checked && String.IsNullOrEmpty(logLocationTextBox.Text))
            {
                MessageBox.Show(Properties.Resources.err_BadPath);
                return;
            }
            if (satellitesWindow != null) satellitesWindow.Close();//To prevent it from using different GPS provider
            HookGps(false);
            gps_instance = null;

            AdvancedConfig.COMPort = ComboBoxCOMPorts.Text;
            AdvancedConfig.BaudRate = (OpenNETCF.IO.Serial.BaudRates)int.Parse(ComboBaudRate.Text);
            AdvancedConfig.PollingInterval = (int)NumericUpDownInterval.Value;
            AdvancedConfig.LogToTextBox = CheckBoxToTextBox.Checked;
            AdvancedConfig.LogToLogFile = CheckBoxToFile.Checked;
            AdvancedConfig.LogFileLocation = logLocationTextBox.Text;
            AdvancedConfig.LogAltitude = chkAltitude.Checked;
            AdvancedConfig.UseWindowsDriver = chkUseWindowsDriver.Checked;

            if (radioButtonKML.Checked) AdvancedConfig.LogFormat = LogFormat.KML;
            else if (optCSV.Checked) AdvancedConfig.LogFormat = LogFormat.CSV;
            else AdvancedConfig.LogFormat = LogFormat.GPX;
            if (optTrack.Checked) AdvancedConfig.TrackType = TrackType.Track;
            else AdvancedConfig.TrackType = TrackType.Points;

            AdvancedConfig.Store();
            ConfigurationManager.Save();

            HidePanel(settingsPanel);
            HidePanel(aboutPanel);
            ShowPanel(mainPanel);
        }
        private void saveDialogButton_Click(object sender, EventArgs e)
        {
            FolderBrowseDialog fbd = new FolderBrowseDialog();
            try
            {
                fbd.SelectedPath = logLocationTextBox.Text;
            }
            catch { }
            DialogResult result = fbd.ShowDialog();
            if (result == DialogResult.OK)
            {
                logLocationTextBox.Text = fbd.SelectedPath;
                //Parse and get the folder path
                //logLocationTextBox.Text = fi.DirectoryName; 
            } /*else {
                logLocationTextBox.Text = string.Empty;
            }*/
        }
        private void backMenuItem_Click(object sender, EventArgs e)
        {

            ShowSettings();

            HidePanel(settingsPanel);
            HidePanel(aboutPanel);
            ShowPanel(mainPanel);
        }

        private void mniAdvanced_Click(object sender, EventArgs e)
        {
            var frm = new AdvancedConfigForm();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                stsStatus.Visible = AdvancedConfig.StatusBar;
                panInfoPane.Visible = AdvancedConfig.InfoPane;
            }
        }
        private void chkUseWindowsDriver_CheckStateChanged(object sender, EventArgs e)
        {
            label4.Enabled = ComboBoxCOMPorts.Enabled = lblRecommendedPort.Enabled = ComboBaudRate.Enabled = label5.Enabled = !chkUseWindowsDriver.Checked;
        }
        #endregion

        #region Windows UI
        /// <summary>Initiali size of infopane label (to measure)</summary>
        Size initMeasure;
        private void panInfoPane_Resize(object sender, EventArgs e)
        {
            if (panInfoPane.Visible)
            {
                int LineSize = (panInfoPane.ClientSize.Width >= 8 * initMeasure.Width * 0.8) ? 8 : 4;
                int x = panInfoPane.ClientRectangle.Left, y = panInfoPane.ClientRectangle.Top;
                int i = 0;
                foreach (Control ctl in panInfoPane.Controls)
                {
                    ctl.Width = panInfoPane.ClientSize.Width / LineSize;
                    ctl.Left = x; ctl.Top = y;
                    if (++i % LineSize == 0)
                    {
                        x = panInfoPane.ClientRectangle.Left;
                        y += initMeasure.Height;
                    }
                    else
                    {
                        x += panInfoPane.ClientRectangle.Width / LineSize;
                    }
                    panInfoPane.Height = (int)Math.Ceiling((float)panInfoPane.Controls.Count / LineSize) * initMeasure.Height;
                }
            }
        }
        private void ShowPanel(Panel panel)
        {
            this.SuspendLayout();
            try
            {
                panel.Location = new Point(0, 0);
                panel.Dock = DockStyle.Fill;
                panel.Visible = true;
                switch (panel.Name)
                {
                    case "settingsPanel":
                        {
                            if (this.Menu != settingsPanelMenu) this.Menu = settingsPanelMenu;
                            break;
                        }
                    case "aboutPanel":
                        {
                            if (this.Menu != aboutPanelMenu) this.Menu = aboutPanelMenu;
                            break;
                        }
                    case "mainPanel":
                        {
                            if (this.Menu != mainPanelMenu) this.Menu = mainPanelMenu;
                            break;
                        }
                    default:
                        {
                            if (this.Menu != mainPanelMenu) this.Menu = mainPanelMenu;
                            break;
                        }
                }
            }
            finally { this.ResumeLayout(); }
        }

        private void HidePanel(Panel panel)
        {
            panel.Dock = DockStyle.None;
            panel.Visible = false;
        }
        #endregion

        /// <summary>Statistic</summary>
        private GpsStatistics statistic;
        /// <summary>Last known elevation</summary>
        private decimal? stat_lastEle;
        /// <summary>Last time of logged/not logged point</summary>
        private DateTime stat_lastLogTime;
        /// <summary>User pressed stop and start and chosedto continue</summary>
        private bool Continuing;
        /// <summary>Called when point is nearly logged</summary>
        /// <param name="time">Logged time</param>
        /// <param name="gpsPoint">Actual position</param>
        /// <param name="LogThisPoint">true when point was logged false when it was not</param>
        private void OnPoint(DateTime time, GpsPoint gpsPoint, bool LogThisPoint)
        {
            if (Continuing && statistic != null) statistic.PauseTime += time - stat_lastLogTime;//User pause
            if (statistic == null)
            {//Initialize
                statistic = new GpsStatistics(this, time, gpsPoint);
                stat_lastEle = gpsPoint.Altitude;
                stat_lastLogTime = time;
                return;
            }
            statistic.CurentTime = time;
            if (LogThisPoint)
            {//This point is logged, count it
                var delta = gpsPoint - statistic.CurrentPos;//km
                statistic.SumLength += delta;
                statistic.CurrentPos = gpsPoint;
                statistic.PointsTotal++;
            }
            else
            {//This point is not logged, pause
                statistic.PauseTime += time - stat_lastLogTime;
            }
            stat_lastLogTime = time;
            if (stat_lastEle == null)
            {//Initialize altitude if necessary
                stat_lastEle = gpsPoint.Altitude;
                return;
            }
            if (LogThisPoint && gpsPoint.Altitude.HasValue)
            {//Elevation
                var delta = gpsPoint.Altitude.Value - stat_lastEle.Value;
                if (delta < 0) statistic.SumEleMinus -= delta; else statistic.SumElePlus += delta;
                stat_lastEle = gpsPoint.Altitude;
            }
        }
        /// <summary>Holds GPS statistic data</summary>
        private class GpsStatistics
        {
            public GpsStatistics(TrackerForm form, DateTime startTime, GpsPoint startPosition)
            {
                if (form == null) throw new ArgumentNullException("form");
                _StartTime = CurentTime = startTime;
                _StartPos = CurrentPos = startPosition;
                this.form = form;
                ShowValues(ValueKind.All);
            }
            /// <summary>Owning form</summary>
            private TrackerForm form;
            private decimal _ActualSpeed;
            /// <summary>Actual speed in km/h</summary>
            public decimal ActualSpeed
            {
                get { return _ActualSpeed; }
                set
                {
                    _ActualSpeed = value;
                    ShowValues(ValueKind.ActualSpeed);
                }
            }
            private DateTime _StartTime;
            /// <summary>Time when logging started</summary>
            public DateTime StartTime { get { return _StartTime; } }
            private DateTime _CurentTime;
            /// <summary>Current time</summary>
            public DateTime CurentTime
            {
                get { return _CurentTime; }
                set
                {
                    _CurentTime = value;
                    ShowValues(ValueKind.Avg | ValueKind.Time);
                }
            }
            private TimeSpan _PauseTime;
            /// <summary>Total pause time</summary>
            public TimeSpan PauseTime
            {
                get { return _PauseTime; }
                set
                {
                    _PauseTime = value;
                    ShowValues(ValueKind.StopTime);
                }
            }
            private GpsPoint _StartPos;
            /// <summary>Start position</summary>
            public GpsPoint StartPos { get { return _StartPos; } }
            private GpsPoint _CurrentPos;
            /// <summary>Current position</summary>
            public GpsPoint CurrentPos
            {
                get { return _CurrentPos; }
                set
                {
                    _CurrentPos = value;
                    ShowValues(ValueKind.Aerial);
                }
            }
            private decimal _SumLength;
            /// <summary>Total track length in km</summary>
            public decimal SumLength
            {
                get { return _SumLength; }
                set
                {
                    _SumLength = value;
                    ShowValues(ValueKind.Avg | ValueKind.Distance);
                }
            }
            private decimal _SumElePlus;
            /// <summary>Positive elevation</summary>
            public decimal SumElePlus
            {
                get { return _SumElePlus; }
                set
                {
                    _SumElePlus = value;
                    ShowValues(ValueKind.ElePlus);
                }
            }
            private decimal _SumEleMinus;
            /// <summary>Negative elevation</summary>
            public decimal SumEleMinus
            {
                get { return _SumEleMinus; }
                set
                {
                    _SumEleMinus = value;
                    ShowValues(ValueKind.EleMinus);
                }
            }
            private int _PointsTotal;
            /// <summary>Total points count</summary>
            public int PointsTotal
            {
                get { return _PointsTotal; }
                set
                {
                    _PointsTotal = value;
                    ShowValues(ValueKind.Points);
                }
            }
            private int _CurrentSats;
            /// <summary>Current number of satellites in view</summary>
            public int CurrentSats
            {
                get { return _CurrentSats; }
                set
                {
                    _CurrentSats = value;
                    ShowValues(ValueKind.Sats);
                }
            }
            /// <summary>Average speed in km/h</summary>
            public decimal AverageSpeed
            {
                get
                {
                    return (CurentTime - StartTime - PauseTime == TimeSpan.Zero) ? 0 : SumLength / (decimal)(CurentTime - StartTime - PauseTime).TotalHours;
                }
            }
            /// <summary>Gets aerial distance of start and current point in km.</summary>
            public decimal AerialDistance { get { return CurrentPos - StartPos; } }
            /// <summary>Gets time of moving (excluding pauses)</summary>
            public TimeSpan Time { get { return CurentTime - StartTime - PauseTime; } }
            /// <summary>Shows values to the user</summary>
            /// <param name="vk">Which values to show</param>
            public void ShowValues(ValueKind vk)
            {
                if (!AdvancedConfig.InfoPane) return;
                if (form == null) return;
                if (form.InvokeRequired) { form.BeginInvoke(new Action<ValueKind>(ShowValues), vk); return; }
                if (!form.panInfoPane.Visible) return;
                if ((vk & ValueKind.ActualSpeed) != 0)
                    form.lblSpeed.Text = (ActualSpeed * AdvancedConfig.SpeedMultiplier).ToString("0.0") + AdvancedConfig.SpeedUnitName;
                if ((vk & ValueKind.Avg) != 0)
                    form.lblAverage.Text = (AverageSpeed * AdvancedConfig.SpeedMultiplier).ToString("0.0") + AdvancedConfig.SpeedUnitName;
                if ((vk & ValueKind.Distance) != 0)
                    form.lblDistance.Text = (SumLength * AdvancedConfig.DistanceMultiplier).ToString("0.000") + AdvancedConfig.DistanceUnitName;
                if ((vk & ValueKind.Aerial) != 0)
                    form.lblAerial.Text = (AerialDistance * AdvancedConfig.DistanceMultiplier).ToString("0.000") + AdvancedConfig.DistanceUnitName;
                if ((vk & ValueKind.ElePlus) != 0)
                    form.lblElevation.Text = (SumElePlus * AdvancedConfig.ElevationMultiplier).ToString("0") + AdvancedConfig.ElevationUnitName;
                if ((vk & ValueKind.EleMinus) != 0)
                    form.lblElevationMinus.Text = (SumEleMinus * AdvancedConfig.ElevationMultiplier).ToString("0") + AdvancedConfig.ElevationUnitName;
                if ((vk & ValueKind.Time) != 0)
                    form.lblTime.Text = string.Format("{1:0}{0}{2:00}{0}{3:00}", System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.TimeSeparator, Math.Floor(Time.TotalHours), Math.Abs(Time.Minutes), Math.Abs(Time.Seconds));
                if ((vk & ValueKind.StopTime) != 0)
                    form.lblStopTime.Text = string.Format("{1:0}{0}{2:00}{0}{3:00}", System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.TimeSeparator, Math.Floor(PauseTime.TotalHours), Math.Abs(PauseTime.Minutes), Math.Abs(PauseTime.Seconds));
                if ((vk & ValueKind.Points) != 0)
                    form.lblPoints.Text = PointsTotal.ToString();
                if ((vk & ValueKind.Sats) != 0)
                    form.lblSatellites.Text = CurrentSats.ToString();
            }
            /// <summary>Shows all-zero values</summary>
            /// <param name="form">Form holding the data</param>
            public static void ClearAll(TrackerForm form)
            {
                if (!AdvancedConfig.InfoPane) return;
                GpsStatistics st = new GpsStatistics(form, DateTime.Now, new GpsPoint(0, 0, 0));
                st.ShowValues(ValueKind.All);
            }
            /// <summary>Identifies values shown on form</summary>
            public enum ValueKind
            {
                ActualSpeed = 1,
                Avg = 2,
                SumLenght = 4,
                Distance = 8,
                Aerial = 16,
                ElePlus = 32,
                EleMinus = 64,
                Time = 128,
                StopTime = 256,
                Points = 512,
                Sats = 1024,
                All = 2047
            }
        }

        //private void TrackerForm_Resize(object sender, EventArgs e)
        //{
        //    //This is to prevent StatusBars from being covered by main menu (not a 100% successfull)
        //    if (this.mainPanel.Dock == DockStyle.Fill && active /*&& !this.IsDisposed*/)
        //    {
        //        this.SuspendLayout();
        //        this.ResumeLayout(true);
        //        var th = new System.Threading.Thread(new System.Threading.ThreadStart(() =>
        //                                                                                  {
        //                                                                                      System.Threading.Thread.
        //                                                                                          Sleep(1000);

        //                                                                                      if (
        //                                                                                          //this.IsDisposed) return;
        //                                                                                          this.Invoke(
        //                                                                                              new Action<string>
        //                                                                                                  ((s) =>
        //                                                                                                       {
        //                                                                                                           //if (this.IsDisposed) return;
        //                                                                                                           this.
        //                                                                                                               SuspendLayout
        //                                                                                                               ();
        //                                                                                                           this.
        //                                                                                                               ResumeLayout
        //                                                                                                               (true);
        //                                                                                                       }
        //                                                                                      ;))) 
        //                                                                                  };))
        //    }




        //    //}));
        //        th.Start();
        //    }
        //}




        /// <summary>Kepps rack if form is active or not</summary>
        private bool active = false;
        private void TrackerForm_Activated(object sender, EventArgs e)
        {
            active = true;
        }

        private void TrackerForm_Deactivate(object sender, EventArgs e)
        {
            active = false;
        }
    }
}
/// <summary>GPS position</summary>
internal struct GpsPoint
{
    /// <summary>Latitude +North/-South</summary>
    public decimal Latitude { get; set; }
    /// <summary>Longitude -West/+East</summary>
    public decimal Longitude { get; set; }
    /// <summary>Altitude (above sea level or geoid)</summary>
    public decimal? Altitude { get; set; }
    /// <summary>CTor</summary>
    /// <param name="latitude">Latitude +North/-South</param>
    /// <param name="longitude">Logitude (-West/+East)</param>
    /// <param name="altitude">Altitude (above sea level or geoid)</param>
    public GpsPoint(decimal latitude, decimal longitude, decimal? altitude)
        : this()
    {
        Latitude = latitude;
        Longitude = longitude;
        Altitude = altitude;
    }
    /// <summary>Calculates distance of two positions in km</summary>
    /// <param name="pos1">A position</param>
    /// <param name="pos2">A position</param>
    /// <returns>Distance between given points in km.</returns>
    public static decimal CalculateDistance(GpsPoint pos1, GpsPoint pos2)
    {
        /*double num = ((double)pos2.Latitude - (double)pos1.Latitude) * (Math.PI / 180.0);
        double num2 = ((double)pos2.Longitude - (double)pos1.Longitude) * (Math.PI * 180.0);
        double a = (Math.Sin(num / 2.0) * Math.Sin(num / 2.0)) + (((Math.Cos((double)pos1.Latitude * (Math.PI / 180.0)) * Math.Cos((double)pos2.Latitude * (Math.PI * 180.0))) * Math.Sin(num2 / 2.0)) * Math.Sin(num2 / 2.0));
        if (a < 0) return 0;
        double num4 = 2.0 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1.0 - a));
        return (decimal)(6371 * num4);*/
        return (decimal)Calc((double)pos1.Latitude, (double)pos1.Longitude, (double)pos2.Latitude, (double)pos2.Longitude);
    }

    private static double Calc(double Lat1, double Long1, double Lat2, double Long2)
    {
        /*
            The Haversine formula according to Dr. Math.
            http://mathforum.org/library/drmath/view/51879.html
                
            dlon = lon2 - lon1
            dlat = lat2 - lat1
            a = (sin(dlat/2))^2 + cos(lat1) * cos(lat2) * (sin(dlon/2))^2
            c = 2 * atan2(sqrt(a), sqrt(1-a)) 
            d = R * c
                
            Where
                * dlon is the change in longitude
                * dlat is the change in latitude
                * c is the great circle distance in Radians.
                * R is the radius of a spherical Earth.
                * The locations of the two points in spherical coordinates (longitude and latitude) are lon1,lat1 and lon2, lat2.
        */

        double dDistance = Double.MinValue;
        double dLat1InRad = Lat1 * (Math.PI / 180.0);
        double dLong1InRad = Long1 * (Math.PI / 180.0);
        double dLat2InRad = Lat2 * (Math.PI / 180.0);
        double dLong2InRad = Long2 * (Math.PI / 180.0);

        double dLongitude = dLong2InRad - dLong1InRad;
        double dLatitude = dLat2InRad - dLat1InRad;

        // Intermediate result a.
        double a = Math.Pow(Math.Sin(dLatitude / 2.0), 2.0) +
                   Math.Cos(dLat1InRad) * Math.Cos(dLat2InRad) *
                   Math.Pow(Math.Sin(dLongitude / 2.0), 2.0);

        // Intermediate result c (great circle distance in Radians).
        double c = 2.0 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1.0 - a));

        // Distance.
        //const Double kEarthRadiusMiles = 3956.0;
        const Double kEarthRadiusKms = 6376.5;
        dDistance = kEarthRadiusKms * c;

        return dDistance;
    }


    ////-----------------    computes distance using direction    ---------------------
    //public double Calc(string NS1, double Lat1, double Lat1Min, string EW1, double Long1, double Long1Min,
    //    string NS2, double Lat2, double Lat2Min, string EW2, double Long2, double Long2Min) {
    //    //  usage: ComputeDistance.Calc(" N 43 35.500 W 80 27.800 N 43 35.925 W 80 28.318");

    //    double NS1Sign = NS1.ToUpper() == "N" ? 1.0 : -1.0;
    //    double EW1Sign = NS1.ToUpper() == "E" ? 1.0 : -1.0;
    //    double NS2Sign = NS2.ToUpper() == "N" ? 1.0 : -1.0;
    //    double EW2Sign = EW2.ToUpper() == "E" ? 1.0 : -1.0;

    //    return (Calc((Lat1 + (Lat1Min / 60)) * NS1Sign,
    //        (Long1 + (Long1Min / 60)) * EW1Sign,
    //        (Lat2 + (Lat2Min / 60)) * NS2Sign,
    //        (Long2 + (Long2Min / 60)) * EW2Sign
    //        ));
    //}

    /// <summary>Calculates distance of two positions in km</summary>
    /// <param name="pos1">A position</param>
    /// <param name="pos2">A position</param>
    /// <returns>Distance between given points in km.</returns>
    public static decimal operator -(GpsPoint pos1, GpsPoint pos2)
    {
        return Math.Abs(CalculateDistance(pos2, pos1));
    }
    /// <summary>Gets value indicating if this instance equals to given object</summary>
    /// <param name="obj">Object to compare</param>
    /// <returns>True if <paramref name="obj"/> is <see cref="GpsPoint"/> and its <see cref="Latitude"/>, <see cref="Longitude"/> and <see cref="Altitude"/> equals to current object</returns>
    public override bool Equals(object obj)
    {
        if (obj is GpsPoint) return ((GpsPoint)obj).Altitude == this.Altitude && ((GpsPoint)obj).Latitude == this.Latitude && ((GpsPoint)obj).Longitude == this.Longitude;
        return base.Equals(obj);
    }
    /// <summary>Compares two <see cref="GpsPoint">GpsPoints</see> for equality</summary>
    /// <param name="pos1">A position</param>
    /// <param name="pos2">A position</param>
    /// <returns>True if given points represent same location; false if not</returns>
    public static bool operator ==(GpsPoint pos1, GpsPoint pos2) { return pos2.Equals(pos1); }
    /// <summary>Compares two <see cref="GpsPoint">GpsPoints</see> for inequality</summary>
    /// <param name="pos1">A position</param>
    /// <param name="pos2">A position</param>
    /// <returns>False if given points represent same location; true if not</returns>
    public static bool operator !=(GpsPoint pos1, GpsPoint pos2) { return !pos2.Equals(pos1); }
    /// <summary>Gets has code of current object</summary>
    public override int GetHashCode()
    {
        return Latitude.GetHashCode() | Longitude.GetHashCode();
    }
}
