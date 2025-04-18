using EmpleosSur.Domain.Entities;
using EmpleosSur.Domain.Helpers;

namespace EmpleosSur.Application.Interfaces.IServices
{
    public interface IEmpresaService : IService<Empresa>
    {
        Task<Empresa> GetEmpresaByRNC(string rnc);
        Task<IEnumerable<Empresa>> GetEmpresaByLocalidad(string localidad);
        Task<IEnumerable<Empresa>> GetEmpresaByNombre(string nombre);
    }
}
