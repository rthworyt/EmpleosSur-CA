using EmpleosSur.Domain.Entities;

namespace EmpleosSur.Application.Interfaces.IServices
{
    public interface IExperienciaLaboralService : IService<ExperienciaLaboral>
    {
        Task<IEnumerable<ExperienciaLaboral>> GetExpByCandidatoIdAsync(int candidatoId);
    }
}
