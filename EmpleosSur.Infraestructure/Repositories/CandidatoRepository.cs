using EmpleosSur.Application.Interfaces.IRepositories;
using EmpleosSur.Domain.Entities;
using EmpleosSur.Infraestructure.Data;
using EmpleosSur.Infraestructure.Repositories;
using Microsoft.EntityFrameworkCore;

public class CandidatoRepository : Repository<Candidato>, ICandidatoRepository
{
    private readonly EmpleosSurDBContext _context;

    public CandidatoRepository(EmpleosSurDBContext context)
        : base(context)
    {
        _context = context;
    }

    public async Task<Candidato> GetAllDataCandidatoById(int id)
    {
        return await _context
            .Candidatos.Include(c => c.InformacionesAcademicas)
            .Include(c => c.ExperienciasLaborales)
            .Include(c => c.Postulaciones)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Candidato> GetCandidatoByEmail(string email)
    {
        return await _context
            .Candidatos.Include(c => c.Postulaciones)
            .ThenInclude(p => p.Empleo)
            .FirstOrDefaultAsync(c => c.Email == email);
    }

    public async Task<IEnumerable<Candidato>> GetCandidatoByCiudad(string ciudad)
    {
        return await _context.Candidatos.Where(c => c.Ciudad == ciudad).ToListAsync();
    }
}
