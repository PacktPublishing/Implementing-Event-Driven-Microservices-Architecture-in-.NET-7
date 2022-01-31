using MTAEDA.Core.Interfaces;
using MTAEDA.Equipment.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTAEDA.Equipment.Domain.Aggregates
{
    public class Turnstile : IDomainAggregateRoot
    {
        public EquipmentInfo Model { get; private set; }
        public int TurnstileId { get; private set; }
        public int StationId { get; private set; }
        public DateTime? DateInstalled { get; private set; }
        public DateTime? DateDecommissioned { get; private set; }

        public  Turnstile (EquipmentInfo info, int turnstileId, int stationId, DateTime? installDate, DateTime? decommDate)
        {

            Model = info;
            TurnstileId = turnstileId;
                StationId = stationId;
                DateInstalled = installDate;
                DateDecommissioned = decommDate;
            
        }
    }
}
