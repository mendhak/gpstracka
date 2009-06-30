using System;
using OpenNetGps = OpenNETCF.IO.Serial.GPS;
using MSGps = Microsoft.WindowsMobile.Samples.Location;

using System.Collections.Generic;
using System.Text;
using GPSTracka;
using OpenNETCF.IO.Serial.GPS;

namespace GPSTracka.Gps
{
    #region "General GPS"
    /// <summary>Base class for Gps driver providers</summary>
    public abstract class GpsProvider
    {
        /// <summary>When overriden in derived class starts GPS polling</summary>
        public abstract void Start();
        /// <summary>When overriden in derived class stops GPS polling</summary>
        public abstract void Stop();
        /// <summary>Raised when event occurs</summary>
        public event GpsErrorEventHandler Error;
        /// <summary>Raises the <see cref="Error"/> event</summary>
        /// <param name="message">Error message</param>
        /// <param name="ex">Exception itself. May be null.</param>
        protected virtual void OnError(string message, Exception ex)
        {
            if (Error != null) Error(this, message, ex);
        }
        /// <summary>Raised when GPS satellite information is received</summary>
        public event GpsSatelliteEventHandler Satellite;
        /// <summary>Raises the <see cref="Satellite"/> event</summary>
        /// <param name="e">Event arguments</param>
        protected virtual void OnSatellite(GpsSatelliteEventArgs e)
        {
            if (Satellite != null) Satellite(this, e);
        }
        /// <summary>Raised when GPS position information is received</summary>
        public event GpsPositionEventHandler Position;
        /// <summary>Raises the <see cref="Position"/> event</summary>
        /// <param name="e">Event arguments</param>
        protected virtual void OnPosition(GpsPositionEventArgs e)
        {
            if (Position != null) Position(this, e);
        }
        /// <summary>Raised when GPS state changes</summary>
        public event GpsStateEventHandler StateChanged;
        /// <summary>Raises the <see cref="StateChanged"/> event</summary>
        /// <param name="state">A new state</param>
        protected virtual void OnStateChanged(GpsState state)
        {
            if (StateChanged != null) StateChanged(this, state);
        }
        /// <summary>Raised whenever raw GPS sentence is received</summary>
        /// <remarks>Derived class may or may not raise this event depending on if it has access to raw GPS data</remarks>
        public event GpsSentenceEventHandler GpsSentence;
        /// <summary>Raises the <see cref="GpsSentence"/> event</summary>
        /// <param name="e">Event arguments</param>
        protected virtual void OnGpsSentence(GpsSentenceEventArgs e)
        {
            if (GpsSentence != null) GpsSentence(this, e);
        }
        /// <summary>When overriden in derived class gets current GPS state</summary>
        public abstract GpsState State { get; }
        /// <summary>Raised when GPS movement data are received</summary>
        public event MovementEventHandler Movement;
        /// <summary>Raises the <see cref="Movement"/> event</summary>
        /// <param name="e">Event arguments</param>
        protected virtual void OnMovement(GpsMovementEventArgs e)
        {
            if (Movement != null) Movement.Invoke(this, e);
        }
    }
    /// <summary>Event handler for the <see cref="GpsProvider.Error"/> event</summary>
    /// <param name="sender">The source of the event</param>
    /// <param name="message">Error message</param>
    /// <param name="ex">Exception itself. May be null.</param>
    public delegate void GpsErrorEventHandler(GpsProvider sender, string message, Exception ex);
    /// <summary>Event handler for the <see cref="GpsProvider.Satellite"/> event</summary>
    /// <param name="sender">The source of the event</param>
    /// <param name="e">Event arguments</param>
    public delegate void GpsSatelliteEventHandler(GpsProvider sender, GpsSatelliteEventArgs e);
    /// <summary>Event handler for the <see cref="GpsProvider.Position"/> event</summary>
    /// <param name="sender">The source of the event</param>
    /// <param name="e">Event arguments</param>
    public delegate void GpsPositionEventHandler(GpsProvider sender, GpsPositionEventArgs e);
    /// <summary>Event handler for the <see cref="GpsProvider.StateChanged"/> event</summary>
    /// <param name="sender">The source of the event</param>
    /// <param name="state">A new state</param>
    public delegate void GpsStateEventHandler(GpsProvider sender, GpsState state);
    /// <summary>Event handler for the <see cref="GpsProvider.GpsSentence"/> event</summary>
    /// <param name="sender">The source of the event</param>
    /// <param name="e">Event arguments</param>
    public delegate void GpsSentenceEventHandler(GpsProvider sender, GpsSentenceEventArgs e);
    /// <summary>Event arguments of the <see cref="GpsProvider.Satellite"/> event</summary>
    public class GpsSatelliteEventArgs
    {
        /// <summary>Contains value of the <see cref="Satellites"/> property</summary>
        private GpsSatellite[] satellites;
        /// <summary>Gets information about GPS satellites</summary>
        public GpsSatellite[] Satellites
        {
            get
            {
                return satellites;
            }
        }
        /// <summary>CTor</summary>
        /// <param name="satellites">Information about GPS satellites</param>
        /// <exception cref="ArgumentNullException"><paramref name="satellites"/> is null</exception>
        public GpsSatelliteEventArgs(params GpsSatellite[] satellites)
        {
            if (satellites == null) throw new ArgumentNullException("satellites");
            this.satellites = satellites;
        }
    }
    /// <summary>Arguments of the <see cref="GpsProvider.Position"/> event</summary>
    public class GpsPositionEventArgs
    {
        /// <summary>Contains value of the <see cref="Position"/> property</summary>
        private GpsPosition position;
        /// <summary>CTor</summary>
        /// <param name="position">Current GPS posotion</param>
        public GpsPositionEventArgs(GpsPosition position) { this.position = position; }
        /// <summary>Gets current GPS position infromation</summary>
        public GpsPosition Position
        {
            get { return position; }
        }
    }
    /// <summary>Represents GPS position information</summary>
    public struct GpsPosition
    {
        /// <summary>Contains value of the <see cref="Latitude"/> property</summary>
        private decimal? latitude;
        /// <summary>Gets or sets current latitude (north+ or south-) information in degrees.</summary>
        /// <value>Value can be null if information is not available</value>
        public decimal? Latitude
        {
            get { return latitude; }
            set { latitude = value; }
        }
        /// <summary>Contains value of the <see cref="Altitude"/> property</summary>
        private decimal? altitude;
        /// <summary>Gets or sets current altitude information in meters</summary>
        /// <value>Value can be null if information is not available</value>
        public decimal? Altitude
        {
            get { return altitude; }
            set { altitude = value; }
        }
        /// <summary>Contains value of the <see cref="Longitude"/> property</summary>
        private decimal? longitude;
        /// <summary>Gets or sets current longitude (west- or east+) information in degrees</summary>
        /// <value>Value can be null if information is not available</value>
        public decimal? Longitude
        {
            get { return longitude; }
            set { longitude = value; }
        }
        /// <summary>Contains value of the <see cref="GpsTime"/> property</summary>
        private DateTime gpsTime;
        /// <summary>Gets GPS time from satellite in UTC</summary>
        public DateTime GpsTime
        {
            get { return gpsTime; }
            set { gpsTime = value; }
        }



