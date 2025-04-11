using EmpleosSur.Application.Interfaces.IServices;
using EmpleosSur.Application.Services;
using EmpleosSur.Domain.Entities;
using Moq;

namespace EmpleosSur.Tests.Services
{
    [TestClass]
    public class CandidatoServiceTests
    {
        private Mock<IRepository<Candidato>> _mockRepository;
        private CandidatoService _candidatoService;

        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<IRepository<Candidato>>();
            _candidatoService = new CandidatoService(_mockRepository.Object);
        }

        [TestMethod]
        public async Task SiExiste_RetornaCandidato()
        {
            // Arrange
            var email = "sabrinaperez@email.com";
            var candidato = new Candidato { Id = 1, Email = email };
            _mockRepository
                .Setup(r =>
                    r.GetAllAsync(
                        It.IsAny<System.Linq.Expressions.Expression<System.Func<Candidato, bool>>>()
                    )
                )
                .ReturnsAsync(new List<Candidato> { candidato });

            // Act
            var result = await _candidatoService.GetCandidatoByEmailAsync(email);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(email, result.Email);
        }

        [TestMethod]
        public async Task CreacionDeCandidato()
        {
            // Arrange
            var candidato = new Candidato { Id = 2, Email = "joelperez@yahoo.com" };

            _mockRepository.Setup(r => r.CreateAsync(candidato)).Returns(Task.CompletedTask);

            // Act
            var result = await _candidatoService.CreateCandidatoAsync(candidato);

            // Assert
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        [ExpectedException(typeof(System.Exception), "El candidato no existe.")]
        public async Task EliminaCandidato_ErrorSiNoEncuentra()
        {
            // Arrange
            int id = 99;
            _mockRepository.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Candidato)null);

            // Act
            await _candidatoService.DeleteCandidatoAsync(id);
        }

        [TestMethod]
        public async Task EliminaCandidato_SiExiste()
        {
            // Arrange
            var candidato = new Candidato { Id = 1 };
            _mockRepository.Setup(r => r.GetByIdAsync(candidato.Id)).ReturnsAsync(candidato);
            _mockRepository.Setup(r => r.DeleteAsync(candidato.Id)).ReturnsAsync(true);

            // Act
            await _candidatoService.DeleteCandidatoAsync(candidato.Id);

            // Assert
            _mockRepository.Verify(r => r.DeleteAsync(candidato.Id), Times.Once);
        }

        [TestMethod]
        public async Task ModificaCandidato()
        {
            // Arrange
            var candidato = new Candidato { Id = 3 };
            _mockRepository.Setup(r => r.UpdateAsync(candidato)).Returns(Task.CompletedTask);

            // Act
            await _candidatoService.UpdateCandidatoAsync(candidato);

            // Assert
            _mockRepository.Verify(r => r.UpdateAsync(candidato), Times.Once);
        }

        [TestMethod]
        public async Task EncuentraCandidatoById()
        {
            // Arrange
            int id = 5;
            var candidato = new Candidato { Id = id };
            _mockRepository.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(candidato);

            // Act
            var result = await _candidatoService.GetCandidatoByIdAsync(id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(id, result.Id);
        }
    }
}
