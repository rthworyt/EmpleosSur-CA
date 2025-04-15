using EmpleosSur.Application.Interfaces.IServices;
using EmpleosSur.Application.Services;
using EmpleosSur.Domain.Entities;
using Moq;

namespace EmpleosSur.Tests.Services
{
    [TestClass]
    public class InformacionAcademicaServiceTests
    {
        private Mock<IInformacionAcademicaRepository> _repositorioMock;
        private IInformacionAcademicaService _servicio;

        [TestInitialize]
        public void Setup()
        {
            _repositorioMock = new Mock<IInformacionAcademicaRepository>();
            _servicio = new InformacionAcademicaService(_repositorioMock.Object);
        }

        [TestMethod]
        public async Task GetByCandidatoIdAsync_RetornaInformacionAcademica()
        {
            var candidatoId = 1;
            var informacionAcademica = new List<InformacionAcademica>
            {
                new InformacionAcademica { CandidatoId = candidatoId }
            };
            _repositorioMock
                .Setup(r => r.GetInformacionAcademicaByCandidatoIdAsync(candidatoId))
                .ReturnsAsync(informacionAcademica);

            var resultado = await _servicio.GetByCandidatoIdAsync(candidatoId);

            CollectionAssert.AreEqual(informacionAcademica, (List<InformacionAcademica>)resultado);
        }
    }
}
