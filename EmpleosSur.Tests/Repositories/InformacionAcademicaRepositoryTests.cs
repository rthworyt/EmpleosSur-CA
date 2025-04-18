using EmpleosSur.Domain.Entities;
using EmpleosSur.Infraestructure.Data;
using EmpleosSur.Infraestructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EmpleosSur.Tests.Repositories
{
    [TestClass]
    public class InformacionAcademicaRepositoryTests
    {
        private EmpleosSurDBContext _context;
        private InformacionAcademicaRepository _repository;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<EmpleosSurDBContext>()
                .UseInMemoryDatabase(databaseName: "EmpleosSurDB" + Guid.NewGuid())
                .EnableSensitiveDataLogging()
                .Options;

            _context = new EmpleosSurDBContext(options);
            _repository = new InformacionAcademicaRepository(_context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [TestMethod]
        public async Task GetInformacionAcademicaByCandidatoIdAsync_RetornaInformacionDeCandidato()
        {
            var candidatoId = 1;

            var info1 = new InformacionAcademica
            {
                TituloObtenido = "Ingeniería en Sistemas",
                Institucion = "Universidad Regional A",
                FechaInicio = new DateTime(2015, 1, 1),
                FechaFin = new DateTime(2019, 1, 1),
                CandidatoId = candidatoId
            };

            var info2 = new InformacionAcademica
            {
                TituloObtenido = "Maestría en Ciencia de Datos",
                Institucion = "Universidad Catolica B",
                FechaInicio = new DateTime(2020, 1, 1),
                FechaFin = new DateTime(2022, 1, 1),
                CandidatoId = candidatoId
            };

            var info3 = new InformacionAcademica
            {
                TituloObtenido = "Diplomado en Redes",
                Institucion = "Instituto Tecnologico C",
                FechaInicio = new DateTime(2018, 1, 1),
                FechaFin = new DateTime(2019, 1, 1),
                CandidatoId = 2
            };

            _context.InformacionesAcademicas.AddRange(info1, info2, info3);
            await _context.SaveChangesAsync();

            var result = await _repository.GetInformacionAcademicaByCandidatoIdAsync(candidatoId);

            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.Any(ia => ia.TituloObtenido == "Ingeniería en Sistemas"));
            Assert.IsTrue(result.Any(ia => ia.TituloObtenido == "Maestría en Ciencia de Datos"));
        }
    }
}
