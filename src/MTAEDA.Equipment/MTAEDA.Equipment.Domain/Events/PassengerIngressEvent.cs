using MTAEDA.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTAEDA.Equipment.Domain.Events
{
    public class PassengerIngressEvent : IDomainEvent
    {
        public string? DomainData { get; set; }
        public int TurnstileId { get; }
        public DateTime TransactionDate { get; }

        public PassengerIngressEvent(int turnstileId, DateTime tranDate)
        {
            TurnstileId = turnstileId;
            TransactionDate = tranDate;
        }

        public static PassengerIngressEvent Create(int turnstileId)
        {
            return new PassengerIngressEvent(turnstileId, DateTime.Now);
        }
    }
}
