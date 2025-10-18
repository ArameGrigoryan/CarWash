using CarWash.Core.DTOs;

namespace CarWash.Application.IServiceInterfaces;

public interface IWashStationService : IService<WashStationDto, CreateWashStationDto>
{
    Task<IEnumerable<WashStationDto>> GetAvailableStationsAsync(DateTime startDate);
}