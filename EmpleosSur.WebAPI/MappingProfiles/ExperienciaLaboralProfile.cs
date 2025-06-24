using AutoMapper;
using EmpleosSur.Domain.Entities;
using EmpleosSur.WebAPI.DTOs;

public class ExperienciaLaboralProfile : Profile
{
    public ExperienciaLaboralProfile()
    {
        CreateMap<ExperienciaLaboralDTO, ExperienciaLaboral>()
            .ForMember(dest => dest.CandidatoId, opt => opt.MapFrom(src => src.CandidatoId))
            .ForMember(dest => dest.FechaFin, opt => opt.MapFrom(src => src.FechaFin ?? DateTime.MinValue));

        CreateMap<ExperienciaLaboral, ExperienciaLaboralReadOnlyDTO>()
            .ForMember(dest => dest.Candidato, opt => opt.MapFrom(src => src.Candidato));
    }
}
