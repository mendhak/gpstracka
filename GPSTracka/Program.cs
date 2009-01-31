using System;

using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace GPSTracka
{
    static class Program
    {


        [DllImport("CoreDLL")]public static extern int PowerPolicyNotify(PPNMessage dwMessage, int option);

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
            PowerPolicyNotify(PPNMessage.PPN_UNATTENDEDMODE, 1);
            Application.Run(new GPSTracka());
            PowerPolicyNotify(PPNMessage.PPN_UNATTENDEDMODE, -1);
        }
    }
}