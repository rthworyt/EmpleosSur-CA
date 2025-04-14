using System.Collections.Generic;
using System.Threading.Tasks;
using EmpleosSur.Domain.Common;

namespace EmpleosSur.Application.Interfaces.IServices
{
    public interface IService<T>
        where T : EntityBase
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);
    }
}
