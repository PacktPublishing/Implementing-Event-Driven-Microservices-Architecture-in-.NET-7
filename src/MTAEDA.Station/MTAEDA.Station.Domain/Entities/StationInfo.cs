using MTAEDA.Station.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTAEDA.Station.Domain.Entities
{
    public class StationInfo
    {
        public StationAddress Address { get; private set; } = default!;
        public int StationId {  get; private set; }
        public string StationName { get; private set; } = default!;
        public DateTime OpenedDate { get; private set; }
        public DateTime? ClosedDate {  get; private set; }
        public DateTime? LastModified { get;private set; }

    }
}
