using EmpleosSur.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpleosSur.Application.Interfaces.IServices
{
    public interface IInformacionAcademica
    {
        Task AddInformacionAcademicaAsync(InformacionAcademica informacionAcademica);
        Task<bool> DeleteInformacionAcademicaAsync(int idInformacionAcademica);
        Task<IEnumerable<InformacionAcademica>> GetByCandidatoIdAsync(int candidatoId);
        Task UpdateInformacionAcademicaAsync(InformacionAcademica informacionAcademica);
    }
}
