using System;

namespace EmpleosSur.WebAPI.DTOs
{
    public class InformacionAcademicaReadOnlyDTO
    {
        public int Id { get; set; }

        public string TituloObtenido { get; set; }

        public string Institucion { get; set; }

        public string Nivel { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime? FechaFin { get; set; }

        public bool EnCurso { get; set; }

        public CandidatoReadOnlyDTO Candidato { get; set; }
    }
}
