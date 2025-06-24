using AutoMapper;
using EmpleosSur.Domain.Entities;
using EmpleosSur.WebAPI.DTOs;

public class CandidatoProfile : Profile
{
    public CandidatoProfile()
    {
        CreateMap<Candidato, CandidatoDTO>();

        CreateMap<CandidatoDTO, Candidato>()
            .ForMember(dest => dest.Postulaciones, opt => opt.Ignore())
            .ForMember(dest => dest.ExperienciasLaborales, opt => opt.Ignore())
            .ForMember(dest => dest.InformacionesAcademicas, opt => opt.Ignore());

        CreateMap<Candidato, CandidatoReadOnlyDTO>();

        CreateMap<CandidatoReadOnlyDTO, Candidato>()
            .ForMember(dest => dest.Postulaciones, opt => opt.Ignore())
            .ForMember(dest => dest.ExperienciasLaborales, opt => opt.Ignore())
            .ForMember(dest => dest.InformacionesAcademicas, opt => opt.Ignore());
    }
}
