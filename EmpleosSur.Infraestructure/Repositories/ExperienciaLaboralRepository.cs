using EmpleosSur.Application.Interfaces.IRepositories;
using EmpleosSur.Core.Interfaces;
using EmpleosSur.Domain.Entities;
using EmpleosSur.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EmpleosSur.Infraestructure.Repositories
{
    public class ExperienciaLaboralRepository
            : Repository<ExperienciaLaboral>,
                IExperienciaLaboralRepository
    {
        private readonly EmpleosSurDBContext _context;

        public ExperienciaLaboralRepository(EmpleosSurDBContext context)
            : base(context) 
        {
            _context = context;
        }

        public async Task AddExperienciaAsync(ExperienciaLaboral experienciaLaboral)
        {
            await _context.ExperienciasLaborales.AddAsync(experienciaLaboral);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateExperienciaAsync(ExperienciaLaboral experienciaLaboral)
        {
            _context.ExperienciasLaborales.Update(experienciaLaboral);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteExperienciaAsync(int id)
        {
            var experiencia = await _context.ExperienciasLaborales.FindAsync(id);
            if (experiencia != null)
            {
                _context.ExperienciasLaborales.Remove(experiencia);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ExperienciaLaboral>> GetExperienciaByCandidatoIdAsync(int candidatoId)
        {
            return await _context
                .ExperienciasLaborales.Where(e => e.CandidatoId == candidatoId)
                .ToListAsync();
        }
    }
}
