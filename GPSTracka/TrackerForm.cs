using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml;
using Microsoft.WindowsCE.Forms;
using System.IO.Ports;
using OpenNETCF.IO.Serial.GPS;
using OpenNETCF.IO.Serial;



namespace GPSTracka
{


    public partial class GPSTracka : Form
    {




        [DllImport("CoreDll.dll")]
        private static extern void SystemIdleTimerReset();

        private static System.Threading.Timer preventSleepTimer = null;

        public bool settingsFileExists = false;
        //public static GPSHandler GPS;
        public static GPS gps = new GPS();
        public static bool LogEverything = false;
        public delegate void UpdateTextboxDelegate(string theText);
        public delegate void UpdateFileDelegate(double latitude, double longitude);
        bool positionLogged = true;
        string previousSentence = String.Empty;





        public GPSTracka()
        {
            InitializeComponent();


            string[] portNames = SerialPort.GetPortNames();
            foreach (string port in portNames)
            {
                this.ComboBoxCOMPorts.Items.Add(port);
            }

            gps.Position += new GPS.PositionEventHandler(gps_Position);
            gps.GpsSentence += new GPS.GpsSentenceEventHandler(gps_GpsSentence);
            gps.GpsCommState += new GPS.GpsCommStateEventHandler(gps_GpsCommState);
            gps.Error += new GPS.ErrorEventHandler(gps_Error);

        }

        
        void gps_Error(object sender, Exception exception, string message, string gps_data)
        {
            WriteToTextbox(message + ". The GPS data is: " + gps_data);
            writeExceptionToTextBox(exception);
        }

