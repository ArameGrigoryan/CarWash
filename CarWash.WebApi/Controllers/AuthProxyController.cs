using CarWash.Application.IServiceInterfaces;
using CarWash.Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CarWash.WebApi.Controllers
{
    [ApiController]
    [Route("api/proxy-auth")]
    public class AuthProxyController : ControllerBase
    {
        private readonly IUserServiceClient _userClient;

        public AuthProxyController(IUserServiceClient userClient)
        {
            _userClient = userClient;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto dto)
        {
            var result = await _userClient.LoginAsync(dto);
            if (result == null) return Unauthorized();

            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(LoginRequestDto dto)
        {
            var result = await _userClient.RegisterAsync(dto);
            if (result == null) return BadRequest("User exists");

            return Ok(result);
        }

        [HttpGet("all-users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _userClient.GetAllUsersAsync();
            return Ok(result);
        }
    }
}