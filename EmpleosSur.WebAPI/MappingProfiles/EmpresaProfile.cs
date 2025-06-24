using AutoMapper;
using EmpleosSur.Domain.Entities;
using EmpleosSur.WebAPI.DTOs;

public class EmpresaProfile : Profile
{
    public EmpresaProfile()
    {
        CreateMap<Empresa, EmpresaDTO>();

        CreateMap<EmpresaDTO, Empresa>()
            .ForMember(dest => dest.Empleos, opt => opt.Ignore());

        CreateMap<Empresa, EmpresaReadOnlyDTO>();

        CreateMap<EmpresaReadOnlyDTO, Empresa>()
            .ForMember(dest => dest.Empleos, opt => opt.Ignore());
    }
}
