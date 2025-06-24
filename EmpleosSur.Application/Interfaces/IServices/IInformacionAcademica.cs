using EmpleosSur.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmpleosSur.Application.Interfaces.IServices
{
    public interface IInformacionAcademicaService : IService<InformacionAcademica>
    {
        Task<IEnumerable<InformacionAcademica>> GetByCandidatoIdAsync(int candidatoId);
    }
}
