using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MultiTenantManagement.Abstractions.Models.Dto.Application.Product;
using MultiTenantManagement.Abstractions.Models.Dto.Authentication.Register;
using MultiTenantManagement.Abstractions.Models.Dto.Authentication.Tenant;
using MultiTenantManagement.Abstractions.Models.Entities.Application;
using MultiTenantManagement.Abstractions.Models.Entities.Authentication;

namespace MultiTenantManagement.Mapper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            _ = CreateMap<RegisterRequestDto, ApplicationUser>()
                .ForMember(dst => dst.UserName, src => src.MapFrom(src => src.Email))
                .ReverseMap();

            _ = CreateMap<RegisterResponseDto, CreateTenantResponse>().ReverseMap();
            _ = CreateMap<IdentityResult, RegisterResponseDto>()
                .ForMember(dst => dst.IsSuccess, src => src.MapFrom(src => src.Succeeded))
                .ForMember(dst => dst.Errors, src => src.MapFrom(src => src.Errors.Select(e => e.Description)))
                .ReverseMap();

            _ = CreateMap<ProductDto, Product>().ReverseMap();

            _ = CreateMap<TenantDto, Tenant>().ReverseMap();
        }
    }
}
