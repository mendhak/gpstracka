//using System;
//using System.Collections.Generic;
//using System.Runtime.InteropServices;
//using System.Text;

//namespace GPSTracka
//{
//    public static class UIHelper
//    {
//        [DllImport("AYGShell.dll")]
//        static extern Int32 SHFullScreen(IntPtr hwndRequester, UInt32 dwState);

//        public const UInt32 SHFS_SHOWTASKBAR = 0x0001;
//        public const UInt32 SHFS_HIDETASKBAR = 0x0002;

//        public const UInt32 SHFS_SHOWSIPBUTTON = 0x0004;
//        public const UInt32 SHFS_HIDESIPBUTTON = 0x0008;

//        public const UInt32 SHFS_SHOWSTARTICON = 0x0010;
//        public const UInt32 SHFS_HIDESTARTICON = 0x0020;

//        public static void ShowSIPButton(IntPtr requester)
//        {
//            SHFullScreen(requester, SHFS_SHOWSIPBUTTON);
//        }

//        public static void HideSIPButton(IntPtr requester)
//        {
//            SHFullScreen(requester, SHFS_HIDESIPBUTTON);
//        }
//    }
//}
