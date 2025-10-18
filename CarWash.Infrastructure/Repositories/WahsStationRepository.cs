using CarWash.Application.IRepositoryInterfaces;
using CarWash.Core.Models;
using CarWash.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CarWash.Infrastructure.Repositories;

public class WashStationRepository : Repository<WashStation>, IWashStationRepository
{
    private readonly CarWashContext _context;
    public WashStationRepository(CarWashContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<WashStation>> GetAvailableStationsAsync(DateTime startTime)
    {
        var endTime = startTime.AddMinutes(30);
        return await _context.WashStations.
            Where(ws => !ws.Bookings.Any(b => b.StartTime < endTime &&  b.EndTime > startTime))
            .ToListAsync();
    }
}