        /// <summary>CTor</summary>
        /// <param name="altitude">Altitude information in meters. Null if not available.</param>
        /// <param name="longitude">Longitude (west or east) information in degrees. Null if not available.</param>
        /// <param name="latitude">Latitude (north or south) information in degrees. Null if not available.</param>
        /// <param name="gpsTime">Current GPS time in UTC</param>
        public GpsPosition(decimal? altitude, decimal? longitude, decimal? latitude, DateTime gpsTime)
        {
            this.altitude = altitude;
            this.longitude = longitude;
            this.latitude = latitude;
            this.gpsTime = gpsTime;

        }
        /// <summary>Calculates distance of two positions in given unit</summary>
        /// <param name="pos1">A position</param>
        /// <param name="pos2">A position</param>
        /// <param name="unit">The unit to get result in</param>
        /// <returns>Distance between given points in given unit; 0 if <paramref name="unit"/> is not one of the <see cref="OpenNetGps.Units"/> values.</returns>
        /// <exception cref="ArgumentException"><see cref="Latitude"/> or <paramref name="Longitude"/> of <paramref name="pos1"/> or <paramref name="pos2"/> is null.</exception>
        public static decimal CalculateDistance(GpsPosition pos1, GpsPosition pos2, OpenNetGps.Units unit)
        {
            if (!pos1.Longitude.HasValue || !pos1.Latitude.HasValue) throw new ArgumentException(GPSTracka.Properties.Resources.err_PositionIsNull, "pos1");
            if (!pos2.Longitude.HasValue || !pos2.Latitude.HasValue) throw new ArgumentException(GPSTracka.Properties.Resources.err_PositionIsNull, "pos2");
            var ret = GpsPoint.CalculateDistance(new GpsPoint(pos1.Latitude.Value, pos1.Longitude.Value, pos1.Altitude), new GpsPoint(pos2.Latitude.Value, pos1.Longitude.Value, pos2.Altitude));
            switch (unit)
            {
                case OpenNETCF.IO.Serial.GPS.Units.Kilometers: return ret;
                case OpenNETCF.IO.Serial.GPS.Units.Knots: return ret / 1.85200m;
                case OpenNETCF.IO.Serial.GPS.Units.Miles: return ret / 1.609344m;
                default: throw new ArgumentException("Invalid enum argument");
            }
        }
    }
    /// <summary>Arguments of the <see cref="GpsProvider.GpsSentence"/> event</summary>
    public class GpsSentenceEventArgs
    {
        /// <summary>CTor</summary>
        /// <param name="sentence">GPS sentence string</param>
        /// <param name="counter">GPS sentence counter</param>
        public GpsSentenceEventArgs(int counter, string sentence) { this.counter = counter; this.sentence = sentence; }
        /// <summary>Contains value of the sentence property</summary>
        private string sentence;
        /// <summary>Contains value of the <see cref="Counter"/> property</summary>
        private int counter;
        /// <summary>Gets GPS sentence counter value</summary>
        public int Counter { get { return counter; } }
        /// <summary>Gets raw GPS sentence data</summary>
        public string Sentence
        {
            get { return sentence; }
        }
    }
    /// <summary>States of GPS device</summary>
    public enum GpsState
    {
        /// <summary>Device is closed - not running, not raising events</summary>
        Closed,
        /// <summary>Device is opened - running, raising events</summary>
        Open,
        /// <summary>Device is opening - preparing to run</summary>
        Opening,
        /// <summary>Device is being closed (stopping)</summary>
        Closing
    }
    /// <summary>Contains information about GPS satellite</summary>
    public struct GpsSatellite
    {
        /// <summary>Contains value of the <see cref="ID"/> property</summary>
        private int id;
        /// <summary>CTor</summary>
        /// <param name="ID">Satellite ID</param>
        /// <param name="signalToNoiseRatio">Satelite signal-to-noise ratio</param>
        /// <param name="elevation">Satellite elevation</param>
        /// <param name="azimuth">Satellite azimuth</param>
        /// <param name="active">Determines if satelite is actively used by the GPS device to resolve position</param>
        public GpsSatellite(int ID, int signalToNoiseRatio, int elevation, int azimuth, bool active)
        {
            this.id = ID;
            this.signalToNoiseRatio = signalToNoiseRatio;
            this.elevation = elevation;
            this.azimuth = azimuth;
            this.active = active;
        }
        /// <summary>Gets or sets satellite ID</summary>
        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        /// <summary>Contains value of the <see cref="SignalToNoiseRatio"/> property</summary>
        private int signalToNoiseRatio;
        /// <summary>Gets or sets satellite signal-to-noise ratio</summary>
        /// <remarks>Higher value means better signal</remarks>
        public int SignalToNoiseRatio
        {
            get { return signalToNoiseRatio; }
            set { signalToNoiseRatio = value; }
        }
        /// <summary>Contains value of the <see cref="Azimuth"/> property</summary>
        private int azimuth;
        /// <summary>Gets or sets GPS satellite azimuth</summary>
        public int Azimuth
        {
            get { return azimuth; }
            set { azimuth = value; }
        }
        /// <summary>Contains value of the elevation property</summary>
        private int elevation;
        /// <summary>Gets or sets GPS satellite elevation</summary>
        public int Elevation
        {
            get { return elevation; }
            set { elevation = value; }
        }
        /// <summary>Contains value of the <see cref="Active"/> property</summary>
        private bool active;
        /// <summary>Gets or sets value indicating if the satellite is actively used by GPS device to resolve its position</summary>
        public bool Active
        {
            get { return active; }
            set { active = value; }
        }
    }
    /// <summary>Gps movement event arguments</summary>
    public class GpsMovementEventArgs : EventArgs
    {
        /// <summary>Movement speed (in km/h)</summary>
        public decimal Speed { get; set; }
        /// <summary>CTor</summary>
        /// <param name="speed">Speed in km/h</param>
        public GpsMovementEventArgs(decimal speed)
        {
            Speed = speed;
        }
    }
    /// <summary>Delegate for the <see cref="GpsProvider.Movement"/> event</summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">Event arguments</param>
    public delegate void MovementEventHandler(GpsProvider sender, GpsMovementEventArgs e);
    #endregion
    #region "OpenNet GPS"
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
    }
    #endregion
    #region "Microsoft.Location"
    /// <summary>Wraps Microsft GPS Intermediate driver</summary>
    public class MSGpsWrapper : GpsProvider
    {
        /// <summary>Driver managed wrapper instance</summary>
        private MSGps.Gps gps = new MSGps.Gps();
        /// <summary>CTor</summary>
        public MSGpsWrapper()
        {
            gps.DeviceStateChanged += new Microsoft.WindowsMobile.Samples.Location.DeviceStateChangedEventHandler(gps_DeviceStateChanged);
            gps.LocationChanged += new Microsoft.WindowsMobile.Samples.Location.LocationChangedEventHandler(gps_LocationChanged);
        }
        /// <summary>Starts aquiring GPS data</summary>
        public override void Start()
        {
            gps.Open();
            OnStateChanged(State);
        }
        /// <summary>Stops GPS</summary>
        public override void Stop()
        {
            gps.Close();
            OnStateChanged(State);
        }
        /// <summary>Gets GPS state</summary>
        /// <returns><see cref="GpsState.Open"/> or <see cref="GpsState.Closed"/></returns>
        public override GpsState State
        {
            get { return gps.Opened ? GpsState.Open : GpsState.Closed; }
        }
        private void gps_LocationChanged(object sender, Microsoft.WindowsMobile.Samples.Location.LocationChangedEventArgs args)
        {
            if (args.Position == null) return;
            OnPosition(new GpsPositionEventArgs(new GpsPosition(
                AdvancedConfig.SeaLevelAltitude ?
                    args.Position.EllipsoidAltitudeValid ? new decimal?((decimal)args.Position.EllipsoidAltitude) : null :
                    args.Position.SeaLevelAltitudeValid ? new decimal?((decimal)args.Position.SeaLevelAltitude) : null,
                args.Position.LongitudeValid ? new decimal?((decimal)args.Position.Longitude) : null,
                args.Position.LatitudeValid ? new decimal?((decimal)args.Position.Latitude) : null,
                args.Position.TimeValid ? args.Position.Time : DateTime.MinValue)));
            if (args.Position.SatellitesInViewValid)
            {
                List<int> usedSats = new List<int>();
                if (args.Position.SatellitesInSolutionValid)
                    foreach (var sat in args.Position.GetSatellitesInSolution())
                        usedSats.Add(sat.Id);
                List<GpsSatellite> sats = new List<GpsSatellite>(args.Position.SatellitesInViewCountValid ? args.Position.SatellitesInViewCount : 3);
                var visibleSats = args.Position.GetSatellitesInView();
                if (visibleSats != null)
                {
                    foreach (var sat in visibleSats)
                    {
                        sats.Add(new GpsSatellite(sat.Id, sat.SignalStrength, sat.Elevation, sat.Azimuth, usedSats.Contains(sat.Id)));
                    }
                }
                OnSatellite(new GpsSatelliteEventArgs(sats.ToArray()));
            }
            if (args.Position.SpeedValid)
            {
                OnMovement(new GpsMovementEventArgs((decimal)args.Position.Speed * (decimal)1.852));
            }
        }
        GpsServiceState oldState = GpsServiceState.Unknown;
        private void gps_DeviceStateChanged(object sender, Microsoft.WindowsMobile.Samples.Location.DeviceStateChangedEventArgs args)
        {
            if (oldState == args.DeviceState.ServiceState) return;
            //switch (args.DeviceState.ServiceState) {
            //    case GpsServiceState.Uninitialized:
            //    case GpsServiceState.Unknown :
            //    case GpsServiceState.Off: OnStateChanged(GpsState.Closed);
            //        break;
            //    case GpsServiceState.On: OnStateChanged(GpsState.Open);
            //        break;
            //    case GpsServiceState.Unloading :
            //    case GpsServiceState.ShuttingDown:OnStateChanged(GpsState.Closing);
            //        break;
            //    case GpsServiceState.StartingUp: OnStateChanged(GpsState.Opening);
            //        break;
            //}
            oldState = args.DeviceState.ServiceState;
        }
    }
    #endregion
}
