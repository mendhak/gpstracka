using System;

using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Globalization;

namespace GPSTracka
{
    static class Program
    {


        [DllImport("CoreDLL")]
        public static extern int PowerPolicyNotify(PPNMessage dwMessage, int option);

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



        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main()
        {
            AdvancedConfig.Load();
            if (!string.IsNullOrEmpty(AdvancedConfig.Language))
                try
                {
                    CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo(AdvancedConfig.Language);
                }
                catch { }
            PowerPolicyNotify(PPNMessage.PPN_UNATTENDEDMODE, 1);
            try
            {
                Application.Run(new TrackerForm());
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