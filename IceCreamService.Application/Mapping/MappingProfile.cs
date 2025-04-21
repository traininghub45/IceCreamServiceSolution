using AutoMapper;
using IceCreamService.Application.DTOs;
using IceCreamService.Core.Entities;

namespace IceCreamService.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Define your mappings here
            CreateMap<Booking, BookingDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<ContactMessage, ContactMessageDto>().ReverseMap();
        }
    }
}
