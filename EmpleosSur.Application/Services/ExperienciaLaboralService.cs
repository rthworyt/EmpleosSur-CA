using EmpleosSur.Application.Interfaces.IServices;
using EmpleosSur.Domain.Entities;

namespace EmpleosSur.Application.Services
{
    public class ExperienciaLaboralService
    {
        private readonly IRepository<ExperienciaLaboral> _experienciaRepository;
        public ExperienciaLaboralService(IRepository<ExperienciaLaboral> experienciaRepository)
        {
            _experienciaRepository = experienciaRepository;
        }
        public async Task AddExperienciaAsync(ExperienciaLaboral experiencia)
        {
            await _experienciaRepository.CreateAsync(experiencia);
        }
        public async Task<bool> DeleteExperienciaAsync(int experienciaId)
        {
            var experiencia = await GetExpByCandidatoIdAsync(experienciaId);
            if (experiencia == null)
            {
                return false;
            }
            await _experienciaRepository.DeleteAsync(experienciaId);
            return true;
        }
        public async Task<IEnumerable<ExperienciaLaboral>> GetExpByCandidatoIdAsync(int candidatoId)
        {
            return await _experienciaRepository.GetAllAsync(e => ((ExperienciaLaboral)e).CandidatoId == candidatoId);
        }
        public async Task UpdateExperienciaAsync(ExperienciaLaboral experiencia)
        {
            await _experienciaRepository.UpdateAsync(experiencia);
        }
    }
}
