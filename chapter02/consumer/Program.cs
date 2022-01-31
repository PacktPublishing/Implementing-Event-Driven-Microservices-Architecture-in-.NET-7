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
        }


        /// <summary>
        /// Static method used to bootstrap properties, dependency injection, and other environmental settings.
        /// Normally this would be set up in a more traditional ASP.NET Core web or API app. This method is being used
        /// mostly for pulling in configuration items that will populate the Kafka consumer configuration.
        /// Note that AllowAutoCreateTopics is set to true--this will allow you to use any topic name you wish.
        /// </summary>
        /// <param name="args">Additional runtime arguments</param>
        /// <returns>A concrete implementation of the IHostBuilder interface, allowing for the host settings to be instantiated.</returns>
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

