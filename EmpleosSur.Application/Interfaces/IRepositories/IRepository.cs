using EmpleosSur.Domain.Common;
using System.Linq.Expressions;

namespace EmpleosSur.Application.Interfaces.IRepositories
{
    public interface IRepository<T> where T : EntityBase
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate);
        Task CreateAsync(T entity);
        Task<bool> DeleteAsync(int id);
        Task UpdateAsync(T entity);
    }
}
