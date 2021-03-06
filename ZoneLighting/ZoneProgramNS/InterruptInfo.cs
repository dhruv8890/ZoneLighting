﻿using System.Reactive.Subjects;
using ZoneLighting.TriggerDependencyNS;

namespace ZoneLighting.ZoneProgramNS
{
	public class InterruptInfo
	{
		//public Action<object> Action { get; set; };
		public object Data { get; set; }
		public Subject<object> InputSubject { get; set; }
		public Subject<object> StopSubject { get; set; }
		public ZoneProgram ZoneProgram { get; set; }
		public ZoneProgram ZoneProgramToInterrupt { get; set; }
	}
}
