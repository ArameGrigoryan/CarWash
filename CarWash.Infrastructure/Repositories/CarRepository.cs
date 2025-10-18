using CarWash.Application.IRepositoryInterfaces;
using CarWash.Core.Models;
using CarWash.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CarWash.Infrastructure.Repositories;

public class CarRepository : Repository<Car>, ICarRepository
{
    private readonly CarWashContext _context;
    
    public CarRepository(CarWashContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Car>> GetUserByIdAsync(int userId)
    {
        return await _dbSet
            .Where(c => c.UserId == userId)
            .ToListAsync();
    }
}