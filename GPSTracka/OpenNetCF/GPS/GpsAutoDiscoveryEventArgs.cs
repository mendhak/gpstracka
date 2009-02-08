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

namespace OpenNETCF.IO.Serial.GPS
{
	/// <summary>
	/// Summary description for OpenNETCF.
	/// </summary>
	public class GpsAutoDiscoveryEventArgs:EventArgs
	{
		public enum AutoDiscoverStates
		{
			Testing,
			FailedToOpen,
			Opened,
			Failed,
			NoGPSDetected
		}

		private States state;
		private string gpsport;
		private OpenNETCF.IO.Serial.BaudRates gpsbauds;


		public States State
		{
			get
			{
				return state;
			}

		}
		public string GpsPort
		{
			get
			{
				return gpsport;
			}

		}
		public OpenNETCF.IO.Serial.BaudRates GpsBauds
		{
			get
			{
				return gpsbauds;
			}

		}

		public GpsAutoDiscoveryEventArgs(States state,string gpsport,OpenNETCF.IO.Serial.BaudRates gpsbauds)
		{
			this.state=state;
			this.gpsport=gpsport;
			this.gpsbauds=gpsbauds;
		} 
	}
}
