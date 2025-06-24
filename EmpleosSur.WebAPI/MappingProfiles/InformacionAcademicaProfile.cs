using AutoMapper;
using EmpleosSur.Domain.Entities;
using EmpleosSur.WebAPI.DTOs;

public class InformacionAcademicaProfile : Profile
{
    public InformacionAcademicaProfile()
    {
        CreateMap<InformacionAcademica, InformacionAcademicaDTO>()
            .ForMember(dest => dest.Nivel, opt => opt.MapFrom(src => src.Nivel.ToString()));

        CreateMap<InformacionAcademica, InformacionAcademicaReadOnlyDTO>()
            .ForMember(dest => dest.Nivel, opt => opt.MapFrom(src => src.Nivel.ToString()))
            .ForMember(
                dest => dest.Candidato,
                opt =>
                    opt.MapFrom(src => new CandidatoReadOnlyDTO
                    {
                        Id = src.Candidato.Id,
                        Nombre = src.Candidato.Nombre,
                        Apellido = src.Candidato.Apellido
                    })
            );
    }
}
