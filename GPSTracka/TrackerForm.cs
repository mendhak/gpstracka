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
using System.Globalization;
using Microsoft.Win32;
using GPSTracka.Gps;
using GPSTracka.Properties;

namespace GPSTracka
{

    /// <summary>Main form</summary>
    public partial class TrackerForm : Form
    {
        //renamed from GPSTracka to TrackerForm - old name puzzled the designer
        /// <summary>Power states</summary>
        public enum DevicePowerState
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

        /// <summary>Power requirement handle</summary>
        private IntPtr powerHandle;

        int countDown;

        private static System.Threading.Timer preventSleepTimer;

        private delegate void StatusDelegate(string message, int slot);

        /// <summary>Number of currently reported sentence</summary>
        /// <remarks>Synchronize access using <see cref="sentenceCounterSyncObj"/></remarks>
        int sentenceCounter = -1;

        /// <summary>Synchronize access to <see cref="sentenceCounter"/> using this</summary>
        object sentenceCounterSyncObj = new object();

        private bool logSentenceOnStack = false;

        /// <summary>Last date &amp; time when possition was logged (to prevent duplicates)</summary>
        DateTime lastGpsLoggedAt = DateTime.MinValue;

        /// <summary>Counts invalid positions in order to stop GPS after 10</summary>
        int invalidPositionCount;

        /// <summary>Indicates that GPS logging just started and no valid position has been got yet.</summary>
        bool afterStart = true;

        /// <summary>Device local time when invalid position was last reported to user</summary>
        DateTime lastInvPosUserTime = DateTime.MinValue;

        /// <summary>Instance of window showing GPS satellites</summary>
        SatellitesView satellitesWindow;

        /// <summary>Path of file where data is currently written to</summary>
        /// <remarks>Whenever this is set to null <see cref="nmeaFileName"/> must be set to null as well.</remarks>
        private string currentFileName;

        /// <summary>Path of current NMEA log file</summary>
        private string nmeaFileName;

        private const string KmlNs = "http://www.opengis.net/kml/2.2";

        private const string GpxNs = "http://www.topografix.com/GPX/1/0";

        /// <summary>Indicates if verbose mode is enabled</summary>
        static bool logEverything;
        /// <summary>Delegate to update text box with text</summary>
        private delegate void UpdateTextboxDelegate(string theText);

        /// <summary>Contains value of the <see cref="GpsInstance"/> property</summary>
        private GpsProvider gpsInstance;

        /// <summary>True when we are currently logging (or waiting); false otherwise</summary>
        bool gpsRunning;

        bool positionLogged = true;

        string previousSentence = String.Empty;

        //See description of AdvancedConfig.MinimalDistance
        /// <summary>Position where minimal distance rule started to be violated</summary>
        GpsPosition? lastPositionA;

        /// <summary>Time associated with <see cref="lastPositionA"/></summary>
        DateTime lastTimeA;

        /// <summary>Last position not logged due to vilolation of minimal distance rule</summary>
        GpsPosition? lastPositionC;

        /// <summary>Time associated with <see cref="lastPositionC"/>.</summary>
        DateTime lastTimeC;

        private Dictionary<string, IntPtr> PowerHandles = new Dictionary<string, IntPtr>();

        private delegate void StopGpsExtractedDelegate();

        /// <summary>Delegate to the <see cref="SetTimerEnabled"/> method</summary>
        /// <param name="timer"><see cref="Timer"/> to set <see cref="Timer.Enabled">Enabled</see> of</param>
        /// <param name="enabled">New value of the <see cref="Timer.Enabled"/> property</param>
        /// <exception cref="ArgumentNullException"><paramref name="timer"/> is null</exception>
        private delegate void DSetTimerEnabled(Timer timer, bool enabled);

        /// <summary>When true, <see cref="GpsStopAndWait"/> is curently being called</summary>
        private bool waitForStop;

        private delegate void StartupGpsDelegate();

        int tickCount;

        /// <summary>Initial size of infopane label (to measure)</summary>
        Size initMeasure;

        /// <summary>Statistic</summary>
        private GpsStatistics statistic;

        /// <summary>Last known elevation</summary>
        private decimal? lastKnownElevation;

        /// <summary>Last time of logged/not logged point</summary>
        private DateTime lastLoggedPointTime;

        /// <summary>User pressed stop and start and chosedto continue</summary>
        private bool continuing;

        /// <summary>
        /// Start tracking on application start
        /// </summary>
        private bool autoStart;

        /// <summary>
        /// Whether the satellite window is open or not, because this is used to keep the GPS instance running
        /// </summary>
        private bool satelliteWindowOpen;



