using System;

using System.Collections.Generic;
using System.Text;

namespace GPSTracka
{
    /// <summary>GPS position</summary>
    internal struct GpsPoint
    {
        /// <summary>Latitude +North/-South</summary>
        public decimal Latitude { get; set; }
        /// <summary>Longitude -West/+East</summary>
        public decimal Longitude { get; set; }
        /// <summary>Altitude (above sea level or geoid)</summary>
        public decimal? Altitude { get; set; }
        /// <summary>CTor</summary>
        /// <param name="latitude">Latitude +North/-South</param>
        /// <param name="longitude">Logitude (-West/+East)</param>
        /// <param name="altitude">Altitude (above sea level or geoid)</param>
        public GpsPoint(decimal latitude, decimal longitude, decimal? altitude)
            : this()
        {
            Latitude = latitude;
            Longitude = longitude;
            Altitude = altitude;
        }
        /// <summary>Calculates distance of two positions in km</summary>
        /// <param name="pos1">A position</param>
        /// <param name="pos2">A position</param>
        /// <returns>Distance between given points in km.</returns>
        public static decimal CalculateDistance(GpsPoint pos1, GpsPoint pos2)
        {
            /*double num = ((double)pos2.Latitude - (double)pos1.Latitude) * (Math.PI / 180.0);
            double num2 = ((double)pos2.Longitude - (double)pos1.Longitude) * (Math.PI * 180.0);
            double a = (Math.Sin(num / 2.0) * Math.Sin(num / 2.0)) + (((Math.Cos((double)pos1.Latitude * (Math.PI / 180.0)) * Math.Cos((double)pos2.Latitude * (Math.PI * 180.0))) * Math.Sin(num2 / 2.0)) * Math.Sin(num2 / 2.0));
            if (a < 0) return 0;
            double num4 = 2.0 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1.0 - a));
            return (decimal)(6371 * num4);*/
            return (decimal)Calc((double)pos1.Latitude, (double)pos1.Longitude, (double)pos2.Latitude, (double)pos2.Longitude);
        }

        private static double Calc(double firstLatitude, double firstLongitude, double secondLatitude, double secondLongitude)
        {
            /*
                The Haversine formula according to Dr. Math.
                http://mathforum.org/library/drmath/view/51879.html
                
                dlon = lon2 - lon1
                dlat = lat2 - lat1
                a = (sin(dlat/2))^2 + cos(lat1) * cos(lat2) * (sin(dlon/2))^2
                c = 2 * atan2(sqrt(a), sqrt(1-a)) 
                d = R * c
                
                Where
                    * dlon is the change in longitude
                    * dlat is the change in latitude
                    * c is the great circle distance in Radians.
                    * R is the radius of a spherical Earth.
                    * The locations of the two points in spherical coordinates (longitude and latitude) are lon1,lat1 and lon2, lat2.
            */


            double dLat1InRad = firstLatitude * (Math.PI / 180.0);
            double dLong1InRad = firstLongitude * (Math.PI / 180.0);
            double dLat2InRad = secondLatitude * (Math.PI / 180.0);
            double dLong2InRad = secondLongitude * (Math.PI / 180.0);

            double dLongitude = dLong2InRad - dLong1InRad;
            double dLatitude = dLat2InRad - dLat1InRad;

            // Intermediate result a.
            double a = Math.Pow(Math.Sin(dLatitude / 2.0), 2.0) +
                       Math.Cos(dLat1InRad) * Math.Cos(dLat2InRad) *
                       Math.Pow(Math.Sin(dLongitude / 2.0), 2.0);

            // Intermediate result c (great circle distance in Radians).
            double c = 2.0 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1.0 - a));

            // Distance.
            //const Double kEarthRadiusMiles = 3956.0;
            const Double kEarthRadiusKms = 6376.5;
            double dDistance = kEarthRadiusKms * c;

            return dDistance;
        }


        ////-----------------    computes distance using direction    ---------------------
        //public double Calc(string NS1, double Lat1, double Lat1Min, string EW1, double Long1, double Long1Min,
        //    string NS2, double Lat2, double Lat2Min, string EW2, double Long2, double Long2Min) {
        //    //  usage: ComputeDistance.Calc(" N 43 35.500 W 80 27.800 N 43 35.925 W 80 28.318");

        //    double NS1Sign = NS1.ToUpper() == "N" ? 1.0 : -1.0;
        //    double EW1Sign = NS1.ToUpper() == "E" ? 1.0 : -1.0;
        //    double NS2Sign = NS2.ToUpper() == "N" ? 1.0 : -1.0;
        //    double EW2Sign = EW2.ToUpper() == "E" ? 1.0 : -1.0;

        //    return (Calc((Lat1 + (Lat1Min / 60)) * NS1Sign,
        //        (Long1 + (Long1Min / 60)) * EW1Sign,
        //        (Lat2 + (Lat2Min / 60)) * NS2Sign,
        //        (Long2 + (Long2Min / 60)) * EW2Sign
        //        ));
        //}

        /// <summary>Calculates distance of two positions in km</summary>
        /// <param name="pos1">A position</param>
        /// <param name="pos2">A position</param>
        /// <returns>Distance between given points in km.</returns>
        public static decimal operator -(GpsPoint pos1, GpsPoint pos2)
        {
            return Math.Abs(CalculateDistance(pos2, pos1));
        }
        /// <summary>Gets value indicating if this instance equals to given object</summary>
        /// <param name="obj">Object to compare</param>
        /// <returns>True if <paramref name="obj"/> is <see cref="GpsPoint"/> and its <see cref="Latitude"/>, <see cref="Longitude"/> and <see cref="Altitude"/> equals to current object</returns>
        public override bool Equals(object obj)
        {
            if (obj is GpsPoint)
            {
                return ((GpsPoint)obj).Altitude == Altitude && ((GpsPoint)obj).Latitude == Latitude &&
                       ((GpsPoint)obj).Longitude == Longitude;
            }
            return base.Equals(obj);
        }
        /// <summary>Compares two <see cref="GpsPoint">GpsPoints</see> for equality</summary>
        /// <param name="pos1">A position</param>
        /// <param name="pos2">A position</param>
        /// <returns>True if given points represent same location; false if not</returns>
        public static bool operator ==(GpsPoint pos1, GpsPoint pos2) { return pos2.Equals(pos1); }
        /// <summary>Compares two <see cref="GpsPoint">GpsPoints</see> for inequality</summary>
        /// <param name="pos1">A position</param>
        /// <param name="pos2">A position</param>
        /// <returns>False if given points represent same location; true if not</returns>
        public static bool operator !=(GpsPoint pos1, GpsPoint pos2) { return !pos2.Equals(pos1); }
        /// <summary>Gets has code of current object</summary>
        public override int GetHashCode()
        {
            return Latitude.GetHashCode() | Longitude.GetHashCode();
        }
    }
}
