using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace consumer
{
    internal class MessageReceivedEventHandler
    {
        public async Task Handle(ConsumeResult<int, string> result)
        {
            await Task.Run(() => { Console.WriteLine(result.Message.Value); });
        }

    }

}
