using System;

namespace EmpleosSur.WebAPI.DTOs
{
    public class ExperienciaLaboralReadOnlyDTO
    {
        public int Id { get; set; }

        public string Empresa { get; set; }

        public string Cargo { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime? FechaFin { get; set; }

        public string? Descripcion { get; set; }

        public bool EnCurso { get; set; }

        public required CandidatoReadOnlyDTO Candidato { get; set; }
    }
}
