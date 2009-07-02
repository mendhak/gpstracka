using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace GPSTracka
{
    static class Program
    {
        ///// <summary>This function notifies Power Manager of the events required for implementing a power policy created by an OEM.</summary>
        ///// <param name="dwMessage">Set to one of the predefined PPN_* values, or a custom value.</param>
        ///// <param name="onOrOff">32-bit value that varies depending on the <paramref name="dwMessage"/> value.</param>
        ///// <returns>TRUE indicates success. FALSE indicates failure.</returns>
        [DllImport("CoreDLL")]
        public static extern int PowerPolicyNotify(PPNMessage dwMessage, int option);

// ReSharper disable InconsistentNaming
        public enum PPNMessage
        {
            PPN_REEVALUATESTATE = 1,
            PPN_POWERCHANGE = 2,
            PPN_UNATTENDEDMODE = 3,
            PPN_SUSPENDKEYPRESSED = 4,
            PPN_POWERBUTTONPRESSED = 4,
            PPN_SUSPENDKEYRELEASED = 5,
            PPN_APPBUTTONPRESSED = 6,
        }
// ReSharper restore InconsistentNaming

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main(string[] args)
        {
            AdvancedConfig.Load();
            if (!string.IsNullOrEmpty(AdvancedConfig.Language))
            {
                try
                {
                    CurrentUICulture = CultureInfo.GetCultureInfo(AdvancedConfig.Language);
                }
                catch
                {
                }
            }

            PowerPolicyNotify(PPNMessage.PPN_UNATTENDEDMODE, 1);

            //Check for auto start
            bool autoStart = false;
            if ((args != null) && (args.Length > 0))
            {
                foreach (string arg in args)
                {
                    if (String.Compare(arg, "-start", StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        autoStart = true;
                    }
                }
            }

            try
            {
                Application.Run(new TrackerForm(autoStart));
            }
            finally
            {
                PowerPolicyNotify(PPNMessage.PPN_UNATTENDEDMODE, -1);
            }
        }

        public static CultureInfo CurrentUICulture
        {
            get
            {
                return CultureInfo.CurrentUICulture;
            }
            set
            {
                typeof(CultureInfo).GetField("m_userDefaultUICulture", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
                        .SetValue(null, value);
            }
        }
    }
}