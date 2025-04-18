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
    public class ExperienciaLaboralController : ControllerBase
    {
        private readonly IExperienciaLaboralService _experienciaLaboralService;
        private readonly IMapper _mapper;
        private readonly FakeDataGenerator _fakeDataGenerator;

        public ExperienciaLaboralController(
            IExperienciaLaboralService experienciaLaboralService,
            IMapper mapper,
            FakeDataGenerator fakeDataGenerator
        )
        {
            _experienciaLaboralService = experienciaLaboralService;
            _mapper = mapper;
            _fakeDataGenerator = fakeDataGenerator;
        }

        // GET - Generar experiencias laborales aleatorias
        [HttpGet("GeneraFakeExperienciasLaborales")]
        public async Task<IActionResult> GenerateFakeExperienciasLaborales(int count = 10)
        {
            await _fakeDataGenerator.GenerateFakeExperienciasLaborales(count);
            return Ok(
                new
                {
                    message = $"{count} experiencias laborales generadas correctamente en la base de datos."
                }
            );
        }

        // POST
        [HttpPost("CreateExperienciaLaboral")]
        public async Task<ActionResult<ExperienciaLaboralReadOnlyDTO>> CreateExperienciaLaboral(
            ExperienciaLaboralDTO experienciaLaboralDTO
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

            var experienciaLaboral = _mapper.Map<ExperienciaLaboral>(experienciaLaboralDTO);
            await _experienciaLaboralService.CreateAsync(experienciaLaboral);

            var experienciaLaboralReadOnlyDTO = _mapper.Map<ExperienciaLaboralReadOnlyDTO>(
                experienciaLaboral
            );
            return CreatedAtAction(
                nameof(GetExperienciaLaboralById),
                new { id = experienciaLaboral.Id },
                new
                {
                    message = "Experiencia laboral creada correctamente.",
                    data = experienciaLaboralReadOnlyDTO
                }
            );
        }

        // GET
        [HttpGet("GetExperienciaLaboralById")]
        public async Task<ActionResult<ExperienciaLaboralReadOnlyDTO>> GetExperienciaLaboralById(
            int id
        )
        {
            var experienciaLaboral = await _experienciaLaboralService.GetByIdAsync(id);
            if (experienciaLaboral == null)
            {
                return NotFound(new { message = "Experiencia laboral no encontrada." });
            }

            var experienciaLaboralDTO = _mapper.Map<ExperienciaLaboralReadOnlyDTO>(
                experienciaLaboral
            );
            return Ok(
                new
                {
                    message = "Experiencia laboral encontrada correctamente.",
                    data = experienciaLaboralDTO
                }
            );
        }

        // GET (por CandidatoId)
        [HttpGet("GetExperienciaByCandidatoId")]
        public async Task<
            ActionResult<IEnumerable<ExperienciaLaboralReadOnlyDTO>>
        > GetExperienciaLaboralByCandidatoId(int candidatoId)
        {
            var experienciasLaborales = await _experienciaLaboralService.GetExpByCandidatoIdAsync(
                candidatoId
            );
            if (experienciasLaborales == null || !experienciasLaborales.Any())
            {
                return NotFound(
                    new
                    {
                        message = "No se encontraron experiencias laborales para este candidato."
                    }
                );
            }

            var experienciasLaboralesDTO = _mapper.Map<IEnumerable<ExperienciaLaboralReadOnlyDTO>>(
                experienciasLaborales
            );
            return Ok(
                new
                {
                    message = "Experiencias laborales encontradas correctamente.",
                    data = experienciasLaboralesDTO
                }
            );
        }

        // PUT
        [HttpPut("UpdateExperienciaLaboral")]
        public async Task<ActionResult<ExperienciaLaboralReadOnlyDTO>> UpdateExperienciaLaboral(
            int id,
            ExperienciaLaboralDTO experienciaLaboralDTO
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

            var experienciaLaboral = await _experienciaLaboralService.GetByIdAsync(id);
            if (experienciaLaboral == null)
            {
                return NotFound(new { message = "Experiencia laboral no encontrada." });
            }

            _mapper.Map(experienciaLaboralDTO, experienciaLaboral); // Mapeo de datos para actualizar
            await _experienciaLaboralService.UpdateAsync(experienciaLaboral);

            var experienciaLaboralReadOnlyDTO = _mapper.Map<ExperienciaLaboralReadOnlyDTO>(
                experienciaLaboral
            );
            return Ok(
                new
                {
                    message = "Experiencia laboral actualizada correctamente.",
                    data = experienciaLaboralReadOnlyDTO
                }
            );
        }

        // DELETE
        [HttpDelete("DeleteExperienciaLaboral")]
        public async Task<ActionResult> DeleteExperienciaLaboral(int id)
        {
            var experienciaLaboral = await _experienciaLaboralService.GetByIdAsync(id);
            if (experienciaLaboral == null)
            {
                return NotFound(new { message = "Experiencia laboral no encontrada." });
            }

            var result = await _experienciaLaboralService.DeleteAsync(id);
            if (!result)
            {
                return BadRequest(new { message = "No se pudo eliminar la experiencia laboral." });
            }

            return Ok(new { message = "Experiencia laboral eliminada correctamente." });
        }
    }
}
