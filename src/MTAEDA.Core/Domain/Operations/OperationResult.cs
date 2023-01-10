using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTAEDA.Core.Domain.Operations
{
    public abstract class OperationResult<T>
    {
        public abstract T? Value { get; set; }
        public Exception? Exception { get; set; }
        public bool IsSuccessful { get => this.Exception == null; }
        public OperationResult() {
            
        }

    }
}
