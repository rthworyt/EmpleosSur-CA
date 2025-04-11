using EmpleosSur.Core.Interfaces;
using EmpleosSur.Domain.Entities;
using EmpleosSur.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EmpleosSur.Infraestructure.Repositories
{
    public class InformacionAcademicaRepository : Repository<InformacionAcademica>,
                        IInformacionAcademicaRepository
    {
        private readonly EmpleosSurDBContext _context;

        public InformacionAcademicaRepository(EmpleosSurDBContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task AddInformacionAcademicaAsync(InformacionAcademica informacionAcademica)
        {
            await _context.InformacionesAcademicas.AddAsync(informacionAcademica);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateInformacionAcademicaAsync(InformacionAcademica informacionAcademica)
        {
            _context.InformacionesAcademicas.Update(informacionAcademica);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteInformacionAcademicaAsync(int id)
        {
            var entity = await _context.InformacionesAcademicas.FindAsync(id);
            if (entity != null)
            {
                _context.InformacionesAcademicas.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<InformacionAcademica>> GetInformacionAcademicaByCandidatoIdAsync(int candidatoId)
        {
            return await _context.InformacionesAcademicas
                .Where(ia => ia.CandidatoId == candidatoId)
                .ToListAsync();
        }
    }
}
