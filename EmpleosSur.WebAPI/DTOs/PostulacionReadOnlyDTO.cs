using System;
using System.ComponentModel.DataAnnotations;
using EmpleosSur.Domain.Entities;

namespace EmpleosSur.WebAPI.DTOs
{
    public class PostulacionReadOnlyDTO
    {
        public int Id { get; set; }
        public string TituloEmpleo { get; set; }
        public CandidatoReadOnlyDTO Candidato { get; set; }
        public EmpleoReadOnlyDTO Empleo { get; set; }
        public DateTime FechaPostulacion { get; set; }

        [EnumDataType(typeof(EstadoPostulacion), ErrorMessage = "Estado inválido.")]
        public EstadoPostulacion Estado { get; set; }
    }
}
