using CarWash.Application.IServiceInterfaces;
using CarWash.Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CarWash.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServiceController : ControllerBase
{
    private readonly IServiceService _service;

    public ServiceController(IServiceService service)
    {
        _service = service;
    }

    [HttpGet("price/{name}")]
    public async Task<ActionResult<int>> GetTotalPriceByNameAsync(string name)
    {
        var price = await _service.GetTotalPriceByNameAsync(name);
        return Ok(price);
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ServiceDto>>> Get()
    {
        var services = await _service.GetAllAsync();
        return Ok(services);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceDto>> GetById(int id)
    {
        var service = await _service.GetByIdAsync(id);
        return Ok(service);
    }

    [HttpPost]
    public async Task<ActionResult<ServiceDto>> Post([FromBody] CreateServiceDto service)
    {
        var entity = await _service.CreateAsync(service);
        return CreatedAtAction(nameof(GetById), new {id = entity.Id}, entity);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ServiceDto>> Put(int id, [FromBody] CreateServiceDto service)
    {
        var update =  await _service.UpdateAsync(id, service);
        if (update == null) return NotFound();
        return Ok(update);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        bool flag = await _service.DeleteAsync(id);
        if (!flag) return NotFound($"Service with id {id} not found.");
        return NoContent();
    }
    
}