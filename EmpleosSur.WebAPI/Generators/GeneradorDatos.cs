using Bogus;
using EmpleosSur.Domain.Entities;
using EmpleosSur.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EmpleosSur.WebAPI.Generators
{
    public class FakeDataGenerator
    {
        private readonly EmpleosSurDBContext _context;

        public FakeDataGenerator(EmpleosSurDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task GenerateFakeCandidatos(int count = 10)
        {
            var faker = new Faker<Candidato>()
                .RuleFor(c => c.Nombre, f => f.Name.FirstName())
                .RuleFor(c => c.Apellido, f => f.Name.LastName())
                .RuleFor(c => c.Email, f => f.Internet.Email())
                .RuleFor(c => c.Genero, f => f.PickRandom("Masculino", "Femenino"))
                .RuleFor(c => c.FechaNacimiento, f => f.Date.Past(18)) // Asegura que sea mayor de edad
                .RuleFor(c => c.Telefono, f => f.Phone.PhoneNumber("##########"))
                .RuleFor(c => c.Ciudad, f => f.Address.City())
                .RuleFor(c => c.Direccion, f => f.Address.StreetAddress())
                .RuleFor(c => c.Descripcion, f => f.Lorem.Sentence());

            // Generar los candidatos falsos
            var candidatos = faker.Generate(count);

            // Insertar en la base de datos
            _context.Candidatos.AddRange(candidatos);
            await _context.SaveChangesAsync();
        }

        public async Task GenerateFakeEmpresas(int count = 10)
        {
            var faker = new Faker<Empresa>()
                .RuleFor(e => e.Nombre, f => f.Company.CompanyName())
                .RuleFor(e => e.NombreRepresentante, f => f.Name.FullName())
                .RuleFor(e => e.EmailCorporativo, f => f.Internet.Email())
                .RuleFor(e => e.Telefono, f => f.Phone.PhoneNumber("##########"))
                .RuleFor(e => e.RNC, f => f.Random.String2(11, "0123456789"))
                .RuleFor(e => e.Direccion, f => f.Address.StreetAddress())
                .RuleFor(e => e.CedulaRepresentante, f => f.Random.String2(11, "0123456789"))
                .RuleFor(e => e.TelefonoRepresentante, f => f.Phone.PhoneNumber("##########"))
                .RuleFor(e => e.RegistroMercantil, f => f.Random.String2(100));

            var empresas = faker.Generate(count);

            _context.Empresas.AddRange(empresas);
            await _context.SaveChangesAsync();
        }

        public async Task GenerateFakeEmpleos(int count = 10)
        {
            var faker = new Faker<Empleo>()
                .RuleFor(e => e.Titulo, f => f.Name.JobTitle())
                .RuleFor(e => e.Descripcion, f => f.Lorem.Paragraph())
                .RuleFor(e => e.Requisitos, f => f.Lorem.Paragraph())
                .RuleFor(e => e.Ubicacion, f => f.Address.City())
                .RuleFor(e => e.Salario, f => f.Finance.Amount(0, 100000))
                .RuleFor(e => e.FechaPublicacion, f => f.Date.Past())
                .RuleFor(e => e.EmpresaId, f => f.Random.Int(1, 100));

            var empleos = faker.Generate(count);

            _context.Empleos.AddRange(empleos);
            await _context.SaveChangesAsync();
        }

        public async Task GenerateFakePostulaciones(int count = 10)
        {
            var faker = new Faker<Postulacion>()
                .RuleFor(p => p.FechaPostulacion, f => f.Date.Past())
                .RuleFor(p => p.Estado, f => f.PickRandom<EstadoPostulacion>())
                .RuleFor(p => p.CandidatoId, f => f.Random.Int(1, 100))
                .RuleFor(p => p.EmpleoId, f => f.Random.Int(1, 100));

            var postulaciones = faker.Generate(count);

            _context.Postulaciones.AddRange(postulaciones);
            await _context.SaveChangesAsync();
        }

        public async Task GenerateFakeExperienciasLaborales(int count = 10)
        {
            var faker = new Faker<ExperienciaLaboral>()
                .RuleFor(e => e.Empresa, f => f.Company.CompanyName())
                .RuleFor(e => e.Cargo, f => f.Name.JobTitle())
                .RuleFor(e => e.FechaInicio, f => f.Date.Past())
                .RuleFor(e => e.FechaFin, f => f.Date.Past())
                .RuleFor(e => e.Descripcion, f => f.Lorem.Paragraph())
                .RuleFor(e => e.EnCurso, f => f.Random.Bool())
                .RuleFor(e => e.CandidatoId, f => f.Random.Int(1, 100));

            var experienciasLaborales = faker.Generate(count);

            _context.ExperienciasLaborales.AddRange(experienciasLaborales);
            await _context.SaveChangesAsync();
        }

        public async Task GenerateFakeInformacionesAcademicas(int count = 10)
        {
            var faker = new Faker<InformacionAcademica>()
                .RuleFor(i => i.TituloObtenido, f => f.Name.JobTitle())
                .RuleFor(i => i.Institucion, f => f.Company.CompanyName())
                .RuleFor(i => i.Nivel, f => f.PickRandom<NivelAcademico>())
                .RuleFor(i => i.FechaInicio, f => f.Date.Past())
                .RuleFor(i => i.FechaFin, f => f.Date.Past())
                .RuleFor(i => i.EnCurso, f => f.Random.Bool())
                .RuleFor(i => i.CandidatoId, f => f.Random.Int(1, 100));

            var informacionesAcademicas = faker.Generate(count);

            _context.InformacionesAcademicas.AddRange(informacionesAcademicas);
            await _context.SaveChangesAsync();
        }
    }
}
