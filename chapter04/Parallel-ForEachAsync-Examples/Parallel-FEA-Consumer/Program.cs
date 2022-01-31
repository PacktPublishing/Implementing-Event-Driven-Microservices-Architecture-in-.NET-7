// See https://aka.ms/new-console-template for more information
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json")
                 .Build();

var topics = config["Topics"].Split("|").ToList();
var consumerList = new List<IConsumer<int, string>>();

topics.ForEach(topic =>
{
    var consumer = new ConsumerBuilder<int, string>(new ConsumerConfig()
    {
        GroupId = config["ConsumerConfig:GroupId"],
        BootstrapServers = config["ConsumerConfig:BootstrapServers"],
        AllowAutoCreateTopics = true,

    }).Build();
    consumer.Subscribe(topic);
    consumerList.Add(consumer);
});

ParallelOptions options = new ParallelOptions()
{
    MaxDegreeOfParallelism = 3
};

await Parallel.ForEachAsync(consumerList, options, async (s, t) =>
{
    if (t.IsCancellationRequested)
    {
        s.Close();
    }
    else
    {
        await Task.Factory.StartNew(() =>
        {
            Console.WriteLine($"Consuming {s.Subscription.First()}...");
            s.Consume(t);
        });
    }
});




