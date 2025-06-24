using Moq;
using EmpleosSur.Application.Services;
using EmpleosSur.Core.Interfaces;
using EmpleosSur.Domain.Entities;
using EmpleosSur.Application.Interfaces;

namespace EmpleosSur.Tests.Services
{
    [TestClass]
    public class PostulacionServiceTests
    {
        private Mock<IPostulacionRepository> _repositorioPostulacionMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private PostulacionService _servicio;

        [TestInitialize]
        public void Setup()
        {
            _repositorioPostulacionMock = new Mock<IPostulacionRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _servicio = new PostulacionService(_unitOfWorkMock.Object, _repositorioPostulacionMock.Object);
        }

        [TestMethod]
        public async Task CreatePostulacion_CreaPostulacionConExito()
        {
            var postulacion = new Postulacion { EmpleoId = 1, CandidatoId = 1 };
            var empleo = new Empleo { Id = 1 };
            var candidato = new Candidato { Id = 1 };

            _unitOfWorkMock.Setup(u => u.Empleos.GetByIdAsync(postulacion.EmpleoId)).ReturnsAsync(empleo);
            _unitOfWorkMock.Setup(u => u.Candidatos.GetByIdAsync(postulacion.CandidatoId)).ReturnsAsync(candidato);
            _repositorioPostulacionMock.Setup(r => r.CreateAsync(postulacion)).Returns(Task.CompletedTask);
            _unitOfWorkMock.Setup(u => u.CompletarAsync()).ReturnsAsync(1);

            var resultado = await _servicio.CreatePostulacion(postulacion);

            Assert.IsTrue(resultado.Success);
        }

        [TestMethod]
        public async Task CreatePostulacion_DaErrorSiEmpleoOCandidatoNoGood()
        {
            var postulacion = new Postulacion { EmpleoId = 1, CandidatoId = 1 };

            _unitOfWorkMock.Setup(u => u.Empleos.GetByIdAsync(postulacion.EmpleoId)).ReturnsAsync((Empleo)null);
            _unitOfWorkMock.Setup(u => u.Candidatos.GetByIdAsync(postulacion.CandidatoId)).ReturnsAsync((Candidato)null);

            var resultado = await _servicio.CreatePostulacion(postulacion);

            Assert.IsFalse(resultado.Success);
            Assert.AreEqual("Empleo o candidato no válido.", resultado.ErrorMessage);
        }

        [TestMethod]
        public async Task UpdateEstadoPostulacion_ActualizaEstadoCorrectamente()
        {
            var postulacion = new Postulacion { Id = 1, Estado = EstadoPostulacion.Pendiente };
            var estadoPostulacion = "Aceptado";

            _repositorioPostulacionMock.Setup(r => r.GetByIdAsync(postulacion.Id)).ReturnsAsync(postulacion);
            _repositorioPostulacionMock.Setup(r => r.UpdateAsync(postulacion)).Returns(Task.CompletedTask);
            _unitOfWorkMock.Setup(u => u.CompletarAsync()).ReturnsAsync(1);

            var resultado = await _servicio.UpdateEstadoPostulacion(postulacion.Id, estadoPostulacion);

            Assert.IsTrue(resultado.Success);
            Assert.AreEqual(EstadoPostulacion.Aceptado, postulacion.Estado);
        }

        [TestMethod]
        public async Task UpdateEstadoPostulacion_DRetornaErrorSiPostulacionNoExiste()
        {
            var idPostulacion = 999;
            var estadoPostulacion = "Aprobado";

            _repositorioPostulacionMock.Setup(r => r.GetByIdAsync(idPostulacion)).ReturnsAsync((Postulacion)null);

            var resultado = await _servicio.UpdateEstadoPostulacion(idPostulacion, estadoPostulacion);

            Assert.IsFalse(resultado.Success);
            Assert.AreEqual("Postulación no encontrada.", resultado.ErrorMessage);
        }

        [TestMethod]
        public async Task DeletePostulacion_EliminaPostulacionCorrectamente()
        {
            var idPostulacion = 1;
            var postulacion = new Postulacion { Id = idPostulacion };

            _repositorioPostulacionMock.Setup(r => r.GetByIdAsync(idPostulacion)).ReturnsAsync(postulacion);
            _repositorioPostulacionMock.Setup(r => r.DeleteAsync(idPostulacion)).ReturnsAsync(true);
            _unitOfWorkMock.Setup(u => u.CompletarAsync()).ReturnsAsync(1);

            var resultado = await _servicio.DeletePostulacion(idPostulacion);

            Assert.IsTrue(resultado.Success);
        }

        [TestMethod]
        public async Task DeletePostulacion_RetornaErrorSiPostulacionNoExiste()
        {
            var idPostulacion = 999;

            _repositorioPostulacionMock.Setup(r => r.GetByIdAsync(idPostulacion)).ReturnsAsync((Postulacion)null);

            var resultado = await _servicio.DeletePostulacion(idPostulacion);

            Assert.IsFalse(resultado.Success);
            Assert.AreEqual("Postulación no encontrada.", resultado.ErrorMessage);
        }

        [TestMethod]
        public async Task GetPostulacionesByCandidatoId_RetornaPostulacionesByCandidato()
        {
            var candidatoId = 1;
            var postulaciones = new List<Postulacion> { new Postulacion { CandidatoId = candidatoId } };

            _repositorioPostulacionMock.Setup(r => r.GetPostulacionByCandidatoAsync(candidatoId)).ReturnsAsync(postulaciones);

            var resultado = await _servicio.GetPostulacionesByCandidatoId(candidatoId);

            CollectionAssert.AreEqual(postulaciones, (List<Postulacion>)resultado);
        }

        [TestMethod]
        public async Task GetPostulacionesByEmpleoId_RetornaPostulacionesByEmpleo()
        {
            var empleoId = 1;
            var postulaciones = new List<Postulacion> { new Postulacion { EmpleoId = empleoId } };

            _repositorioPostulacionMock.Setup(r => r.GetPostulacionByEmpleoAsync(empleoId)).ReturnsAsync(postulaciones);

            var resultado = await _servicio.GetPostulacionesByEmpleoId(empleoId);

            CollectionAssert.AreEqual(postulaciones, (List<Postulacion>)resultado);
        }
    }
}
