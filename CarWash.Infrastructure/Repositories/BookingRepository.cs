using CarWash.Application.IRepositoryInterfaces;
using CarWash.Core.Models;
using CarWash.Core.Models.Enums;
using CarWash.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CarWash.Infrastructure.Repositories;

public class BookingRepository : Repository<Booking>, IBookingRepository
{
    private readonly CarWashContext _context;
    
    public BookingRepository(CarWashContext context) : base(context)
    {
        _context = context;
    }

    public async Task<bool> IsStationAvailableAsync(int stationId, DateTime startTime)
    {
        var startUtc = startTime.ToUniversalTime();
        var endUtc = startUtc.AddMinutes(30);

        var hasOverlap = await _context.Bookings
            .AnyAsync(b =>
                b.WashStationId == stationId &&
                (b.Status == BookingStatus.InQueue || b.Status == BookingStatus.Washing) &&
                b.StartTime < endUtc &&
                b.EndTime > startUtc
            );

        return !hasOverlap;
    }


}