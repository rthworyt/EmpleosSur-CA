using EmpleosSur.Application.Interfaces.IServices;
using EmpleosSur.Core.Interfaces;
using EmpleosSur.Domain.Entities;

namespace EmpleosSur.Application.Services
{
    public class EmpleoService : Service<Empleo>, IEmpleoService
    {
        private readonly IEmpleoRepository _empleoRepository;

        public EmpleoService(IEmpleoRepository empleoRepository)
            : base(empleoRepository)
        {
            _empleoRepository = empleoRepository;
        }

        public async Task<IEnumerable<Empleo>> SearchEmpleosByTitulo(string titulo)
        {
            return await _empleoRepository.GetEmpleosByTituloAsync(titulo);
        }

        public async Task<List<Empleo>> GetAllEmpleosAsync()
        {
            return await _empleoRepository.GetAllEmpleosAsync();
        }

        public async Task<IEnumerable<Empleo>> GetRecentEmpleosAsync(int cantidad)
        {
            return await _empleoRepository.GetRecentEmpleosAsync(cantidad);
        }

        public async Task<IEnumerable<Empleo>> GetEmpleosByEmpresaId(int empresaId)
        {
            return await _empleoRepository.GetEmpleosByEmpresaIdAsync(empresaId);
        }
    }
}
