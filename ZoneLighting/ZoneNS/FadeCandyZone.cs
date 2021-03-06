﻿using ZoneLighting.Communication;
using ZoneLighting.ZoneProgramNS;

namespace ZoneLighting.ZoneNS
{
	public class FadeCandyZone : Zone
	{
		public FadeCandyZone(string name = "", double? brightness = null, ILightingController lightingController = null)
			: base(lightingController ?? FadeCandyController.Instance, name, brightness)
		{
		}

		public void AddFadeCandyLights(PixelType pixelType, int numLights, byte fcChannel)
		{
			for (int i = 0; i < numLights; i++)
			{
				AddLight(new LED(logicalIndex: i, fadeCandyChannel: fcChannel, fadeCandyIndex: i, pixelType: pixelType));
			}
		}
	}
}
