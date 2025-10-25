using System.Net.Http.Headers;
using System.Net.Http.Json;
using CarWash.Application.IServiceInterfaces;
using CarWash.Core.DTOs;
using Microsoft.AspNetCore.Http;

namespace CarWash.Application.Services;

public class UserServiceClient : IUserServiceClient
{
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _contextAccessor;

    public UserServiceClient(IHttpClientFactory httpClientFactory, IHttpContextAccessor accessor)
    {
        _httpClient = httpClientFactory.CreateClient("UserService");
        _contextAccessor = accessor;
    }

    private void AddAuthHeader()
    {
        var token = _contextAccessor.HttpContext?.Request.Headers["Authorization"].ToString();
        if (!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(token);
        }
    }

    public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto dto)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/Auth/login", dto);
        if (!response.IsSuccessStatusCode) return null;
        return await response.Content.ReadFromJsonAsync<LoginResponseDto>();
    }

    public async Task<LoginResponseDto?> RegisterAsync(LoginRequestDto dto)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/Auth/register", dto);
        if (!response.IsSuccessStatusCode) return null;
        return await response.Content.ReadFromJsonAsync<LoginResponseDto>();
    }

    public async Task<List<UserDto>> GetAllUsersAsync()
    {
        AddAuthHeader();
        var result = await _httpClient.GetFromJsonAsync<List<UserDto>>("/api/Auth/all-users");
        return result ?? new List<UserDto>();
    }
}