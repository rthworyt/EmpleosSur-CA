using AutoMapper;
using EmpleosSur.Domain.Entities;
using EmpleosSur.WebAPI.DTOs;

public class CandidatoProfile : Profile
{
    public CandidatoProfile()
    {
        // Candidato a CandidatoDTO
        CreateMap<Candidato, CandidatoDTO>()
            .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
            .ForMember(dest => dest.Apellido, opt => opt.MapFrom(src => src.Apellido))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Genero, opt => opt.MapFrom(src => src.Genero))
            .ForMember(dest => dest.FechaNacimiento, opt => opt.MapFrom(src => src.FechaNacimiento))
            .ForMember(dest => dest.Telefono, opt => opt.MapFrom(src => src.Telefono))
            .ForMember(dest => dest.Ciudad, opt => opt.MapFrom(src => src.Ciudad))
            .ForMember(dest => dest.Direccion, opt => opt.MapFrom(src => src.Direccion))
            .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion));

        // CandidatoDTO a Candidato
        CreateMap<CandidatoDTO, Candidato>()
            .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
            .ForMember(dest => dest.Apellido, opt => opt.MapFrom(src => src.Apellido))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Genero, opt => opt.MapFrom(src => src.Genero))
            .ForMember(dest => dest.FechaNacimiento, opt => opt.MapFrom(src => src.FechaNacimiento))
            .ForMember(dest => dest.Telefono, opt => opt.MapFrom(src => src.Telefono))
            .ForMember(dest => dest.Ciudad, opt => opt.MapFrom(src => src.Ciudad))
            .ForMember(dest => dest.Direccion, opt => opt.MapFrom(src => src.Direccion))
            .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion))
            .ForMember(dest => dest.Postulaciones, opt => opt.Ignore())
            .ForMember(dest => dest.ExperienciasLaborales, opt => opt.Ignore())
            .ForMember(dest => dest.InformacionesAcademicas, opt => opt.Ignore());

        // Candidato a CandidatoReadOnlyDTO
        CreateMap<Candidato, CandidatoReadOnlyDTO>()
            .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
            .ForMember(dest => dest.Apellido, opt => opt.MapFrom(src => src.Apellido))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Genero, opt => opt.MapFrom(src => src.Genero))
            .ForMember(dest => dest.FechaNacimiento, opt => opt.MapFrom(src => src.FechaNacimiento))
            .ForMember(dest => dest.Telefono, opt => opt.MapFrom(src => src.Telefono))
            .ForMember(dest => dest.Ciudad, opt => opt.MapFrom(src => src.Ciudad))
            .ForMember(dest => dest.Direccion, opt => opt.MapFrom(src => src.Direccion))
            .ForMember(dest => dest.QuienSoy, opt => opt.MapFrom(src => src.Descripcion))
            .ForMember(dest => dest.ExperienciasLaborales, opt => opt.MapFrom(src => src.ExperienciasLaborales))
            .ForMember(dest => dest.InformacionesAcademicas, opt => opt.MapFrom(src => src.InformacionesAcademicas));
    }
}
