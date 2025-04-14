using EmpleosSur.Core.Interfaces;
using EmpleosSur.Domain.Entities;
using EmpleosSur.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EmpleosSur.Infraestructure.Repositories
{
    public class EmpleoRepository : Repository<Empleo>, IEmpleoRepository
    {
        private readonly EmpleosSurDBContext _context;

        public EmpleoRepository(EmpleosSurDBContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Empleo>> GetEmpleosByEmpresaIdAsync(int empresa)
        {
            return await _context.Empleos.Where(e => e.EmpresaId == empresa).ToListAsync();
        }

        public async Task<IEnumerable<Empleo>> GetRecentEmpleosAsync(int conteo)
        {
            return await _context
                .Empleos.OrderByDescending(e => e.FechaPublicacion)
                .Take(conteo)
                .ToListAsync();
        }

        public async Task<IEnumerable<Empleo>> GetEmpleosByTituloAsync(string titulo)
        {
            return await _context
                .Empleos.Where(e => EF.Functions.Like(e.Titulo.ToLower(), $"%{titulo.ToLower()}%"))
                .ToListAsync();
        }

        public async Task<List<Empleo>> GetAllEmpleosAsync()
        {
            return await _context.Empleos.Include(e => e.Empresa).ToListAsync();
        }

        public async Task DeleteByEmpresaAsync(int empresa)
        {
            var empleos = await _context.Empleos.Where(e => e.EmpresaId == empresa).ToListAsync();
            _context.Empleos.RemoveRange(empleos);
            await _context.SaveChangesAsync();
        }
    }
}