        void gps_GpsCommState(object sender, GpsCommStateEventArgs e)
        {
   

                switch (e.State)
                {
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

        void gps_GpsSentence(object sender, GpsSentenceEventArgs e)
        {
            if (LogEverything)
            {
                System.Threading.Thread.Sleep(190); //It might go too fast.
                
                if (e.Sentence != previousSentence)
                {
                    this.WriteToTextbox(e.Sentence);
                    previousSentence = e.Sentence;
                }
            }
        }




        public void WriteToTextbox(string theText)
        {

            if (this.TextBoxRawLog.InvokeRequired)
            {
                UpdateTextboxDelegate theDelegate = new UpdateTextboxDelegate(WriteToTextbox);
                this.Invoke(theDelegate, new object[] { theText });
            }
            else
            {

                if (LogEverything || CheckBoxToTextBox.Checked)
                {
                    this.TextBoxRawLog.Text = this.TextBoxRawLog.Text + theText + "\r\n";


                    if (TextBoxRawLog.Text.Length > 1)
                    {
                        TextBoxRawLog.SelectionStart = TextBoxRawLog.Text.Length - 1;
                        TextBoxRawLog.SelectionLength = 0;
                        TextBoxRawLog.ScrollToCaret();
                    }
                }
                

            }
        }


        void gps_Position(object sender, Position pos)
        {
            if (pos.Longitude_Decimal != 0 && pos.Latitude_Decimal != 0)
            {
                int latMultiplier = 1;
                int longMultiplier = 1;

                if (pos.DirectionLatitude == CardinalDirection.South)
                {
                    latMultiplier = -1;
                }

                if (pos.DirectionLongitude == CardinalDirection.West)
                {
                    longMultiplier = -1;
                }

                string gpsData = DateTime.Now.ToString("HHmmss") + "," + Math.Round(pos.Latitude_Decimal * latMultiplier, 7).ToString() + "," + Math.Round(pos.Longitude_Decimal * longMultiplier, 7).ToString();

                if (!positionLogged)
                {
                    WriteToTextbox(gpsData);
                    WriteToFile(Convert.ToDouble(Math.Round(pos.Latitude_Decimal * latMultiplier, 7)), Convert.ToDouble(Math.Round(pos.Longitude_Decimal * longMultiplier, 7)));
                    StopGps();
                }
                positionLogged = true;

            }
        }



        //private void GPSEventHandler(object sender, GPSHandler.GPSEventArgs e)
        //{

        //    try
        //    {
        //        if (!LogEverything)
        //        {

        //            switch (e.TypeOfEvent)
        //            {

        //                case GPSEventType.GPGLL:
        //                    if (GPS.GPGLL.DataValid)
        //                    {

        //                        string gpsData = DateTime.Now.ToString("HHmmss") + "," + Math.Round(GPS.GPGGA.Position.Latitude, 7).ToString() + "," + Math.Round(GPS.GPGGA.Position.Longitude, 7).ToString();

        //                        if (CheckBoxToTextBox.Checked)
        //                        {
        //                            TextBoxRawLog.Text = TextBoxRawLog.Text + gpsData + "\r\n";
        //                            TextBoxRawLog.SelectionStart = TextBoxRawLog.Text.Length - 1;
        //                            TextBoxRawLog.SelectionLength = 0;
        //                            TextBoxRawLog.ScrollToCaret();
        //                        }
        //                        if (CheckBoxToFile.Checked)
        //                        {
        //                            //WriteToFile(gpsData);
        //                            WriteToFile(Math.Round(GPS.GPGGA.Position.Latitude, 7), Math.Round(GPS.GPGGA.Position.Longitude, 7));
        //                        }
        //                        StopGps();
        //                    }
        //                    break;

        //                case GPSEventType.GPGGA:
        //                    if (GPS.GPGGA.FixQuality != SharpGis.SharpGps.NMEA.GPGGA.FixQualityEnum.Invalid)
        //                    {

        //                        string gpsData = DateTime.Now.ToString("HHmmss") + "," + Math.Round(GPS.GPGGA.Position.Latitude, 7).ToString() + "," + Math.Round(GPS.GPGGA.Position.Longitude, 7).ToString();

        //                        if (CheckBoxToTextBox.Checked)
        //                        {
        //                            TextBoxRawLog.Text = TextBoxRawLog.Text + gpsData + "\r\n";
        //                            TextBoxRawLog.SelectionStart = TextBoxRawLog.Text.Length - 1;
        //                            TextBoxRawLog.SelectionLength = 0;
        //                            TextBoxRawLog.ScrollToCaret();
        //                        }
        //                        if (CheckBoxToFile.Checked)
        //                        {
        //                            WriteToFile(Math.Round(GPS.GPGGA.Position.Latitude, 7), Math.Round(GPS.GPGGA.Position.Longitude, 7));
        //                        }

        //                        StopGps();
        //                    }

        //                    break;

        //            }
        //        }
        //        else
        //        {
        //            TextBoxRawLog.Text = TextBoxRawLog.Text + e.Sentence + "\r\n";
        //            TextBoxRawLog.SelectionStart = TextBoxRawLog.Text.Length - 1;
        //            TextBoxRawLog.SelectionLength = 0;
        //            TextBoxRawLog.ScrollToCaret();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        writeExceptionToTextBox(ex);
        //    }




        //}


        private void WriteToFile(double latitude, double longitude)
        {
            if (this.CheckBoxToFile.InvokeRequired)
            {
                UpdateFileDelegate theDelegate = new UpdateFileDelegate(WriteToFile);
                this.Invoke(theDelegate, new object[] { latitude, longitude });
            }
            else
            {
                try
                {

                    if (CheckBoxToFile.Checked && !LogEverything)
                    {
                        XmlDocument doc = new XmlDocument();
                        string currentFileName = "\\My Documents\\GPSLogs\\" + DateTime.Now.ToString("yyyyMMdd") + ".gpx";

                        if (!File.Exists(currentFileName))
                        {
                            StreamWriter stream = File.CreateText(currentFileName);
                            stream.WriteLine(@"<?xml version=""1.0""?>");
                            stream.WriteLine(@"<gpx
                             version=""1.0""
                             creator=""GPSTracka - http://www.mendhak.com/""
                             xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
                             xmlns=""http://www.topografix.com/GPX/1/0""
                             xsi:schemaLocation=""http://www.topografix.com/GPX/1/0 http://www.topografix.com/GPX/1/0/gpx.xsd"">");
                            stream.WriteLine("<time>" + DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ") + "</time><bounds />");
                            stream.WriteLine("</gpx>");
                            stream.Close();
                        }

                        doc.Load(currentFileName);

                        XmlNode gpx = doc.ChildNodes[1];

                        XmlNode wpt = doc.CreateElement("wpt");

                        XmlAttribute latAttrib = doc.CreateAttribute("lat");
                        latAttrib.Value = latitude.ToString();
                        wpt.Attributes.Append(latAttrib);

                        XmlAttribute longAttrib = doc.CreateAttribute("lon");
                        longAttrib.Value = longitude.ToString();
                        wpt.Attributes.Append(longAttrib);

                        XmlNode timeNode = doc.CreateElement("time");
                        timeNode.InnerText = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");

                        wpt.AppendChild(timeNode);

                        gpx.AppendChild(wpt);

                        doc.Save(currentFileName);
                    }

                   
                }
                catch (Exception ex)
                {
                    writeExceptionToTextBox(ex);
                }
            }
            

        }


        private void writeExceptionToTextBox(Exception ex)
        {



            string errorMessage = String.Concat("\r\n\r\n",
                            "****ERROR****", "\r\n",
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


        private void timer1_Tick(object sender, EventArgs e)
        {
            positionLogged = false;
            StartupGps();
            timer1.Enabled = true;
            
        }



        private void StopGps()
        {

            //GPS.Stop(); //Close serial port
            gps.Stop();
            timer1.Enabled = true;


        }

        private void StartupGps()
        {
            string comPort = ComboBoxCOMPorts.Text;
            int baudRate = Convert.ToInt32(ComboBaudRate.Text);

            if (String.IsNullOrEmpty(comPort))
            {
                comPort = "COM1";
            }

            gps.ComPort = comPort;
            gps.BaudRate = (BaudRates)baudRate;

            gps.Start();


            //if (!GPS.IsPortOpen)
            //{
            //    try
            //    {
            //        string comPort = ComboBoxCOMPorts.Text;
            //        int baudRate = Convert.ToInt32(ComboBaudRate.Text);

            //        if (String.IsNullOrEmpty(comPort))
            //        {
            //            comPort = "COM1";
            //        }

            //        GPS.Start(comPort, baudRate); //Open serial port comPort at baudRate //COM1 at 4800


            //    }
            //    catch (System.Exception ex)
            //    {
            //        writeExceptionToTextBox(ex);
            //        MessageBox.Show("An error occured when trying to open port: " + ex.Message);
            //    }
            //}
        }


        private void GPSTracka_Closing(object sender, CancelEventArgs e)
        {
            try
            {

                gps.Stop();

                //if (GPS != null && GPS.IsPortOpen)
                //{
                //    GPS.Stop();
                //}

                //if (GPS != null)
                //{
                //    GPS.Dispose();
                //}

                //Now save settings to file.
                string currentDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
                string settingsFile = currentDir + "\\settings.cfg";

                //com1,4800,300,True,True
                //COMPort,baud,seconds,totextbox,tologfile
                TextWriter writer = new StreamWriter(settingsFile, false);
                writer.WriteLine(ComboBoxCOMPorts.Text + "," + ComboBaudRate.Text + "," +
                    NumericUpDownInterval.Value.ToString() + "," + CheckBoxToTextBox.Checked.ToString() +
                    "," + CheckBoxToFile.Checked.ToString());
                writer.Close();

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
                //TODO: Use CeSetUserNotificationEx to request the app to run rather than keeping the device always on

                SystemIdleTimerReset();
            }
            catch (Exception)
            {
                // Nothing

            }

        }

        private void GPSTracka_Load(object sender, EventArgs e)
        {

            //Read from CFG file
            //com1,4800,300,True,True
            //COMPort,baud,seconds,totextbox,tologfile
            string currentDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);

            string settingsFile = currentDir + "\\settings.cfg";
            string settingsRawLine = String.Empty;
            string settingsCOMPort = String.Empty;
            string settingsBaudRate = String.Empty;
            int settingsSeconds = 60;
            bool settingsToTextBox = true;
            bool settingsToLogFile = false;

            try
            {
                if (!File.Exists(settingsFile))
                {
                    //Create it
                    StreamWriter stream = File.CreateText(settingsFile);
                    stream.WriteLine("COM1,4800,300,1,1");
                    stream.Close();
                    settingsFileExists = true;
                }
                else
                {
                    StreamReader stream = File.OpenText(settingsFile);
                    settingsRawLine = stream.ReadToEnd();
                    stream.Close();
                    settingsFileExists = true;
                }
            }
            catch (Exception ex)
            {
                settingsFileExists = false;
            }


            if (!String.IsNullOrEmpty(settingsRawLine))
            {
                string[] allSettings = settingsRawLine.Split(',');

                settingsCOMPort = allSettings[0];
                settingsBaudRate = allSettings[1];
                settingsSeconds = Int32.Parse(allSettings[2]);
                settingsToTextBox = Boolean.Parse(allSettings[3]);
                settingsToLogFile = Boolean.Parse(allSettings[4]);
            }


            TextBoxRawLog.ScrollBars = ScrollBars.Vertical;
            ComboBoxCOMPorts.SelectedIndex = 0;
            ComboBaudRate.SelectedIndex = 0;


            if (settingsFileExists)
            {

                if (!String.IsNullOrEmpty(settingsCOMPort))
                {
                    ComboBoxCOMPorts.SelectedItem = settingsCOMPort;
                }

                if (!String.IsNullOrEmpty(settingsBaudRate))
                {
                    ComboBoxCOMPorts.SelectedItem = settingsBaudRate;
                }

                NumericUpDownInterval.Value = settingsSeconds;
                CheckBoxToTextBox.Checked = settingsToTextBox;
                CheckBoxToFile.Checked = settingsToLogFile;
            }




            //GPS = new GPSHandler(this); //Initialize GPS handler
            //GPS.TimeOut = 50; //Set timeout to 5 seconds
            //GPS.NewGPSFix += new GPSHandler.NewGPSFixHandler(this.GPSEventHandler); //Hook up GPS data events to a handler

            //Call keepdeviceawake every 30 seconds in its own timer
            //Cannot use existing timer because it may have 5 minute intervals.
            preventSleepTimer = new System.Threading.Timer(new System.Threading.TimerCallback(KeepDeviceAwake), null, 0, 30000);

        }

        private void ButtonStartStop_Click(object sender, EventArgs e)
        {

            if (LogEverything)
            {
                gps.Stop();
                //GPS.Stop();
                LogEverything = false;
                timer1.Enabled = false;
                ButtonStartStop.Text = "Start";
                return;
            }

            timer1.Interval = Convert.ToInt16(NumericUpDownInterval.Value) * 1000;

            timer1.Enabled = !timer1.Enabled;

            if (timer1.Enabled)
            {
                WriteToTextbox("Will begin to read GPS data after " + NumericUpDownInterval.Value.ToString() + " seconds.");
                ButtonStartStop.Text = "Stop";
                ButtonLogAll.Enabled = false;
                CheckBoxToFile.Enabled = false;
                ComboBoxCOMPorts.Enabled = false;
                ComboBaudRate.Enabled = false;
                NumericUpDownInterval.Enabled = false;
            }
            else
            {
                gps.Stop();
                //GPS.Stop();
                ButtonLogAll.Enabled = true;
                CheckBoxToFile.Enabled = true;
                ComboBoxCOMPorts.Enabled = true;
                ComboBaudRate.Enabled = true;
                NumericUpDownInterval.Enabled = true;
                ButtonStartStop.Text = "Start";
            }
        }

        private void ButtonLogAll_Click(object sender, EventArgs e)
        {
            LogEverything = true;
            StartupGps();
            ButtonStartStop.Text = "STOP!";

        }

        private void TextBoxCls_Click(object sender, EventArgs e)
        {
            TextBoxRawLog.Text = string.Empty;
        }
    }
}