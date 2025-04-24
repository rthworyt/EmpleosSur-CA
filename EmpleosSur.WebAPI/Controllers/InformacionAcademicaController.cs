using AutoMapper;
using EmpleosSur.Application.Interfaces.IServices;
using EmpleosSur.Domain.Entities;
using EmpleosSur.WebAPI.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace EmpleosSur.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InformacionAcademicaController : ControllerBase
    {
        private readonly IInformacionAcademicaService _informacionAcademicaService;
        private readonly IMapper _mapper;

        public InformacionAcademicaController(
            IInformacionAcademicaService informacionAcademicaService,
            IMapper mapper
        )
        {
            _informacionAcademicaService = informacionAcademicaService;
            _mapper = mapper;
        }

        // GET - Obtener información académica por ID
        [HttpGet("{id}")]
        public async Task<
            ActionResult<InformacionAcademicaReadOnlyDTO>
        > GetInformacionAcademicaById(int id)
        {
            var informacionAcademica = await _informacionAcademicaService.GetByIdAsync(id);
            if (informacionAcademica == null)
            {
                return NotFound(
                    new { message = $"Información académica con id {id} no encontrada." }
                );
            }

            var informacionAcademicaDTO = _mapper.Map<InformacionAcademicaReadOnlyDTO>(
                informacionAcademica
            );
            return Ok(
                new
                {
                    message = "Información académica encontrada correctamente.",
                    data = informacionAcademicaDTO
                }
            );
        }

        // GET - Obtener información académica por CandidatoId
        [HttpGet("candidato/{candidatoId}")]
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
                    new
                    {
                        message = $"No se encontró información académica para el candidato con id {candidatoId}."
                    }
                );
            }

            var informacionAcademicaDTO = _mapper.Map<IEnumerable<InformacionAcademicaReadOnlyDTO>>(
                informacionAcademica
            );
            return Ok(
                new
                {
                    message = "Información académica encontrada correctamente.",
                    data = informacionAcademicaDTO
                }
            );
        }

        // POST - Crear nueva información académica
        [HttpPost]
        public async Task<ActionResult<InformacionAcademicaReadOnlyDTO>> CreateInformacionAcademica(
            InformacionAcademicaDTO informacionAcademicaDTO
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

            var informacionAcademicaReadOnlyDTO = _mapper.Map<InformacionAcademicaReadOnlyDTO>(
                informacionAcademica
            );
            return CreatedAtAction(
                nameof(GetInformacionAcademicaById),
                new { id = informacionAcademica.Id },
                new
                {
                    message = "Información académica creada correctamente.",
                    data = informacionAcademicaReadOnlyDTO
                }
            );
        }

        // PUT - Actualizar información académica
        [HttpPut("{id}")]
        public async Task<ActionResult<InformacionAcademicaReadOnlyDTO>> UpdateInformacionAcademica(
            int id,
            InformacionAcademicaDTO informacionAcademicaDTO
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

            var informacionAcademica = await _informacionAcademicaService.GetByIdAsync(id);
            if (informacionAcademica == null)
            {
                return NotFound(
                    new { message = $"Información académica con id {id} no encontrada." }
                );
            }

            _mapper.Map(informacionAcademicaDTO, informacionAcademica); // Mapeo de datos para actualizar
            await _informacionAcademicaService.UpdateAsync(informacionAcademica);

            var informacionAcademicaReadOnlyDTO = _mapper.Map<InformacionAcademicaReadOnlyDTO>(
                informacionAcademica
            );
            return Ok(
                new
                {
                    message = "Información académica actualizada correctamente.",
                    data = informacionAcademicaReadOnlyDTO
                }
            );
        }

        // DELETE - Eliminar información académica
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteInformacionAcademica(int id)
        {
            var informacionAcademica = await _informacionAcademicaService.GetByIdAsync(id);
            if (informacionAcademica == null)
            {
                return NotFound(
                    new { message = $"Información académica con id {id} no encontrada." }
                );
            }

            var result = await _informacionAcademicaService.DeleteAsync(id);
            if (!result)
            {
                return BadRequest(
                    new { message = $"No se pudo eliminar la información académica con id {id}." }
                );
            }

            return Ok(new { message = "Información académica eliminada correctamente." });
        }
    }
}
