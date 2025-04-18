using EmpleosSur.Domain.Entities;
using EmpleosSur.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EmpleosSur.Tests.Repositories
{
    [TestClass]
    public class CandidatoRepositoryTests
    {
        private EmpleosSurDBContext _context;
        private CandidatoRepository _repository;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<EmpleosSurDBContext>()
                .UseInMemoryDatabase(databaseName: "EmpleosSurDB" + Guid.NewGuid())
                .EnableSensitiveDataLogging()
                .Options;

            _context = new EmpleosSurDBContext(options);
            _repository = new CandidatoRepository(_context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [TestMethod]
        public async Task GetAllDataCandidatoById_RetornaCandidatos()
        {
            var empresa = new Empresa
            {
                Nombre = "TechRD",
                Direccion = "Av. Siempre Viva",
                Telefono = "8091234567",
                CedulaRepresentante = "00123456789",
                EmailCorporativo = "contacto@techrd.com",
                NombreRepresentante = "Carlos M�ndez",
                RNC = "123456789"
            };

            var empleo = new Empleo
            {
                Titulo = "Desarrollador .NET",
                Descripcion = "Desarrollador con experiencia en C# y .NET",
                Requisitos = "Experiencia en ASP.NET",
                Ubicacion = "Santo Domingo",
                Salario = 55000,
                FechaPublicacion = DateTime.Today,
                Empresa = empresa
            };

            var postulacion = new Postulacion
            {
                Empleo = empleo,
                FechaPostulacion = DateTime.Today,
                Estado = EstadoPostulacion.Pendiente
            };

            var candidato = new Candidato
            {
                Nombre = "Juan",
                Apellido = "P�rez",
                Email = "juanpi27@icloud.com",
                Telefono = "1234567890",
                Ciudad = "Santo Domingo",
                Direccion = "Calle Falsa 123",
                Descripcion = "Soy un desarrollador apasionado.",
                FechaNacimiento = new DateTime(1990, 5, 10),
                Genero = "Masculino",
                InformacionesAcademicas = new List<InformacionAcademica>
                {
                    new InformacionAcademica
                    {
                        TituloObtenido = "Ingeniero",
                        Institucion = "Universidad XYZ",
                        Nivel = NivelAcademico.Universitario,
                        FechaInicio = new DateTime(2008, 1, 1),
                        FechaFin = new DateTime(2012, 1, 1),
                        EnCurso = false
                    }
                },
                ExperienciasLaborales = new List<ExperienciaLaboral>
                {
                    new ExperienciaLaboral
                    {
                        Empresa = "Empresa X",
                        Cargo = "Desarrollador Junior",
                        FechaInicio = new DateTime(2013, 1, 1),
                        FechaFin = new DateTime(2015, 1, 1),
                        Descripcion = "Desarrollador de software"
                    }
                },
                Postulaciones = new List<Postulacion> { postulacion }
            };

            _context.Empresas.Add(empresa);
            _context.Empleos.Add(empleo);
            _context.Candidatos.Add(candidato);
            await _context.SaveChangesAsync();

            var result = await _repository.GetAllDataCandidatoById(candidato.Id);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.InformacionesAcademicas.Count);
            Assert.AreEqual(1, result.ExperienciasLaborales.Count);
            Assert.AreEqual(1, result.Postulaciones.Count);
            Assert.AreEqual("Desarrollador .NET", result.Postulaciones.First().Empleo.Titulo);
        }

        [TestMethod]
        public async Task GetCandidatoByEmail_RetornaCandidatoConPostulaciones()
        {
            var empresa = new Empresa
            {
                Nombre = "Innovatech",
                Direccion = "Calle Principal 456",
                Telefono = "8096543210",
                CedulaRepresentante = "00234567890",
                EmailCorporativo = "info@innovatech.com",
                NombreRepresentante = "Ana Lopez",
                RNC = "987654321",
                Ciudad = "Santiago"
            };

            var empleo = new Empleo
            {
                Titulo = "Desarrollador Web",
                Descripcion = "Desarrollador con experiencia en HTML, CSS y JavaScript",
                Requisitos = "Conocimientos en frameworks modernos",
                Ubicacion = "Santiago",
                Salario = 45000,
                FechaPublicacion = DateTime.Today,
                Empresa = empresa
            };

            var postulacion = new Postulacion
            {
                Empleo = empleo,
                FechaPostulacion = DateTime.Today,
                Estado = EstadoPostulacion.Pendiente
            };

            var candidato = new Candidato
            {
                Nombre = "Maria",
                Apellido = "Dominicana",
                Email = "mariadominicana@gmail.com",
                Telefono = "8091239876",
                Ciudad = "Santiago",
                Direccion = "Calle Secundaria 789",
                Descripcion = "Desarrolladora web con experiencia en proyectos freelance.",
                FechaNacimiento = new DateTime(1992, 3, 15),
                Genero = "Femenino",
                InformacionesAcademicas = new List<InformacionAcademica>
                {
                    new InformacionAcademica
                    {
                        TituloObtenido = "Licenciada en Dise�o Web",
                        Institucion = "Universidad Creativa",
                        Nivel = NivelAcademico.Universitario,
                        FechaInicio = new DateTime(2010, 8, 1),
                        FechaFin = new DateTime(2014, 5, 1),
                        EnCurso = false
                    }
                },
                ExperienciasLaborales = new List<ExperienciaLaboral>
                {
                    new ExperienciaLaboral
                    {
                        Empresa = "Freelance",
                        Cargo = "Desarrolladora Web",
                        FechaInicio = new DateTime(2015, 1, 1),
                        FechaFin = new DateTime(2020, 1, 1),
                        Descripcion = "Dise�o y desarrollo de sitios web personalizados"
                    }
                },
                Postulaciones = new List<Postulacion> { postulacion }
            };

            _context.Empresas.Add(empresa);
            _context.Empleos.Add(empleo);
            _context.Candidatos.Add(candidato);
            await _context.SaveChangesAsync();

            var result = await _repository.GetCandidatoByEmail("mariadominicana@gmail.com");

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Postulaciones.Count);
            Assert.IsNotNull(result.Postulaciones.First().Empleo);
            Assert.AreEqual("Desarrollador Web", result.Postulaciones.First().Empleo.Titulo);
        }

        [TestMethod]
        public async Task GetCandidatosByCiudad_RetornaCandidatosFiltrados()
        {
            var candidato1 = new Candidato
            {
                Nombre = "Pedro",
                Apellido = "Gomez",
                Email = "pedro.gomez@example.com",
                Telefono = "8091234567",
                Ciudad = "Santo Domingo",
                Direccion = "Calle Principal 123",
                Descripcion = "Desarrollador de software con 5 años de experiencia.",
                FechaNacimiento = new DateTime(1985, 4, 23),
                Genero = "Masculino",
                InformacionesAcademicas = new List<InformacionAcademica>
                {
                    new InformacionAcademica
                    {
                        TituloObtenido = "Ingeniero en Sistemas",
                        Institucion = "Universidad ABC",
                        Nivel = NivelAcademico.Universitario,
                        FechaInicio = new DateTime(2003, 8, 1),
                        FechaFin = new DateTime(2007, 5, 1),
                        EnCurso = false
                    }
                },
                ExperienciasLaborales = new List<ExperienciaLaboral>
                {
                    new ExperienciaLaboral
                    {
                        Empresa = "Tech Solutions",
                        Cargo = "Desarrollador Senior",
                        FechaInicio = new DateTime(2010, 1, 1),
                        FechaFin = new DateTime(2015, 1, 1),
                        Descripcion = "Desarrollo de aplicaciones web"
                    }
                }
            };

            var candidato2 = new Candidato
            {
                Nombre = "Laura",
                Apellido = "Martinez",
                Email = "laura.martinez@example.com",
                Telefono = "8097654321",
                Ciudad = "Santo Domingo",
                Direccion = "Avenida Central 456",
                Descripcion = "Analista de sistemas con 3 años de experiencia.",
                FechaNacimiento = new DateTime(1990, 7, 15),
                Genero = "Femenino",
                InformacionesAcademicas = new List<InformacionAcademica>
                {
                    new InformacionAcademica
                    {
                        TituloObtenido = "Licenciada en Informática",
                        Institucion = "Universidad XYZ",
                        Nivel = NivelAcademico.Universitario,
                        FechaInicio = new DateTime(2008, 8, 1),
                        FechaFin = new DateTime(2012, 5, 1),
                        EnCurso = false
                    }
                },
                ExperienciasLaborales = new List<ExperienciaLaboral>
                {
                    new ExperienciaLaboral
                    {
                        Empresa = "Data Corp",
                        Cargo = "Analista de Sistemas",
                        FechaInicio = new DateTime(2013, 1, 1),
                        FechaFin = new DateTime(2016, 1, 1),
                        Descripcion = "Análisis y diseño de sistemas"
                    }
                }
            };

            var candidato3 = new Candidato
            {
                Nombre = "Kelvis",
                Apellido = "Rodriguez",
                Email = "kelvis.rodriguez@example.com",
                Telefono = "8099876543",
                Ciudad = "La Vega",
                Direccion = "Calle Secundaria 789",
                Descripcion = "Ingeniero de software con 7 años de experiencia.",
                FechaNacimiento = new DateTime(1988, 11, 30),
                Genero = "Masculino",
                InformacionesAcademicas = new List<InformacionAcademica>
                {
                    new InformacionAcademica
                    {
                        TituloObtenido = "Ingeniero en Computación",
                        Institucion = "Instituto Tecnológico",
                        Nivel = NivelAcademico.Universitario,
                        FechaInicio = new DateTime(2005, 8, 1),
                        FechaFin = new DateTime(2009, 5, 1),
                        EnCurso = false
                    }
                },
                ExperienciasLaborales = new List<ExperienciaLaboral>
                {
                    new ExperienciaLaboral
                    {
                        Empresa = "Innovatech",
                        Cargo = "Ingeniero de Software",
                        FechaInicio = new DateTime(2011, 1, 1),
                        FechaFin = new DateTime(2018, 1, 1),
                        Descripcion = "Desarrollo de soluciones tecnológicas"
                    }
                }
            };

            _context.Candidatos.AddRange(candidato1, candidato2, candidato3);
            await _context.SaveChangesAsync();

            var result = await _repository.GetCandidatoByCiudad("Santo Domingo");

            Assert.AreEqual(2, result.Count()); // Ahora esperamos que se devuelvan 2 candidatos
            Assert.IsTrue(result.Any(c => c.Nombre == "Pedro"));
            Assert.IsTrue(result.Any(c => c.Nombre == "Laura"));
        }
    }
}
