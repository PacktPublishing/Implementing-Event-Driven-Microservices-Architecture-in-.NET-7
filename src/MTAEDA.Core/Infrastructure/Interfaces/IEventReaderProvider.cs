using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTAEDA.Core.Infrastructure.Interfaces
{
    public interface IEventReaderProvider
    {
        IEnumerable<string> TopicNames { get; }
        Task Receive();
    }
}
