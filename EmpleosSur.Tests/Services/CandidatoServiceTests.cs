using Moq;
using EmpleosSur.Application.Interfaces.IRepositories;
using EmpleosSur.Application.Interfaces.IServices;
using EmpleosSur.Domain.Entities;

namespace EmpleosSur.Tests.Services
{
    [TestClass]
    public class CandidatoServiceTests
    {
        private Mock<ICandidatoRepository> _mockRepo;
        private ICandidatoService _service;

        [TestInitialize]
        public void Setup()
        {
            _mockRepo = new Mock<ICandidatoRepository>();
            _service = new CandidatoService(_mockRepo.Object);
        }

        [TestMethod]
        public async Task GetAllDataCandidatoById_RetornarCandidatoPorId()
        {
            var candidato = new Candidato { Id = 1 };
            _mockRepo.Setup(r => r.GetAllDataCandidatoById(1)).ReturnsAsync(candidato);

            var result = await _service.GetAllDataCandidatoById(1);

            Assert.AreEqual(candidato, result);
        }

        [TestMethod]
        public async Task GetCandidatoByEmail_RetornaCandidatoPorEmail()
        {
            var email = "turealpicapollo@hotmail.com";
            var candidato = new Candidato { Email = email };
            _mockRepo.Setup(r => r.GetCandidatoByEmail(email)).ReturnsAsync(candidato);

            var result = await _service.GetCandidatoByEmail(email);

            Assert.AreEqual(candidato, result);
        }

        [TestMethod]
        public async Task GetCandidatoByCiudad_RetornaListaCandidatos()
        {
            var ciudad = "Barahona";
            var candidatos = new List<Candidato> { new Candidato { Ciudad = ciudad } };
            _mockRepo.Setup(r => r.GetCandidatoByCiudad(ciudad)).ReturnsAsync(candidatos);

            var result = await _service.GetCandidatoByCiudad(ciudad);

            CollectionAssert.AreEqual(candidatos, (List<Candidato>)result);
        }

        [TestMethod]
        public async Task CreateCandidato_RetornaConExito()
        {
            var candidato = new Candidato();
            _mockRepo.Setup(r => r.CreateAsync(candidato)).Returns(Task.CompletedTask);

            var result = await _service.CreateCandidato(candidato);

            Assert.IsTrue(result.Success);
            Assert.IsNull(result.ErrorMessage);
        }

        [TestMethod]
        public async Task CreateCandidato_RetornaFallo()
        {
            var candidato = new Candidato();
            _mockRepo.Setup(r => r.CreateAsync(candidato)).ThrowsAsync(new System.Exception("Error"));

            var result = await _service.CreateCandidato(candidato);

            Assert.IsFalse(result.Success);
            Assert.IsTrue(result.ErrorMessage.Contains("Error"));
        }

        [TestMethod]
        public async Task DeleteCandidato_RetornarFallo_CuandoNoEncontrado()
        {
            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Candidato)null);

            var result = await _service.DeleteCandidato(1);

            Assert.IsFalse(result.Success);
            Assert.AreEqual("El candidato no existe.", result.ErrorMessage);
        }

        [TestMethod]
        public async Task DeleteCandidato_RetornaExito_SiExiste()
        {
            var candidato = new Candidato { Id = 1 };
            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(candidato);
            _mockRepo.Setup(r => r.DeleteAsync(1)).ReturnsAsync(true);

            var result = await _service.DeleteCandidato(1);

            Assert.IsTrue(result.Success);
            Assert.IsNull(result.ErrorMessage);
        }

        [TestMethod]
        public async Task UpdateCandidato_RetornaExito()
        {
            var candidato = new Candidato { Id = 1 };
            _mockRepo.Setup(r => r.UpdateAsync(candidato)).Returns(Task.CompletedTask);

            var result = await _service.UpdateCandidato(candidato);

            Assert.IsTrue(result.Success);
            Assert.IsNull(result.ErrorMessage);
        }
    }
}
