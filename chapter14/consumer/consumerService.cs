using Confluent.Kafka;
using Microsoft.ApplicationInsights;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace consumer
{
    internal class consumerService : BackgroundService, IConsumerService
    {
        private readonly IConsumer<int, CloudEvent> consumer;
        private readonly string topicName;
        private readonly TelemetryClient _telemetryClient;

        public consumerService(ConsumerConfig config, string topic, TelemetryClient telemetryClient)
        {
            consumer = new ConsumerBuilder<int, CloudEvent>(config)
                .SetValueDeserializer(new CloudEvent()).Build();
            topicName = topic;
            _telemetryClient = telemetryClient;
        }
        public async Task Receive()
        {
            await ExecuteAsync(CancellationToken.None);
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                consumer.Subscribe(topicName);
                try
                {
                    Console.WriteLine($"Trying to consume events on topic '{topicName}'...");
                    var result = consumer.Consume(stoppingToken);
                    consumer.Commit(result);
                    await new MessageReceivedEventHandler().Handle(result, _telemetryClient);
                }
                catch (ConsumeException ex)
                {
                    _telemetryClient.TrackException(ex);
                    Console.WriteLine($"Failed to consume events on topic '{topicName}': {ex.Message}");
                    Thread.Sleep(10000);
                }

            }
        }
    }

    internal interface IConsumerService
    {
        Task Receive();
    }

    internal class CloudEvent : IDeserializer<CloudEvent>
    {
        public string? Id { get; set; }
        public string? OperationId { get; set; }
        public string? OperationParentId { get; set; }
        public string? Message { get; set; }

        public CloudEvent Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            var messageParts = Encoding.UTF8.GetString(data).Split("|");
            return new CloudEvent()
            {
                Id = messageParts[0],
                OperationId = messageParts[1],
                OperationParentId = messageParts[2],
                Message = messageParts[3]
            };
        }
    }

}
