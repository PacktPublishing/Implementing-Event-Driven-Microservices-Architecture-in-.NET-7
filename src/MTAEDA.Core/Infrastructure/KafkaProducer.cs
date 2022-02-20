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
    public class KafkaProducer: BackgroundService, IEventWriterProvider
    {
        public string TopicName { get; private set; }
        private readonly ILogger _logger;
        private readonly ProducerConfig _config;
        private string _message = "";
        private readonly CancellationTokenSource cts = new CancellationTokenSource();
        public KafkaProducer(string topic, ProducerConfig config, ILogger logger)
        {
            TopicName = topic;
            _logger = logger;
            _config = config;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await StartAsync(stoppingToken);
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }
            using (var p = new ProducerBuilder<Null, string>(_config).Build())
            {
                var m = new Message<Null, string>();
                m.Value = _message;
                await p.ProduceAsync(TopicName, m, cancellationToken);
            }


        }

        public async Task Send(CloudEvent evt)
        {
            _message = JsonConvert.SerializeObject(evt);
            await ExecuteAsync(cts.Token);
        }
    }
}
