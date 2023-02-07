using Azure.Messaging.EventHubs.Producer;
using CloudNative.CloudEvents;
using MTAEDA.Core.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTAEDA.Equipment.Infrastructure
{
    public class EventHubDataWriter : IEventWriterProvider
    {
        EventHubProducerClient _client = default!;
        public EventHubDataWriter()
        {
            TopicName = "default";
        }

        public EventHubDataWriter(string hubConnectionString) : this()
        {
            _client = new EventHubProducerClient(hubConnectionString);
        }
        public string TopicName { get; private set; }

        public Task Send(CloudEvent evt)
        {
            return Task.Run(() =>
            {
                _client.SendAsync(new[] { new Azure.Messaging.EventHubs.EventData() { MessageId = evt.Id, EventBody = new BinaryData(evt.Data) } });
            });

        }
    }
}
