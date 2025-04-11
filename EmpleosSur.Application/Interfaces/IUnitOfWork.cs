using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmpleosSur.Application.Interfaces.IServices;
using EmpleosSur.Domain.Entities;

namespace EmpleosSur.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Postulacion> Postulaciones { get; }
        IRepository<Candidato> Candidatos { get; }
        IRepository<Empleo> Empleos { get; }
        Task<int> CompletarAsync();
    }
}
