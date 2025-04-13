using EmpleosSur.Application.Interfaces.IRepositories;
using EmpleosSur.Application.Interfaces.IServices;
using EmpleosSur.Application.Services;
using EmpleosSur.Domain.Entities;
using EmpleosSur.Domain.Helpers;

public class CandidatoService : Service<Candidato>, ICandidatoService
{
    private readonly ICandidatoRepository _candidatoRepository;

    public CandidatoService(ICandidatoRepository candidatoRepository)
        : base(candidatoRepository)
    {
        _candidatoRepository = candidatoRepository;
    }

    public async Task<Candidato> GetCandidatoById(int id)
    {
        return await _candidatoRepository.GetByIdAsync(id);
    }

    public async Task<Candidato> GetCandidatoByEmail(string email)
    {
        return await _candidatoRepository.GetCandidatoByEmail(email);
    }

    public async Task<OperationResult> CreateCandidato(Candidato candidato)
    {
        try
        {
            await _candidatoRepository.CreateAsync(candidato);
            return OperationResult.SuccessResult();
        }
        catch (Exception ex)
        {
            return OperationResult.Failure($"Error al crear candidato: {ex.Message}");
        }
    }

    public async Task<OperationResult> DeleteCandidato(int id)
    {
        var candidato = await GetCandidatoById(id);
        if (candidato == null)
            return OperationResult.Failure("El candidato no existe.");

        await _candidatoRepository.DeleteAsync(id);
        return OperationResult.SuccessResult();
    }

    public async Task<OperationResult> UpdateCandidato(Candidato candidato)
    {
        await _candidatoRepository.UpdateAsync(candidato);
        return OperationResult.SuccessResult();
    }

    public async Task<Candidato> GetAllDataCandidatoById(int id)
    {
        return await _candidatoRepository.GetAllDataCandidatoById(id);
    }
}
