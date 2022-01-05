using MTAEDA.Core.Interfaces;
using MTAEDA.Maintenance.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MTAEDA.Maintenance.Domain.EventHandlers
{
    public class TurnstileMaintenanceScheduleTriggeredEventHandler : IDomainEventHandler<TurnstileMaintenanceScheduleTriggeredEvent>
    {
        public Task Handle(TurnstileMaintenanceScheduleTriggeredEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
