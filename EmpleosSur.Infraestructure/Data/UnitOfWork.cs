using EmpleosSur.Application.Interfaces;
using EmpleosSur.Application.Interfaces.IRepositories;
using EmpleosSur.Domain.Entities;
using EmpleosSur.Infraestructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly EmpleosSurDBContext _context;

    public IRepository<Postulacion> Postulaciones { get; }
    public IRepository<Candidato> Candidatos { get; }
    public IRepository<Empleo> Empleos { get; }

    public UnitOfWork(
        EmpleosSurDBContext context,
        IRepository<Postulacion> postulaciones,
        IRepository<Candidato> candidatos,
        IRepository<Empleo> empleos
    )
    {
        _context = context;
        Postulaciones = postulaciones;
        Candidatos = candidatos;
        Empleos = empleos;
    }

    public async Task<int> CompletarAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}
