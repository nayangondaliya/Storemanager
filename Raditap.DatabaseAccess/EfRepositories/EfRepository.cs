using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Raditap.DatabaseAccess.Interfaces;
using Raditap.DatabaseAccess.Specifications;
using Raditap.Utilities.Helpers;
using Microsoft.EntityFrameworkCore;
using Raditap.DatabaseAccess.Entities;

namespace Raditap.DatabaseAccess.EfRepositories
{
    /// <summary>
    /// Source: https://github.com/dotnet-architecture/eShopOnWeb
    /// https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-implemenation-entity-framework-core
    /// 
    /// "There's some repetition here - couldn't we have some the sync methods call the async?"
    /// https://blogs.msdn.microsoft.com/pfxteam/2012/04/13/should-i-expose-synchronous-wrappers-for-asynchronous-methods/
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EfRepository<T> : IAsyncRepository<T> where T : class
    {
        private readonly RaditapContext _writeContext;
        private readonly ReadRaditapContext _readContext;
        private readonly ProcessingTimeHelper _timeHelper;

        public EfRepository(RaditapContext writeContext, ReadRaditapContext readContext, ProcessingTimeHelper timeHelper)
        {
            _writeContext = writeContext;
            _readContext = readContext;
            _timeHelper = timeHelper;
        }

        public virtual async Task<T> GetByIdAsync(int id, bool useWriteDb = true)
        {
            _timeHelper.StartLogging(MeasureOn.Database, "GetById", typeof(T).Name);
            var result = await GetContext(useWriteDb).Set<T>().FindAsync(id);
            _timeHelper.StopLogging();

            return result;
        }

        public virtual async Task<T> GetByIdAsync(long id, bool useWriteDb = true)
        {
            _timeHelper.StartLogging(MeasureOn.Database, "GetById", typeof(T).Name);
            var result = await GetContext(useWriteDb).Set<T>().FindAsync(id);
            _timeHelper.StopLogging();

            return result;
        }

        public virtual async Task<T> GetByIdAsync(string id, bool useWriteDb = true)
        {
            _timeHelper.StartLogging(MeasureOn.Database, "GetById", typeof(T).Name);
            var result = await GetContext(useWriteDb).Set<T>().FindAsync(id);
            _timeHelper.StopLogging();

            return result;
        }

        public async Task<IReadOnlyList<T>> ListAllAsync(bool useWriteDb = true)
        {
            _timeHelper.StartLogging(MeasureOn.Database, "ListAll", typeof(T).Name);
            var result = await GetContext(useWriteDb).Set<T>().AsNoTracking().ToListAsync();
            _timeHelper.StopLogging();

            return result;
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec, bool useWriteDb = true)
        {
            _timeHelper.StartLogging(MeasureOn.Database, "List", typeof(T).Name);
            var result = await ApplySpecification(spec, useWriteDb).AsNoTracking().ToListAsync();
            _timeHelper.StopLogging();

            return result;
        }

        public async Task<int> CountAsync(ISpecification<T> spec, bool useWriteDb = true)
        {
            _timeHelper.StartLogging(MeasureOn.Database, "Count", typeof(T).Name);
            var result = await ApplySpecification(spec, useWriteDb).AsNoTracking().CountAsync();
            _timeHelper.StopLogging();

            return result;
        }

        public async Task<T> AddAsync(T entity)
        {
            _timeHelper.StartLogging(MeasureOn.Database, "Insert", typeof(T).Name);

            await _writeContext.Set<T>().AddAsync(entity);
            await _writeContext.SaveChangesAsync();

            _timeHelper.StopLogging();

            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _timeHelper.StartLogging(MeasureOn.Database, "Update", typeof(T).Name);

            _writeContext.Entry(entity).State = EntityState.Modified;
            await _writeContext.SaveChangesAsync();

            _timeHelper.StopLogging();
        }

        public async Task DeleteAsync(T entity)
        {
            _timeHelper.StartLogging(MeasureOn.Database, "Delete", typeof(T).Name);

            _writeContext.Set<T>().Remove(entity);
            await _writeContext.SaveChangesAsync();

            _timeHelper.StopLogging();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec, bool useWriteDb = true)
        {
            if (spec.IsReadUncommitted)
            {
                using (new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
                {
                    return SpecificationEvaluator<T>.GetQuery(GetContext(useWriteDb).Set<T>().AsQueryable(), spec);
                }
            }
            else
            {
                return SpecificationEvaluator<T>.GetQuery(GetContext(useWriteDb).Set<T>().AsQueryable(), spec);
            }
        }

        private DbContext GetContext(bool useWriteDb)
        {
            return useWriteDb ? _writeContext : _readContext;
        }
    }
}
