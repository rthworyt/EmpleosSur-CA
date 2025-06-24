using EmpleosSur.Domain.Entities;
using EmpleosSur.Domain.Helpers;

namespace EmpleosSur.Application.Interfaces.IServices
{
    public interface ICandidatoService : IService<Candidato>
    {
        Task<Candidato> GetCandidatoByEmail(string email);
        Task<Candidato> GetAllDataCandidatoById(int id);
        Task<IEnumerable<Candidato>> GetCandidatoByCiudad(string ciudad);

        Task<OperationResult> CreateCandidato(Candidato candidato);
        Task<OperationResult> UpdateCandidato(Candidato candidato);
        Task<OperationResult> DeleteCandidato(int id);
    }
}
