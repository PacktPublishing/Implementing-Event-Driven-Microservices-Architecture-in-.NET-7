using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace producer
{
    class producerService : BackgroundService, IProducerService
    {
        private readonly ProducerConfig _config = default!;
        private readonly ILogger<producerService> _logger = default!;
        private readonly IProducer<int, CloudEvent> _producer = default!;
        private readonly ProducerBuilder<int, CloudEvent> _builder = default!;
        public string Topic { get; private set; }
        public producerService(ProducerConfig config, ILogger<producerService> logger)
        {
            _config = config;
            _logger = logger;
            Topic = "";
            _builder = new ProducerBuilder<int, CloudEvent>(_config).SetValueSerializer(new CloudEvent());
            _producer = _builder.Build();

        }

        public async Task Send(string message, string correlationId, string causationId)
        {
            await Task.Run(async () =>
            {
                var messagePacket = new Message<int, CloudEvent>()
                {
                    Key = 1,
                    Value = new CloudEvent()
                    {
                        Id = Guid.NewGuid().ToString(),
                        OperationId = correlationId,
                        OperationParentId = causationId,
                        Message = message
                    }
                };

                bool brokerRecievedEvent;
                do
                {
                    try
                    {
                        var deliveryResult = await _producer.ProduceAsync(Topic, messagePacket, CancellationToken.None);
                        brokerRecievedEvent = deliveryResult.Status == PersistenceStatus.Persisted;
                    }
                    catch (ProduceException<int, CloudEvent>)
                    {
                        brokerRecievedEvent = false;
                        Thread.Sleep(1000);
                    }

                } while (!brokerRecievedEvent);

                _logger.LogInformation("Producer sent the message: " + message + " - and with an Id of: " + messagePacket.Value.Id);
            });
        }

        public async Task SetTopic(string topicName)
        {
            await Task.Run(() => { Topic = topicName; });
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            throw new NotImplementedException();
        }

    }

    internal interface IProducerService
    {
        Task Send(string message, string correlationId, string causationId);
        Task SetTopic(string topicName);
    }

    internal class CloudEvent : ISerializer<CloudEvent>
    {
        public string? Id { get; set; }
        public string? OperationId { get; set; }
        public string? OperationParentId { get; set; }
        public string? Message { get; set; }

        public byte[] Serialize(CloudEvent data, SerializationContext context)
        {
            return Encoding.UTF8.GetBytes(string.Join("|", data.Id, data.OperationId, data.OperationParentId, data.Message));
        }
    }
}
