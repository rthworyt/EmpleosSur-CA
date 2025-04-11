using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EmpleosSur.Domain.Common;

namespace EmpleosSur.Domain.Entities
{
    public class Empresa : EntityBase
    {
        [Required(ErrorMessage = "El nombre de la empresa es obligatorio.")]
        [StringLength(
            100,
            ErrorMessage = "El nombre de la empresa no puede exceder los 100 caracteres."
        )]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El nombre del representante es obligatorio.")]
        [StringLength(
            100,
            ErrorMessage = "El nombre del representante no puede exceder los 100 caracteres."
        )]
        public string NombreRepresentante { get; set; }

        [Required(ErrorMessage = "El email corporativo es obligatorio.")]
        [EmailAddress(ErrorMessage = "El correo debe tener un formato válido.")]
        [StringLength(
            150,
            ErrorMessage = "El email corporativo no puede exceder los 150 caracteres."
        )]
        public string EmailCorporativo { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [RegularExpression(
            @"^\d{10}$",
            ErrorMessage = "El teléfono debe contener exactamente 10 dígitos."
        )]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El RNC es obligatorio.")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "El RNC debe tener 11 caracteres.")]
        public string RNC { get; set; }

        [Required(ErrorMessage = "La dirección es obligatoria.")]
        [StringLength(200, ErrorMessage = "La dirección no puede exceder los 200 caracteres.")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "La cédula del representante es obligatoria.")]
        [StringLength(
            11,
            MinimumLength = 11,
            ErrorMessage = "La cédula del representante debe tener 11 caracteres."
        )]
        public string CedulaRepresentante { get; set; }

        [Phone(ErrorMessage = "El teléfono del representante debe tener un formato válido.")]
        public string? TelefonoRepresentante { get; set; }

        [StringLength(
            100,
            ErrorMessage = "El registro mercantil no puede exceder los 100 caracteres."
        )]
        public string? RegistroMercantil { get; set; }
        public ICollection<Empleo> Empleos { get; set; } = new List<Empleo>();
    }
}
