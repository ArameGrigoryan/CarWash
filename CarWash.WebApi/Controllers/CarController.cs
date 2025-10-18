using CarWash.Application.IServiceInterfaces;
using CarWash.Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CarWash.WebApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CarController : ControllerBase
{
    private readonly ICarService _carService;

    public CarController(ICarService carService)
    {
        _carService = carService;
    }

    [HttpGet("by-userId/{userId}")]
    public async Task<ActionResult<CarDto>> GetFirstBookedCarAsync(int userId)
    {
        try
        {
            var car = await _carService.GetFirstBookedCarAsync(userId);
            return Ok(car);
        }
        catch (InvalidOperationException e)
        {
            return NotFound(e.Message);
        }
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<CarDto>>> Get()
    {
        var cars = await _carService.GetAllAsync();
        return Ok(cars);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CarDto>> GetById(int id)
    {
        var car = await _carService.GetByIdAsync(id);
        return Ok(car);
    }

    [HttpPost("userId{userId}")]
    public async Task<ActionResult<CarDto>> Post(int userId, [FromBody] CreateCarDto car)
    {
        var carDto = await _carService.CreateCarAsync(car, userId);
        return CreatedAtAction(nameof(GetById), new { id = carDto.Id }, carDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CarDto>> Put(int id, [FromBody] CreateCarDto car)
    {
        var update = await _carService.UpdateAsync(id, car);
        if (update == null)
        {
            return NotFound($"Car with id {id} not found.");
        }

        return Ok(update);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        bool flag = await _carService.DeleteAsync(id);
        if (!flag)
        {
            return NotFound($"Car with id {id} not found.");
        }

        return NoContent();
    }
}
