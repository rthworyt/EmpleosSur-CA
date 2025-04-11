using System.ComponentModel.DataAnnotations;
using EmpleosSur.Domain.Common;

namespace EmpleosSur.Domain.Entities
{
    public class Empleo : EntityBase
    {
        [Required(ErrorMessage = "El título es obligatorio.")]
        [StringLength(100, ErrorMessage = "El título no puede exceder los 100 caracteres.")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [StringLength(1000, ErrorMessage = "La descripción no puede exceder los 1000 caracteres.")]
        public string Descripcion { get; set; }

        [StringLength(1000, ErrorMessage = "Los requisitos no pueden exceder los 1000 caracteres.")]
        public string? Requisitos { get; set; }

        [Required(ErrorMessage = "La ubicación es obligatoria.")]
        [StringLength(200, ErrorMessage = "La ubicación no puede exceder los 200 caracteres.")]
        public string Ubicacion { get; set; }

        [Required(ErrorMessage = "El salario es obligatorio.")]
        [Range(0, double.MaxValue, ErrorMessage = "El salario debe ser un valor positivo.")]
        public decimal Salario { get; set; }

        [Required(ErrorMessage = "La fecha de publicación es obligatoria.")]
        [DataType(
            DataType.Date,
            ErrorMessage = "La fecha de publicación debe tener un formato válido (YYYY-MM-DD)."
        )]
        public DateTime FechaPublicacion { get; set; }

        [Required]
        public int EmpresaId { get; set; }

        public Empresa Empresa { get; set; }

        public ICollection<Postulacion> Postulaciones { get; set; } = new List<Postulacion>();
    }
}
