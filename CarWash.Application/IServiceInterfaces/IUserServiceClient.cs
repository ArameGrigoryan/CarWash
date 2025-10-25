using CarWash.Core.DTOs;

namespace CarWash.Application.IServiceInterfaces;

public interface IUserServiceClient
{
    Task<List<UserDto>> GetAllUsersAsync();
    Task<LoginResponseDto?> LoginAsync(LoginRequestDto dto);
    Task<LoginResponseDto?> RegisterAsync(LoginRequestDto dto);
}