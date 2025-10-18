using CarWash.Application.IServiceInterfaces;
using CarWash.Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CarWash.WebApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class BookingController : ControllerBase
{
    private readonly IBookingService _service;

    public BookingController(IBookingService service)
    {
        _service = service;
    }

    // [HttpGet("by-stationId/{stationId}")]
    // public async Task<ActionResult<bool>> GetAvailableByStationId(int stationId, DateTime dateTime)
    // {
    //     bool isAvailable = await _service.IsStationAvailableAsync(stationId, dateTime);
    //     return Ok(isAvailable);
    // }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookingDto>>> Get()
    {
        var bookings = await _service.GetAllAsync();
        return Ok(bookings);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BookingDto>> GetById(int id)
    {
        var booking = await _service.GetByIdAsync(id);
        return Ok(booking);
    }

    [HttpPost("Create-By-StationId/{stationId}")]
    public async Task<ActionResult<CreateBookingDto>> CreateBookingDto([FromBody] CreateBookingDto booking, int stationId)
    {
        var createbooking = await _service.CreateBookingAsync(booking, stationId);
        return CreatedAtAction(nameof(GetById), new { id = createbooking.Id }, createbooking);
    }
    
    [HttpPost]
    public async Task<ActionResult<BookingDto>> Post([FromBody] CreateBookingDto dto)
    {
        var booking = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = booking.Id }, booking);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<BookingDto>> Put(int id, [FromBody] CreateBookingDto dto)
    {
        var booking = await _service.UpdateAsync(id, dto);
        return Ok(booking);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        bool flag = await _service.DeleteAsync(id);
        return flag ? NoContent() : NotFound();
    }
}

