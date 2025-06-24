using EmpleosSur.Application.Interfaces.IRepositories;
using EmpleosSur.Application.Interfaces.IServices;
using EmpleosSur.Domain.Entities;

namespace EmpleosSur.Application.Services
{
    public class EmpresaService : Service<Empresa>, IEmpresaService
    {
        private readonly IEmpresaRepository _empresaRepository;

        public EmpresaService(IEmpresaRepository empresaRepository) 
            : base(empresaRepository)
        {
            _empresaRepository = empresaRepository;
        }

        public async Task<Empresa> GetEmpresaByRNC(string rnc)
        {
            return await _empresaRepository.GetEmpresaByRNC(rnc);
        }

        public async Task<IEnumerable<Empresa>> GetEmpresaByLocalidad(string localidad)
        {
            return await _empresaRepository.GetEmpresaByLocalidad(localidad);
        }

        public async Task<IEnumerable<Empresa>> GetEmpresaByNombre(string nombre)
        {
            return await _empresaRepository.GetEmpresaByNombre(nombre);
        }
    }
}
