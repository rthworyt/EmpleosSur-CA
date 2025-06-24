using EmpleosSur.Domain.Entities;
using EmpleosSur.Infraestructure.Data;
using EmpleosSur.Infraestructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EmpleosSur.Tests.Repositories
{
    [TestClass]
    public class EmpresaRepositoryTests
    {
        private EmpleosSurDBContext _context;
        private EmpresaRepository _repository;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<EmpleosSurDBContext>()
                .UseInMemoryDatabase(databaseName: "EmpleosSurDB" + Guid.NewGuid())
                .EnableSensitiveDataLogging()
                .Options;

            _context = new EmpleosSurDBContext(options);
            _repository = new EmpresaRepository(_context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [TestMethod]
        public async Task GetEmpresaByLocalidad_RetornaEmpresasFiltradas()
        {
            var empresa1 = new Empresa
            {
                Nombre = "TechRD",
                Direccion = "Av. Siempre Viva",
                Ciudad = "Santo Domingo",
                Telefono = "8091234567",
                CedulaRepresentante = "00123456789",
                EmailCorporativo = "contacto@techrd.com",
                NombreRepresentante = "Carlos Mendez",
                RNC = "123456789"
            };

            var empresa2 = new Empresa
            {
                Nombre = "Innovatech",
                Direccion = "Calle Principal 456",
                Ciudad = "Santiago",
                Telefono = "8096543210",
                CedulaRepresentante = "00234567890",
                EmailCorporativo = "info@innovatech.com",
                NombreRepresentante = "Ana Lopez",
                RNC = "987654321"
            };

            _context.Empresas.AddRange(empresa1, empresa2);
            await _context.SaveChangesAsync();

            var result = await _repository.GetEmpresaByLocalidad("Santo Domingo");

            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("TechRD", result.First().Nombre);
        }

        [TestMethod]
        public async Task GetEmpresaByRNC_RetornaEmpresaCorrecta()
        {
            var empresa = new Empresa
            {
                Nombre = "TechRD",
                Direccion = "Av. Siempre Viva",
                Ciudad = "Santo Domingo",
                Telefono = "8091234567",
                CedulaRepresentante = "00123456789",
                EmailCorporativo = "contacto@techrd.com",
                NombreRepresentante = "Carlos Mendez",
                RNC = "123456789"
            };

            _context.Empresas.Add(empresa);
            await _context.SaveChangesAsync();

            var result = await _repository.GetEmpresaByRNC("123456789");

            Assert.IsNotNull(result);
            Assert.AreEqual("TechRD", result.Nombre);
        }

        [TestMethod]
        public async Task GetEmpresaByNombre_RetornaEmpresasFiltradas()
        {
            var empresa1 = new Empresa
            {
                Nombre = "TechRD",
                Direccion = "Av. Siempre Viva",
                Ciudad = "Santo Domingo",
                Telefono = "8091234567",
                CedulaRepresentante = "00123456789",
                EmailCorporativo = "contacto@techrd.com",
                NombreRepresentante = "Carlos Mendez",
                RNC = "123456789"
            };

            var empresa2 = new Empresa
            {
                Nombre = "Innovatech",
                Direccion = "Calle Principal 456",
                Ciudad = "Santiago",
                Telefono = "8096543210",
                CedulaRepresentante = "00234567890",
                EmailCorporativo = "info@innovatech.com",
                NombreRepresentante = "Ana Lopez",
                RNC = "987654321"
            };

            _context.Empresas.AddRange(empresa1, empresa2);
            await _context.SaveChangesAsync();

            var result = await _repository.GetEmpresaByNombre("Tech");

            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("TechRD", result.First().Nombre);
        }
    }
}