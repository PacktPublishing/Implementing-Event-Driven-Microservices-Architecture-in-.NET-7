using MTAEDA.Core.Interfaces;
using MTAEDA.Scheduling.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTAEDA.Scheduling.Domain.EventHandlers
{
    public class CameraMaintenanceScheduleCreatedEventHandler : IDomainEventHandler<CameraMaintenanceScheduleCreatedEvent>
    {
        public Task Handle(CameraMaintenanceScheduleCreatedEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
