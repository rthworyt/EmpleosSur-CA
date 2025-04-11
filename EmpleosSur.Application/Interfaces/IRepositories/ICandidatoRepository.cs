using EmpleosSur.Application.Interfaces.IServices;
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
        Task<Candidato> GetByIdDataCompletaAsync(int id);
        Task<Candidato> GetByEmailAsync(string email);
        Task<IEnumerable<Candidato>> GetByCiudadAsync(string ciudad);
    }
}
