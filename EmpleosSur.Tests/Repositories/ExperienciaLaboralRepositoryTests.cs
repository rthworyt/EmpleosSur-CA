using EmpleosSur.Domain.Entities;
using EmpleosSur.Infraestructure.Data;
using EmpleosSur.Infraestructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EmpleosSur.Tests.Repositories
{
    [TestClass]
    public class ExperienciaLaboralRepositoryTests
    {
        private EmpleosSurDBContext _context;
        private ExperienciaLaboralRepository _repository;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<EmpleosSurDBContext>()
                .UseInMemoryDatabase(databaseName: "EmpleosSurDB" + Guid.NewGuid())
                .EnableSensitiveDataLogging()
                .Options;

            _context = new EmpleosSurDBContext(options);
            _repository = new ExperienciaLaboralRepository(_context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [TestMethod]
        public async Task GetExperienciaByCandidatoIdAsync_RetornaExperienciasDeCandidato()
        {
            var candidatoId = 1;

            var experiencia1 = new ExperienciaLaboral
            {
                Cargo = "Desarrollador .NET",
                Empresa = "TechRD",
                Descripcion = "Desarrollo de aplicaciones web con .NET",
                FechaInicio = new DateTime(2020, 1, 1),
                FechaFin = new DateTime(2022, 1, 1),
                CandidatoId = candidatoId
            };

            var experiencia2 = new ExperienciaLaboral
            {
                Cargo = "Analista de Datos",
                Empresa = "Innovatech",
                Descripcion = "Análisis de datos y generación de reportes",
                FechaInicio = new DateTime(2018, 1, 1),
                FechaFin = new DateTime(2020, 1, 1),
                CandidatoId = candidatoId
            };

            var experiencia3 = new ExperienciaLaboral
            {
                Cargo = "Administrador de Sistemas",
                Empresa = "SysAdmin Corp",
                Descripcion = "Mantenimiento de servidores y redes",
                FechaInicio = new DateTime(2015, 1, 1),
                FechaFin = new DateTime(2017, 1, 1),
                CandidatoId = 2 // Otro candidato
            };

            _context.ExperienciasLaborales.AddRange(experiencia1, experiencia2, experiencia3);
            await _context.SaveChangesAsync();

            var result = await _repository.GetExperienciaByCandidatoIdAsync(candidatoId);

            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.Any(e => e.Cargo == "Desarrollador .NET"));
            Assert.IsTrue(result.Any(e => e.Cargo == "Analista de Datos"));
        }
    }
}
