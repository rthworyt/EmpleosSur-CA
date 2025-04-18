using EmpleosSur.Domain.Entities;
using EmpleosSur.Infraestructure.Data;
using EmpleosSur.Infraestructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EmpleosSur.Tests.Repositories
{
    [TestClass]
    public class PostulacionRepositoryTests
    {
        private EmpleosSurDBContext _context;
        private PostulacionRepository _repository;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<EmpleosSurDBContext>()
                .UseInMemoryDatabase(databaseName: "EmpleosSurDB" + Guid.NewGuid())
                .EnableSensitiveDataLogging()
                .Options;

            _context = new EmpleosSurDBContext(options);
            _repository = new PostulacionRepository(_context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [TestMethod]
        public async Task ConteoByEmpleoAsync_RetornaConteoDePostulaciones()
        {
            var empleo = new Empleo
            {
                Titulo = "Desarrollador .NET",
                Descripcion = "Desarrollador con experiencia en C# y .NET",
                Requisitos = "Experiencia en ASP.NET",
                Ubicacion = "Santo Domingo",
                Salario = 55000,
                FechaPublicacion = DateTime.Today
            };

            var postulacion1 = new Postulacion { Empleo = empleo, CandidatoId = 1 };
            var postulacion2 = new Postulacion { Empleo = empleo, CandidatoId = 2 };

            _context.Empleos.Add(empleo);
            _context.Postulaciones.AddRange(postulacion1, postulacion2);
            await _context.SaveChangesAsync();

            var result = await _repository.ConteoByEmpleoAsync(empleo.Id);

            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public async Task GetPostulacionByCandidatoAsync_RetornaPostulacionesDeCandidato()
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
                Descripcion = "Analista con experiencia en manejo de datos y SQL",
                Requisitos = "Conocimientos en Power BI y Python",
                Ubicacion = "Santiago",
                Salario = 45000,
                FechaPublicacion = DateTime.Today
            };

            var postulacion1 = new Postulacion { Empleo = empleo1, CandidatoId = 1 };
            var postulacion2 = new Postulacion { Empleo = empleo2, CandidatoId = 1 };
            var postulacion3 = new Postulacion { Empleo = empleo1, CandidatoId = 2 };

            _context.Empleos.AddRange(empleo1, empleo2);
            _context.Postulaciones.AddRange(postulacion1, postulacion2, postulacion3);
            await _context.SaveChangesAsync();

            var result = await _repository.GetPostulacionByCandidatoAsync(1);

            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.Any(p => p.Empleo.Titulo == "Desarrollador .NET"));
            Assert.IsTrue(result.Any(p => p.Empleo.Titulo == "Analista de Datos"));
        }

        [TestMethod]
        public async Task GetPostulacionByEmpleoAsync_RetornaPostulacionesDeEmpleo()
        {
            var empleo = new Empleo
            {
                Titulo = "Desarrollador .NET",
                Descripcion = "Desarrollador con experiencia en C# y .NET",
                Requisitos = "Experiencia en ASP.NET",
                Ubicacion = "Santo Domingo",
                Salario = 55000,
                FechaPublicacion = DateTime.Today
            };

            var postulacion1 = new Postulacion { Empleo = empleo, CandidatoId = 1 };
            var postulacion2 = new Postulacion { Empleo = empleo, CandidatoId = 2 };

            _context.Empleos.Add(empleo);
            _context.Postulaciones.AddRange(postulacion1, postulacion2);
            await _context.SaveChangesAsync();

            var result = await _repository.GetPostulacionByEmpleoAsync(empleo.Id);

            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.Any(p => p.CandidatoId == 1));
            Assert.IsTrue(result.Any(p => p.CandidatoId == 2));
        }
    }
}
