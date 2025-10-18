using AutoMapper;
using CarWash.Application.IRepositoryInterfaces;
using CarWash.Application.IServiceInterfaces;
using CarWash.Core.DTOs;
using CarWash.Core.Models;
using CarWash.Core.Models.Enums;

namespace CarWash.Application.Services;

public class BookingService : Service<BookingDto, Booking, CreateBookingDto>, IBookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IMapper _mapper;
    public BookingService(IBookingRepository bookingRepository, IMapper mapper) : base(bookingRepository,mapper)
    {
        _bookingRepository = bookingRepository;
        _mapper = mapper;
    }

    public async Task<bool> IsStationAvailableAsync(int stationId, DateTime dateTime)
    {
        return await _bookingRepository.IsStationAvailableAsync(stationId, dateTime);
    }

    public async Task<BookingDto> CreateBookingAsync(CreateBookingDto dto, int stationId)
    {
        if (dto == null) throw new ArgumentNullException(nameof(dto));
        
        bool avilable = await IsStationAvailableAsync(stationId, dto.StartTime);
        
        if (!avilable) throw new ArgumentException($"{stationId} is not available");
        
        var booking = _mapper.Map<Booking>(dto);
        
        var timeNow =  DateTime.Now;

        if (booking.StartTime > timeNow)
            booking.Status = BookingStatus.InQueue;
        else if (booking.StartTime <= timeNow && timeNow < booking.EndTime)
            booking.Status = BookingStatus.Washing;
        else
            booking.Status = BookingStatus.Complete;
        return _mapper.Map<BookingDto>(await _bookingRepository.CreateAsync(booking));
    }

}