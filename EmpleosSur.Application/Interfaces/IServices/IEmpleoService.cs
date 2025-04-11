using System.Collections.Generic;
using System.Threading.Tasks;
using EmpleosSur.Domain.Entities;

namespace EmpleosSur.Application.Interfaces.IServices
{
    public interface IEmpleoService
    {
        Task CreateEmpleoAsync(Empleo empleo);

        Task<bool> DeleteEmpleoAsync(int id);

        Task<IEnumerable<Empleo>> GetEmpleosByEmpresaIdAsync(int empresaId);

        Task<IEnumerable<Empleo>> SearchEmpleosByTitleAsync(string title);

        Task UpdateEmpleoAsync(Empleo empleo);

        Task<Empleo> GetEmpleoByIdAsync(int id);

        Task<List<Empleo>> GetAllEmpleosAsync();
    }
}
