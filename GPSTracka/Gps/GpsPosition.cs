using System;

using System.Collections.Generic;
using System.Text;

namespace GPSTracka.Gps
{
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
        /// <returns>Distance between given points in given unit; 0 if <paramref name="unit"/> is not one of the <see cref="OpenNETCF.IO.Serial.GPS.Units"/> values.</returns>
        /// <exception cref="ArgumentException"><see cref="Latitude"/> or <paramref name="Longitude"/> of <paramref name="pos1"/> or <paramref name="pos2"/> is null.</exception>
        public static decimal CalculateDistance(GpsPosition pos1, GpsPosition pos2, OpenNETCF.IO.Serial.GPS.Units unit)
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
}
