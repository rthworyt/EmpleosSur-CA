using EmpleosSur.Application.Services;
using EmpleosSur.Domain.Entities;
using Moq;
using EmpleosSur.Application.Interfaces;

namespace EmpleosSur.Tests
{
    [TestClass]
    public class PostulacionServiceTests
    {
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private PostulacionService _postulacionService;

        [TestInitialize]
        public void SetUp()
        {
            // Inicializar los mocks
            _mockUnitOfWork = new Mock<IUnitOfWork>();

            // Crear la instancia del servicio
            _postulacionService = new PostulacionService(_mockUnitOfWork.Object);
        }

        [TestMethod]
        public async Task CreaPostulacion_SiDatosSonValidos()
        {
            // Arrange
            var postulacion = new Postulacion
            {
                EmpleoId = 1,
                CandidatoId = 1,
                Estado = EstadoPostulacion.Pendiente // Usar el enum aquí
            };

            var empleo = new Empleo { Id = 1 };
            var candidato = new Candidato { Id = 1 };

            _mockUnitOfWork.Setup(uow => uow.Empleos.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(empleo);
            _mockUnitOfWork.Setup(uow => uow.Candidatos.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(candidato);
            _mockUnitOfWork.Setup(uow => uow.Postulaciones.CreateAsync(It.IsAny<Postulacion>())).Returns(Task.CompletedTask);

            // Act
            var result = await _postulacionService.CreatePostulacionAsync(postulacion);

            // Assert
            _mockUnitOfWork.Verify(uow => uow.Postulaciones.CreateAsync(postulacion), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.CompletarAsync(), Times.Once);
            Assert.AreEqual("La operación fue exitosa.", result.Success ? "La operación fue exitosa." : result.ErrorMessage);
        }

        [TestMethod]
        public async Task ActualizaPostulacion_SiExiste()
        {
            // Arrange
            var postulacion = new Postulacion { Id = 1, Estado = EstadoPostulacion.Pendiente }; // Usar el enum aquí
            var nuevoEstado = EstadoPostulacion.Aceptado; // Usar el enum aquí
            _mockUnitOfWork.Setup(uow => uow.Postulaciones.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(postulacion);
            _mockUnitOfWork.Setup(uow => uow.Postulaciones.UpdateAsync(It.IsAny<Postulacion>())).Returns(Task.CompletedTask);

            // Act
            var result = await _postulacionService.UpdateEstadoPostulacionAsync(postulacion.Id, nuevoEstado);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(nuevoEstado, postulacion.Estado); // Comparar con el enum
            _mockUnitOfWork.Verify(uow => uow.Postulaciones.UpdateAsync(postulacion), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.CompletarAsync(), Times.Once);
        }

        [TestMethod]
        public async Task EliminaPostulacion_CuandoPostulacionExiste()
        {
            // Arrange
            var postulacionId = 1;
            _mockUnitOfWork.Setup(uow => uow.Postulaciones.DeleteAsync(It.IsAny<int>())).ReturnsAsync(true);

            // Act
            var result = await _postulacionService.DeletePostulacionAsync(postulacionId);

            // Assert
            Assert.IsTrue(result);
            _mockUnitOfWork.Verify(uow => uow.Postulaciones.DeleteAsync(postulacionId), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.CompletarAsync(), Times.Once);
        }
    }
}
