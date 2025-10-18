using CarWash.Core.Models;

namespace CarWash.Application.IRepositoryInterfaces;

public interface IServiceRepository : IRepository<Service>
{
    Task<int> GetPriceByNameAsync(string name);
}