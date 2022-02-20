using MediatR;
using MTAEDA.Core.Interfaces;
using MTAEDA.Scheduling.Domain.Events;
using MTAEDA.Scheduling.Domain.ValueObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTAEDA.Scheduling.Domain.EventHandlers
{
    public class CameraMaintenanceScheduleChangedEventHandler : IDomainEventHandler<CameraMaintenanceScheduleChangedEvent>
    {
        public Task Handle(CameraMaintenanceScheduleChangedEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

    }
}
