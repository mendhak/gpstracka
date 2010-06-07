using System;

using System.Collections.Generic;
using System.Text;
using System.Drawing;
using GPSTracka.Properties;

namespace GPSTracka
{

    /// <summary>Stores, loads and saves advanced settings</summary>
    internal static class AdvancedConfig
    {
        static AdvancedConfig()
        {
            //Set defaults.

            KmlNameFormat = KmlDescFormat = Resources.format_KmlDescDefault;
            TextLogFormat = Resources.format_TextLogDefault;
            ComPort = "COM1";
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
            KmlLineColor = Color.FromArgb(0, 0, 0xff);
            InvalidPositionsMax = 500;
            NmeaLog = false;
            KeepAwakeList = new string[] { "GPD0", "GPS0" };
            SpeedUnit = SpeedUnit.kmh;
            DistanceUnit = DistanceUnit.km;
            ElevationUnit = ElevationUnit.m;
            CsvQualifierUsage = CSVQualifierUsage.AsNeeded;
        }
        /// <summary>Loads advanced settings from <see cref="ConfigurationManager"/></summary>
        public static void Load()
        {
            //Read from config file

            ComPort = ConfigurationManager.AppSettings["COMPort"];

            if (String.IsNullOrEmpty(ComPort))
            {
                ComPort = "COM1";
            }

            try
            {
                BaudRate = (OpenNETCF.IO.Serial.BaudRates)Enum.Parse(typeof(OpenNETCF.IO.Serial.BaudRates), ConfigurationManager.AppSettings["BaudRate"], true);
            }
            catch
            {
                BaudRate = OpenNETCF.IO.Serial.BaudRates.CBR_4800;
            }

            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["UseWindowsDriver"]))
            {
                UseWindowsDriver = Convert.ToBoolean(ConfigurationManager.AppSettings["UseWindowsDriver"]);
            }

            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["PollingInterval"]))
            {
                PollingInterval = Int32.Parse(ConfigurationManager.AppSettings["PollingInterval"], System.Globalization.CultureInfo.InvariantCulture);
            }

            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["LogToTextBox"]))
            {
                LogToTextBox = Convert.ToBoolean(ConfigurationManager.AppSettings["LogToTextBox"]);
            }

            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["LogToFile"]))
            {
                LogToLogFile = Convert.ToBoolean(ConfigurationManager.AppSettings["LogToFile"]);
            }

            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["LogFileLocation"]))
            {
                LogFileLocation = ConfigurationManager.AppSettings["LogFileLocation"];
            }

            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["LogAltitude"]))
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

            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["UseGPSTime"]))
            {
                UseGpsTime = Convert.ToBoolean(ConfigurationManager.AppSettings["UseGPSTime"]);
            }

            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["StatusBar"]))
            {
                StatusBar = Convert.ToBoolean(ConfigurationManager.AppSettings["StatusBar"]);
            }

            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["MaxLogLength"]))
            {
                MaxLogLength = Int32.Parse(ConfigurationManager.AppSettings["MaxLogLength"], System.Globalization.CultureInfo.InvariantCulture);
            }


            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["AltitudeCorrection"]))
            {
                AltitudeCorrection = Int32.Parse(ConfigurationManager.AppSettings["AltitudeCorrection"], System.Globalization.CultureInfo.InvariantCulture);
            }

            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["StartImmediatelly"]))
            {
                StartImmediatelly = Convert.ToBoolean(ConfigurationManager.AppSettings["StartImmediatelly"]);
            }

            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["KMLNameFormat"]))
            {
                KmlNameFormat = ConfigurationManager.AppSettings["KMLNameFormat"];
            }

            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["KMLDescFormat"]))
            {
                KmlDescFormat = ConfigurationManager.AppSettings["KMLDescFormat"];
            }

            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["KMLLineColor"]))
            {
                KmlLineColor = Color.FromArgb(Int32.Parse(ConfigurationManager.AppSettings["KMLLineColor"], System.Globalization.CultureInfo.InvariantCulture));
            }

            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["MinimalDistance"]))
            {

                MinimalDistance = Int32.Parse(ConfigurationManager.AppSettings["MinimalDistance"], System.Globalization.CultureInfo.InvariantCulture);
            }


            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["TextLogFormat"]))
            {
                TextLogFormat = ConfigurationManager.AppSettings["TextLogFormat"];
            }

            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["InvalidPositionsMax"]))
            {
                InvalidPositionsMax = Int32.Parse(ConfigurationManager.AppSettings["InvalidPositionsMax"], System.Globalization.CultureInfo.InvariantCulture);
            }

            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["NMEALog"]))
            {
                NmeaLog = Convert.ToBoolean(ConfigurationManager.AppSettings["NMEALog"]);
            }

            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["SeaLevelAltitude"]))
            {
                SeaLevelAltitude = Convert.ToBoolean(ConfigurationManager.AppSettings["SeaLevelAltitude"]);
            }

            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["InfoPane"]))
            {
                InfoPane = Convert.ToBoolean(ConfigurationManager.AppSettings["InfoPane"]);
            }

            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["BeepTimer"]))
            {
                BeepTimer = Int32.Parse(ConfigurationManager.AppSettings["BeepTimer"], System.Globalization.CultureInfo.InvariantCulture);
            }

            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["MainFormBottomEmptySpace"]))
            {
                MainFormBottomEmptySpace = Int32.Parse(ConfigurationManager.AppSettings["MainFormBottomEmptySpace"], System.Globalization.CultureInfo.InvariantCulture);
            }

            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["KeepAwake"]))
            {
                KeepAwakeList = ConfigurationManager.AppSettings["KeepAwake"].Split(' ');
            }

            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["SpeedUnit"]))
            {
                SpeedUnit = (SpeedUnit)Enum.Parse(typeof(SpeedUnit), ConfigurationManager.AppSettings["SpeedUnit"], true);
            }

            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["DistanceUnit"]))
            {
                DistanceUnit = (DistanceUnit)Enum.Parse(typeof(DistanceUnit), ConfigurationManager.AppSettings["DistanceUnit"], true);
            }

            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["ElevationUnit"]))
            {
                ElevationUnit = (ElevationUnit)Enum.Parse(typeof(ElevationUnit), ConfigurationManager.AppSettings["ElevationUnit"], true);
            }



            CsvSeparator = String.IsNullOrEmpty(ConfigurationManager.AppSettings["CSVSeparator"]) ? ',' : ConfigurationManager.AppSettings["CSVSeparator"][0];
            CsvTextQualifier = String.IsNullOrEmpty(ConfigurationManager.AppSettings["CSVTextQualifier"]) ? '"' : ConfigurationManager.AppSettings["CSVTextQualifier"][0];

            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["CSVQualifierUsage"]))
            {
                CsvQualifierUsage = (CSVQualifierUsage)int.Parse(ConfigurationManager.AppSettings["CSVQualifierUsage"], System.Globalization.CultureInfo.InvariantCulture);
            }

            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["CSVHeader"]))
            {
                CsvHeader = ConfigurationManager.AppSettings["CSVHeader"];
            }

            CsvFields = String.IsNullOrEmpty(ConfigurationManager.AppSettings["CSVFields"]) ? "3012" : ConfigurationManager.AppSettings["CSVFields"];
            CsvNewLine = String.IsNullOrEmpty(ConfigurationManager.AppSettings["CSVNewLine"]) ? "\r\n" : ConfigurationManager.AppSettings["CSVNewLine"];
            CsvDateFormat = String.IsNullOrEmpty(ConfigurationManager.AppSettings["CSVDateFormat"]) ? "yyyy-MM-dd HH:mm:ss" : ConfigurationManager.AppSettings["CSVDateFormat"];

            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["CSVUTC"]))
            {
                CsvUtc = Convert.ToBoolean(ConfigurationManager.AppSettings["CSVUTC"]);
            }

            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["Language"]))
            {
                Language = ConfigurationManager.AppSettings["Language"];
            }
        }
        /// <summary>Stores advanced settings to <see cref="ConfigurationManager"/></summary>
        public static void Store()
        {
            ConfigurationManager.AppSettings["COMPort"] = ComPort;
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
            ConfigurationManager.AppSettings["KMLNameFormat"] = KmlNameFormat;
            ConfigurationManager.AppSettings["KMLDescFormat"] = KmlDescFormat;
            ConfigurationManager.AppSettings["KMLLineColor"] = KmlLineColor.ToArgb().ToString(System.Globalization.CultureInfo.InvariantCulture);
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
            ConfigurationManager.AppSettings["CSVSeparator"] = CsvSeparator.ToString();
            ConfigurationManager.AppSettings["CSVTextQualifier"] = CsvTextQualifier.ToString();
            ConfigurationManager.AppSettings["CSVNewLine"] = CsvNewLine;
            ConfigurationManager.AppSettings["CSVQualifierUsage"] = ((int)CsvQualifierUsage).ToString(System.Globalization.CultureInfo.InvariantCulture);
            ConfigurationManager.AppSettings["CSVHeader"] = CsvHeader;
            ConfigurationManager.AppSettings["CSVFields"] = CsvFields;
            ConfigurationManager.AppSettings["CSVDateFormat"] = CsvDateFormat;
            ConfigurationManager.AppSettings["CSVUTC"] = CsvUtc ? "true" : "false";
            ConfigurationManager.AppSettings["Language"] = Language;
            ConfigurationManager.AppSettings["MainFormBottomEmptySpace"] = MainFormBottomEmptySpace.ToString(System.Globalization.CultureInfo.InvariantCulture);
        }

        /// <summary>Gets or sets COM port name to get GPS data from</summary>
        /// <remarks>Ignored when <see cref="UseWindowsDriver"/> is true</remarks>
        public static string ComPort { get; set; }
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
        public static string KmlNameFormat { get; set; }

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
        public static string KmlDescFormat { get; set; }

        /// <summary>Gets or sets color used for KML line</summary>
        public static Color KmlLineColor { get; set; }

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
        public static char CsvSeparator { get; set; }

        /// <summary>CSV text qualifier</summary>
        public static char CsvTextQualifier { get; set; }

        /// <summary>When to use CSV text qualifier</summary>
        public static CSVQualifierUsage CsvQualifierUsage { get; set; }

        /// <summary>CSV new linestring</summary>
        public static string CsvNewLine { get; set; }

        /// <summary>CSV file header</summary>
        public static string CsvHeader { get; set; }

        /// <summary>Order of SCV fileds - single-digits from 0 to 3.</summary>
        /// <remarks>
        /// <list type="table"><listheader><term>Number</term><description>Value</description></listheader>
        /// <item><term>0</term><description>Latitude</description></item>
        /// <item><term>1</term><description>Longitude</description></item>
        /// <item><term>2</term><description>Altitude</description></item>
        /// <item><term>3</term><description>Date</description></item>
        /// </list></remarks>
        public static string CsvFields { get; set; }

        /// <summary>Format of date in CSV file</summary>
        public static string CsvDateFormat { get; set; }

        /// <summary>Use UTC time in CSV file</summary>
        public static bool CsvUtc { get; set; }

        /// <summary>Langauge (culture name)</summary>
        public static string Language { get; set; }

        /// <summary>Gest or sets height of empty space (in pixels) at bottom of main form</summary>
        public static int MainFormBottomEmptySpace { get; set; }

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
