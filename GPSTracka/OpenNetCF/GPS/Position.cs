//==========================================================================================
//
//		OpenNETCF.IO.Serial.GPS
//		Copyright (c) 2003-2006, OpenNETCF.org
//
//		This library is free software; you can redistribute it and/or modify it under 
//		the terms of the OpenNETCF.org Shared Source License.
//
//		This library is distributed in the hope that it will be useful, but 
//		WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or 
//		FITNESS FOR A PARTICULAR PURPOSE. See the OpenNETCF.org Shared Source License 
//		for more details.
//
//		You should have received a copy of the OpenNETCF.org Shared Source License 
//		along with this library; if not, email licensing@opennetcf.org to request a copy.
//
//		If you wish to contact the OpenNETCF Advisory Board to discuss licensing, please 
//		email licensing@opennetcf.org.
//
//		For general enquiries, email enquiries@opennetcf.org or visit our website at:
//		http://www.opennetcf.org
//
//==========================================================================================
using System;
using OpenNETCF.IO.Serial;
using System.Text;
using System.Collections;
using System.Threading;
using System.IO;
using System.Globalization;

namespace OpenNETCF.IO.Serial.GPS
{

	public class Position
	{
		#region initialization
		// position stuff
		decimal latitude_fractional=0;
		string latitude_sexagesimal="";
		decimal latitude_decimal=0;
		decimal latitude_decimal_mem=0;
		CardinalDirection latitude_direction=CardinalDirection.North;

		decimal longitude_fractional=0;
		string longitude_sexagesimal="";
		decimal longitude_decimal=0;
		decimal longitude_decimal_mem=0;
		CardinalDirection longitude_direction=CardinalDirection.West;

		decimal altitudemax=0;
		decimal altitude=0;

		decimal geoidseparation=0;

		DateTime sattime=DateTime.MinValue;
		DateTime satdate=DateTime.MinValue;
		#endregion

		#region properties

    public Position clone() 
    {
      Position newPos = new Position();
      newPos.latitude_fractional=latitude_fractional;
      newPos.latitude_sexagesimal=latitude_sexagesimal;
      newPos.latitude_decimal=latitude_decimal;
      newPos.latitude_decimal_mem=latitude_decimal_mem;
      newPos.latitude_direction=latitude_direction;

      newPos.longitude_fractional=longitude_fractional;
      newPos.longitude_sexagesimal=longitude_sexagesimal;
      newPos.longitude_decimal=longitude_decimal;
      newPos.longitude_decimal_mem=longitude_decimal_mem;
      newPos.longitude_direction=longitude_direction;

      newPos.altitudemax=altitudemax;
      newPos.altitude=altitude;

      newPos.geoidseparation=geoidseparation;

      newPos.sattime=sattime;
      newPos.satdate=satdate;

      return newPos;
    }

		public string Latitude_Sexagesimal
		{
			get
			{
				return latitude_sexagesimal;
			}
		}

		public string Longitude_Sexagesimal
		{
			get
			{
				return longitude_sexagesimal;
			}

		}

		public CardinalDirection DirectionLongitude
		{
			get
			{
				return longitude_direction;
			}
			set
			{
				longitude_direction=value;
				if ((longitude_direction == CardinalDirection.South && longitude_decimal > 0) ||
				  (longitude_direction == CardinalDirection.North && longitude_decimal < 0))
				  longitude_decimal = -longitude_decimal;
			}
		}

		public CardinalDirection DirectionLatitude
		{
			get
			{
				return latitude_direction;
			}
			set
			{
				latitude_direction=value;
				if ((latitude_direction == CardinalDirection.West && latitude_decimal > 0) ||
				  (latitude_direction == CardinalDirection.East && latitude_decimal < 0))
				  latitude_decimal = -latitude_decimal;
			}
		}

		public decimal Latitude_Fractional
		{
			get
			{
				return latitude_fractional;
			}
			set
			{
				latitude_fractional=value;
				latitude_sexagesimal=Misc.DecimalToSexagesimal(value);
				// if direction latitude is SOUTH * -1
				decimal Sens=1;
				if (DirectionLatitude==CardinalDirection.South) Sens=-1;
				latitude_decimal=Misc.FractionalToDecimalDegrees(value) * Sens;
			}

		}

		public decimal Latitude_Decimal
		{
			get
			{
				return latitude_decimal;
			}
		}

		public decimal Latitude_Decimal_Mem
		{
			set
		{
			latitude_decimal_mem=value;
		}
			get
			{
				return latitude_decimal_mem;
			}
		}
		public decimal Longitude_Fractional
		{
			get
			{
				return longitude_fractional;
			}
			set
			{
				longitude_fractional=value;
				longitude_sexagesimal=Misc.DecimalToSexagesimal(value);
				// if direction longitude is WEST * -1
				decimal Sens=1;
				if (DirectionLatitude==CardinalDirection.West) Sens=-1;
				longitude_decimal=Misc.FractionalToDecimalDegrees(value) * Sens;
			}

		}

		public decimal Longitude_Decimal
		{
			get
			{
				return longitude_decimal;
			}
		}
		public decimal Longitude_Decimal_Mem
		{
			set
		{
			longitude_decimal_mem=value;
		}
			get
			{
				return longitude_decimal_mem;
			}
		}

		public decimal Altitude
		{
			get
			{
				return altitude;
			}
			set
			{
				altitude=value;
				if (altitude > altitudemax)
					altitudemax=altitude;
			}
		}
		public decimal AltitudeMax
		{
			get
			{
				return altitudemax;
			}
		}
		public decimal GeoidSeparation
		{
			get
			{
				return geoidseparation;
			}
			set
			{
				geoidseparation=value;
			}
		}
		public DateTime SatTime
		{
			get
			{
				return sattime;
			}
			set
			{
				sattime=value;
			}
		}

		public DateTime SatDate
		{
			get
			{
				return satdate;
			}
			set
			{
				satdate=value;
			}
		}

		#endregion
	}
}
