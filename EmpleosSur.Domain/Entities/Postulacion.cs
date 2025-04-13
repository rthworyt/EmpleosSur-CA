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
        public DateTime FechaPostulacion { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio.")]
        public EstadoPostulacion Estado { get; set; }

        [Required]
        public int CandidatoId { get; set; }

        [ForeignKey("CandidatoId")]
        public Candidato Candidato { get; set; }
    }
}
