using EmpleosSur.Domain.Entities;

namespace EmpleosSur.Core.Interfaces
{
    public interface IExperienciaLaboralRepository
    {
        Task AddExperienciaAsync(ExperienciaLaboral experienciaLaboral);
        Task UpdateExperienciaAsync(ExperienciaLaboral experienciaLaboral);
        Task DeleteExperienciaAsync(int id);
        Task<IEnumerable<ExperienciaLaboral>> GetExperienciaByCandidatoIdAsync(int candidatoId);
    }
}