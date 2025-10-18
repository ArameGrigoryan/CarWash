using CarWash.Core.Models;

namespace CarWash.Application.IRepositoryInterfaces;

public interface IBookingRepository : IRepository<Booking>
{
    Task<bool> IsStationAvailableAsync(int stationId, DateTime dateTime);
}