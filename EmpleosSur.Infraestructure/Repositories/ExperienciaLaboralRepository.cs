using EmpleosSur.Core.Interfaces;
using EmpleosSur.Domain.Entities;
using EmpleosSur.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpleosSur.Infraestructure.Repositories
{
    public class ExperienciaLaboralRepository : Repository<ExperienciaLaboral>, IExperienciaLaboralRepository
    {
        private readonly EmpleosSurDBContext _context;

        public ExperienciaLaboralRepository(EmpleosSurDBContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ExperienciaLaboral>> GetExperienciaByCandidatoIdAsync(int candidatoId)
        {
            return await _context.ExperienciasLaborales
                                 .Where(e => e.CandidatoId == candidatoId)
                                 .ToListAsync();
        }
    }
}
