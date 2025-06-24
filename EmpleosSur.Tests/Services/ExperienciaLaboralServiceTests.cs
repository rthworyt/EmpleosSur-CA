using Moq;
using EmpleosSur.Application.Interfaces.IServices;
using EmpleosSur.Application.Services;
using EmpleosSur.Domain.Entities;
using EmpleosSur.Core.Interfaces;

namespace EmpleosSur.Tests.Services
{
    [TestClass]
    public class ExperienciaLaboralServiceTests
    {
        private Mock<IExperienciaLaboralRepository> _repositorioMock;
        private IExperienciaLaboralService _servicio;

        [TestInitialize]
        public void Setup()
        {
            _repositorioMock = new Mock<IExperienciaLaboralRepository>();
            _servicio = new ExperienciaLaboralService(null, _repositorioMock.Object);
        }

        [TestMethod]
        public async Task GetExpByCandidatoIdAsync_RetornaExpLaboral()
        {
            var candidatoId = 1;
            var experiencias = new List<ExperienciaLaboral> { new ExperienciaLaboral { CandidatoId = candidatoId } };
            _repositorioMock.Setup(r => r.GetExperienciaByCandidatoIdAsync(candidatoId)).ReturnsAsync(experiencias);

            var resultado = await _servicio.GetExpByCandidatoIdAsync(candidatoId);

            CollectionAssert.AreEqual(experiencias, (List<ExperienciaLaboral>)resultado);
        }
    }
}
