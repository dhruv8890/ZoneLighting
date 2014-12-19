﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using ZoneLighting.Communication;
using ZoneLighting.ZoneProgramNS;

namespace ZoneLighting.ZoneNS
{
	/// <summary>
	/// Represents a zone that contains the lights to be controlled. A zone is the unit of control 
	/// from the ZoneLightingManager's point of view.
	/// </summary>
	[DataContract]
    public class Zone : IDisposable
	{
		#region CORE

		/// <summary>
		/// Name of the zone.
		/// </summary>
		[DataMember]
		public string Name;

		/// <summary>
		/// All lights in the zone.
		/// </summary>
		public IList<ILogicalRGBLight> Lights { get; private set; }

		/// <summary>
		/// The Lights list as a dictionary with the logical index as the key and the light as the value.
		/// </summary>
		public Dictionary<int, ILogicalRGBLight> SortedLights
		{
			get { return Lights.ToDictionary(x => x.LogicalIndex); }
		}

		/// <summary>
		/// The Lighting Controller used to control this Zone.
		/// </summary>
		public LightingController LightingController { get; private set; }

		/// <summary>
		/// The program that is active on this zone.
		/// </summary>
		[DataMember]
		public ZoneProgram ZoneProgram { get; private set; }

		#endregion

		#region C+I

		public Zone(LightingController lightingController, string name = "", ZoneProgram program = null, InputStartingValues inputStartingValues = null)
		{
			Lights = new List<ILogicalRGBLight>();
			LightingController = lightingController;
			Name = name;
			if (program == null) return;
			if (inputStartingValues == null)
			{
				SetProgram(program);
			}
			else
			{
				Initialize(program, inputStartingValues);
			}
		}

		private void Initialize(InputStartingValues inputStartingValues = null)
		{
			if (!Initialized)
			{
				if (ZoneProgram != null)
					StartProgram(inputStartingValues);
				Initialized = true;
			}
		}

		public void Initialize(ZoneProgram zoneProgram, InputStartingValues inputStartingValues = null)
		{
			if (!Initialized)
			{
				SetProgram(zoneProgram);
				Initialize(inputStartingValues);
			}
		}

		public bool Initialized { get; private set; }

		public void Uninitialize(bool force = false)
		{
			if (Initialized)
			{
				StopProgram(force);
				Initialized = false;
			}
		}

		public void Dispose(bool force)
		{
			Uninitialize(force);
			Lights.Clear();
			Lights = null;
			ZoneProgram = null;
			LightingController = null;
			Name = null;
		}

		public void Dispose()
		{
			Dispose(false);
		}

		#endregion

		#region MISC

		public override string ToString()
		{
			return Name;
		}

		#endregion

		#region API

		/// <summary>
		/// Sets this zone's program to the given program.
		/// </summary>
		/// <param name="program"></param>
		public void SetProgram(ZoneProgram program)
		{
			ZoneProgram = program;
			ZoneProgram.Zone = this;
		}
	
		/// <summary>
		/// Starts this zone's program with the given starting values for the inputs.
		/// </summary>
		public void StartProgram(InputStartingValues inputStartingValues = null)
		{
			ZoneProgram.Start(inputStartingValues);
		}

		/// <summary>
		/// Stops this zone's program.
		/// </summary>
		public void StopProgram(bool force = false)
		{
			ZoneProgram.Stop(force);
		}

		/// <summary>
		/// Sets all lights in zone to a given color.
		/// </summary>
		/// <param name="color"></param>
		public void SetAllLightsColor(Color color)
		{
			Lights.ToList().ForEach(x => x.SetColor(color));
		}

		/// <summary>
		/// Adds a new light to this zone.
		/// </summary>
		/// <param name="light"></param>
		public void AddLight(ILogicalRGBLight light)
		{
			Lights.Add(light);
		}

		#endregion
	}
}
