using EmpleosSur.Domain.Entities;
using EmpleosSur.Domain.Helpers;

namespace EmpleosSur.Application.Interfaces.IServices
{
    public interface IPostulacionService
    {
        Task<OperationResult> CreatePostulacion(Postulacion postulacion);
        Task<OperationResult> UpdateEstadoPostulacion(int idPostulacion, string estadoPostulacion);
        Task<OperationResult> DeletePostulacion(int id);
        Task<IEnumerable<Postulacion>> GetPostulacionesByCandidatoId(int candidatoId);
        Task<IEnumerable<Postulacion>> GetPostulacionesByEmpleoId(int empleoId);
    }
}
