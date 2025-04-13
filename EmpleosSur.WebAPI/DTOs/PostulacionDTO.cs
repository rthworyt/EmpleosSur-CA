using System;
using System.ComponentModel.DataAnnotations;
using EmpleosSur.Domain.Entities;

namespace EmpleosSur.WebAPI.DTOs
{
    public class PostulacionDTO
    {
        [Required(ErrorMessage = "El empleo es obligatorio.")]
        public int EmpleoId { get; set; }

        [Required(ErrorMessage = "La fecha de postulación es obligatoria.")]
        [DataType(DataType.Date, ErrorMessage = "La fecha debe tener un formato válido (YYYY-MM-DD).")]
        [CustomValidation(typeof(PostulacionDTO), nameof(ValidarFechaNoFutura))]
        public DateTime FechaPostulacion { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio.")]
        public EstadoPostulacion Estado { get; set; }

        [Required(ErrorMessage = "El candidato es obligatorio.")]
        public int CandidatoId { get; set; }

        public static ValidationResult ValidarFechaNoFutura(DateTime fecha, ValidationContext context)
        {
            return fecha > DateTime.Today
                ? new ValidationResult("La fecha de postulación no puede ser en el futuro.")
                : ValidationResult.Success;
        }
    }
}
