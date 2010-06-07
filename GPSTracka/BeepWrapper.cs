using System.Runtime.InteropServices;
 
namespace GPSTracka
{
    /// <summary>
    /// This class encapsulates the MessageBeep functionality for
    /// the compact framework.
    /// </summary>
    public class BeepWrapper
    {
 
        [DllImport("Coredll.dll")]
        private static extern void MessageBeep(int flags);
 
        /// <summary>
        /// Types of beep alerts
        /// </summary>
        public enum BeepAlert
        {
            /// <summary>Hand, stop or critical error</summary>
            Hand = 16,
            /// <summary>Question</summary>
            Question = 32,
            /// <summary>Warning</summary>
            Exclamation = 48,
            /// <summary>Another type</summary>
            Asterisk = 64,
        }
 
        /// <summary>
        /// Executes a general beep, statically
        /// </summary>
        public static void Beep()
        {
            Beep(BeepAlert.Hand);
        }
 
        /// <summary>
        /// Execute a the given alert
        /// </summary>
        /// <param name="alert">Type of alert</param>
        public static void Beep(BeepAlert alert)
        {
            MessageBeep((int)alert);
        }
 
        /// <summary>
        /// Execute an error alert
        /// </summary>
        public static void BeepError()
        {
            Beep(BeepAlert.Exclamation);
        }
    }
}
