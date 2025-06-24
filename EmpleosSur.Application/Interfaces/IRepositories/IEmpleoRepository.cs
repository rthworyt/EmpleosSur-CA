using EmpleosSur.Application.Interfaces.IRepositories;
using EmpleosSur.Domain.Entities;

namespace EmpleosSur.Core.Interfaces
{
    public interface IEmpleoRepository : IRepository<Empleo>
    {
        Task DeleteByEmpresaAsync(int empresa);
        Task<IEnumerable<Empleo>> GetEmpleosByEmpresaIdAsync(int empresa);
        Task<IEnumerable<Empleo>> GetRecentEmpleosAsync(int conteo);
        Task<IEnumerable<Empleo>> GetEmpleosByTituloAsync(string titulo);
        Task<List<Empleo>> GetAllEmpleosAsync();
    }
}