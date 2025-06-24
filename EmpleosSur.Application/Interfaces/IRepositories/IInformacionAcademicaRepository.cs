using EmpleosSur.Application.Interfaces.IRepositories;
using EmpleosSur.Domain.Entities;

public interface IInformacionAcademicaRepository : IRepository<InformacionAcademica>
{
    Task<IEnumerable<InformacionAcademica>> GetInformacionAcademicaByCandidatoIdAsync(int candidatoId);
}
