using AutoMapper;
using CarWash.Core.DTOs;
using CarWash.Core.Models;

namespace CarWash.Application.Mapping;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Booking, BookingDto>().ReverseMap();
        
        CreateMap<CreateBookingDto, Booking>();
        
        CreateMap<Car, CarDto>().ReverseMap();

        CreateMap<Service, ServiceDto>().ReverseMap();

        CreateMap<User, UserDto>().ReverseMap();
        
        CreateMap<WashStation, WashStationDto>().ReverseMap();
        
        CreateMap<CreateUserDto, User>();

        CreateMap<CreateCarDto, Car>();

        CreateMap<CreateServiceDto, Service>();
        
        CreateMap<Service, ServiceDto>();
        
        CreateMap<CreateWashStationDto, WashStation>();

    }
}    