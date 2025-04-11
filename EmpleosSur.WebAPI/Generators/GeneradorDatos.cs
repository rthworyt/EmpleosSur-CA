using Bogus;
using EmpleosSur.Domain.Entities;
using EmpleosSur.Infraestructure.Data;

namespace EmpleosSur.WebAPI.Generators
{
    public class FakeDataGenerator
    {
        private readonly EmpleosSurDBContext _context;

        public FakeDataGenerator(EmpleosSurDBContext context)
        {
            _context = context;
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
    }
}
