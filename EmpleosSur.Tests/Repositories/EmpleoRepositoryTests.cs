using EmpleosSur.Domain.Entities;
using EmpleosSur.Infraestructure.Data;
using EmpleosSur.Infraestructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EmpleosSur.Tests.Repositories
{
    [TestClass]
    public class EmpleoRepositoryTests
    {
        private EmpleosSurDBContext _context;
        private EmpleoRepository _repository;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<EmpleosSurDBContext>()
                .UseInMemoryDatabase(databaseName: "EmpleosSurDB" + Guid.NewGuid())
                .EnableSensitiveDataLogging()
                .Options;

            _context = new EmpleosSurDBContext(options);
            _repository = new EmpleoRepository(_context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [TestMethod]
        public async Task GetEmpleosByEmpresaIdAsync_RetornaEmpleosDeEmpresa()
        {
            var empresa = new Empresa
            {
                Nombre = "TechRD",
                Direccion = "Av. Siempre Viva",
                Ciudad = "Santo Domingo",
                Telefono = "8091234567",
                CedulaRepresentante = "00123456789",
                EmailCorporativo = "contacto@techrd.com",
                NombreRepresentante = "Carlos Mendez",
                RNC = "123456789"
            };

            var empleo1 = new Empleo
            {
                Titulo = "Desarrollador .NET",
                Descripcion = "Desarrollador con experiencia en C# y .NET",
                Requisitos = "Experiencia en ASP.NET",
                Ubicacion = "Santo Domingo",
                Salario = 55000,
                FechaPublicacion = DateTime.Today,
                Empresa = empresa
            };

            var empleo2 = new Empleo
            {
                Titulo = "Analista de Datos",
                Descripcion = "Experiencia en análisis de datos y SQL",
                Requisitos = "Conocimientos en Power BI",
                Ubicacion = "Santiago",
                Salario = 45000,
                FechaPublicacion = DateTime.Today,
                Empresa = empresa
            };

            _context.Empresas.Add(empresa);
            _context.Empleos.AddRange(empleo1, empleo2);
            await _context.SaveChangesAsync();

            var result = await _repository.GetEmpleosByEmpresaIdAsync(empresa.Id);

            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.Any(e => e.Titulo == "Desarrollador .NET"));
            Assert.IsTrue(result.Any(e => e.Titulo == "Analista de Datos"));
        }

        [TestMethod]
        public async Task GetRecentEmpleosAsync_RetornaEmpleosRecientes()
        {
            var empleo1 = new Empleo
            {
                Titulo = "Desarrollador .NET",
                Descripcion = "Desarrollador con experiencia en C# y .NET",
                Requisitos = "Experiencia en ASP.NET",
                Ubicacion = "Santo Domingo",
                Salario = 55000,
                FechaPublicacion = DateTime.Today.AddDays(-5)
            };

            var empleo2 = new Empleo
            {
                Titulo = "Analista de Datos",
                Descripcion = "Experiencia en análisis de datos y SQL",
                Requisitos = "Conocimientos en Power BI",
                Ubicacion = "Santiago",
                Salario = 45000,
                FechaPublicacion = DateTime.Today
            };

            _context.Empleos.AddRange(empleo1, empleo2);
            await _context.SaveChangesAsync();

            var result = await _repository.GetRecentEmpleosAsync(1);

            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("Analista de Datos", result.First().Titulo);
        }

        [TestMethod]
        public async Task GetEmpleosByTituloAsync_RetornaEmpleosFiltradosPorTitulo()
        {
            var empleo1 = new Empleo
            {
                Titulo = "Desarrollador .NET",
                Descripcion = "Desarrollador con experiencia en C# y .NET",
                Requisitos = "Experiencia en ASP.NET",
                Ubicacion = "Santo Domingo",
                Salario = 55000,
                FechaPublicacion = DateTime.Today
            };

            var empleo2 = new Empleo
            {
                Titulo = "Analista de Datos",
                Descripcion = "Experiencia en análisis de datos y SQL",
                Requisitos = "Conocimientos en Power BI",
                Ubicacion = "Santiago",
                Salario = 45000,
                FechaPublicacion = DateTime.Today
            };

            _context.Empleos.AddRange(empleo1, empleo2);
            await _context.SaveChangesAsync();

            var result = await _repository.GetEmpleosByTituloAsync(".NET");

            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("Desarrollador .NET", result.First().Titulo);
        }
    }
}
