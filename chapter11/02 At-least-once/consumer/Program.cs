// See https://aka.ms/new-console-template for more information
using Confluent.Kafka;
using consumer;
using Microsoft.ApplicationInsights;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Sockets;

namespace consumer
{
    static class Program
    {
        static ConsumerConfig _config = new ConsumerConfig();
        static string topicName = "";
        static async Task Main(string[] args)
        {
            Task.Run(() => {
                var tcpHealthEndpoint = new TcpListener(IPAddress.Any, 123);
                tcpHealthEndpoint.Start();
                while (true)
                {
                    TcpClient client = tcpHealthEndpoint.AcceptTcpClient();
                    NetworkStream stream = client.GetStream();
                    var msg = System.Text.Encoding.ASCII.GetBytes("Ok!");
                    stream.Write(msg, 0, msg.Length);
                    client.Close();
                }
            });

            using IHost host = CreateHostBuilder(args).Build();
            using var scope = host.Services.CreateScope();
            var telemetryClient = scope.ServiceProvider.GetRequiredService<TelemetryClient>();

            _config.EnableAutoCommit = false;

            var service = new consumerService(_config, topicName, telemetryClient);
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
                    })
                    .ConfigureServices(
                        (config) => config.AddApplicationInsightsTelemetryWorkerService(
                         (options) =>
                            options.ConnectionString =
                            "--- insert your app insights connection string ---"
                         ).AddApplicationInsightsKubernetesEnricher()
                      );
    }
    
}

