using System;
using System.Linq;
using API.DTOs;
using API.Entities;
using AutoMapper;

namespace API.Others
{
    public class AutoMapperManager : Profile
    {
        public AutoMapperManager()
        {
            CreateMap<AppUser, UserToRegisterDTO>().ReverseMap();
            CreateMap<UserToReturnDTO, AppUser>();
            CreateMap<AppUser, UserToReturnDTO>().
                ForMember(u => u.Role, opt => opt.MapFrom(src => src.UserRoles.FirstOrDefault(x => x.UserId==src.Id).Role.Name));
            CreateMap<PackageToAddDTO, Package>();
            CreateMap<Package, PackageToReturnDTO>()
                .ForMember(p => p.DeliverManUserName, opt => opt.MapFrom(src => src.DeliveryMan.UserName))
                .ForMember(p => p.DeliveryManPhoneNumber, opt => opt.MapFrom(src => src.DeliveryMan.PhoneNumber))
                .ForMember(p => p.CurrentStatus, opt => opt.MapFrom(src => src.Statuses.Count > 0 ? src.Statuses.Last().Name : "Oczekuje na nadanie."))
                .ForMember(p => p.CurrentStatusDate, opt => opt.MapFrom(src => src.Statuses.Count > 0 ? src.Statuses.Last().StatusDate : src.DateSent));

            CreateMap<PackageStatus, PackageStatusToAddDTO>().ReverseMap();
            CreateMap<PackageStatus, PackageStatusToReturnDTO>().ReverseMap();
        }
    }
}