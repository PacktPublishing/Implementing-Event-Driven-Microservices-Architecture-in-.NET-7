using CloudNative.CloudEvents;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MTAEDA.Core.Infrastructure.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MTAEDA.Core.Infrastructure
{
    public class KafkaConsumer: BackgroundService, IEventReaderProvider
    {
        public IEnumerable<string> TopicNames { get; private set; } = default!;
        private ConsumerConfig _config = default!;
        private CancellationTokenSource cts;
        //TODO: Not is use (yet!?)
        private CloudEvent evt  = default!;
        private ILogger _logger = default!;
        public IServiceProvider Services { get; } = default!;

        public KafkaConsumer()
        {
            cts = new CancellationTokenSource();
        }
        public KafkaConsumer(string topic, ConsumerConfig config, IServiceProvider services, ILogger logger) : this()
        {

            TopicNames = new List<string>() { topic };
            Services = services;
            _config = config;
            _logger = logger;
        }

        public KafkaConsumer(IEnumerable<string> topics, ConsumerConfig config, IServiceProvider services, ILogger logger) : this()
        {

            TopicNames = topics;
            Services = services;
            _config = config;
            _logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogTrace("Beginning execution of KafkaConsumer service");
            await StartConsumer(stoppingToken);

        }

        //TODO: Introduce await, await Task.Run() {} or remove Async declaration
        private async Task StartConsumer(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var consumer = new ConsumerBuilder<Null, string>(_config).Build())
                {
                    consumer.Subscribe(TopicNames);
                    var consumeResult = consumer.Consume().Message.Value;
                    if (!String.IsNullOrEmpty(consumeResult))
                    {
                        var evt = JsonConvert.DeserializeObject<CloudEvent>(consumeResult);
                        Console.WriteLine(evt?.Data?.ToString());

                    }
                }
            }



        }

        public async Task Receive()
        {
            await ExecuteAsync(cts.Token);
        }
    }
}
