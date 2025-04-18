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
    public class PostulacionController : ControllerBase
    {
        private readonly IPostulacionService _postulacionService;
        private readonly IMapper _mapper;
        private readonly FakeDataGenerator _fakeDataGenerator;

        public PostulacionController(
            IPostulacionService postulacionService,
            IMapper mapper,
            FakeDataGenerator fakeDataGenerator
        )
        {
            _postulacionService = postulacionService;
            _mapper = mapper;
            _fakeDataGenerator = fakeDataGenerator;
        }

        // GET - Generar postulaciones aleatorias
        [HttpGet("GeneraFakePostulaciones")]
        public async Task<IActionResult> GenerateFakePostulaciones(int count = 10)
        {
            await _fakeDataGenerator.GenerateFakePostulaciones(count);
            return Ok(
                new
                {
                    message = $"{count} postulaciones generadas correctamente en la base de datos."
                }
            );
        }

        // GET by CandidatoId
        [HttpGet("GetPostulacionesByCandidatoId")]
        public async Task<
            ActionResult<IEnumerable<PostulacionReadOnlyDTO>>
        > GetPostulacionesByCandidatoId(int candidatoId)
        {
            var postulaciones = await _postulacionService.GetPostulacionesByCandidatoId(
                candidatoId
            );

            if (postulaciones == null || !postulaciones.Any())
            {
                return NotFound(
                    new { message = "No se encontraron postulaciones para este candidato." }
                );
            }

            var postulacionesDTO = _mapper.Map<IEnumerable<PostulacionReadOnlyDTO>>(postulaciones);
            return Ok(
                new { message = "Postulaciones obtenidas correctamente.", data = postulacionesDTO }
            );
        }

        // GET by EmpleoId
        [HttpGet("GetPostulacionesByEmpleoId")]
        public async Task<
            ActionResult<IEnumerable<PostulacionReadOnlyDTO>>
        > GetPostulacionesByEmpleoId(int empleoId)
        {
            var postulaciones = await _postulacionService.GetPostulacionesByEmpleoId(empleoId);

            if (postulaciones == null || !postulaciones.Any())
            {
                return NotFound(
                    new { message = "No se encontraron postulaciones para este empleo." }
                );
            }

            var postulacionesDTO = _mapper.Map<IEnumerable<PostulacionReadOnlyDTO>>(postulaciones);
            return Ok(
                new { message = "Postulaciones obtenidas correctamente.", data = postulacionesDTO }
            );
        }

        // POST
        [HttpPost("CreatePostulacion")]
        public async Task<ActionResult> CreatePostulacion([FromBody] PostulacionDTO postulacionDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(
                    new { message = "Hay errores en el modelo de datos.", errors = ModelState }
                );
            }

            var operationResult = await _postulacionService.CreatePostulacion(
                _mapper.Map<Postulacion>(postulacionDTO)
            );
            if (operationResult.Success)
            {
                return CreatedAtAction(
                    nameof(GetPostulacionesByCandidatoId),
                    new { candidatoId = postulacionDTO.CandidatoId },
                    new { message = "Postulación creada correctamente." }
                );
            }

            return BadRequest(new { message = operationResult.ErrorMessage });
        }

        // PUT
        [HttpPut("UpdateEstadoPostulacion")]
        public async Task<ActionResult> UpdateEstadoPostulacion(
            int id,
            [FromBody] string estadoPostulacion
        )
        {
            var operationResult = await _postulacionService.UpdateEstadoPostulacion(
                id,
                estadoPostulacion
            );

            if (operationResult.Success)
            {
                return Ok(new { message = "Estado de postulación actualizado correctamente." });
            }

            return BadRequest(new { message = operationResult.ErrorMessage });
        }

        // DELETE
        [HttpDelete("DeletePostulacion")]
        public async Task<ActionResult> DeletePostulacion(int id)
        {
            var operationResult = await _postulacionService.DeletePostulacion(id);

            if (operationResult.Success)
            {
                return NoContent();
            }

            return NotFound(new { message = operationResult.ErrorMessage });
        }
    }
}
