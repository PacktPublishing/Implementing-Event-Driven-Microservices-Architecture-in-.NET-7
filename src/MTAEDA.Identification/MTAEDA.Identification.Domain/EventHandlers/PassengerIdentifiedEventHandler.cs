using MTAEDA.Core.Interfaces;
using MTAEDA.Identification.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTAEDA.Identification.Domain.EventHandlers
{
    public class PassengerIdentifiedEventHandler : IDomainEventHandler<PassengerIdentifiedEvent>
    {
        public Task Handle(PassengerIdentifiedEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
