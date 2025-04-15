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

    [HttpGet("GeneraDatosAleatorios")]
    public async Task<IActionResult> GenerateFakeCandidatos(int count = 10)
    {
        await _fakeDataGenerator.GenerateFakeCandidatos(count);
        return Ok(
            new { message = $"{count} candidatos generados correctamente en la base de datos." }
        );
    }

    // GET
    [HttpGet("email/{email}")]
    public async Task<ActionResult<CandidatoReadOnlyDTO>> GetCandidatoByEmail(string email)
    {
        var candidato = await _candidatoService.GetCandidatoByEmail(email);

        if (candidato == null)
        {
            return NotFound();
        }

        var candidatoDTO = _mapper.Map<CandidatoReadOnlyDTO>(candidato);
        return Ok(candidatoDTO);
    }

    // GET
    [HttpGet("{id}")]
    public async Task<ActionResult<CandidatoReadOnlyDTO>> GetCandidatoById(int id)
    {
        var candidato = await _candidatoService.GetAllDataCandidatoById(id);

        if (candidato == null)
        {
            return NotFound();
        }

        var candidatoDTO = _mapper.Map<CandidatoReadOnlyDTO>(candidato);
        return Ok(candidatoDTO);
    }

    // GET
    [HttpGet("ciudad/{ciudad}")]
    public async Task<ActionResult<IEnumerable<CandidatoReadOnlyDTO>>> GetCandidatosByCiudad(string ciudad)
    {
        var candidatos = await _candidatoService.GetCandidatoByCiudad(ciudad);

        if (candidatos == null || !candidatos.Any())
        {
            return NotFound();
        }

        var candidatosDTO = _mapper.Map<IEnumerable<CandidatoReadOnlyDTO>>(candidatos);
        return Ok(candidatosDTO);
    }

    // POST
    [HttpPost]
    public async Task<ActionResult<CandidatoDTO>> CreateCandidato(CandidatoDTO candidatoDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var candidato = _mapper.Map<Candidato>(candidatoDTO);
        var result = await _candidatoService.CreateCandidato(candidato);

        if (!result.Success)
        {
            return BadRequest(result.ErrorMessage);
        }

        var newCandidatoDTO = _mapper.Map<CandidatoDTO>(candidato);
        return CreatedAtAction(
            nameof(GetCandidatoById),
            new { id = candidato.Id },
            newCandidatoDTO
        );
    }

    // PUT
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateCandidato(int id, CandidatoDTO candidatoDTO)
    {
        if (id == 0 || candidatoDTO == null)
        {
            return BadRequest("Id o datos inválidos.");
        }

        var candidato = _mapper.Map<Candidato>(candidatoDTO);
        candidato.Id = id;

        var result = await _candidatoService.UpdateCandidato(candidato);

        if (!result.Success)
        {
            return BadRequest(result.ErrorMessage);
        }

        var updatedCandidatoDTO = _mapper.Map<CandidatoDTO>(candidato);
        return Ok(updatedCandidatoDTO);
    }

    // DELETE
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCandidato(int id)
    {
        var result = await _candidatoService.DeleteCandidato(id);

        if (!result.Success)
        {
            return BadRequest(result.ErrorMessage);
        }

        return Ok(new { message = "Candidato eliminado correctamente." });
    }
}
