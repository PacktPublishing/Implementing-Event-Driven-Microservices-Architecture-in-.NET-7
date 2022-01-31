using MTAEDA.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTAEDA.Equipment.Domain.Events
{
    public class TurnstileDecommissionedEvent : IDomainEvent
    {
        public string? DomainData { get; set; }
        public int TurnstileId { get; set; }
        public DateTime DecommissionDate { get; set; }

        public TurnstileDecommissionedEvent(int turnstileId, DateTime decomDate)
        {
            TurnstileId = turnstileId;
            DecommissionDate = decomDate;
        }
    }
}
