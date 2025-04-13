using System;
using System.ComponentModel.DataAnnotations;

namespace EmpleosSur.WebAPI.DTOs
{
    public class ExperienciaLaboralDTO
    {
        [Required(ErrorMessage = "El nombre de la empresa es obligatorio.")]
        [StringLength(
            100,
            ErrorMessage = "El nombre de la empresa no puede exceder los 100 caracteres."
        )]
        public string Empresa { get; set; }

        [Required(ErrorMessage = "El cargo es obligatorio.")]
        [StringLength(100, ErrorMessage = "El cargo no puede exceder los 100 caracteres.")]
        public string Cargo { get; set; }

        [Required(ErrorMessage = "La fecha de inicio es obligatoria.")]
        [DataType(
            DataType.Date,
            ErrorMessage = "La fecha de inicio debe tener un formato válido (YYYY-MM-DD)."
        )]
        public DateTime FechaInicio { get; set; }

        [DataType(
            DataType.Date,
            ErrorMessage = "La fecha de fin debe tener un formato válido (YYYY-MM-DD)."
        )]
        public DateTime? FechaFin { get; set; }

        [StringLength(1000, ErrorMessage = "La descripción no puede exceder los 1000 caracteres.")]
        public string? Descripcion { get; set; }

        public bool EnCurso { get; set; }

        [Required(ErrorMessage = "El candidato es obligatorio.")]
        public int CandidatoId { get; set; }
    }
}
