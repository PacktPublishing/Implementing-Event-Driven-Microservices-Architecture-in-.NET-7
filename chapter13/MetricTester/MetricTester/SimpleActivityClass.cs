using OpenTelemetry.Trace;
using System;
using System.Diagnostics;

namespace MetricTester
{
	public class SimpleActivityClass: IDisposable
	{
		Activity _activity = default!;
		Tracer _tracer = default!;
		TelemetrySpan _telemetrySpan = default!;
		public SimpleActivityClass(Tracer tracer)
		{
			_activity = new Activity("SimpleActivity");
			_tracer = tracer;
			_telemetrySpan = _tracer.StartActiveSpan("SimpleTracer", SpanKind.Internal);
			DoSomethingSimple();
		}

        public void Dispose()
        {
			_telemetrySpan.Dispose();
        }

        public void DoSomethingSimple()
		{
            _activity.Start();
            Thread.Sleep(50);
            Console.WriteLine("##### ctor() Finished");
            _activity.Stop();
        }

		public void DoSomethingTraceable()
		{
			_telemetrySpan.AddEvent("DoSomethingTraceable");
		}

		private void AddSomeEvents()
		{
			_telemetrySpan.AddEvent("SubEvent1");
		}
	}
}

