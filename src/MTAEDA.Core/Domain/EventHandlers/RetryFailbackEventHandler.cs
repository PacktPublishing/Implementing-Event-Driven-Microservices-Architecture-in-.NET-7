using CloudNative.CloudEvents;
using MTAEDA.Core.Domain.Events;
using MTAEDA.Core.Interfaces;
using MTAEDA.Core.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MTAEDA.Core.Domain.EventHandlers
{
    public class RetryFailbackEventHandler : IDomainEventHandler<RetryFailbackEvent>
    {
        public Task Handle(RetryFailbackEvent notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                File.WriteAllText($"{Environment.ProcessPath}\\{notification.Domain}\\{notification.Action}\\{notification.DomainData?.Cast<CloudEvent>().Single().Id}.json", 
                    notification.DomainData);
            });
            

        }
    }
}
