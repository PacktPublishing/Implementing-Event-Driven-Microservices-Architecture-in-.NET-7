using Microsoft.EntityFrameworkCore;
using MTAEDA.Maintenance.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTAEDA.Maintenance.Infrastructure.Contexts
{
    internal class MaintenanceDbContext: DbContext
    {
        public DbSet<Camera> Cameras { get; set; } = default!;
        public DbSet<Turnstile> Turnstiles { get; set; } = default!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
