using EmpleosSur.Application.Interfaces.IServices;
using EmpleosSur.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpleosSur.Application.Interfaces.IRepositories
{
    public interface IEmpresaRepository : IRepository<Empresa>
    {
        Task<IEnumerable<Empresa>> GetByLocalidadAsync(string localidad);
        Task<Empresa> GetByRNCAsync(string rnc);
        Task<Empresa> GetByEmailAsync(string email);
        Task<IEnumerable<Empresa>> GetByNombreAsync(string nombre);
    }
}
