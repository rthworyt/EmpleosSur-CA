using EmpleosSur.Domain.Entities;
using EmpleosSur.Infraestructure.Data;
using EmpleosSur.Infraestructure.Repositories;
using Microsoft.EntityFrameworkCore;

public class InformacionAcademicaRepository : Repository<InformacionAcademica>, IInformacionAcademicaRepository
{
    private readonly EmpleosSurDBContext _context;

    public InformacionAcademicaRepository(EmpleosSurDBContext context)
        : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<InformacionAcademica>> GetInformacionAcademicaByCandidatoIdAsync(int candidatoId)
    {
        return await _context.InformacionesAcademicas
            .Where(ia => ia.CandidatoId == candidatoId)
            .ToListAsync();
    }
}
