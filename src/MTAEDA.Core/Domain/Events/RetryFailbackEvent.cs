using MTAEDA.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTAEDA.Core.Domain.Events
{
    public class RetryFailbackEvent : IDomainEvent
    {
        public string? DomainData { get; set; }
        public DateTime TransactionDate { get; }
        public string FailoverSite { get;private set; }
        public string Domain { get; private set; }
        public string Action { get; private set; }
        public RetryFailbackEvent(DateTime transactionDate = default)
        {
            TransactionDate = transactionDate;
            FailoverSite = Domain = Action = default!;
        }
        
        public static RetryFailbackEvent Create(string failoverSite, string domain, string action)
        {
            return new RetryFailbackEvent(DateTime.Now)
            {
                Action = action,
                FailoverSite = failoverSite,
                Domain = domain
            };
        }
    }
}
