using CarWash.Core.DTOs;

namespace CarWash.Application.IServiceInterfaces;

public interface IUserService : IService<UserDto, CreateUserDto>
{
    Task<UserDto>  GetAllInfoByEmailAsync(string emile); 
}