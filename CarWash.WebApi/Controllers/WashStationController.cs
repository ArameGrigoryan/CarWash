using CarWash.Application.IServiceInterfaces;
using CarWash.Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CarWash.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WashStationController : ControllerBase
{
    private readonly IWashStationService _washStationService;

    public WashStationController(IWashStationService washStationService)
    {
        _washStationService = washStationService;
    }

    [HttpGet("available/{dateTime}")]
    public async Task<ActionResult<IEnumerable<WashStationDto>>> GetStationsByDateTime(DateTime dateTime)
    {
        var dtos = await _washStationService.GetAvailableStationsAsync(dateTime);
        return Ok(dtos);
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<WashStationDto>>> Get()
    {
        var washStation = await _washStationService.GetAllAsync();
        return Ok(washStation);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<WashStationDto>> GetById(int id)
    {
        var washStation = await _washStationService.GetByIdAsync(id);
        if (washStation == null) return NotFound();
        return Ok(washStation);
    }
    
    [HttpPost]
    public async Task<ActionResult<WashStationDto>> Post([FromBody] CreateWashStationDto dto)
    {
        var washStation = await _washStationService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = washStation.Id }, washStation);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<WashStationDto>> Put(int id, [FromBody] CreateWashStationDto dto)
    {
        var washStation = await _washStationService.UpdateAsync(id, dto);
        if (washStation == null) return NotFound($"Wash station with id {id} not found.");
        return Ok(washStation);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        bool flag = await _washStationService.DeleteAsync(id);
        return flag ? NoContent() : NotFound();
    }
}