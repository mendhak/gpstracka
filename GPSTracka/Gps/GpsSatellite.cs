using System;

using System.Collections.Generic;
using System.Text;

namespace GPSTracka.Gps
{
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
}
