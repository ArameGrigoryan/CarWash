using CarWash.Application.IServiceInterfaces;
using CarWash.Core.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarWash.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> Get()
    {
        var users = await _userService.GetAllAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetById(int id)
    {
        var user = await _userService.GetByIdAsync(id);
        return Ok(user);
    }
    [HttpGet("by-email/{email}")]
    public async Task<ActionResult<UserDto>> GetByEmail(string email)
    {
        return await _userService.GetAllInfoByEmailAsync(email);
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> Post([FromBody] CreateUserDto user)
    {
        var entity = await _userService.CreateAsync(user);
        return CreatedAtAction(nameof(GetById), new {id = entity.Id}, entity);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UserDto>> Put(int id, [FromBody] CreateUserDto user)
    {
        var update =  await _userService.UpdateAsync(id, user);
        if (update == null) return NotFound();
        return Ok(update);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        bool flag = await _userService.DeleteAsync(id);
        if (!flag) return NotFound($"User with id {id} not found.");
        return NoContent();
    }

}