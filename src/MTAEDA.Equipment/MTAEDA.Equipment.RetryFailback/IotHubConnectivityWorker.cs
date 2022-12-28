using MTAEDA.Core.Infrastructure;
using System.ComponentModel;

namespace MTAEDA.Equipment.RetryFailback
{
    public class IotHubConnectivityWorker: BackgroundWorker
    {
        KafkaProducer _producer;
        public IotHubConnectivityWorker(KafkaProducer producer)
        {
            _producer = producer;
        }
        protected override void OnDoWork(DoWorkEventArgs e)
        {
            base.OnDoWork(e);
        }
    }
}
