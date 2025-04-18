using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmpleosSur.WebAPI.DTOs
{
    public class EmpresaReadOnlyDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string NombreRepresentante { get; set; }

        public string EmailCorporativo { get; set; }

        public string Telefono { get; set; }

        public string RNC { get; set; }

        public string Ciudad { get; set; }

        public string Direccion { get; set; }

        public string CedulaRepresentante { get; set; }

        public string? TelefonoRepresentante { get; set; }

        public string? RegistroMercantil { get; set; }

        public ICollection<EmpleoReadOnlyDTO> Empleos { get; set; } = new List<EmpleoReadOnlyDTO>();
    }
}
