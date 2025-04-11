using EmpleosSur.Domain.Entities;
using EmpleosSur.Domain.Helpers;

namespace EmpleosSur.Application.Interfaces.IServices
{
    public interface IPostulacionService
    {
        Task<OperationResult> CreatePostulacionAsync(Postulacion postulacion);

        Task<IEnumerable<Postulacion>> GetPostulacionesByCandidatoIdAsync(int candidatoId);

        Task<IEnumerable<Postulacion>> GetPostulacionesByEmpleoIdAsync(int empleoId);

        Task UpdateEstadoPostulacionAsync(int idPostulacion, string estadoPostulacion);

        Task<bool> DeletePostulacionAsync(int id);

        Task<List<Postulacion>> GetPostulacionesAsync();

        Task<Postulacion> GetByIdAsync(int id);
    }
}
