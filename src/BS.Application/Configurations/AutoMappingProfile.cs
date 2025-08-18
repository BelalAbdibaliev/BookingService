using AutoMapper;
using BS.Application.Dto;
using BS.Domain.Entities;

namespace BS.Application.Configurations;

public class AutoMappingProfile: Profile
{
    public AutoMappingProfile()
    {
        CreateMap<CreateResourceDto, Resource>();
        CreateMap<RegisterUserDto, User>();
        CreateMap<BookingDto, Booking>();
        CreateMap<Booking, BookingDto>();
    }
}