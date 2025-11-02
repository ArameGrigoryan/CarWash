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
    private readonly ICacheService _cache;

    public UserService(IUserRepository repository, IMapper mapper, ICacheService cache)
        : base(repository, mapper)
    {
        _userRepository = repository;
        _mapper = mapper;
        _cache = cache;
    }

    public async Task<UserDto> GetAllInfoByEmailAsync(string email)
    {
        string cacheKey = $"user_email_{email}";
        var cachedUser = await _cache.GetAsync<UserDto>(cacheKey);
        if (cachedUser != null) return cachedUser;

        var user = await _userRepository.GetUserByEmile(email);
        if (user == null) throw new Exception("User not found");

        var userDto = _mapper.Map<User, UserDto>(user);

        await _cache.SetAsync(cacheKey, userDto, TimeSpan.FromMinutes(10));

        return userDto;
    }
}