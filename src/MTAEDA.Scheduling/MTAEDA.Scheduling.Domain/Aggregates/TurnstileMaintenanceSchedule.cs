using MTAEDA.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTAEDA.Scheduling.Domain.Aggregates
{
    public class TurnstileMaintenanceSchedule: IDomainAggregateRoot
    {
        public int TurnstileId { get; set; }
    }
}
