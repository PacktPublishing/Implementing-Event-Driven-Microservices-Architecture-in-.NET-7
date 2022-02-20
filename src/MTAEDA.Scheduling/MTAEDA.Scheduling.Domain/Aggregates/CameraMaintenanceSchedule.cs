using MTAEDA.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTAEDA.Scheduling.Domain.Aggregates
{
    public class CameraMaintenanceSchedule: IDomainAggregateRoot
    {
        public int CameraId { get; set; }
    }
}
