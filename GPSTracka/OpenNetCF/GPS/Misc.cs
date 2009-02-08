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

	public class Misc // utility functions
	{
		
		public static decimal ToDecimal(object obin)
		{
			string StringSeparator = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
			
			if (StringSeparator == ",")
				return Convert.ToDecimal(obin.ToString().Replace(".",","));
			else
				return Convert.ToDecimal(obin);
		}

		public static decimal KnotsToMph(decimal Knots)
		{
			return Knots * 1.151m;
		}

		public static decimal KnotsToKph(decimal Knots)
		{
			return Knots * 1.852m;
		}

		
		public static decimal MetersToFeet(decimal metres)
		{
			return metres * 3.280839895013122m;
		}

		public static decimal MetersToInches(decimal metres)
		{
			return metres * 39.700874015748m;
		}

		public static decimal MetersToYards(decimal metres)
		{
			return metres *  1.0936132983377087m;
		}

		public static decimal MetersToKM(decimal metres)
		{
			return metres * 0.001m;
		}

		public static decimal MetersToCm(decimal metres)
		{
			return metres * 100m;
		}

		public static decimal MetersToMm(decimal metres)
		{
			return metres * 1000m;
		}

		public static decimal MetersToMiles(decimal metres)
		{
			return metres * 0.0006213711922373347m;
		}

		public static decimal KmToMiles(decimal km)
		{
			return km * 0.6213711922373347m;
		}

		public static decimal NmToMeters(decimal nm)
		{
			return nm * 1852m;
		}

		public static decimal NmToMiles(decimal nm)
		{
			return nm * 1151m;
		}

		/// <summary>
		/// Converts fractional degrees to decimal degrees
		/// </summary>
		/// <param name="decin"></param>
		/// <returns></returns>
		public static decimal FractionalToDecimalDegrees(decimal decin) 
		{
			bool positve = decin>0;
			string dm=Math.Abs(decin).ToString("00000.0000");

			//Get the fractional part of minutes
			int intdelim=dm.IndexOf(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
			
			decimal fm = Misc.ToDecimal("0" + dm.Substring(intdelim));

			//Get the minutes.
			decimal min = Misc.ToDecimal(dm.Substring(intdelim - 2, 2));

			//Degrees
			decimal deg= Misc.ToDecimal(dm.Substring(0, intdelim - 2));

			decimal result = deg + (min + fm) / 60;
			if (positve)
				return result;
			else
				return -result;
				
		}

		//format (dd.dddd) to decimal minute format (ddmm.mmmm).
		//Example 58.65375° => 5839.225 (58° 39.225min)
		public static decimal DecimalToFractionalDegrees(decimal dec)
		{
			decimal d=(Decimal.Truncate(dec)); //58
			decimal m=(dec-d)*60; //39.2250 
			m=Math.Round(m,4); //39.2250
			return (d*100)+m; //5839.2250
		}
	
	
		/// <summary>
		/// convert decimal degrees to sexagesimal format
		/// </summary>
		/// <param name="inputdata"></param>
		/// <returns></returns>
		/// 
		public static string DecimalToSexagesimal(decimal inputdata)
		{

			// orignal input = the data 4533.3512

			string strdata=Math.Abs(inputdata).ToString("00000.0000");

			// degrees is 45
			string degrees=strdata.Substring(0,3);


			string minutes = strdata.Substring(3,2);
			// minutes is 33

			decimal decseconds = Misc.ToDecimal(strdata.Substring(5));
			// decseconds = .3512

			decseconds = Math.Round(decseconds*60,2);

			// seconds = 21.07

			return degrees+"°"+minutes+"'"+decseconds.ToString("00.00")+"\"";


		}

	}
}