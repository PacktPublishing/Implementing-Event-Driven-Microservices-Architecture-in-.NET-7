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
    public class CameraMaintenanceScheduleTriggeredEventHandler : IDomainEventHandler<CameraMaintenanceScheduleTriggeredEvent>
    {
        public Task Handle(CameraMaintenanceScheduleTriggeredEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
