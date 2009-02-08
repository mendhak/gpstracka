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

	public class GpsCommStateEventArgs:EventArgs
	{

		private States state;


		public States State
		{
			get
			{
				return state;
			}

		}
		public GpsCommStateEventArgs(States state)
		{
			this.state=state;
		} 
	}
}