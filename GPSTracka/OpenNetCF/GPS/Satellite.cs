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

	public class Satellite
	{
		#region Initialization
		int id=0;
		int snr=0;
		int elevation=0;
		int azimuth=0;
		bool active=false;
		int channel=0;
		#endregion

		#region properties
		public int ID
		{
			get
			{
				return id;
			}
			set
			{
				id=value;
			}
		}

		public int SNR
		{
			get
			{
				return snr;
			}
			set
			{
				snr=value;
			}
		}

		public int Elevation
		{
			get
			{
				return elevation;
			}
			set
			{
				elevation=value;
			}
		}
		public int Azimuth
		{
			get
			{
				return azimuth;
			}
			set
			{
				azimuth=value;
			}
		}

		public bool Active
		{
			get
			{
				return active;
			}
			set
			{
				active=value;
			}
		}

		public int Channel
		{
			get
			{
				return channel;
			}
			set
			{
				channel=value;
			}
		}

		#endregion
	}
}