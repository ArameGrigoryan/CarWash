using CarWash.Application.IRepositoryInterfaces;
using CarWash.Core.Models;
using CarWash.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CarWash.Infrastructure.Repositories;

public class UserRepository : Repository<User>,  IUserRepository
{
    private readonly CarWashContext _context;
    public UserRepository(CarWashContext context) : base(context)
    {
        _context = context;
    }

    public async Task<User?> GetUserByEmile(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }
}