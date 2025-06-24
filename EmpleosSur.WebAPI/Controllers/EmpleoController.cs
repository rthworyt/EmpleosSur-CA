using AutoMapper;
using EmpleosSur.Application.Interfaces.IServices;
using EmpleosSur.Domain.Entities;
using EmpleosSur.WebAPI.DTOs;
using EmpleosSur.WebAPI.Generators;
using Microsoft.AspNetCore.Mvc;

namespace EmpleosSur.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleoController : ControllerBase
    {
        private readonly IEmpleoService _empleoService;
        private readonly IMapper _mapper;
        private readonly FakeDataGenerator _fakeDataGenerator;

        public EmpleoController(
            IEmpleoService empleoService,
            IMapper mapper,
            FakeDataGenerator fakeDataGenerator
        )
        {
            _empleoService = empleoService;
            _mapper = mapper;
            _fakeDataGenerator = fakeDataGenerator;
        }

        //// GET - Generar empleos aleatorios
        //[HttpGet("GeneraFakeEmpleos")]
        //public async Task<IActionResult> GenerateFakeEmpleos(int count = 10)
        //{
        //    await _fakeDataGenerator.GenerateFakeEmpleos(count);
        //    return Ok(
        //        new { message = $"{count} empleos generados correctamente en la base de datos." }
        //    );
        //}

        // POST
        [HttpPost("CreateEmpleo")]
        public async Task<ActionResult<EmpleoReadOnlyDTO>> CreateEmpleo(EmpleoDTO empleoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Datos inválidos", errors = ModelState });
            }

            var empleo = _mapper.Map<Empleo>(empleoDTO);
            await _empleoService.CreateAsync(empleo);

            var newEmpleoDTO = _mapper.Map<EmpleoReadOnlyDTO>(empleo);
            return CreatedAtAction(
                nameof(GetEmpleoById),
                new { id = empleo.Id },
                new { message = "Empleo creado correctamente.", data = newEmpleoDTO }
            );
        }

        // GET
        [HttpGet("GetEmpleoById")]
        public async Task<ActionResult<EmpleoReadOnlyDTO>> GetEmpleoById(int id)
        {
            var empleo = await _empleoService.GetByIdAsync(id);
            if (empleo == null)
            {
                return NotFound(new { message = "Empleo no encontrado." });
            }

            var empleoDTO = _mapper.Map<EmpleoReadOnlyDTO>(empleo);
            return Ok(new { message = "Empleo encontrado correctamente.", data = empleoDTO });
        }

        //// GET
        //[HttpGet("GetAllEmpleos")]
        //public async Task<ActionResult<IEnumerable<EmpleoReadOnlyDTO>>> GetAllEmpleos()
        //{
        //    var empleos = await _empleoService.GetAllEmpleosAsync();
        //    var empleosDTO = _mapper.Map<IEnumerable<EmpleoReadOnlyDTO>>(empleos);
        //    return Ok(
        //        new { message = "Lista de empleos obtenida correctamente.", data = empleosDTO }
        //    );
        //}

        // PUT
        [HttpPut("UpdateEmpleo")]
        public async Task<ActionResult<EmpleoReadOnlyDTO>> UpdateEmpleo(int id, EmpleoDTO empleoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Datos inválidos", errors = ModelState });
            }

            var empleo = await _empleoService.GetByIdAsync(id);
            if (empleo == null)
            {
                return NotFound(new { message = "Empleo no encontrado." });
            }

            _mapper.Map(empleoDTO, empleo);
            await _empleoService.UpdateAsync(empleo);

            var updatedEmpleoDTO = _mapper.Map<EmpleoReadOnlyDTO>(empleo);
            return Ok(
                new { message = "Empleo actualizado correctamente.", data = updatedEmpleoDTO }
            );
        }

        // DELETE
        [HttpDelete("DeleteEmpleoById")]
        public async Task<ActionResult> DeleteEmpleo(int id)
        {
            var empleo = await _empleoService.GetByIdAsync(id);
            if (empleo == null)
            {
                return NotFound(new { message = "Empleo no encontrado." });
            }

            var result = await _empleoService.DeleteAsync(id);
            if (!result)
            {
                return BadRequest(new { message = "No se pudo eliminar el empleo." });
            }

            return Ok(new { message = "Empleo eliminado correctamente." });
        }

        // GET
        [HttpGet("GetEmpleosByTitulo")]
        public async Task<ActionResult<IEnumerable<EmpleoReadOnlyDTO>>> GetEmpleosByTitulo(
            string titulo
        )
        {
            var empleos = await _empleoService.SearchEmpleosByTitulo(titulo);
            if (empleos == null || !empleos.Any())
            {
                return NotFound(new { message = "No se encontraron empleos con ese título." });
            }

            var empleosDTO = _mapper.Map<IEnumerable<EmpleoReadOnlyDTO>>(empleos);
            return Ok(new { message = "Empleos encontrados correctamente.", data = empleosDTO });
        }

        // GET
        [HttpGet("GetEmpleosRecientes")]
        public async Task<ActionResult<IEnumerable<EmpleoReadOnlyDTO>>> GetEmpleosRecientes(
            int cantidad
        )
        {
            var empleos = await _empleoService.GetRecentEmpleosAsync(cantidad);
            var empleosDTO = _mapper.Map<IEnumerable<EmpleoReadOnlyDTO>>(empleos);
            return Ok(
                new { message = "Empleos recientes encontrados correctamente.", data = empleosDTO }
            );
        }

        // GET
        [HttpGet("GetEmpleosByEmpresaId")]
        public async Task<ActionResult<IEnumerable<EmpleoReadOnlyDTO>>> GetEmpleosByEmpresaId(
            int empresaId
        )
        {
            var empleos = await _empleoService.GetEmpleosByEmpresaId(empresaId);
            if (empleos == null || !empleos.Any())
            {
                return NotFound(new { message = "No se encontraron empleos para esa empresa." });
            }

            var empleosDTO = _mapper.Map<IEnumerable<EmpleoReadOnlyDTO>>(empleos);
            return Ok(new { message = "Empleos encontrados correctamente.", data = empleosDTO });
        }
    }
}
