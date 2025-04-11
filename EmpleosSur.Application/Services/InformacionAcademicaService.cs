using EmpleosSur.Application.Interfaces.IServices;
using EmpleosSur.Domain.Entities;

namespace EmpleosSur.Application.Services
{
    public class InformacionAcademicaService
    {
        private readonly IRepository<InformacionAcademica> _informacionAcademicaRepository;

        public InformacionAcademicaService(
            IRepository<InformacionAcademica> informacionAcademicaRepository
        )
        {
            _informacionAcademicaRepository = informacionAcademicaRepository;
        }

        public async Task AddInformacionAcademicaAsync(InformacionAcademica informacionAcademica)
        {
            await _informacionAcademicaRepository.CreateAsync(informacionAcademica);
        }

        public async Task<bool> DeleteInformacionAcademicaAsync(int idInformacionAcademica)
        {
            var informacionAcademica = await GetByIdAsync(idInformacionAcademica);
            if (informacionAcademica == null)
            {
                return false;
            }
            await _informacionAcademicaRepository.DeleteAsync(idInformacionAcademica);
            return true;
        }

        public async Task<IEnumerable<InformacionAcademica>> GetByCandidatoIdAsync(int candidatoId)
        {
            return await _informacionAcademicaRepository.GetAllAsync(e =>
                ((InformacionAcademica)e).CandidatoId == candidatoId
            );
        }

        public async Task UpdateInformacionAcademicaAsync(InformacionAcademica informacionAcademica)
        {
            await _informacionAcademicaRepository.UpdateAsync(informacionAcademica);
        }

        private async Task<InformacionAcademica> GetByIdAsync(int id)
        {
            return await _informacionAcademicaRepository.GetByIdAsync(id);
        }
    }
}
