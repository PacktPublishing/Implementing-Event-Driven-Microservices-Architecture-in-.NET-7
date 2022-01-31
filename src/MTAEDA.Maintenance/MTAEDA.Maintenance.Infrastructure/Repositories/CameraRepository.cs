using MTAEDA.Core.Interfaces;
using MTAEDA.Maintenance.Domain.Aggregates;
using MTAEDA.Maintenance.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTAEDA.Maintenance.Infrastructure.Repositories
{
    public class CameraRepository : ICameraRepository
    {
        public bool CreateOrUpdate<TEntity>(TEntity entity) where TEntity : IDomainEntity
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateOrUpdateAsync<TEntity>(TEntity entity) where TEntity : IDomainEntity
        {
            throw new NotImplementedException();
        }

        public bool Delete<TEntity>(TEntity entity) where TEntity : IDomainEntity
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Camera entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync<TEntity>(TEntity entity) where TEntity : IDomainEntity
        {
            throw new NotImplementedException();
        }

        public bool Exists(Camera entity)
        {
            throw new NotImplementedException();
        }

        public TEntity Get<TEntity>(int id) where TEntity : IDomainEntity
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> GetAll<TEntity>() where TEntity : IDomainEntity
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<TEntity>> GetAllAsync<TEntity>() where TEntity : IDomainEntity
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetAsync<TEntity>(int id) where TEntity : IDomainEntity
        {
            throw new NotImplementedException();
        }

        public Task<Camera> SaveAsync(Camera entity)
        {
            throw new NotImplementedException();
        }
    }
}
