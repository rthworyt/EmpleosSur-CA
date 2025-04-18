using Moq;
using EmpleosSur.Application.Interfaces.IServices;
using EmpleosSur.Application.Services;
using EmpleosSur.Core.Interfaces;
using EmpleosSur.Domain.Entities;

namespace EmpleosSur.Tests.Services
{
    [TestClass]
    public class EmpleoServiceTests
    {
        private Mock<IEmpleoRepository> _mockRepo;
        private IEmpleoService _service;

        [TestInitialize]
        public void Setup()
        {
            _mockRepo = new Mock<IEmpleoRepository>();
            _service = new EmpleoService(_mockRepo.Object);
        }

        [TestMethod]
        public async Task SearchEmpleosByTitulo_RetornaEmpleosCoincidentes()
        {
            var titulo = "Desarrollador Web";
            var empleos = new List<Empleo> { new Empleo { Titulo = titulo } };
            _mockRepo.Setup(r => r.GetEmpleosByTituloAsync(titulo)).ReturnsAsync(empleos);

            var result = await _service.SearchEmpleosByTitulo(titulo);

            CollectionAssert.AreEqual(empleos, (List<Empleo>)result);
        }

        [TestMethod]
        public async Task GetAllEmpleosAsync_RetornaTodosLosEmpleos()
        {
            var empleos = new List<Empleo> { new Empleo { Id = 1 }, new Empleo { Id = 2 } };
            _mockRepo.Setup(r => r.GetAllEmpleosAsync()).ReturnsAsync(empleos);

            var result = await _service.GetAllEmpleosAsync();

            CollectionAssert.AreEqual(empleos, result);
        }

        [TestMethod]
        public async Task GetRecentEmpleosAsync_RetornaEmpleosRecientes()
        {
            var cantidad = 3;
            var empleos = new List<Empleo> { new Empleo(), new Empleo(), new Empleo() };
            _mockRepo.Setup(r => r.GetRecentEmpleosAsync(cantidad)).ReturnsAsync(empleos);

            var result = await _service.GetRecentEmpleosAsync(cantidad);

            CollectionAssert.AreEqual(empleos, (List<Empleo>)result);
        }

        [TestMethod]
        public async Task GetEmpleosByEmpresaId_RetornaEmpleosPorEmpresa()
        {
            var empresaId = 5;
            var empleos = new List<Empleo> { new Empleo { EmpresaId = empresaId } };
            _mockRepo.Setup(r => r.GetEmpleosByEmpresaIdAsync(empresaId)).ReturnsAsync(empleos);

            var result = await _service.GetEmpleosByEmpresaId(empresaId);

            CollectionAssert.AreEqual(empleos, (List<Empleo>)result);
        }
    }
}
