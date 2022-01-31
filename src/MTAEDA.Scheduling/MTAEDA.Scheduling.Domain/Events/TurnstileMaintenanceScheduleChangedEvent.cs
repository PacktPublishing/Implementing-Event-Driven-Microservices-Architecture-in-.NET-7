using MTAEDA.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTAEDA.Scheduling.Domain.Events
{
    public class TurnstileMaintenanceScheduleChangedEvent : IDomainEvent
    {
        public string? DomainData { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
