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
        private readonly IProducer<int, string> _producer = default!;
        private readonly ProducerBuilder<int, string> _builder = default!;
        public string Topic { get; private set; }
        public producerService(ProducerConfig config, ILogger<producerService> logger)
        {
            _config = config;  
            _logger = logger;
            Topic = "";
            _builder = new ProducerBuilder<int, string>(_config);
            _producer = _builder.Build();

        }

        public async Task Send(string message)
        {
            await Task.Run(() =>
            {
                var messagePacket = new Message<int, string>() { Key = 1, Value = message };
                _producer.ProduceAsync(Topic, messagePacket, CancellationToken.None);

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
        Task Send(string message);
        Task SetTopic(string topicName);
    }
}
