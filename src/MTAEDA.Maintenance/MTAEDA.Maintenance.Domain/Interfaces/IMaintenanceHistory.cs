using MTAEDA.Maintenance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTAEDA.Maintenance.Domain.Interfaces
{
    public interface IMaintenanceHistory
    {
        IList<WorkOrder>? MaintenanceHistory { get; set; }
    }
}
