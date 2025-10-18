using CarWash.Core.DTOs;

namespace CarWash.Application.IServiceInterfaces;

public interface ICarService : IService<CarDto, CreateCarDto>
{
    Task<CarDto> GetFirstBookedCarAsync(int userId);
    Task<CarDto> CreateCarAsync(CreateCarDto dto, int userId);
}