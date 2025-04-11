using EmpleosSur.Domain.Entities;

namespace EmpleosSur.Core.Interfaces
{
    public interface IInformacionAcademicaRepository
    {
        Task AddInformacionAcademicaAsync(InformacionAcademica informacionAcademica);
        Task UpdateInformacionAcademicaAsync(InformacionAcademica informacionAcademica);
        Task DeleteInformacionAcademicaAsync(int id);
        Task<IEnumerable<InformacionAcademica>> GetInformacionAcademicaByCandidatoIdAsync(int candidatoId);
    }
}