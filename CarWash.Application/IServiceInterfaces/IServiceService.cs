using CarWash.Core.DTOs;
namespace CarWash.Application.IServiceInterfaces;

public interface IServiceService : IService<ServiceDto, CreateServiceDto>
{
    Task<int> GetTotalPriceByNameAsync(string names);
}