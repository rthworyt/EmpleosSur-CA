using AutoMapper;
using EmpleosSur.Application.Interfaces.IServices;
using EmpleosSur.Domain.Entities;
using EmpleosSur.WebAPI.DTOs;
using EmpleosSur.WebAPI.Generators;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CandidatosController : ControllerBase
{
    private readonly ICandidatoService _candidatoService;
    private readonly IMapper _mapper;
    private readonly FakeDataGenerator _fakeDataGenerator;

    public CandidatosController(
        ICandidatoService candidatoService,
        IMapper mapper,
        FakeDataGenerator fakeDataGenerator
    )
    {
        _candidatoService = candidatoService;
        _mapper = mapper;
        _fakeDataGenerator = fakeDataGenerator;
    }

    // GET por Id
    [HttpGet("GetCandidatoById")]
    public async Task<ActionResult<CandidatoReadOnlyDTO>> GetCandidatoById(int id)
    {
        if (id <= 0)
        {
            return BadRequest(new { message = "ID inválido." });
        }

        var candidato = await _candidatoService.GetAllDataCandidatoById(id);

        if (candidato == null)
        {
            return NotFound(new { message = "Candidato no encontrado." });
        }

        var candidatoDTO = _mapper.Map<CandidatoReadOnlyDTO>(candidato);
        return Ok(candidatoDTO);
    }

    // GET por email
    [HttpGet("GetCandidatoByEmail")]
    public async Task<ActionResult<CandidatoReadOnlyDTO>> GetCandidatoByEmail(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            return BadRequest(new { message = "El correo no puede estar vacío." });
        }

        var candidato = await _candidatoService.GetCandidatoByEmail(email);

        if (candidato == null)
        {
            return NotFound(new { message = "Candidato no encontrado con ese email." });
        }

        var candidatoDTO = _mapper.Map<CandidatoReadOnlyDTO>(candidato);
        return Ok(candidatoDTO);
    }

    // GET por ciudad
    [HttpGet("GetCandidatosByCiudad")]
    public async Task<ActionResult<IEnumerable<CandidatoReadOnlyDTO>>> GetCandidatosByCiudad(
        string ciudad
    )
    {
        if (string.IsNullOrEmpty(ciudad))
        {
            return BadRequest(new { message = "La ciudad no puede estar vacía." });
        }

        var candidatos = await _candidatoService.GetCandidatoByCiudad(ciudad);

        if (candidatos == null || !candidatos.Any())
        {
            return NotFound(new { message = "No se encontraron candidatos en esa ciudad." });
        }

        var candidatosDTO = _mapper.Map<IEnumerable<CandidatoReadOnlyDTO>>(candidatos);
        return Ok(candidatosDTO);
    }

    // POST - Crear candidato
    [HttpPost("CreateCandidato")]
    public async Task<ActionResult<CandidatoDTO>> CreateCandidato(CandidatoDTO candidatoDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(
                new { message = "Los datos proporcionados no son válidos.", errors = ModelState }
            );
        }

        var candidato = _mapper.Map<Candidato>(candidatoDTO);
        var result = await _candidatoService.CreateCandidato(candidato);

        if (!result.Success)
        {
            return BadRequest(
                new { message = "No se pudo crear el candidato.", error = result.ErrorMessage }
            );
        }

        var newCandidatoDTO = _mapper.Map<CandidatoDTO>(candidato);
        return CreatedAtAction(
            nameof(GetCandidatoById),
            new { id = candidato.Id },
            new { message = "Candidato creado correctamente.", data = newCandidatoDTO }
        );
    }

    // PUT - Actualizar candidato
    [HttpPut("UpdateCandidato")]
    public async Task<ActionResult> UpdateCandidato(int id, CandidatoDTO candidatoDTO)
    {
        if (id <= 0 || candidatoDTO == null)
        {
            return BadRequest(new { message = "ID o datos inválidos." });
        }

        var candidato = _mapper.Map<Candidato>(candidatoDTO);
        candidato.Id = id;

        var result = await _candidatoService.UpdateCandidato(candidato);

        if (!result.Success)
        {
            return BadRequest(new { message = result.ErrorMessage });
        }

        var updatedCandidatoDTO = _mapper.Map<CandidatoDTO>(candidato);
        return Ok(
            new { message = "Candidato actualizado correctamente.", data = updatedCandidatoDTO }
        );
    }

    // DELETE - Eliminar candidato
    [HttpDelete("DeleteCandidato")]
    public async Task<ActionResult> DeleteCandidato(int id)
    {
        if (id <= 0)
        {
            return BadRequest(new { message = "ID inválido." });
        }

        var result = await _candidatoService.DeleteCandidato(id);

        if (!result.Success)
        {
            return BadRequest(new { message = result.ErrorMessage });
        }

        return Ok(new { message = "Candidato eliminado correctamente." });
    }

    //// GET - Generar datos aleatorios (comentado)
    //[HttpGet("GeneraDatosAleatorios")]
    //public async Task<IActionResult> GenerateFakeCandidatos(int count = 10)
    //{
    //    await _fakeDataGenerator.GenerateFakeCandidatos(count);
    //    return Ok(
    //        new { message = $"{count} candidatos generados correctamente en la base de datos." }
    //    );
    //}
}
