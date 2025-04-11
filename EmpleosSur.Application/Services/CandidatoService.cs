using EmpleosSur.Application.Interfaces.IServices;
using EmpleosSur.Domain.Entities;
using EmpleosSur.Domain.Helpers;

namespace EmpleosSur.Application.Services
{
    public class CandidatoService : ICandidatoService
    {
        private readonly IRepository<Candidato> _repository;

        public CandidatoService(IRepository<Candidato> repository)
        {
            _repository = repository;
        }

        public async Task<Candidato> GetCandidatoByEmailAsync(string email)
        {
            return (await _repository.GetAllAsync(c => ((Candidato)c).Email == email)).FirstOrDefault();
        }

        public async Task<OperationResult> CreateCandidatoAsync(Candidato candidato)
        {
            if (string.IsNullOrEmpty(candidato.Descripcion))
            {
                candidato.Descripcion = "Descripción no disponible";
            }
            await _repository.CreateAsync(candidato);
            return OperationResult.SuccessResult();
        }

        public async Task DeleteCandidatoAsync(int id)
        {
            var candidato = await GetCandidatoByIdAsync(id);
            if (candidato == null)
            {
                throw new Exception("El candidato no existe.");
            }
            await _repository.DeleteAsync(id);
        }

        public async Task UpdateCandidatoAsync(Candidato candidato)
        {
            await _repository.UpdateAsync(candidato);
        }

        public async Task<Candidato> GetCandidatoByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
    }
}
