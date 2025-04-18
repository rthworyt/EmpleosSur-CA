using AutoMapper;
using EmpleosSur.Domain.Entities;
using EmpleosSur.WebAPI.DTOs;

public class PostulacionProfile : Profile
{
    public PostulacionProfile()
    {
        CreateMap<PostulacionDTO, Postulacion>()
            .ForMember(dest => dest.FechaPostulacion, opt => opt.MapFrom(src => src.FechaPostulacion))
            .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Estado));

        CreateMap<Postulacion, PostulacionReadOnlyDTO>()
            .ForMember(dest => dest.TituloEmpleo, opt => opt.MapFrom(src => src.Empleo.Titulo))
            .ForMember(dest => dest.Candidato, opt => opt.MapFrom(src => src.Candidato))
            .ForMember(dest => dest.Empleo, opt => opt.MapFrom(src => src.Empleo));
    }
}
