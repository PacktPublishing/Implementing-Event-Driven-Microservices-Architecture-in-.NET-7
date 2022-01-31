using MTAEDA.Core.Infrastructure.Interfaces;
using MTAEDA.Maintenance.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MTAEDA.Maintenance.Infrastructure.Providers
{
    public class SqlServerProvider : DbContext, IDataProvider
    {
        
    }
}
