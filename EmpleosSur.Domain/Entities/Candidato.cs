using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EmpleosSur.Domain.Common;

namespace EmpleosSur.Domain.Entities
{
    public class Candidato : EntityBase
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [StringLength(50, ErrorMessage = "El apellido no puede exceder los 50 caracteres.")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El email es obligatorio.")]
        [EmailAddress(ErrorMessage = "Ingrese un email válido.")]
        [StringLength(150, ErrorMessage = "El email no puede exceder los 150 caracteres.")]
        public string Email { get; set; }

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

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
        [DataType(
            DataType.Date,
            ErrorMessage = "La fecha de nacimiento debe tener un formato válido (YYYY-MM-DD)."
        )]
        public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = "El género es obligatorio.")]
        public string Genero { get; set; }

        public ICollection<Postulacion> Postulaciones { get; set; } = new List<Postulacion>();
        public ICollection<ExperienciaLaboral> ExperienciasLaborales { get; set; } =
            new List<ExperienciaLaboral>();
        public ICollection<InformacionAcademica> InformacionesAcademicas { get; set; } =
            new List<InformacionAcademica>();
    }
}
