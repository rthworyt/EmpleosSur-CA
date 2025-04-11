using EmpleosSur.Domain.Entities;
using EmpleosSur.Domain.Helpers;

namespace EmpleosSur.Application.Interfaces.IServices
{
    public interface IEmpresaService
    {
        Task<Empresa> GetEmpresaByEmailAsync(string email);
        Task<OperationResult> CreateEmpresaAsync(Empresa empresa);
        Task DeleteEmpresaAsync(int id);
        Task UpdateEmpresaAsync(Empresa empresa);
        Task<Empresa> GetEmpresaByIdAsync(int id);
        Task<Empresa> GetEmpresaByRNCAsync(string rnc);
    }
}
