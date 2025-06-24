using EmpleosSur.Application.Interfaces.IServices;
using EmpleosSur.Core.Interfaces;
using EmpleosSur.Application.Interfaces.IRepositories;
using EmpleosSur.Domain.Entities;

namespace EmpleosSur.Application.Services
{
    public class ExperienciaLaboralService : Service<ExperienciaLaboral>, IExperienciaLaboralService
    {
        private readonly IExperienciaLaboralRepository _experienciaLaboralRepository;

        public ExperienciaLaboralService(
            IRepository<ExperienciaLaboral> repository,
            IExperienciaLaboralRepository experienciaLaboralRepository)
            : base(repository)
        {
            _experienciaLaboralRepository = experienciaLaboralRepository;
        }

        public async Task<IEnumerable<ExperienciaLaboral>> GetExpByCandidatoIdAsync(int candidatoId)
        {
            return await _experienciaLaboralRepository.GetExperienciaByCandidatoIdAsync(candidatoId);
        }
    }
}
