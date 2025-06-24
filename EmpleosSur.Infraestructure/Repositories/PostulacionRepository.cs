using EmpleosSur.Core.Interfaces;
using EmpleosSur.Domain.Entities;
using EmpleosSur.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EmpleosSur.Infraestructure.Repositories
{
    public class PostulacionRepository : Repository<Postulacion>, IPostulacionRepository
    {
        private readonly EmpleosSurDBContext _context;

        public PostulacionRepository(EmpleosSurDBContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<int> ConteoByEmpleoAsync(int empleoId)
        {
            return await _context.Postulaciones.CountAsync(p => p.EmpleoId == empleoId);
        }

        public async Task<IEnumerable<Postulacion>> GetPostulacionByCandidatoAsync(int candidatoId)
        {
            return await _context.Postulaciones.Where(p => p.CandidatoId == candidatoId).ToListAsync();
        }

        public async Task<IEnumerable<Postulacion>> GetPostulacionByEmpleoAsync(int empleoId)
        {
            return await _context.Postulaciones.Where(p => p.EmpleoId == empleoId).ToListAsync();
        }
    }
}
