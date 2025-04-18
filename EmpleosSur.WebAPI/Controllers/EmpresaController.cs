using AutoMapper;
using EmpleosSur.Application.Interfaces.IServices;
using EmpleosSur.Domain.Entities;
using EmpleosSur.WebAPI.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace EmpleosSur.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private readonly IEmpresaService _empresaService;
        private readonly IMapper _mapper;

        public EmpresaController(IEmpresaService empresaService, IMapper mapper)
        {
            _empresaService = empresaService;
            _mapper = mapper;
        }

        // POST  
        [HttpPost("CreateEmpresa")]
        public async Task<ActionResult<EmpresaReadOnlyDTO>> CreateEmpresa(EmpresaDTO empresaDTO)
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

            var empresa = _mapper.Map<Empresa>(empresaDTO);
            await _empresaService.CreateAsync(empresa);

            var newEmpresaDTO = _mapper.Map<EmpresaReadOnlyDTO>(empresa);
            return CreatedAtAction(
                nameof(GetEmpresaById),
                new { id = empresa.Id },
                new { message = "Empresa creada correctamente.", data = newEmpresaDTO }
            );
        }

        // GET 
        [HttpGet("GetEmpresaById")]
        public async Task<ActionResult<EmpresaReadOnlyDTO>> GetEmpresaById(int id)
        {
            var empresa = await _empresaService.GetByIdAsync(id);
            if (empresa == null)
            {
                return NotFound(new { message = "Empresa no encontrada." });
            }

            var empresaDTO = _mapper.Map<EmpresaReadOnlyDTO>(empresa);
            return Ok(new { message = "Empresa encontrada correctamente.", data = empresaDTO });
        }

        //// GET 
        //[HttpGet("GetAllEmpresas")]
        //public async Task<ActionResult<IEnumerable<EmpresaReadOnlyDTO>>> GetAllEmpresas()
        //{
        //    var empresas = await _empresaService.GetAllAsync();
        //    var empresasDTO = _mapper.Map<IEnumerable<EmpresaReadOnlyDTO>>(empresas);
        //    return Ok(
        //        new { message = "Lista de empresas obtenida correctamente.", data = empresasDTO }
        //    );
        //}

        // PUT 
        [HttpPut("UpdateEmpresa")]
        public async Task<ActionResult<EmpresaReadOnlyDTO>> UpdateEmpresa(
            int id,
            EmpresaDTO empresaDTO
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

            var empresa = await _empresaService.GetByIdAsync(id);
            if (empresa == null)
            {
                return NotFound(new { message = "Empresa no encontrada." });
            }

            _mapper.Map(empresaDTO, empresa); // Mapear los cambios a la entidad existente  
            await _empresaService.UpdateAsync(empresa);

            var updatedEmpresaDTO = _mapper.Map<EmpresaReadOnlyDTO>(empresa);
            return Ok(
                new { message = "Empresa actualizada correctamente.", data = updatedEmpresaDTO }
            );
        }

        // DELETE  
        [HttpDelete("DeleteEmpresaById")]
        public async Task<ActionResult> DeleteEmpresa(int id)
        {
            var empresa = await _empresaService.GetByIdAsync(id);
            if (empresa == null)
            {
                return NotFound(new { message = "Empresa no encontrada." });
            }

            var result = await _empresaService.DeleteAsync(id);

            if (!result)
            {
                return BadRequest(new { message = "No se pudo eliminar la empresa." });
            }

            return Ok(new { message = "Empresa eliminada correctamente." });
        }

        // GET
        [HttpGet("GetEmpresaByRNC")]
        public async Task<ActionResult<EmpresaReadOnlyDTO>> GetEmpresaByRNC(string rnc)
        {
            var empresa = await _empresaService.GetEmpresaByRNC(rnc);
            if (empresa == null)
            {
                return NotFound(new { message = "Empresa no encontrada con ese RNC." });
            }

            var empresaDTO = _mapper.Map<EmpresaReadOnlyDTO>(empresa);
            return Ok(new { message = "Empresa encontrada correctamente.", data = empresaDTO });
        }

        // GET
        [HttpGet("GetEmpresasByLocalidad")]
        public async Task<ActionResult<IEnumerable<EmpresaReadOnlyDTO>>> GetEmpresasByLocalidad(string localidad)
        {
            var empresas = await _empresaService.GetEmpresaByLocalidad(localidad);
            if (empresas == null || !empresas.Any())
            {
                return NotFound(new { message = "No se encontraron empresas en esa localidad." });
            }

            var empresasDTO = _mapper.Map<IEnumerable<EmpresaReadOnlyDTO>>(empresas);
            return Ok(new { message = "Empresas encontradas correctamente.", data = empresasDTO });
        }

        // GET
        [HttpGet("GetEmpresasByNombre")]
        public async Task<ActionResult<IEnumerable<EmpresaReadOnlyDTO>>> GetEmpresasByNombre(string nombre)
        {
            var empresas = await _empresaService.GetEmpresaByNombre(nombre);
            if (empresas == null || !empresas.Any())
            {
                return NotFound(new { message = "No se encontraron empresas con ese nombre." });
            }

            var empresasDTO = _mapper.Map<IEnumerable<EmpresaReadOnlyDTO>>(empresas);
            return Ok(new { message = "Empresas encontradas correctamente.", data = empresasDTO });
        }

    }
}
