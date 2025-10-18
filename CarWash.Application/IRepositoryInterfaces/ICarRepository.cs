using CarWash.Core.Models;

namespace CarWash.Application.IRepositoryInterfaces;

public interface ICarRepository : IRepository<Car>
{
    Task<IEnumerable<Car>> GetUserByIdAsync(int userId);
}