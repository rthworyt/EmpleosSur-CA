using EmpleosSur.Application.Interfaces.IServices;
using EmpleosSur.Domain.Entities;

namespace EmpleosSur.Application.Services
{
    public class InformacionAcademicaService : Service<InformacionAcademica>, IInformacionAcademicaService
    {
        private readonly IInformacionAcademicaRepository _informacionAcademicaRepository;

        public InformacionAcademicaService(
            IInformacionAcademicaRepository informacionAcademicaRepository
        ) : base(informacionAcademicaRepository) //
        {
            _informacionAcademicaRepository = informacionAcademicaRepository;
        }

        public async Task<IEnumerable<InformacionAcademica>> GetByCandidatoIdAsync(int candidatoId)
        {
            return await _informacionAcademicaRepository.GetInformacionAcademicaByCandidatoIdAsync(candidatoId);
        }
    }
}
