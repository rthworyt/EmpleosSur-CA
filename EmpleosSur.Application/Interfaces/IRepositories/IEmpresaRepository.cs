using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmpleosSur.Domain.Entities;
using EmpleosSur.Domain.Helpers;

namespace EmpleosSur.Application.Interfaces.IRepositories
{
    public interface IEmpresaRepository : IRepository<Empresa>
    {
        Task<Empresa> GetEmpresaByRNC(string rnc);
        Task<IEnumerable<Empresa>> GetEmpresaByNombre(string nombre);
        Task<IEnumerable<Empresa>> GetEmpresaByLocalidad(string localidad);
    }
}
