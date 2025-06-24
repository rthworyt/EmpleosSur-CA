using System.Collections.Generic;
using System.Threading.Tasks;
using EmpleosSur.Domain.Entities;

namespace EmpleosSur.Application.Interfaces.IServices
{
    public interface IEmpleoService : IService<Empleo>
    {
        Task<IEnumerable<Empleo>> GetEmpleosByEmpresaId(int empresaId);
        Task<IEnumerable<Empleo>> SearchEmpleosByTitulo(string titulo);
        Task<List<Empleo>> GetAllEmpleosAsync();
        Task<IEnumerable<Empleo>> GetRecentEmpleosAsync(int cantidad);
    }
}
