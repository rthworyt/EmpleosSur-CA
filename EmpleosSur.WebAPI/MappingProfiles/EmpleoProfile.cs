using AutoMapper;
using EmpleosSur.Domain.Entities;
using EmpleosSur.WebAPI.DTOs;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<EmpleoDTO, Empleo>();

        CreateMap<Empleo, EmpleoReadOnlyDTO>()
            .ForMember(dest => dest.Empresa, opt => opt.MapFrom(src => src.Empresa));

        CreateMap<EmpresaDTO, Empresa>();

        CreateMap<Empresa, EmpresaReadOnlyDTO>();
    }
}
