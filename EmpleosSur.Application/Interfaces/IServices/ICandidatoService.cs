using EmpleosSur.Domain.Entities;
using EmpleosSur.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpleosSur.Application.Interfaces.IServices
{
    public interface ICandidatoService
    {
        // Métodos específicos para la entidad Candidato
        Task<Candidato> GetCandidatoByEmailAsync(string email);
        Task<OperationResult> CreateCandidatoAsync(Candidato candidato);
        Task DeleteCandidatoAsync(int id);
        Task UpdateCandidatoAsync(Candidato candidato);
        Task<Candidato> GetCandidatoByIdAsync(int id);
    }
}
