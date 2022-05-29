using System.Collections.Generic;
using System.Threading.Tasks;


namespace Raditap.DatabaseAccess.Interfaces
{
    public interface IAsyncRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id, bool useWriteDb = true);
        Task<T> GetByIdAsync(long id, bool useWriteDb = true);
        Task<T> GetByIdAsync(string id, bool useWriteDb = true);
        Task<IReadOnlyList<T>> ListAllAsync(bool useWriteDb = true);
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec, bool useWriteDb = true);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<int> CountAsync(ISpecification<T> spec, bool useWriteDb = true);
    }
}
