using EmpleosSur.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpleosSur.Application.Interfaces.IRepositories
{
    public interface ICandidatoRepository : IRepository<Candidato>
    {
        Task<Candidato> GetAllDataCandidatoById(int id);
        Task<Candidato> GetCandidatoByEmail(string email);
        Task<IEnumerable<Candidato>> GetCandidatoByCiudad(string ciudad);
    }
}
