using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MultiTenantManagement.Abstractions.Models.Dto.Application;
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

            _ = CreateMap<TenantDto, Tenant>().ReverseMap();

            _ = CreateMap<ProductDto, Product>().ReverseMap();//TODO: remove it

            _ = CreateMap<Customer, CustomerDto>()
                .ForMember(dest => dest.Activities, opt => opt.MapFrom(src => src.CustomersActivities.Select(ca => ca.Activity)))
                .ReverseMap();

            _ = CreateMap<RequestActivity, Activity>().ReverseMap();

            _ = CreateMap<Activity, ActivityDto>()
                .ForMember(dest => dest.Customers, opt => opt.MapFrom(src => src.CustomersActivities.Select(ca => ca.Customer)))
                .ReverseMap();

            _ = CreateMap<SiteDto, Site>().ReverseMap();
            _ = CreateMap<RatesDto, Rates>().ReverseMap();
            _ = CreateMap<CertificateDto, Certificate>().ReverseMap();
            _ = CreateMap<HoursActivityDto, HoursActivity>().ReverseMap();
            _ = CreateMap<FederalCardDto, FederalCard>().ReverseMap();
            _ = CreateMap<MembershipCardDto, MembershipCard>().ReverseMap();
        }
    }
}
