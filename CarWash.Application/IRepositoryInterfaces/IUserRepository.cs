using CarWash.Core.Models;

namespace CarWash.Application.IRepositoryInterfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetUserByEmile(string emile); 
}