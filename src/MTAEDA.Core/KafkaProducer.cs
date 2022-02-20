using Confluent.Kafka;
using Confluent.Kafka.Admin;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MTAEDA.Core
{
    public class KafkaProducer
    {
        private ClientConfig _config;
        public KafkaProducer(string? configFilePath)
        {
            var configPath = configFilePath ?? $"{System.IO.Directory.GetCurrentDirectory()}\\kafka.config";
            var cloudConfig = File.ReadAllLines(configPath)
                    .Where(line => !line.StartsWith("#"))
                    .ToDictionary(
                        line => line.Substring(0, line.IndexOf('=')),
                        line => line.Substring(line.IndexOf('=') + 1));
            _config = new ClientConfig(cloudConfig);
            
        }
    }
}
