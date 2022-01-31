using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using MTAEDA.Core.Infrastructure;
using MTAEDA.Equipment.Infrastructure;
using System;
using System.Configuration;

namespace MTAEDA.Equipment.Listener
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = LoggerFactory.Create(c =>
            {
                c.AddConsole();
            });
            var config = new ConsumerConfig() { BootstrapServers = ConfigurationManager.AppSettings["BootstrapServers"], GroupId ="consumers.equipment" };
            var consumer = new KafkaConsumer(ConfigurationManager.AppSettings["SubscribeTo"], config, null,logger.CreateLogger("console") );
            consumer.Receive().Wait();
            
        }
    }
}
