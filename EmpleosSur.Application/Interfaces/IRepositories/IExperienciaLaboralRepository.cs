using EmpleosSur.Domain.Entities;

namespace EmpleosSur.Core.Interfaces
{
    public interface IExperienciaLaboralRepository
    {
        Task<IEnumerable<ExperienciaLaboral>> GetExperienciaByCandidatoIdAsync(int candidatoId);
    }
}
