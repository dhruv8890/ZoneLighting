﻿using System.Configuration;
using System.Net.WebSockets;
using FakeItEasy;
using NUnit.Framework;
using ZoneLighting.Communication;
using WebSocketState = WebSocketSharp.WebSocketState;

namespace ZoneLightingTests
{
	public class FadeCandyControllerTests
	{
		[Test]
		public void PixelType_ReturnsIFadeCandyPixel()
		{
			var fadeCandyController = new FadeCandyController(A.Dummy<string>());
			var result = fadeCandyController.PixelType == typeof (IFadeCandyPixelContainer);
            fadeCandyController.Dispose();
			Assert.True(result);
		}

		[Test]
		public void Initialize_StartsFCServer()
		{
			var fadeCandyController = new FadeCandyController(ConfigurationManager.AppSettings["FadeCandyServerURL"]);
			fadeCandyController.Initialize();
			var result = fadeCandyController.ConnectionState == WebSocketState.Open;
			fadeCandyController.Dispose();
            Assert.True(result);
		}
	}
}
