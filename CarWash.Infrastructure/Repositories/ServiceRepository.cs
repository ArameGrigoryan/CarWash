using CarWash.Application.IRepositoryInterfaces;
using CarWash.Core.Models;
using CarWash.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CarWash.Infrastructure.Repositories;

public class ServiceRepository : Repository<Service>, IServiceRepository
{
    private readonly CarWashContext _context;
    
    public ServiceRepository(CarWashContext context) : base(context)
    {
        _context = context;
    }

    public async Task<int> GetPriceByNameAsync(string name)
    {
        return await _context.Services
            .Where(s => s.Name == name)
            .Select(s => s.Price).FirstOrDefaultAsync();
    }
}