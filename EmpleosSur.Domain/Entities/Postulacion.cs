using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EmpleosSur.Domain.Common;

namespace EmpleosSur.Domain.Entities
{
    public enum EstadoPostulacion
    {
        Pendiente,
        Aceptado,
        Rechazado
    }

    public class Postulacion : EntityBase
    {
        [Required]
        public int EmpleoId { get; set; }

        [ForeignKey("EmpleoId")]
        public Empleo Empleo { get; set; }

        [Required(ErrorMessage = "La fecha de postulación es obligatoria.")]
        [DataType(DataType.Date, ErrorMessage = "La fecha debe tener un formato válido (YYYY-MM-DD).")]
        [CustomValidation(typeof(Postulacion), nameof(ValidarFechaNoFutura))]
        public DateTime FechaPostulacion { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio.")]
        public EstadoPostulacion Estado { get; set; }

        [Required]
        public int CandidatoId { get; set; }

        [ForeignKey("CandidatoId")]
        public Candidato Candidato { get; set; }

        public static ValidationResult ValidarFechaNoFutura(DateTime fecha, ValidationContext context)
        {
            return fecha > DateTime.Today
                ? new ValidationResult("La fecha de postulación no puede ser en el futuro.")
                : ValidationResult.Success;
        }
    }
}
