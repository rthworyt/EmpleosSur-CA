using EmpleosSur.Application.Interfaces.IServices;
using EmpleosSur.Domain.Entities;

namespace EmpleosSur.Core.Interfaces
{
    public interface IEmpleoRepository : IRepository<Empleo>
    {
        Task DeleteByEmpresaAsync(int empresa);
        Task<IEnumerable<Empleo>> GetByEmpresaAsync(int empresa);
        Task<IEnumerable<Empleo>> GetRecentEmpleosAsync(int conteo);
        Task<IEnumerable<Empleo>> GetByTituloAsync(string titulo);
        Task<List<Empleo>> GetAllEmpleosAsync();
    }
}