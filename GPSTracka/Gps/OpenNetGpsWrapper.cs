using System;
using System.Collections.Generic;
using System.Text;
using GPSTracka;
using OpenNETCF.IO.Serial.GPS;
using OpenNetGps = OpenNETCF.IO.Serial.GPS;

namespace GPSTracka.Gps
{
    /// <summary>Wraps <see cref="OpenNetGps.GPS"/> as <see cref="GpsProvider"/></summary>
    public class OpenNetGpsWrapper : GpsProvider
    {
        /// <summary>Sets COM port GPS device is attached to</summary>
        public string Port { set { gps.ComPort = value; } }
        /// <summary>Sets ĆOM port baud rate</summary>
        public OpenNETCF.IO.Serial.BaudRates BaudRate { set { gps.BaudRate = value; } }
        /// <summary>The GPS instance</summary>
        private OpenNetGps.GPS gps = new OpenNetGps.GPS();
        /// <summary>CTor</summary>
        public OpenNetGpsWrapper()
        {
            gps.Error += gps_Error;
            gps.GpsCommState += gps_GpsCommState;
            gps.GpsSentence += gps_GpsSentence;
            gps.Position += gps_Position;
            gps.Satellite += gps_Satellite;
            gps.Movement += gps_Movement;
        }
        /// <summary>Starts GPS polling</summary>
        public override void Start() { gps.Start(); }
        /// <summary>Terminates GPS polling</summary>
        public override void Stop() { gps.Stop(); }
        /// <summary>Gets GPS device current state</summary>
        public override GpsState State
        {
            get { return State2State(gps.State); }
        }
        /// <summary>Converts <see cref="OpenNetGps.States"/> to <see cref="GpsState"/></summary>
        /// <param name="state">Value to be converted</param>
        /// <remarks>Value of type <see cref="GpsState"/> equivalent to <paramref name="state"/>. For <see cref="OpenNetGps.States.AutoDiscovery"/> and unknown values returns <see cref="GpsState.Closed"/>.</remarks>
        private static GpsState State2State(OpenNetGps.States state)
        {
            switch (state)
            {
                case OpenNetGps.States.Opening: return GpsState.Opening;
                case OpenNetGps.States.Running: return GpsState.Open;
                case OpenNetGps.States.Stopping: return GpsState.Closing;
                default: return GpsState.Closed;
            }
        }
        private void gps_Error(object sender, Exception exception, string message, string gps_data)
        {
            OnError(message, exception);
        }
        private void gps_GpsCommState(object sender, OpenNetGps.GpsCommStateEventArgs e)
        {
            OnStateChanged(State2State(e.State));
        }
        private void gps_GpsSentence(object sender, OpenNetGps.GpsSentenceEventArgs e)
        {
            OnGpsSentence(new GpsSentenceEventArgs(e.Counter, e.Sentence));
        }
        private void gps_Position(object sender, OpenNetGps.Position pos)
        {

            int latMultipler = (pos.DirectionLatitude == CardinalDirection.South) ? -1 : 1;
            int longMultiplier = (pos.DirectionLongitude == CardinalDirection.West) ? -1 : 1;

            OnPosition(new GpsPositionEventArgs(new GpsPosition(
                pos.Altitude == 0 ? null : new Decimal?(pos.Altitude),
                pos.Longitude_Decimal == 0 ? null : new Decimal?(pos.Longitude_Decimal * longMultiplier),
                pos.Latitude_Decimal == 0 ? null : new Decimal?(pos.Latitude_Decimal * latMultipler),
                pos.SatTime)));
        }
        private void gps_Satellite(object sender, OpenNetGps.Satellite[] satellites)
        {
            List<GpsSatellite> sats = new List<GpsSatellite>(satellites.Length);
            foreach (OpenNetGps.Satellite sat in satellites)
            {
                sats.Add(new GpsSatellite(sat.ID, sat.SNR, sat.Elevation, sat.Azimuth, sat.Active));
            }
            OnSatellite(new GpsSatelliteEventArgs(sats.ToArray()));
        }
        private void gps_Movement(object sender, OpenNETCF.IO.Serial.GPS.Movement mov)
        {
            OnMovement(new GpsMovementEventArgs(mov.SpeedKph));
        }

        /// <summary>Disposes the object</summary>
        public override void Dispose()
        {
            var wasDisposed = IsDisposed;
            base.Dispose();
            if (!wasDisposed)
            {
                gps.Error -= gps_Error;
                gps.GpsCommState -= gps_GpsCommState;
                gps.GpsSentence -= gps_GpsSentence;
                gps.Position -= gps_Position;
                gps.Satellite -= gps_Satellite;
                gps.Movement -= gps_Movement;                
                gps = null;
            }
        }
    }
}
