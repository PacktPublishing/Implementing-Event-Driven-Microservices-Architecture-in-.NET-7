using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MTAEDA.Equipment.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTAEDA.Equipment.Infrastructure.Repositories
{
    public class EquipmentRepository
    {
        EquipmentDbContext _context;
        private IServiceScopeFactory _serviceScopeFactory;

        public EquipmentRepository(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            IServiceScope scope = _serviceScopeFactory.CreateScope();
            _context = scope.ServiceProvider.GetRequiredService<EquipmentDbContext>();

        }

      
    }
    internal class EquipmentDbContext : DbContext
    {
        public List<Camera> Cameras { get; set; }  = new List<Camera>(); 
        public List<Turnstile> Turnstiles { get; set; } = new List<Turnstile>();    
    }
}
