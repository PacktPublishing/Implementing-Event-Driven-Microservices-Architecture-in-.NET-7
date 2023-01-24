using OpenTelemetry.Trace;
using System;
using System.Diagnostics;

namespace MetricTester
{
	public class SimpleActivityClass
	{
		Activity _activity = default!;
		Tracer tracer = default!;
		public SimpleActivityClass(Tracer tracer)
		{
			_activity = new Activity("SimpleActivity");

			_activity.Start();
			Thread.Sleep(50);
			Console.WriteLine("##### ctor() Finished");
			_activity.Stop();

		}

		public void DoSomethingSimple()
		{
			
		}
	}
}

