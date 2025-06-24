using AutoMapper;
using EmpleosSur.Application.Interfaces.IServices;
using EmpleosSur.Domain.Entities;
using EmpleosSur.Domain.Helpers;
using EmpleosSur.WebAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EmpleosSur.Tests.Controllers
{
    [TestClass]
    public class CandidatosControllerTests
    {
        private Mock<ICandidatoService> _candidatoServiceMock;
        private Mock<IMapper> _mapperMock;
        private CandidatosController _controller;

        [TestInitialize]
        public void Setup()
        {
            _candidatoServiceMock = new Mock<ICandidatoService>();
            _mapperMock = new Mock<IMapper>();

            _controller = new CandidatosController(
                _candidatoServiceMock.Object,
                _mapperMock.Object,
                null
            );
        }

        [TestMethod]
        public async Task GetCandidatoById_ReturnsOk_WhenCandidatoExists()
        {
            var candidato = new Candidato { Id = 1 };
            var dto = new CandidatoReadOnlyDTO { Id = 1 };

            _candidatoServiceMock.Setup(s => s.GetAllDataCandidatoById(1)).ReturnsAsync(candidato);
            _mapperMock.Setup(m => m.Map<CandidatoReadOnlyDTO>(candidato)).Returns(dto);

            var result = await _controller.GetCandidatoById(1);

            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetCandidatoById_ReturnsNotFound_WhenCandidatoIsNull()
        {
            _candidatoServiceMock
                .Setup(s => s.GetAllDataCandidatoById(1))
                .ReturnsAsync((Candidato)null);

            var result = await _controller.GetCandidatoById(1);

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundObjectResult));
        }

        [TestMethod]
        public async Task GetCandidatoByEmail_ReturnsBadRequest_WhenEmailIsEmpty()
        {
            var result = await _controller.GetCandidatoByEmail("");

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task GetCandidatosByCiudad_ReturnsOk_WhenCandidatosFound()
        {
            var candidatos = new List<Candidato> { new Candidato { Id = 1 } };
            var candidatosDTO = new List<CandidatoReadOnlyDTO>
            {
                new CandidatoReadOnlyDTO { Id = 1 }
            };

            _candidatoServiceMock
                .Setup(s => s.GetCandidatoByCiudad("Santo Domingo"))
                .ReturnsAsync(candidatos);

            _mapperMock
                .Setup(m => m.Map<IEnumerable<CandidatoReadOnlyDTO>>(candidatos))
                .Returns(candidatosDTO);

            var result = await _controller.GetCandidatosByCiudad("Santo Domingo");

            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task CreateCandidato_ReturnsCreated_WhenSuccess()
        {
            var dto = new CandidatoDTO
            {
                Nombre = "Juan",
                Apellido = "Perez",
                Email = "juan@mail.com",
                Genero = "M",
                FechaNacimiento = DateTime.Today.AddYears(-20),
                Telefono = "1234567890",
                Ciudad = "SD",
                Direccion = "Calle 1"
            };

            var candidato = new Candidato { Id = 1 };
            var resultSuccess = OperationResult.SuccessResult();

            _mapperMock.Setup(m => m.Map<Candidato>(dto)).Returns(candidato);
            _mapperMock.Setup(m => m.Map<CandidatoDTO>(candidato)).Returns(dto);

            _candidatoServiceMock
                .Setup(s => s.CreateCandidato(candidato))
                .ReturnsAsync(resultSuccess);

            var result = await _controller.CreateCandidato(dto);

            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult));
        }

        [TestMethod]
        public async Task UpdateCandidato_ReturnsOk_WhenSuccess()
        {
            var dto = new CandidatoDTO
            {
                Nombre = "Maria",
                Apellido = "Lopez",
                Email = "maria@mail.com",
                Genero = "F",
                FechaNacimiento = DateTime.Today.AddYears(-25),
                Telefono = "0987654321",
                Ciudad = "SD",
                Direccion = "Calle 2"
            };

            var candidato = new Candidato { Id = 1 };

            _mapperMock.Setup(m => m.Map<Candidato>(dto)).Returns(candidato);
            _mapperMock.Setup(m => m.Map<CandidatoDTO>(candidato)).Returns(dto);

            _candidatoServiceMock
                .Setup(s => s.UpdateCandidato(candidato))
                .ReturnsAsync(OperationResult.SuccessResult());

            var result = await _controller.UpdateCandidato(1, dto);

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task DeleteCandidato_ReturnsOk_WhenSuccess()
        {
            _candidatoServiceMock
                .Setup(s => s.DeleteCandidato(1))
                .ReturnsAsync(OperationResult.SuccessResult());

            var result = await _controller.DeleteCandidato(1);

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }
    }
}
