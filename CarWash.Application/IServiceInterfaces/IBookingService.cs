using CarWash.Core.DTOs;

namespace CarWash.Application.IServiceInterfaces;

public interface IBookingService : IService<BookingDto, CreateBookingDto>
{
    Task<bool> IsStationAvailableAsync(int stationId, DateTime dateTime);
    Task<BookingDto> CreateBookingAsync(CreateBookingDto dto, int stationId);
}