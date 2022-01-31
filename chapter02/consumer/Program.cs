// See https://aka.ms/new-console-template for more information
using Confluent.Kafka;
using consumer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace consumer
{
    static class Program
    {
        static ConsumerConfig _config = new ConsumerConfig();
        static string topicName = "";
        static async Task Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();
            var service = new consumerService(_config, topicName);
            await service.Receive();
            await host.RunAsync();
        }



        static IHostBuilder CreateHostBuilder(string[] args) =>
                    Host.CreateDefaultBuilder(args).ConfigureAppConfiguration((hostingContext, configuration) =>
                    {
                        configuration.Sources.Clear();
                        IHostEnvironment env = hostingContext.HostingEnvironment;
                        configuration
                                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true);
                        IConfigurationRoot config = configuration.Build();
                        _config = new ConsumerConfig() { BootstrapServers = config.GetValue<string>("KafkaServer"), GroupId = config.GetValue<string>("DefaultGroupId"), AllowAutoCreateTopics = true,  };
                        topicName = config.GetValue<string>("Topic");
                    });
    }
    
}

