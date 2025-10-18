using AutoMapper;
using CarWash.Application.IRepositoryInterfaces;
using CarWash.Application.IServiceInterfaces;
using CarWash.Core.DTOs;
using CarWash.Core.Models;

namespace CarWash.Application.Services;

public class UserService : Service<UserDto, User, CreateUserDto>, IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    public UserService(IUserRepository repository, IMapper mapper) : base(repository, mapper)
    {
        _mapper = mapper;
        _userRepository = repository;
    }

    public async Task<UserDto> GetAllInfoByEmailAsync(string emile)
    {
        var user = await _userRepository.GetUserByEmile(emile);
        
        if (user == null) throw new Exception("User not found");
        
        return _mapper.Map<User, UserDto>(user);
    }
}