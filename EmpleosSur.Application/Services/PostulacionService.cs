using EmpleosSur.Application.Interfaces;
using EmpleosSur.Application.Interfaces.IServices;
using EmpleosSur.Domain.Entities;
using EmpleosSur.Domain.Helpers;

namespace EmpleosSur.Application.Services
{
    public class PostulacionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PostulacionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult> CreatePostulacionAsync(Postulacion postulacion)
        {
            var empleo = await _unitOfWork.Empleos.GetByIdAsync(postulacion.EmpleoId);
            var candidato = await _unitOfWork.Candidatos.GetByIdAsync(postulacion.CandidatoId);

            if (empleo == null || candidato == null)
            {
                return OperationResult.Failure("Empleo o candidato no válido.");
            }

            await _unitOfWork.Postulaciones.CreateAsync(postulacion);
            await _unitOfWork.CompletarAsync();

            return OperationResult.SuccessResult();
        }

        public async Task<IEnumerable<Postulacion>> GetPostulacionesByCandidatoIdAsync(
            int candidatoId
        )
        {
            return await _unitOfWork.Postulaciones.GetAllAsync(p => p.CandidatoId == candidatoId);
        }

        public async Task<IEnumerable<Postulacion>> GetPostulacionesByEmpleoIdAsync(int empleoId)
        {
            return await _unitOfWork.Postulaciones.GetAllAsync(p => p.EmpleoId == empleoId);
        }

        public async Task<bool> UpdateEstadoPostulacionAsync(
            int idPostulacion,
            EstadoPostulacion estadoPostulacion
        )
        {
            var postulacion = await _unitOfWork.Postulaciones.GetByIdAsync(idPostulacion);
            if (postulacion == null)
                return false;

            postulacion.Estado = estadoPostulacion; 
            await _unitOfWork.Postulaciones.UpdateAsync(postulacion);
            await _unitOfWork.CompletarAsync();
            return true;
        }

        public async Task<bool> DeletePostulacionAsync(int id)
        {
            var eliminado = await _unitOfWork.Postulaciones.DeleteAsync(id);
            if (eliminado)
            {
                await _unitOfWork.CompletarAsync();
            }
            return eliminado;
        }

        public async Task<List<Postulacion>> GetPostulacionesAsync()
        {
            return (await _unitOfWork.Postulaciones.GetAllAsync(p => true)).ToList();
        }

        public async Task<Postulacion> GetByIdAsync(int id)
        {
            return await _unitOfWork.Postulaciones.GetByIdAsync(id);
        }
    }
}