        public TrackerForm(bool autoStart)
        {
            InitializeComponent();
            initMeasure = lblAerial.Size;
            stsStatus[0] = Properties.Resources.PressStart;//Status 
            stsStatus[1] = "";//Countdown 
            stsStatus[2] = "";//Speed 

            string[] portNames = SerialPort.GetPortNames();
            foreach (string port in portNames)
            {
                ComboBoxCOMPorts.Items.Add(port);
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

            //Auto start property
            this.autoStart = autoStart;
        }

        private void GPSTracka_Load(object sender, EventArgs e)
        {
            TextBoxRawLog.BackColor = SystemColors.Window;

            //prepare Panels
            HidePanel(aboutPanel);
            HidePanel(settingsPanel);
            ShowPanel(mainPanel);

            //prepare form
            Size = new Size(240, 294);
            TextBoxRawLog.ScrollBars = ScrollBars.Vertical;
            //Apply settings
            ShowSettings();

            //Call keepdeviceawake every 30 seconds in its own timer
            //Cannot use existing timer because it may have 5 minute intervals.
            preventSleepTimer = new System.Threading.Timer(new System.Threading.TimerCallback(KeepDeviceAwake), null, 0, 30000);

            if (autoStart)
            {
                startMenuItem_Click(this, new EventArgs());
            }
        }

        /// <summary>Shows basig settings to user using controls</summary>
        private void ShowSettings()
        {
            ComboBoxCOMPorts.Text = AdvancedConfig.ComPort;
            ComboBaudRate.Text = ((int)AdvancedConfig.BaudRate).ToString();
            chkUseWindowsDriver.Checked = AdvancedConfig.UseWindowsDriver;
            NumericUpDownInterval.Value = AdvancedConfig.PollingInterval;
            CheckBoxToTextBox.Checked = AdvancedConfig.LogToTextBox;
            CheckBoxToFile.Checked = AdvancedConfig.LogToLogFile;
            logLocationTextBox.Text = AdvancedConfig.LogFileLocation;

            switch (AdvancedConfig.LogFormat)
            {
                case LogFormat.KML:
                    radioButtonKML.Checked = true;
                    break;
                case LogFormat.CSV:
                    optCSV.Checked = true;
                    break;
                default:
                    radioButtonGPX.Checked = true;
                    break;
            }

            chkAltitude.Checked = AdvancedConfig.LogAltitude;

            switch (AdvancedConfig.TrackType)
            {
                case TrackType.Track:
                    optTrack.Checked = true;
                    break;
                default:
                    optDistinct.Checked = true;
                    break;
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
                GpsInstance.Position += GpsPositionReceived;
                GpsInstance.GpsSentence += GpsSentenceEvent;
                GpsInstance.StateChanged += GpsCommStateChanged;
                GpsInstance.Error += GpsErrorRaised;
                GpsInstance.Movement += GpsMovementEvent;
                GpsInstance.Satellite += GpsSatelliteEvent;
            }
            else
            {
                GpsInstance.Position -= GpsPositionReceived;
                GpsInstance.GpsSentence -= GpsSentenceEvent;
                GpsInstance.StateChanged -= GpsCommStateChanged;
                GpsInstance.Error -= GpsErrorRaised;
                GpsInstance.Movement -= GpsMovementEvent;
                GpsInstance.Satellite -= GpsSatelliteEvent;
            }
        }

        /// <summary>Sets mssage to status bar</summary>
        /// <param name="message">Message to be shown</param>
        void Status(string message)
        {
            if (stsStatus.InvokeRequired)
                try
                {
                    stsStatus.BeginInvoke(new Action<string>(Status), message);
                }
                catch (ObjectDisposedException)
                {

                }
            else
            {
                stsStatus[0] = message;
            }
        }


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
            if (!AdvancedConfig.StatusBar)
            {
                return;
            }

            stsStatus[2] = speed.ToString("0") + AdvancedConfig.SpeedUnitName;
        }

        private void tmrCountDown_Tick(object sender, EventArgs e)
        {
            if (countDown < 0)
            {
                stsStatus[1] = "";
                tmrCountDown.Enabled = false;
            }
            else stsStatus[1] = countDown--.ToString();
        }


