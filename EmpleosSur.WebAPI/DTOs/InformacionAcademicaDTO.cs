using System;
using System.ComponentModel.DataAnnotations;
using EmpleosSur.Domain.Entities;

namespace EmpleosSur.WebAPI.DTOs
{
    public class InformacionAcademicaDTO
    {
        [Required(ErrorMessage = "El título obtenido es obligatorio.")]
        [StringLength(100, ErrorMessage = "El título no puede exceder los 100 caracteres.")]
        public string TituloObtenido { get; set; }

        [Required(ErrorMessage = "El nombre de la institución es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre de la institución no puede exceder los 100 caracteres.")]
        public string Institucion { get; set; }

        [Required(ErrorMessage = "El nivel académico es obligatorio.")]
        [EnumDataType(typeof(NivelAcademico), ErrorMessage = "El nivel debe ser uno de los valores (Universitario, Secundario, Técnico).")]
        public NivelAcademico Nivel { get; set; }

        [Required(ErrorMessage = "La fecha de inicio es obligatoria.")]
        [DataType(DataType.Date, ErrorMessage = "La fecha de inicio debe tener un formato válido (YYYY-MM-DD).")]
        public DateTime FechaInicio { get; set; }

        [DataType(DataType.Date, ErrorMessage = "La fecha de finalización debe tener un formato válido (YYYY-MM-DD).")]
        public DateTime? FechaFin { get; set; }

        public bool EnCurso { get; set; }

        [Required(ErrorMessage = "El candidato es obligatorio.")]
        public int CandidatoId { get; set; }
    }
}
