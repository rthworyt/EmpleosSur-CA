using System.ComponentModel.DataAnnotations;

namespace EmpleosSur.WebAPI.DTOs
{
    public class CandidatoDTO
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [StringLength(50, ErrorMessage = "El apellido no puede exceder los 50 caracteres.")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El email es obligatorio.")]
        [EmailAddress(ErrorMessage = "Ingrese un email válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El género es obligatorio.")]
        public string Genero { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(CandidatoDTO), nameof(ValidarMayorDeEdad))]
        public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [RegularExpression(
            @"^\d{10}$",
            ErrorMessage = "El teléfono debe contener exactamente 10 dígitos."
        )]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "La ciudad es obligatoria.")]
        [StringLength(100, ErrorMessage = "La ciudad no puede exceder los 100 caracteres.")]
        public string Ciudad { get; set; }

        [Required(ErrorMessage = "La dirección es obligatoria.")]
        [StringLength(200, ErrorMessage = "La dirección no puede exceder los 200 caracteres.")]
        public string Direccion { get; set; }

        [StringLength(500, ErrorMessage = "La descripción no puede exceder los 500 caracteres.")]
        public string Descripcion { get; set; }

        public static ValidationResult ValidarMayorDeEdad(
            DateTime fechaNacimiento,
            ValidationContext context
        )
        {
            var edad = DateTime.Today.Year - fechaNacimiento.Year;
            if (fechaNacimiento > DateTime.Today.AddYears(-edad))
                edad--;

            if (edad < 18)
            {
                return new ValidationResult("Debes tener al menos 18 años para registrarte.");
            }

            return ValidationResult.Success;
        }
    }
}