        void LogSentence(GpsSentenceEventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<GpsSentenceEventArgs>(LogSentence), e);
                return;
            }
            lock (sentenceCounterSyncObj)
            {
                if (sentenceCounter - e.Counter > 10)
                {
                    return; //Too much sentences in queue for logging
                }
            }
            WriteToTextbox(e.Sentence);
            if (!logSentenceOnStack)
            {
                logSentenceOnStack = true;
                try { Application.DoEvents(); }
                finally { logSentenceOnStack = false; }
            }
        }

        void GpsSentenceEvent(GpsProvider sender, GpsSentenceEventArgs e)
        {

            if (logEverything)
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
                if (nmeaFileName == null)
                {
                    string nmeaFileLocation;

                    if (String.IsNullOrEmpty(AdvancedConfig.LogFileLocation))
                    {
                        nmeaFileLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
                    }
                    else
                    {
                        nmeaFileLocation = AdvancedConfig.LogFileLocation;
                    }

                    nmeaFileName = Path.Combine(nmeaFileLocation, DateTime.Now.ToString("yyyyMMdd_HHmmss",
                                                                                CultureInfo.InvariantCulture) + ".nmea");
                }

                if (!File.Exists(nmeaFileName))
                {
                    if (!Directory.Exists(Path.GetDirectoryName(nmeaFileName)))
                    {
                        try
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(nmeaFileName));
                        }
                        catch (Exception ex)
                        {
                            WriteToTextbox(string.Format(Properties.Resources.err_NmeaCreateDir, nmeaFileName, ex.Message));
                            return;
                        }
                    }
                }

                try
                {
                    using (FileStream file = File.Open(nmeaFileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write))
                    {
                        file.Seek(file.Length, SeekOrigin.Begin);
                        StreamWriter w = new StreamWriter(file);
                        w.WriteLine(e.Sentence);
                        w.Flush();
                    }
                }
                catch (Exception ex)
                {
                    WriteToTextbox(string.Format(Properties.Resources.err_NmeaOpenFile, nmeaFileName, ex.Message));
                }
            }
        }




        void GpsPositionReceived(GpsProvider sender, GpsPositionEventArgs e)
        {
            GpsPosition pos = e.Position;
            if (waitForStop)
            {
                return;
            }

            if ((DateTime.Now - lastGpsLoggedAt).TotalMilliseconds < 10)
            {
                return; //I've experianced multiple logs at same place in same time
            }

            if (pos.Longitude.HasValue && pos.Latitude.HasValue)
            {
                afterStart = false;
                invalidPositionCount = 0;
                lastGpsLoggedAt = DateTime.Now;
                DateTime time = AdvancedConfig.UseGpsTime ? pos.GpsTime.ToLocalTime() : DateTime.Now;
                decimal? altitude = pos.Altitude.HasValue ? pos.Altitude + (decimal)AdvancedConfig.AltitudeCorrection : null;


                string gpsData = string.Format(
                    AdvancedConfig.TextLogFormat,
                    time.ToUniversalTime(),
                    Math.Round(pos.Longitude.Value, 7),
                    Math.Round(pos.Latitude.Value, 7),
                    altitude.HasValue ? Math.Round(altitude.Value, 7) : 0,
                    time,
                    AdvancedConfig.ElevationUnitName
                    );

                if (!positionLogged)
                {
                    bool logThisPoint = false; //Check if the point is not too neer to stop-start point 
                    decimal? distance = null;
                    if (lastPositionA == null)
                    {
                        //Stop-start point unknown (immediately after start)  
                        logThisPoint = true;
                    }
                    else
                    {
                        distance = Math.Abs(GpsPosition.CalculateDistance(lastPositionA.Value, pos, OpenNETCF.IO.Serial.GPS.Units.Kilometers) * 1000);
                        if (distance < AdvancedConfig.MinimalDistance)
                        {
                            //Point is to near
                            lastPositionC = pos;//Remember it
                            lastTimeC = time; //And remember the time
                        }
                        else
                        {
                            //Points are far enough
                            logThisPoint = true;//Log this point
                            /*//If distance of this point from latest not logged point is long enough log that point as well
                            decimal distAC = Math.Abs(Gps.GpsPosition.CalculateDistance(LastPositionC.Value, pos, OpenNETCF.IO.Serial.GPS.Units.Kilometers) * 1000);*/
                            if (lastPositionC != null && time != lastTimeA && lastTimeA != lastTimeC /*&& distAC >= AdvancedConfig.MinimalDistance*/)
                            {
                                //Commented-out - always log last pause point
                                WriteToFile(
                                    lastTimeC,
                                    Convert.ToDouble(Math.Round(lastPositionC.Value.Latitude.Value /* * latMultiplier*/, 7)),
                                    Convert.ToDouble(Math.Round(lastPositionC.Value.Longitude.Value /* * longMultiplier*/, 7)),
                                    Convert.ToDouble(Math.Round(lastPositionC.Value.Altitude.HasValue ? lastPositionC.Value.Altitude.Value + AdvancedConfig.AltitudeCorrection : 0, 7))
                                );
                            }
                        }
                    }

                    WriteToTextbox(gpsData + (logThisPoint ? "" : " X"));
                    OnPoint(time, new GpsPoint(pos.Latitude.Value, pos.Longitude.Value, pos.Altitude), logThisPoint);

                    if (logThisPoint)
                    {
                        if (distance.HasValue)
                        {
                            ShowSpeed(distance.Value / 1000 / (decimal)(time - lastTimeA).TotalHours);
                        }

                        Status(Properties.Resources.status_PositionReceived);
                        lastPositionA = pos;//Remember position
                        lastTimeA = time;//Remember time
                        lastPositionC = pos;
                        lastTimeC = time;
                        WriteToFile(
                            time,
                            Convert.ToDouble(Math.Round(pos.Latitude.Value, 7)),
                            Convert.ToDouble(Math.Round(pos.Longitude.Value, 7)),
                            Convert.ToDouble(altitude.HasValue ? Math.Round(altitude.Value, 7) : 0));

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
                if (++invalidPositionCount == int.MaxValue)
                {
                    invalidPositionCount = 0;
                }

                if (!afterStart && AdvancedConfig.InvalidPositionsMax != 0 && invalidPositionCount >= AdvancedConfig.InvalidPositionsMax)
                {
                    invalidPositionCount = 0;
                    Status(string.Format(Properties.Resources.status_InvalidPosWait, invalidPositionCount));
                    StopGps();
                }
                else if (afterStart || AdvancedConfig.InvalidPositionsMax == 0)
                {
                    if ((DateTime.Now - lastInvPosUserTime).TotalMilliseconds >= 900)
                    {
                        Status(string.Format("inv. pos. {0}", invalidPositionCount));
                        lastInvPosUserTime = DateTime.Now;
                    }
                }
                else
                {
                    if ((DateTime.Now - lastInvPosUserTime).TotalMilliseconds >= 900)
                    {
                        Status(string.Format(Properties.Resources.status_InvalidPos, invalidPositionCount));
                        lastInvPosUserTime = DateTime.Now;
                    }
                }

            }
        }

        void GpsCommStateChanged(GpsProvider sender, GpsState state)
        {
            switch (state)
            {
                /*case States.AutoDiscovery:
                    if (logEverything)  WriteToTextbox("AutoDiscovery");
                    Status("Auto discovery");
                    break;*/
                case GpsState.Opening:
                    if (logEverything)
                    {
                        WriteToTextbox(Properties.Resources.Opening);
                    }

                    Status(Properties.Resources.status_Opening);
                    break;
                case GpsState.Open:
                    if (logEverything)
                    {
                        WriteToTextbox(Properties.Resources.Running);
                    }
                    Status(Properties.Resources.status_Running);
                    break;
                case GpsState.Closed:
                    if (logEverything)
                    {
                        WriteToTextbox(Properties.Resources.Stopped);
                    }

                    Status(Properties.Resources.Status_Stopped);
                    break;
                case GpsState.Closing:
                    if (logEverything)
                    {
                        WriteToTextbox(Properties.Resources.Stopping);
                    }
                    Status(Properties.Resources.Status_Stopping);
                    break;
            }

        }

        void GpsErrorRaised(GpsProvider sender, string message, Exception exception)
        {
            if (logEverything)
            {
                WriteToTextbox(message/* + ". The GPS data is: " + gps_data*/);
                if (exception != null)
                {
                    WriteExceptionToTextBox(exception);
                }
            }
            Status(Properties.Resources.err_ErrorTitle + " " + (exception == null ? "" : exception.Message));
        }

        private void GpsMovementEvent(GpsProvider sender, GpsMovementEventArgs e)
        {
            if (statistic != null)
            {
                statistic.ActualSpeed = e.Speed;
            }
        }

        private void GpsSatelliteEvent(GpsProvider sender, GpsSatelliteEventArgs e)
        {
            if (statistic == null && InvokeRequired)
            {
                BeginInvoke(new GpsSatelliteEventHandler(GpsSatelliteEvent), sender, e);
                return;
            }
            int cnt = 0;
            foreach (GpsSatellite sat in e.Satellites)
            {
                if (sat.SignalToNoiseRatio != 0)
                {
                    ++cnt;
                }
            }
            if (statistic != null)
            {
                statistic.CurrentSats = cnt;
            }
            else
            {
                lblSatellites.Text = cnt.ToString();
            }
        }

        private void mniSatellites_Click(object sender, EventArgs e)
        {

            Status(Resources.Satellite_Started);

            GpsState currentState = GpsInstance.State;
            bool currentTimerState = timer1.Enabled;
            satelliteWindowOpen = true;
            satellitesWindow = new SatellitesView(GpsInstance);
            satellitesWindow.ShowDialog();

            satellitesWindow.Close();
            satelliteWindowOpen = false;

            if (currentState == GpsState.Closed)
            {
                GpsStopAndWait();
            }

            if (currentTimerState && timer1.Enabled == false)
            {
                SetTimerEnabled(timer1, true);
                countDown = timer1.Interval / tmrCountDown.Interval;
                SetTimerEnabled(tmrCountDown, AdvancedConfig.StatusBar);
            }

            Status(Resources.Satellite_Stopped);

        }


        private void WriteToFile(DateTime time, double latitude, double longitude, double altitude)
        {
            try
            {
                if (AdvancedConfig.LogToLogFile)
                {
                    switch (AdvancedConfig.LogFormat)
                    {
                        case LogFormat.KML: WriteToKmlFile(time, latitude, longitude, altitude); break;
                        case LogFormat.CSV: WriteToCsvFile(time, latitude, longitude, altitude); break;
                        default: WriteToGpxFile(time, latitude, longitude, altitude); break;
                    }
                }
            }
            catch (Exception ex)
            {
                WriteToTextbox(Properties.Resources.err_WriteToFile + " " + ex.Message);
                //writeExceptionToTextBox(ex);
            }
        }



        private void WriteToKmlFile(DateTime time, double latitude, double longitude, double altitude)
        {
            XmlDocument doc = new XmlDocument();
            if (currentFileName == null)
            {
                currentFileName = Path.Combine(AdvancedConfig.LogFileLocation,
                                               time.ToLocalTime().ToString("yyyyMMdd_HHmmss",
                                                                           CultureInfo.InvariantCulture) + ".kml");
            }

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
                                    <name>" + Path.GetFileNameWithoutExtension(currentFileName) + @"</name>
                                    <Folder id='Points'>
                                        <name>Points</name>
                                    </Folder>
                                    " + (AdvancedConfig.TrackType == TrackType.Track ?
                                    @"<Placemark id='track'>
                                        <name>" + Path.GetFileNameWithoutExtension(currentFileName) + @"</name>
                                        <styleUrl>#yellowLineGreenPoly</styleUrl>
                                        <Style id='yellowLineGreenPoly'>
                                            <LineStyle>
                                                <color>" +
                                                    string.Format(CultureInfo.InvariantCulture,
                                                        "{0:x2}{1:x2}{2:x2}{3:x2}",
                                                        AdvancedConfig.KmlLineColor.A, AdvancedConfig.KmlLineColor.B, AdvancedConfig.KmlLineColor.G, AdvancedConfig.KmlLineColor.R)
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

                stream.Close();
            }

            doc.Load(currentFileName);

            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("def", KmlNs);

            //XmlNode kml = doc.ChildNodes[1].SelectSingleNode("Document");
            XmlNode kml = doc.SelectSingleNode("/def:kml/def:Document/def:Folder[@id='Points']", nsmgr);

            XmlNode placemark = doc.CreateElement("Placemark", KmlNs);

            XmlNode plName = doc.CreateElement("name", KmlNs);
            plName.InnerText = string.Format(AdvancedConfig.KmlNameFormat,
                time.ToUniversalTime(), longitude, latitude, altitude, time, AdvancedConfig.ElevationUnitName);
            placemark.AppendChild(plName);

            XmlNode plDescription = doc.CreateElement("description", KmlNs);
            plDescription.InnerText = string.Format(AdvancedConfig.KmlDescFormat,
                time.ToUniversalTime(), longitude, latitude, altitude, time, AdvancedConfig.ElevationUnitName);
            placemark.AppendChild(plDescription);

            XmlNode plPoint = doc.CreateElement("Point", KmlNs);
            XmlNode plPointCoord = doc.CreateElement("coordinates", KmlNs);
            plPointCoord.InnerText = longitude.ToString(CultureInfo.InvariantCulture) + "," +
                                     latitude.ToString(CultureInfo.InvariantCulture) + "," +
                                     (AdvancedConfig.LogAltitude ? altitude.ToString(CultureInfo.InvariantCulture) : "0");
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
                coordinates.InnerText = coordinates.InnerText + String.Format(CultureInfo.InvariantCulture,
                    "\r\n{0},{1},{2}", longitude, latitude, (AdvancedConfig.LogAltitude ? altitude.ToString(CultureInfo.InvariantCulture) : "0"));
            }
            doc.Save(currentFileName);
        }


        private void WriteToGpxFile(DateTime time, double latitude, double longitude, double altitude)
        {
            XmlDocument doc = new XmlDocument();
            if (currentFileName == null)
            {
                currentFileName = Path.Combine(AdvancedConfig.LogFileLocation,
                                               time.ToLocalTime().ToString("yyyyMMdd_HHmmss",
                                                                           CultureInfo.InvariantCulture) + ".gpx");
            }

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
                stream.WriteLine("<time>" + time.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture) + "</time>");
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
            {
                parent = doc.SelectSingleNode("/def:gpx/def:trk/def:trkseg", nsmgr);
            }
            else
            {
                parent = doc.SelectSingleNode("/def:gpx", nsmgr);
            }

            XmlNode wpt = doc.CreateElement(AdvancedConfig.TrackType == TrackType.Track ? "trkpt" : "wpt", GpxNs);

            XmlAttribute latAttrib = doc.CreateAttribute("lat");
            latAttrib.Value = latitude.ToString(CultureInfo.InvariantCulture);
            wpt.Attributes.Append(latAttrib);

            XmlAttribute longAttrib = doc.CreateAttribute("lon");
            longAttrib.Value = longitude.ToString(CultureInfo.InvariantCulture);
            wpt.Attributes.Append(longAttrib);

            if (AdvancedConfig.LogAltitude)
            {
                XmlNode altNode = doc.CreateElement("ele", GpxNs);
                altNode.InnerText = altitude.ToString(CultureInfo.InvariantCulture);
                wpt.AppendChild(altNode);
            }

            XmlNode timeNode = doc.CreateElement("time", GpxNs);
            timeNode.InnerText = time.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);

            wpt.AppendChild(timeNode);

            parent.AppendChild(wpt);

            doc.Save(currentFileName);
        }

        /// <summary>Writes data to CSV file</summary>
        /// <param name="time">Current time</param>
        /// <param name="latitude">Latitude</param>
        /// <param name="longitude">Longitude</param>
        /// <param name="altitude">Altitude</param>
        private void WriteToCsvFile(DateTime time, double latitude, double longitude, double altitude)
        {
            if (currentFileName == null)
            {
                currentFileName = Path.Combine(AdvancedConfig.LogFileLocation,
                                               time.ToLocalTime().ToString("yyyyMMdd_HHmmss",
                                                                           CultureInfo.InvariantCulture) + ".csv");
            }

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
                    if (!string.IsNullOrEmpty(AdvancedConfig.CsvHeader))
                    {
                        stream.Write(AdvancedConfig.CsvHeader + AdvancedConfig.CsvNewLine);
                    }
                    stream.Flush();
                }
            }
            using (IDisposable stream = File.Open(currentFileName, FileMode.Open, FileAccess.ReadWrite, FileShare.Read),
                               xxx = new StreamWriter((FileStream)stream))
            {
                StreamWriter stream2 = (StreamWriter)xxx;
                stream2.BaseStream.Seek(0, SeekOrigin.End);
                StringBuilder fb = new StringBuilder();
                foreach (char ch in AdvancedConfig.CsvFields)
                {
                    if (fb.Length > 0)
                    {
                        fb.Append(AdvancedConfig.CsvSeparator);
                    }
                    string fieldval;
                    switch ((int)char.GetNumericValue(ch))
                    {
                        case 0: fieldval = latitude.ToString(CultureInfo.InvariantCulture); break;
                        case 1: fieldval = longitude.ToString(CultureInfo.InvariantCulture); break;
                        case 2: fieldval = altitude.ToString(CultureInfo.InvariantCulture); break;
                        case 3: fieldval = (AdvancedConfig.CsvUtc ? time.ToLocalTime() : time.ToUniversalTime()).ToString(AdvancedConfig.CsvDateFormat, CultureInfo.InvariantCulture); break;
                        default: fieldval = ""; break;
                    }
                    if (AdvancedConfig.CsvQualifierUsage == CSVQualifierUsage.Always || (fieldval.IndexOf(AdvancedConfig.CsvSeparator.ToString()) > 0 && AdvancedConfig.CsvQualifierUsage == CSVQualifierUsage.AsNeeded))
                    {
                        fb.Append(AdvancedConfig.CsvTextQualifier);
                        fb.Append(fieldval.Replace(AdvancedConfig.CsvTextQualifier.ToString(), AdvancedConfig.CsvTextQualifier.ToString() + AdvancedConfig.CsvTextQualifier.ToString()));
                        fb.Append(AdvancedConfig.CsvTextQualifier);
                    }
                    else
                    {
                        fb.Append(fieldval);
                    }
                }
                stream2.Write(fb.ToString() + AdvancedConfig.CsvNewLine);
                stream2.Flush();
            }
        }


        private void WriteExceptionToTextBox(Exception ex)
        {
            if (ex == null)
            {
                throw new ArgumentNullException("ex");
            }

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
            if (TextBoxRawLog.InvokeRequired)
            {
                UpdateTextboxDelegate theDelegate = new UpdateTextboxDelegate(WriteToTextbox);
                BeginInvoke(theDelegate, new object[] { theText });
            }
            else
            {
                if (!string.IsNullOrEmpty(TextBoxRawLog.Text) && !TextBoxRawLog.Text.EndsWith("\r\n"))
                {
                    TextBoxRawLog.Text = TextBoxRawLog.Text + "\r\n";

                }

                if (logEverything || AdvancedConfig.LogToTextBox)
                {
                    TextBoxRawLog.Text = TextBoxRawLog.Text + theText + "\r\n";

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
                            if (ch == '\n')
                            {
                                nlc++;
                            }
                        }
                        if (nlc > AdvancedConfig.MaxLogLength)
                        {
                            TextBoxRawLog.Clear();
                        }
                    }
                }
            }
        }

        private void verboseMenuItem_Click(object sender, EventArgs e)
        {
            verboseMenuItem.Checked = !verboseMenuItem.Checked;
            logEverything = verboseMenuItem.Checked;
        }


        /// <summary>Gets instance of GPS provider</summary>
        private GpsProvider GpsInstance
        {
            get
            {
                if (gpsInstance == null)
                {
                    if (AdvancedConfig.UseWindowsDriver)
                    {
                        gpsInstance = new MSGpsWrapper();
                    }
                    else
                    {
                        gpsInstance = new OpenNetGpsWrapper();
                    }
                    HookGps(true);
                }
                return gpsInstance;
            }
        }

        private void startMenuItem_Click(object sender, EventArgs e)
        {
            lastGpsLoggedAt = DateTime.MinValue;
            try
            {
                if (!gpsRunning)
                {//Start
                    if (currentFileName == null || MessageBox.Show(string.Format(Properties.Resources.ContinueInCurrentFile, currentFileName), "GPSTracka", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) != DialogResult.Yes)
                    {
                        currentFileName = null;
                        nmeaFileName = null;
                        statistic = null;
                        tickCount = 0;
                    }
                    if (currentFileName != null)
                    {
                        continuing = true;
                    }

                    invalidPositionCount = 0;
                    gpsRunning = true;
                    timer1.Interval = Convert.ToInt16(AdvancedConfig.PollingInterval) * 1000;
                    afterStart = true;
                    //Power requirements
                    if (!AdvancedConfig.UseWindowsDriver)
                    {
                        powerHandle = SetPowerRequirement(AdvancedConfig.ComPort + ":", (int)DevicePowerState.D0, 1,
                                                          IntPtr.Zero, 0);
                    }
                    foreach (string device in AdvancedConfig.KeepAwakeList)
                    {
                        if (!device.EndsWith(":"))
                        {
                            IntPtr devicePowerHandle = SetPowerRequirement(device + ":", DevicePowerState.D0, 1, IntPtr.Zero, 0);
                            if (PowerHandles.ContainsKey(device))
                            {
                                PowerHandles[device] = devicePowerHandle;
                            }
                            else
                            {
                                PowerHandles.Add(device, devicePowerHandle);
                            }
                        }
                    }
                    //if (!PowerPolicyNotify(PPN_UNATTENDEDMODE, 1)) {
                    //    WriteToTextbox(Properties.Resources.err_PowerPolicyNotifiyFailed);
                    //}
                    if (!AdvancedConfig.StartImmediatelly)
                    {
                        WriteToTextbox(string.Format(Properties.Resources.WillBeginReading,
                                                     AdvancedConfig.PollingInterval));
                    }
                    Status(Properties.Resources.Starting);
                    startMenuItem.Text = Properties.Resources.Stop;
                    if (!AdvancedConfig.StartImmediatelly)
                    {
                        timer1.Enabled = true;
                        tmrCountDown.Enabled = AdvancedConfig.StatusBar;
                        countDown = timer1.Interval / tmrCountDown.Interval;
                    }
                    tmrBeep.Enabled = AdvancedConfig.BeepTimer != 0;
                    tmrBeep.Interval = AdvancedConfig.BeepTimer * 1000;
                    if (statistic == null)
                    {
                        GpsStatistics.ClearAll(this);
                    }
                    else
                    {
                        statistic.ShowValues(GpsStatistics.ValueKind.All);
                    }
                }
                else
                {//Stop

                    if (satelliteWindowOpen)
                    {
                        return;
                    }

                    startMenuItem.Enabled = false;
                    startMenuItem.Text = Properties.Resources.PleaseWait;
                    try
                    {
                        GpsStopAndWait();
                    }
                    finally
                    {
                        gpsRunning = false;
                        startMenuItem.Enabled = true;
                        stsStatus[1] = "";
                        stsStatus[2] = "";
                        Status(Properties.Resources.Status_Stopped_simple);

                        //Release power requirements
                        if (powerHandle != IntPtr.Zero)
                        {
                            ReleasePowerRequirement(powerHandle);
                        }

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
                mniSatellites.Enabled = gpsRunning;
                settingsMenuItem.Enabled = !gpsRunning;
                if (!gpsRunning)
                {
                    lastPositionA = null;
                    lastPositionC = null;
                    tmrBeep.Enabled = false;
                }
            }

            if (gpsRunning && AdvancedConfig.StartImmediatelly)
            {
                timer1_Tick(timer1, e);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            positionLogged = false;
            stsStatus[1] = null;
            StartupGps();
            timer1.Enabled = false;
            countDown = timer1.Interval / tmrCountDown.Interval;
            tmrCountDown.Enabled = false;
        }




        /// <summary>Stops GPS and sets timer to count down for next GPS run</summary>
        private void StopGps()
        {
            if (this.InvokeRequired)
            {
                //StopGpsExtractedDelegate theDelegate = new StopGpsExtractedDelegate(StopGpsExtracted);
                StopGpsExtractedDelegate theDelegate = StopGps;
                BeginInvoke(theDelegate);
                ////Note: With MS driver managed wrapper we shall NOT stop device from within GPS event handler - it causes unpredicable behavior (deadlocks, device no longer receiving events after start ...)
                //this.BeginInvoke(new Action(StopGps_Extracted));
            }
            else
            {
                //Don't stop it if the satellite window is open (it needs GPS for data)
                if (satelliteWindowOpen)
                {
                    return;
                }

                try
                {
                    GpsStopAndWait();
                }
                finally
                {
                    StopGpsExtracted();
                }
            }
        }

        /// <summary>Extracted part of <see cref="StopGps"/> - sets timer</summary>
        private void StopGpsExtracted()
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
            countDown = timer1.Interval / tmrCountDown.Interval;
            SetTimerEnabled(tmrCountDown, AdvancedConfig.StatusBar);


        }

        /// <summary>Sets value of the <see cref="Timer.Enabled"/> property of given timer in thread safe way</summary>
        /// <param name="timer"><see cref="Timer"/> to set <see cref="Timer.Enabled">Enabled</see> of</param>
        /// <param name="enabled">New value of the <see cref="Timer.Enabled"/> property</param>
        /// <exception cref="ArgumentNullException"><paramref name="timer"/> is null</exception>
        private void SetTimerEnabled(Timer timer, bool enabled)
        {
            if (timer == null)
            {
                throw new ArgumentNullException("timer");
            }
            if (InvokeRequired)
            {//Required as timer started in bad thread may not work
                BeginInvoke(new DSetTimerEnabled(SetTimerEnabled), timer, enabled);
            }
            else
            {
                timer.Enabled = enabled;
            }
        }



        /// <summary>Stops GPS and waits for it to stop</summary>
        private void GpsStopAndWait()
        {
            try
            {
                while (GpsInstance.State == GpsState.Opening)
                {
                    System.Threading.Thread.Sleep(100);
                    Application.DoEvents();
                }
                GpsInstance.Stop();
                waitForStop = true;
                while (GpsInstance.State != GpsState.Closed)
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
                waitForStop = false;
            }
        }



        private void StartupGps()
        {
            if (InvokeRequired)
            {
                StartupGpsDelegate theDelegate = new StartupGpsDelegate(StartupGps);
                BeginInvoke(theDelegate);

                //this.BeginInvoke(new Action(StartupGps));
                return;
            }
            if (GpsInstance is OpenNetGpsWrapper)
            {
                string comPort = AdvancedConfig.ComPort;
                int baudRate = (int)AdvancedConfig.BaudRate;
                if (String.IsNullOrEmpty(comPort))
                {
                    comPort = "COM1";
                }
                ((OpenNetGpsWrapper)GpsInstance).Port = comPort;
                ((OpenNetGpsWrapper)GpsInstance).BaudRate = (BaudRates)baudRate;
            }
            foreach (string device in AdvancedConfig.KeepAwakeList)
            {
                if (device.EndsWith(":"))
                {
                    IntPtr devicePowerHandle = SetPowerRequirement(device, DevicePowerState.D0, 1, IntPtr.Zero, 0);
                    if (PowerHandles.ContainsKey(device))
                    {
                        PowerHandles[device] = devicePowerHandle;
                    }
                    else
                    {
                        PowerHandles.Add(device, devicePowerHandle);
                    }
                }
            }
            GpsInstance.Start();
        }

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
                    if (powerHandle != IntPtr.Zero)
                    {
                        ReleasePowerRequirement(powerHandle);
                    }
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
            catch
            {
                // Nothing
            }
        }

        private void clearMenuItem_Click(object sender, EventArgs e)
        {
            TextBoxRawLog.Text = string.Empty;
        }


        private void tmrBeep_Tick(object sender, EventArgs e)
        {
            BeepWrapper.Beep(BeepWrapper.BeepAlert.Exclamation);
            //System.Media.SystemSounds.Beep.Play();
            Status((++tickCount).ToString(), 3);
        }

        private void menuExit_Click(object sender, EventArgs e)
        {
            if (gpsRunning)
            {
                if (MessageBox.Show(Properties.Resources.ConfirmExit + (AdvancedConfig.LogToLogFile ? Properties.Resources.TrackSaved : ""), "GPSTRacka", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                {
                    return;
                }

                startMenuItem_Click(sender, e);
            }
            Close();
        }

        private void mniHelp_Click(object sender, EventArgs e)
        {
            string filename = null;
            string helpDir = Path.Combine(Path.GetDirectoryName(typeof(TrackerForm).Assembly.GetModules()[0].FullyQualifiedName), "Help");
            CultureInfo currentUiCulture = CultureInfo.CurrentUICulture;
            while (currentUiCulture != null && !string.IsNullOrEmpty(currentUiCulture.Name))
            {
                if (File.Exists(filename = Path.Combine(helpDir, "GPSTracka." + currentUiCulture.Name + ".htm")))
                {
                    break;
                }

                currentUiCulture = currentUiCulture.Parent;
            }

            if (String.IsNullOrEmpty(filename))
            {
                filename = Path.Combine(helpDir, "GPSTracka.htm");
            }

            try
            {
                System.Diagnostics.Process.Start(filename, null);
            }
            catch (Exception)
            {
                try
                {
                    System.Diagnostics.Process.Start(filename = Path.Combine(helpDir, "GPSTracka.htm"), null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format(Properties.Resources.err_HelpNotAvailable, filename, ex.Message), "GPSTracka", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                }
            }
        }

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
            System.Diagnostics.Process.Start(linkLabel1.Text, String.Empty);
        }

        private void settingsMenuItem_Click(object sender, EventArgs e)
        {
            currentFileName = null;
            nmeaFileName = null;
            HidePanel(mainPanel);
            HidePanel(aboutPanel);
            ShowPanel(settingsPanel);
            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"System\CurrentControlSet\GPS Intermediate Driver\Multiplexer"))
                {
                    lblRecommendedPort.Text = Properties.Resources.Recommended + " " + (string)key.GetValue("DriverInterface");
                }
            }
            catch
            {
                lblRecommendedPort.Text = String.Empty;
            }
        }

        private void saveMenuItem_Click(object sender, EventArgs e)
        {

            if (CheckBoxToFile.Checked && String.IsNullOrEmpty(logLocationTextBox.Text))
            {
                MessageBox.Show(Properties.Resources.err_BadPath);
                return;
            }

            if (satellitesWindow != null)
            {
                satellitesWindow.Close(); //To prevent it from using different GPS provider
            }

            HookGps(false);
            gpsInstance = null;

            AdvancedConfig.ComPort = ComboBoxCOMPorts.Text;
            AdvancedConfig.BaudRate = (BaudRates)int.Parse(ComboBaudRate.Text);
            AdvancedConfig.PollingInterval = (int)NumericUpDownInterval.Value;
            AdvancedConfig.LogToTextBox = CheckBoxToTextBox.Checked;
            AdvancedConfig.LogToLogFile = CheckBoxToFile.Checked;
            AdvancedConfig.LogFileLocation = logLocationTextBox.Text;
            AdvancedConfig.LogAltitude = chkAltitude.Checked;
            AdvancedConfig.UseWindowsDriver = chkUseWindowsDriver.Checked;

            if (radioButtonKML.Checked)
            {
                AdvancedConfig.LogFormat = LogFormat.KML;
            }
            else if (optCSV.Checked)
            {
                AdvancedConfig.LogFormat = LogFormat.CSV;
            }
            else
            {
                AdvancedConfig.LogFormat = LogFormat.GPX;
            }

            if (optTrack.Checked)
            {
                AdvancedConfig.TrackType = TrackType.Track;
            }
            else
            {
                AdvancedConfig.TrackType = TrackType.Points;
            }

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
            catch
            {

            }
            DialogResult result = fbd.ShowDialog();

            if (result == DialogResult.OK)
            {
                logLocationTextBox.Text = fbd.SelectedPath;
                //Parse and get the folder path
                //logLocationTextBox.Text = fi.DirectoryName; 
            } /*else {
                logLocationTextBox.Text = string.Empty;
            }*/
            fbd.Close();
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
            AdvancedConfigForm frm = new AdvancedConfigForm();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                stsStatus.Visible = AdvancedConfig.StatusBar;
                panInfoPane.Visible = AdvancedConfig.InfoPane;
            }
            frm.Close();
        }

        private void chkUseWindowsDriver_CheckStateChanged(object sender, EventArgs e)
        {
            label4.Enabled = ComboBoxCOMPorts.Enabled = lblRecommendedPort.Enabled = ComboBaudRate.Enabled = label5.Enabled = !chkUseWindowsDriver.Checked;
        }


        private void panInfoPane_Resize(object sender, EventArgs e)
        {
            if (panInfoPane.Visible)
            {
                int lineSize = (panInfoPane.ClientSize.Width >= 8 * initMeasure.Width * 0.8) ? 8 : 4;
                int x = panInfoPane.ClientRectangle.Left, y = panInfoPane.ClientRectangle.Top;
                int i = 0;
                foreach (Control ctl in panInfoPane.Controls)
                {
                    ctl.Width = panInfoPane.ClientSize.Width / lineSize;
                    ctl.Left = x; ctl.Top = y;
                    if (++i % lineSize == 0)
                    {
                        x = panInfoPane.ClientRectangle.Left;
                        y = y + initMeasure.Height;
                    }
                    else
                    {
                        x = x + panInfoPane.ClientRectangle.Width / lineSize;
                    }
                    panInfoPane.Height = (int)Math.Ceiling((float)panInfoPane.Controls.Count / lineSize) * initMeasure.Height;
                }
            }
        }

        private void ShowPanel(Panel panel)
        {
            SuspendLayout();
            try
            {
                panel.Location = new Point(0, 0);
                panel.Dock = DockStyle.Fill;
                panel.Visible = true;
                switch (panel.Name)
                {
                    case "settingsPanel":
                        {
                            if (Menu != settingsPanelMenu)
                            {
                                Menu = settingsPanelMenu;
                            }
                            break;
                        }
                    case "aboutPanel":
                        {
                            if (Menu != aboutPanelMenu)
                            {
                                Menu = aboutPanelMenu;
                            }
                            break;
                        }
                    case "mainPanel":
                        {
                            if (Menu != mainPanelMenu)
                            {
                                Menu = mainPanelMenu;
                            }
                            break;
                        }
                    default:
                        {
                            if (Menu != mainPanelMenu)
                            {
                                Menu = mainPanelMenu;
                            }
                            break;
                        }
                }
            }
            finally
            {
                ResumeLayout();
            }
        }

        private void HidePanel(Panel panel)
        {
            panel.Dock = DockStyle.None;
            panel.Visible = false;
        }


        private void mniSaveStatistic_Click(object sender, EventArgs e)
        {
            if (this.currentFileName == null)
            {
                MessageBox.Show("No statistic to save.", "Save statistic", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }
            statistic.Save(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(this.currentFileName), System.IO.Path.GetFileNameWithoutExtension(this.currentFileName) + "_stat.txt"));
        }
        /// <summary>Called when point is nearly logged</summary>
        /// <param name="time">Logged time</param>
        /// <param name="gpsPoint">Actual position</param>
        /// <param name="logThisPoint">true when point was logged false when it was not</param>
        private void OnPoint(DateTime time, GpsPoint gpsPoint, bool logThisPoint)
        {
            if (continuing && statistic != null)
            {
                statistic.PauseTime = statistic.PauseTime + (time - lastLoggedPointTime);
                //User pause
            }

            if (statistic == null)
            {//Initialize
                statistic = new GpsStatistics(this, time, gpsPoint);
                lastKnownElevation = gpsPoint.Altitude;
                lastLoggedPointTime = time;
                return;
            }
            statistic.CurrentTime = time;

            if (logThisPoint)
            {//This point is logged, count it
                decimal delta = gpsPoint - statistic.CurrentPos;//km
                statistic.SumLength = statistic.SumLength + delta;
                statistic.CurrentPos = gpsPoint;
                statistic.PointsTotal++;
            }
            else
            {//This point is not logged, pause
                statistic.PauseTime = statistic.PauseTime + (time - lastLoggedPointTime);
            }
            lastLoggedPointTime = time;

            if (lastKnownElevation == null)
            {//Initialize altitude if necessary
                lastKnownElevation = gpsPoint.Altitude;
                return;
            }

            if (logThisPoint && gpsPoint.Altitude.HasValue)
            {//Elevation
                decimal delta = gpsPoint.Altitude.Value - lastKnownElevation.Value;
                if (delta < 0)
                {
                    statistic.SumEleMinus = statistic.SumEleMinus - delta;
                }
                else
                {
                    statistic.SumElePlus = statistic.SumElePlus + delta;
                }
                lastKnownElevation = gpsPoint.Altitude;
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




        ///// <summary>Keeps rack if form is active or not</summary>
        //private bool active;
        //private void TrackerForm_Activated(object sender, EventArgs e)
        //{
        //    active = true;
        //}

        //private void TrackerForm_Deactivate(object sender, EventArgs e)
        //{
        //    active = false;
        //}
    }
}
