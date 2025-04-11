using EmpleosSur.Application.Interfaces.IServices;
using EmpleosSur.Domain.Entities;

namespace EmpleosSur.Core.Interfaces
{
    public interface IPostulacionRepository : IRepository<Postulacion>
    {
        Task<int> ConteoByEmpleoAsync(int empleoId);
        Task<IEnumerable<Postulacion>> GetByCandidatoAsync(int candidatoId);
        Task<IEnumerable<Postulacion>> GetByEmpleoAsync(int empleoId);
    }
}