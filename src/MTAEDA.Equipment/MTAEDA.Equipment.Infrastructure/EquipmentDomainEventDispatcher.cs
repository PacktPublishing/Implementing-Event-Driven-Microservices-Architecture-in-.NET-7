using MediatR;
using MTAEDA.Core.Utility;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MTAEDA.Equipment.Infrastructure
{
    public class EquipmentDomainEventDispatcher : IMediator
    {
        public Task Publish(object notification, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification
        {
            var ce = EventUtil.Pack(notification, new Uri(""), CloudEventType.DomainEvent,"");
            throw new NotImplementedException();
        }

        public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            // Reserved for request/response pattern
            throw new NotImplementedException();
        }

        public Task<object?> Send(object request, CancellationToken cancellationToken = default)
        {
            // Reserved for request/response pattern
            throw new NotImplementedException();
        }

        IAsyncEnumerable<TResponse> ISender.CreateStream<TResponse>(IStreamRequest<TResponse> request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        IAsyncEnumerable<object?> ISender.CreateStream(object request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
