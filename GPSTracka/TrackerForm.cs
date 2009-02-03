using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using SharpGis.SharpGps;
using System.Runtime.InteropServices;
using System.Xml;
using Microsoft.WindowsCE.Forms;
using System.IO.Ports;



namespace GPSTracka
{


    public partial class GPSTracka : Form
    {




        [DllImport("CoreDll.dll")]
        private static extern void SystemIdleTimerReset();

        private static System.Threading.Timer preventSleepTimer = null;


        public static GPSHandler GPS;
        public static bool LogEverything = false; 
       


        public GPSTracka()
        {
            InitializeComponent();
          
            
            string[] portNames = SerialPort.GetPortNames();
            foreach (string port in portNames)
            {
                this.ComboBoxCOMPorts.Items.Add(port);
            }

        }

       

       


        private void GPSEventHandler(object sender, GPSHandler.GPSEventArgs e)
        {

            try
            {
                if (!LogEverything)
                {

                    switch (e.TypeOfEvent)
                    {

                        case GPSEventType.GPGLL:
                            if (GPS.GPGLL.DataValid)
                            {

                                string gpsData = DateTime.Now.ToString("HHmmss") + "," + Math.Round(GPS.GPGGA.Position.Latitude, 7).ToString() + "," + Math.Round(GPS.GPGGA.Position.Longitude, 7).ToString();

                                if (CheckBoxToTextBox.Checked)
                                {
                                    TextBoxRawLog.Text = TextBoxRawLog.Text + gpsData + "\r\n";
                                    TextBoxRawLog.SelectionStart = TextBoxRawLog.Text.Length - 1;
                                    TextBoxRawLog.SelectionLength = 0;
                                    TextBoxRawLog.ScrollToCaret();
                                }
                                if (CheckBoxToFile.Checked)
                                {
                                    //WriteToFile(gpsData);
                                    WriteToFile(Math.Round(GPS.GPGGA.Position.Latitude, 7), Math.Round(GPS.GPGGA.Position.Longitude, 7));
                                }
                                StopGps();
                            }
                            break;

                        case GPSEventType.GPGGA:
                            if (GPS.GPGGA.FixQuality != SharpGis.SharpGps.NMEA.GPGGA.FixQualityEnum.Invalid)
                            {

                                string gpsData = DateTime.Now.ToString("HHmmss") + "," + Math.Round(GPS.GPGGA.Position.Latitude, 7).ToString() + "," + Math.Round(GPS.GPGGA.Position.Longitude, 7).ToString();

                                if (CheckBoxToTextBox.Checked)
                                {
                                    TextBoxRawLog.Text = TextBoxRawLog.Text + gpsData + "\r\n";
                                    TextBoxRawLog.SelectionStart = TextBoxRawLog.Text.Length - 1;
                                    TextBoxRawLog.SelectionLength = 0;
                                    TextBoxRawLog.ScrollToCaret();
                                }
                                if (CheckBoxToFile.Checked)
                                {
                                    WriteToFile(Math.Round(GPS.GPGGA.Position.Latitude, 7), Math.Round(GPS.GPGGA.Position.Longitude, 7));
                                }

                                StopGps();
                            }

                            break;

                    }
                }
                else
                {
                    TextBoxRawLog.Text = TextBoxRawLog.Text + e.Sentence + "\r\n";
                    TextBoxRawLog.SelectionStart = TextBoxRawLog.Text.Length - 1;
                    TextBoxRawLog.SelectionLength = 0;
                    TextBoxRawLog.ScrollToCaret();
                }

            }
            catch (Exception ex)
            {
                writeExceptionToTextBox(ex);
            }




        }


        private void WriteToFile(double latitude, double longitude)
        {

            try
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
            catch (Exception ex)
            {
                writeExceptionToTextBox(ex);
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

        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            StartupGps();

            timer1.Enabled = false;

        }



        private void StopGps()
        {

            GPS.Stop(); //Close serial port
            timer1.Enabled = true;


        }

        private void StartupGps()
        {
            if (!GPS.IsPortOpen)
            {
                try
                {
                    string comPort = ComboBoxCOMPorts.Text;
                    int baudRate = Convert.ToInt32(ComboBaudRate.Text);

                    if (String.IsNullOrEmpty(comPort))
                    {
                        comPort = "COM1";
                    }

                    GPS.Start(comPort, baudRate); //Open serial port comPort at baudRate //COM1 at 4800


                }
                catch (System.Exception ex)
                {
                    writeExceptionToTextBox(ex);
                    MessageBox.Show("An error occured when trying to open port: " + ex.Message);
                }
            }
        }


        private void GPSTracka_Closing(object sender, CancelEventArgs e)
        {
            try
            {
                if (GPS != null && GPS.IsPortOpen)
                {
                    GPS.Stop();
                }

                if (GPS != null)
                {
                    GPS.Dispose();
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


            TextBoxRawLog.ScrollBars = ScrollBars.Vertical;

            ComboBoxCOMPorts.SelectedIndex = 0;
            ComboBaudRate.SelectedIndex = 0;

            GPS = new GPSHandler(this); //Initialize GPS handler
            GPS.TimeOut = 50; //Set timeout to 5 seconds
            GPS.NewGPSFix += new GPSHandler.NewGPSFixHandler(this.GPSEventHandler); //Hook up GPS data events to a handler

            //Call keepdeviceawake every 30 seconds in its own timer
            //Cannot use existing timer because it may have 5 minute intervals.
            preventSleepTimer = new System.Threading.Timer(new System.Threading.TimerCallback(KeepDeviceAwake), null, 0, 30000);

        }

        private void ButtonStartStop_Click(object sender, EventArgs e)
        {

            if (LogEverything)
            {
                GPS.Stop();
                LogEverything = false;
                timer1.Enabled = false;
                ButtonStartStop.Text = "Start";
                return;
            }

            timer1.Interval = Convert.ToInt16(NumericUpDownInterval.Value) * 1000;

            timer1.Enabled = !timer1.Enabled;

            if (timer1.Enabled)
            {
                ButtonStartStop.Text = "Stop";
                ButtonLogAll.Enabled = false;
                CheckBoxToFile.Enabled = false;
                ComboBoxCOMPorts.Enabled = false;
                ComboBaudRate.Enabled = false;
                NumericUpDownInterval.Enabled = false;
            }
            else
            {
                GPS.Stop();
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