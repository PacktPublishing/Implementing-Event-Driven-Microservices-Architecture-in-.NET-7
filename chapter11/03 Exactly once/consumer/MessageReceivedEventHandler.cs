using Confluent.Kafka;
using Microsoft.ApplicationInsights;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace consumer
{
    internal class MessageReceivedEventHandler
    {
        public async Task Handle(ConsumeResult<int, CloudEvent> result, TelemetryClient telemetryClient)
        {
            await Task.Run(() =>
            {
                telemetryClient.Context.Operation.Id = result.Message.Value.OperationId.ToString();
                telemetryClient.Context.Operation.ParentId = result.Message.Value.Id.ToString();
                telemetryClient.TrackTrace("Consumer reacted to the message: " + result.Message.Value.Message);
                var filename = Path.Combine(Path.GetTempPath(), result.TopicPartitionOffset.ToString());
                if(!File.Exists(filename))
                    File.WriteAllText(filename, result.Message.Value.Message);
            });
        }

    }

  

}
