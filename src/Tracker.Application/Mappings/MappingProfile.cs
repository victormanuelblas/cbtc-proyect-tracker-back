using AutoMapper;
using Tracker.Application.DTOs.User;
using Tracker.Application.DTOs.Customer;
using Tracker.Application.DTOs.Shipment;
using Tracker.Domain.Entities;

namespace Tracker.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateCustomerDto, Customer>();
            
            CreateMap<Customer, CustomerDto>()
            .ForMember(dest => dest.CustomerTypeDescription, opt => opt.MapFrom(src => src.CustomerType.Description));

            CreateMap<UpdateCustomerDto, Customer>();
            CreateMap<CreateUserDto, User>();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<UpdateUserDto, User>();
            CreateMap<Shipment, ShipmentDto>().ReverseMap();
            CreateMap<CreateShipmentDto, Shipment>();
            CreateMap<UpdateShipmentDto, Shipment>();
            CreateMap<UpdateShipmentStatusDto, Shipment>();
        }
    }
}