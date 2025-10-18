using CarWash.Core.Models;

namespace CarWash.Application.IRepositoryInterfaces;

public interface IWashStationRepository : IRepository<WashStation>
{
    Task<IEnumerable<WashStation>> GetAvailableStationsAsync(DateTime startDate);
    
}