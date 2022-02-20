using CloudNative.CloudEvents;
using MediatR;
using MTAEDA.Core.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTAEDA.Core.Utility
{
    public class EventUtil
    {
        public static async Task<CloudEvent> Pack<T>([Required]T evt, Uri source, CloudEventType type, string? subject) 
        {
            return await Task.Run(() => {
                var eventSubject = subject ?? $"{evt?.GetType()?.BaseType?.Name} submitted: {evt?.GetType().Name}";
                return new CloudEvent()
                {
                    Data = evt,
                    Id = Guid.NewGuid().ToString(),
                    Time = DateTime.Now,
                    Subject = eventSubject,
                    Type = type.ToString(),
                };
            });
        }

        public static async Task<T> Unpack<T>([Required]CloudEvent cloudEvent) 
        {
            return await Task.Run( () => {
                 return JsonConvert.DeserializeObject<T>(cloudEvent?.Data?.ToString());
            });
        }


    }

    public enum CloudEventType
    {
        DomainEvent,
        DomainEventHandler,
        DomainException,
    }
}
