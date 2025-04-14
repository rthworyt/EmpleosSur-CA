using EmpleosSur.Application.Interfaces.IRepositories;
using EmpleosSur.Domain.Entities;
using EmpleosSur.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EmpleosSur.Infraestructure.Repositories
{
    public class EmpresaRepository : Repository<Empresa>, IEmpresaRepository
    {
        private readonly EmpleosSurDBContext _context;

        public EmpresaRepository(EmpleosSurDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Empresa>> GetEmpresaByLocalidad(string localidad)
        {
            return await _context.Empresas
                .Where(e => e.Direccion.Contains(localidad)).ToListAsync();
        }

        public async Task<Empresa> GetEmpresaByRNC(string rnc)
        {
            return await _context.Empresas
                .FirstOrDefaultAsync(e => e.RNC == rnc);
        }

        public async Task<Empresa> GetEmpresaByEmail(string email)
        {
            return await _context.Empresas
                .FirstOrDefaultAsync(e => e.EmailCorporativo == email);
        }

        public async Task<IEnumerable<Empresa>> GetEmpresaByNombre(string nombre)
        {
            return await _context.Empresas
                .Where(e => e.Nombre.Contains(nombre))
                .ToListAsync();
        }
    }
}
