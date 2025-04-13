using System.ComponentModel.DataAnnotations;

namespace EmpleosSur.WebAPI.DTOs
{
    public class CandidatoReadOnlyDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Telefono { get; set; }
        public string Genero { get; set; }
        public string Ciudad { get; set; }
        public string Direccion { get; set; }
        public string QuienSoy { get; set; }
        public List<ExperienciaLaboralDTO> ExperienciasLaborales { get; set; }
        public List<InformacionAcademicaDTO> InformacionesAcademicas { get; set; }
    }
}
