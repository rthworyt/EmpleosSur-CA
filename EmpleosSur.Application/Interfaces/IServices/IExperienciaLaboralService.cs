using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmpleosSur.Domain.Entities;

namespace EmpleosSur.Application.Interfaces.IServices
{
    public interface IExperienciaLaboralService
    {
        Task AddExperienciaAsync(ExperienciaLaboral experiencia);

        Task<bool> DeleteExperienciaAsync(int experienciaId);

        Task<IEnumerable<ExperienciaLaboral>> GetExpByCandidatoIdAsync(int candidatoId);

        Task UpdateExperienciaAsync(ExperienciaLaboral experiencia);
    }
}
