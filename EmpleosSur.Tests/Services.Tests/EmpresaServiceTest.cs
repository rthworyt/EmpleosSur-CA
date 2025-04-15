using EmpleosSur.Application.Interfaces.IRepositories;
using EmpleosSur.Application.Interfaces.IServices;
using EmpleosSur.Application.Services;
using EmpleosSur.Domain.Entities;
using Moq;

namespace EmpleosSur.Tests.Services
{
    [TestClass]
    public class EmpresaServiceTests
    {
        private Mock<IEmpresaRepository> _mockRepo;
        private IEmpresaService _service;

        [TestInitialize]
        public void Setup()
        {
            _mockRepo = new Mock<IEmpresaRepository>();
            _service = new EmpresaService(_mockRepo.Object);
        }

        [TestMethod]
        public async Task GetEmpresaByEmail_RetornaEmpresaByEmail()
        {
            var email = "shaddaybarahona@gmail.com";
            var empresa = new Empresa { EmailCorporativo = email };
            _mockRepo.Setup(r => r.GetEmpresaByEmail(email)).ReturnsAsync(empresa);

            var result = await _service.GetEmpresaByEmail(email);

            Assert.AreEqual(empresa, result);
        }

        [TestMethod]
        public async Task GetEmpresaByRNC_RetornaEmpresaByRNC()
        {
            var rnc = "117013123";
            var empresa = new Empresa { RNC = rnc };
            _mockRepo.Setup(r => r.GetEmpresaByRNC(rnc)).ReturnsAsync(empresa);

            var result = await _service.GetEmpresaByRNC(rnc);

            Assert.AreEqual(empresa, result);
        }

        [TestMethod]
        public async Task GetEmpresaByCiudad_RetornaEmpresasByCiudad()
        {
            var ciudad = "Santo Domingo";
            var empresas = new List<Empresa> { new Empresa { Direccion = ciudad } };
            _mockRepo.Setup(r => r.GetEmpresaByLocalidad(ciudad)).ReturnsAsync(empresas);

            var result = await _service.GetEmpresaByLocalidad(ciudad);

            CollectionAssert.AreEqual(empresas, (List<Empresa>)result);
        }

        [TestMethod]
        public async Task GetEmpresaByNombre_RetornaEmpresasByNombre()
        {
            var nombre = "Shadday Alta Tecnologia";
            var empresas = new List<Empresa> { new Empresa { Nombre = nombre } };
            _mockRepo.Setup(r => r.GetEmpresaByNombre(nombre)).ReturnsAsync(empresas);

            var result = await _service.GetEmpresaByNombre(nombre);

            CollectionAssert.AreEqual(empresas, (List<Empresa>)result);
        }
    }
}
