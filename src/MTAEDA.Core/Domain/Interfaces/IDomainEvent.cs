using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTAEDA.Core.Interfaces
{
    public interface IDomainEvent: INotification
    {
        string? DomainData { get; set; }
    }
}
