using MTAEDA.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTAEDA.Notification.Domain.Events
{
    public class NotificationConfigurationChangedEvent : IDomainEvent
    {
        public string? DomainData { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
