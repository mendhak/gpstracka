//==========================================================================================
//
//		OpenNETCF.IO.Serial.GPS.Enums
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

	public enum CardinalDirection
	{
		/// <summary>
		/// North
		/// </summary>
		North = 0,
		/// <summary>
		/// East
		/// </summary>
		East = 1,
		/// <summary>
		/// South
		/// </summary>
		South = 2,
		/// <summary>
		/// West
		/// </summary>
		West = 4,
		/// <summary>
		/// Northwest
		/// </summary>
		NorthWest = 5,
		/// <summary>
		/// Northeast
		/// </summary>
		NorthEast = 6,
		/// <summary>
		/// Southwest
		/// </summary>
		SouthWest = 7,
		/// <summary>
		/// Southeast
		/// </summary>
		SouthEast = 8,
		/// <summary>
		/// Stationary
		/// </summary>
		Stationary = 9
	}

	public enum States
	{
		/// <summary>
		/// Auto-Discovery
		/// </summary>
		AutoDiscovery,
		/// <summary>
		/// Opening
		/// </summary>
		Opening,
		/// <summary>
		/// Running
		/// </summary>
		Running,
		/// <summary>
		/// Stopping
		/// </summary>
		Stopping,
		/// <summary>
		/// Stopped
		/// </summary>
		Stopped
	}

	public enum StatusType
	{
		NotSet,
		OK, //A
		Warning //V
	}

	public enum AutoDiscoverStates
	{
		Testing,
		FailedToOpen,
		Opened,
		Failed,
		NoGPSDetected
	}
	public enum Fix_Mode
	{
		Auto,
		Manual
	}
	public enum Fix_Indicator
	{
		NotSet,
		Mode2D,
		Mode3D
	}
	public enum Fix_Type
	{
		NotSet,
		NoAltitude,
		WithAltitude
	}
	public enum Fix_TypeMode
	{
		NotSet,
		SPS,
		DSPS,
		PPS,
		RTK
	}
	public enum Units
	{
		Kilometers,
		Miles,
		Knots
	}


}	

