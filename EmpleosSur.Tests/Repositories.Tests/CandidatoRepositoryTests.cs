//using EmpleosSur.Domain.Entities;
//using EmpleosSur.Infraestructure.Data;
//using EmpleosSur.Infraestructure.Repositories;
//using Microsoft.EntityFrameworkCore;

//namespace EmpleosSur.Tests.Repositories
//{
//    [TestClass]
//    public class CandidatoRepositoryTests
//    {
//        private EmpleosSurDBContext _context;
//        private Repository<Candidato> _repository;

//        [TestInitialize]
//        public void Setup()
//        {
//            var options = new DbContextOptionsBuilder<EmpleosSurDBContext>()
//                .UseInMemoryDatabase(databaseName: "PruebaDB")
//                .Options;

//            _context = new EmpleosSurDBContext(options);
//            _repository = new Repository<Candidato>(_context);
//        }

//        [TestMethod]
//        public async Task CreaCandidato()
//        {
//            // Arrange
//            var candidato = new Candidato
//            {
//                Nombre = "Juan Pérez Soro",
//                Email = "juansoro@gmail.com"
//            };

//            // Act
//            await _repository.CreateAsync(candidato);
//            var result = await _repository.GetByIdAsync(candidato.Id);

//            // Assert
//            Assert.IsNotNull(result);
//            Assert.AreEqual("juansoro@gmail.com", result.Email);
//        }
//    }
//}
