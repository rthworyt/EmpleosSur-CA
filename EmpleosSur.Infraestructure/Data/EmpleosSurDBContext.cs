using EmpleosSur.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EmpleosSur.Infraestructure.Data
{
    public class EmpleosSurDBContext : DbContext
    {
        public EmpleosSurDBContext(DbContextOptions<EmpleosSurDBContext> options) : base(options) { }

        public DbSet<Candidato> Candidatos { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Empleo> Empleos { get; set; }
        public DbSet<Postulacion> Postulaciones { get; set; }
        public DbSet<ExperienciaLaboral> ExperienciasLaborales { get; set; }
        public DbSet<InformacionAcademica> InformacionesAcademicas { get; set; }
    }
}
