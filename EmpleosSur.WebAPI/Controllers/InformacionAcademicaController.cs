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
    public class InformacionAcademicaController : ControllerBase
    {
        private readonly IInformacionAcademicaService _informacionAcademicaService;
        private readonly IMapper _mapper;
        private readonly FakeDataGenerator _fakeDataGenerator;

        public InformacionAcademicaController(
            IInformacionAcademicaService informacionAcademicaService,
            IMapper mapper,
            FakeDataGenerator fakeDataGenerator
        )
        {
            _informacionAcademicaService = informacionAcademicaService;
            _mapper = mapper;
            _fakeDataGenerator = fakeDataGenerator;
        }

        // GET - Generar informaciones académicas aleatorias
        [HttpGet("GeneraFakeInformacionesAcademicas")]
        public async Task<IActionResult> GenerateFakeInformacionesAcademicas(int count = 10)
        {
            await _fakeDataGenerator.GenerateFakeInformacionesAcademicas(count);
            return Ok(
                new
                {
                    message = $"{count} informaciones académicas generadas correctamente en la base de datos."
                }
            );
        }

        // GET by Id
        [HttpGet("GetInfoAcademicaByCandidatoId")]
        public async Task<
            ActionResult<IEnumerable<InformacionAcademicaReadOnlyDTO>>
        > GetInformacionAcademicaByCandidatoId(int candidatoId)
        {
            var informacionAcademica = await _informacionAcademicaService.GetByCandidatoIdAsync(
                candidatoId
            );

            if (informacionAcademica == null || !informacionAcademica.Any())
            {
                return NotFound(
                    new { message = "No se encontró información académica para el candidato." }
                );
            }

            var informacionAcademicaDTO = _mapper.Map<IEnumerable<InformacionAcademicaReadOnlyDTO>>(
                informacionAcademica
            );
            return Ok(
                new
                {
                    message = "Información académica obtenida correctamente.",
                    data = informacionAcademicaDTO
                }
            );
        }

        // POST
        [HttpPost("CreateInfoAcademica")]
        public async Task<ActionResult> CreateInformacionAcademica(
            [FromBody] InformacionAcademicaDTO informacionAcademicaDTO
        )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(
                    new
                    {
                        message = "Los datos proporcionados no son válidos.",
                        errors = ModelState
                    }
                );
            }

            var informacionAcademica = _mapper.Map<InformacionAcademica>(informacionAcademicaDTO);

            await _informacionAcademicaService.CreateAsync(informacionAcademica);
            return CreatedAtAction(
                nameof(GetInformacionAcademicaByCandidatoId),
                new { candidatoId = informacionAcademica.CandidatoId },
                new { message = "Información académica creada correctamente." }
            );
        }

        // PUT
        [HttpPut("UpdateInfoAcademica")]
        public async Task<ActionResult> UpdateInformacionAcademica(
            int id,
            [FromBody] InformacionAcademicaDTO informacionAcademicaDTO
        )
        {
            if (id != informacionAcademicaDTO.CandidatoId)
            {
                return BadRequest(new { message = "El ID del candidato no coincide." });
            }

            var informacionAcademica = _mapper.Map<InformacionAcademica>(informacionAcademicaDTO);
            informacionAcademica.Id = id;

            await _informacionAcademicaService.UpdateAsync(informacionAcademica);
            return NoContent();
        }

        // DELETE
        [HttpDelete("DeleteInfoAcademica")]
        public async Task<ActionResult> DeleteInformacionAcademica(int id)
        {
            var success = await _informacionAcademicaService.DeleteAsync(id);

            if (!success)
            {
                return NotFound(
                    new { message = "No se encontró la información académica para eliminar." }
                );
            }

            return NoContent();
        }
    }
}
