using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MTAEDA.Core
{
    public class KafkaConsumer: BackgroundService, IDisposable
    {
        private ClientConfig _config;
        private IConsumer<string,string> _consumer;
        public KafkaConsumer(string? configFilePath)
        {
            var configPath = configFilePath ?? $"{System.IO.Directory.GetCurrentDirectory()}\\kafka.config";
            var cloudConfig = File.ReadAllLines(configPath)
                    .Where(line => !line.StartsWith("#"))
                    .ToDictionary(
                        line => line.Substring(0, line.IndexOf('=')),
                        line => line.Substring(line.IndexOf('=') + 1));
            _config = new ClientConfig(cloudConfig);
            
        }

        public void Consume(string topic)
        {
            var consumerConfig = new ConsumerConfig(_config);
            consumerConfig.GroupId = "dotnet-example-group-1";
            consumerConfig.AutoOffsetReset = AutoOffsetReset.Earliest;
            consumerConfig.EnableAutoCommit = false;

            CancellationTokenSource cts = new CancellationTokenSource();
            Console.CancelKeyPress += (_, e) => {
                e.Cancel = true; // prevent the process from terminating.
                cts.Cancel();
            };

            _consumer = new ConsumerBuilder<string, string>(consumerConfig).Build();
            
                _consumer.Subscribe(topic);
                var totalCount = 0;
                try
                {
                    while (true)
                    {
                        var cr = _consumer.Consume(cts.Token);
                        totalCount += JObject.Parse(cr.Message.Value).Value<int>("count");
                        Console.WriteLine($"Consumed record with key {cr.Message.Key} and value {cr.Message.Value}, and updated total count to {totalCount}");
                    }
                }
                catch (OperationCanceledException)
                {
                    // Ctrl-C was pressed.
                }
                finally
                {
                    _consumer.Close();
                }
            
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
        public override Task StartAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            { //read user from kafka  
                using (var consumer = new ConsumerBuilder<string, string>(_config).Build())
                {
                    consumer.Subscribe("kafkaListenTopic_From_Producer");
                    var consumeResult = consumer.Consume().Message.Value;
                    if (!consumeResult.IsNullOrEmpty())
                    { //write your business logic to invoke IMyBusinessServices here  
                    }
                }
            }
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Task.Run(() => StartAsync(stoppingToken));
            return Task.CompletedTask;
        }
    }
}
