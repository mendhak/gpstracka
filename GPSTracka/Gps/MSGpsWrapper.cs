using System;
using System.Collections.Generic;
using System.Text;
using GPSTracka;
using MSGps = Microsoft.WindowsMobile.Samples.Location;

namespace GPSTracka.Gps
{
    /// <summary>Wraps Microsft GPS Intermediate driver</summary>
    public class MSGpsWrapper : GpsProvider
    {
        /// <summary>Driver managed wrapper instance</summary>
        private MSGps.Gps gps = new MSGps.Gps();
        /// <summary>CTor</summary>
        public MSGpsWrapper()
        {
            gps.DeviceStateChanged += gps_DeviceStateChanged;
            gps.LocationChanged += gps_LocationChanged;
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

        /// <summary>Disposes the object</summary>
        public override void Dispose()
        {
            var wasDisposed = IsDisposed;
            base.Dispose();
            if (!wasDisposed)
            {
                gps.DeviceStateChanged -= gps_DeviceStateChanged;
                gps.LocationChanged -= gps_LocationChanged;
                gps = null;
            }
        }
    }
}
