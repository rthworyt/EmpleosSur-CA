using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmpleosSur.Domain.Entities;
using EmpleosSur.Domain.Helpers;

namespace EmpleosSur.Application.Interfaces.IServices
{
    public interface ICandidatoService
    {
        Task<Candidato> GetCandidatoById(int id);
        Task<Candidato> GetCandidatoByEmail(string email);
        Task<Candidato> GetAllDataCandidatoById(int id);

        Task<OperationResult> CreateCandidato(Candidato candidato);
        Task<OperationResult> UpdateCandidato(Candidato candidato);
        Task<OperationResult> DeleteCandidato(int id);
    }
}
