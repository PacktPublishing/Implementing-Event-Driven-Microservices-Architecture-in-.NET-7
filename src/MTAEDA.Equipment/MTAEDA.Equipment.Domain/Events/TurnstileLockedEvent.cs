using MTAEDA.Core.Interfaces;
using System;

namespace MTAEDA.Equipment.Domain.Events
{
    public class TurnstileLockedEvent : IDomainEvent
    {
        public int TurnstileId { get; set; }
        public DateTime LockOutTime { get; set; }
        public string? DomainData { get; set; }
        public static TurnstileLockedEvent Create(int turnstileId, DateTime lockOutTime)
        {
            return new TurnstileLockedEvent()
            {
                TurnstileId = turnstileId,
                LockOutTime = lockOutTime,
            };
            

        }
    }
}
