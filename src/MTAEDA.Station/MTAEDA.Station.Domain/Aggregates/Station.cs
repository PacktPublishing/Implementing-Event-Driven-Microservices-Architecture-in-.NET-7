using MTAEDA.Core.Interfaces;
using MTAEDA.Station.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTAEDA.Station.Domain.Aggregates
{
    public class Station : IDomainAggregateRoot
    {
        public StationInfo StationData { get; private set; } = default!;
    }
}
