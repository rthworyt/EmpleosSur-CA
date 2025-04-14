using EmpleosSur.Application.Interfaces;
using EmpleosSur.Application.Interfaces.IRepositories;
using EmpleosSur.Application.Interfaces.IServices;
using EmpleosSur.Core.Interfaces;
using EmpleosSur.Domain.Entities;
using EmpleosSur.Domain.Helpers;

namespace EmpleosSur.Application.Services
{
    public class PostulacionService : IPostulacionService
    {
        private readonly IPostulacionRepository _postulacionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PostulacionService(IUnitOfWork unitOfWork, IPostulacionRepository postulacionRepository)
        {
            _unitOfWork = unitOfWork;
            _postulacionRepository = postulacionRepository;
        }

        public async Task<OperationResult> CreatePostulacion(Postulacion postulacion)
        {
            var empleo = await _unitOfWork.Empleos.GetByIdAsync(postulacion.EmpleoId);
            var candidato = await _unitOfWork.Candidatos.GetByIdAsync(postulacion.CandidatoId);

            if (empleo == null || candidato == null)
            {
                return OperationResult.Failure("Empleo o candidato no válido.");
            }

            await _postulacionRepository.CreateAsync(postulacion);
            await _unitOfWork.CompletarAsync();

            return OperationResult.SuccessResult();
        }

        public async Task<OperationResult> UpdateEstadoPostulacion(
            int idPostulacion,
            string estadoPostulacion
        )
        {
            var postulacion = await _postulacionRepository.GetByIdAsync(idPostulacion);
            if (postulacion == null)
                return OperationResult.Failure("Postulación no encontrada.");

            if (!Enum.TryParse<EstadoPostulacion>(estadoPostulacion, out var estado))
                return OperationResult.Failure("Estado de postulación no válido.");

            postulacion.Estado = estado;
            await _postulacionRepository.UpdateAsync(postulacion);
            await _unitOfWork.CompletarAsync();

            return OperationResult.SuccessResult();
        }

        public async Task<OperationResult> DeletePostulacion(int id)
        {
            var postulacion = await _postulacionRepository.GetByIdAsync(id);
            if (postulacion == null)
                return OperationResult.Failure("Postulación no encontrada.");

            var eliminado = await _postulacionRepository.DeleteAsync(id);
            if (eliminado)
            {
                await _unitOfWork.CompletarAsync();
                return OperationResult.SuccessResult();
            }

            return OperationResult.Failure("No se pudo eliminar la postulación.");
        }

        public async Task<IEnumerable<Postulacion>> GetPostulacionesByCandidatoId(int candidatoId)
        {
            return await _postulacionRepository.GetPostulacionByCandidatoAsync(candidatoId);
        }

        public async Task<IEnumerable<Postulacion>> GetPostulacionesByEmpleoId(int empleoId)
        {
            return await _postulacionRepository.GetPostulacionByEmpleoAsync(empleoId);
        }
    }
}
