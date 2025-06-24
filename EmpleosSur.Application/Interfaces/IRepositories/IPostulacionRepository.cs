using EmpleosSur.Application.Interfaces.IRepositories;
using EmpleosSur.Domain.Entities;

namespace EmpleosSur.Core.Interfaces
{
    public interface IPostulacionRepository : IRepository<Postulacion>
    {
        Task<int> ConteoByEmpleoAsync(int empleoId);
        Task<IEnumerable<Postulacion>> GetPostulacionByCandidatoAsync(int candidatoId);
        Task<IEnumerable<Postulacion>> GetPostulacionByEmpleoAsync(int empleoId);
    }
}