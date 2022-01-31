using Confluent.Kafka;

namespace MTAEDA.Equipment.Infrastructure
{
    public class KafkaDataProvider 
    {
        
        public IConsumer<string, string> Consumer { get; private set; }
        public IProducer<string, string> Producer { get;private set;  }
        private Confluent.Kafka.IClient _client;
        public KafkaDataProvider()
        {

        }

        public static KafkaDataProvider CreateProducer(string topic)
        {
            return null;
        }

        public static KafkaDataProvider CreateConsumer(string topic)
        {
            return null;
        }
    }
}
