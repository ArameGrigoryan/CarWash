using AutoMapper;
using CarWash.Application.IRepositoryInterfaces;
using CarWash.Application.IServiceInterfaces;
using CarWash.Core.DTOs;
using CarWash.Core.Models;

namespace CarWash.Application.Services;

public class CarService : Service<CarDto, Car, CreateCarDto>, ICarService
{
    private readonly ICarRepository _carRepository;
    private readonly IMapper _mapper;
    public CarService(ICarRepository repository, IMapper mapper) : base(repository, mapper)
    {
        _carRepository = repository;
        _mapper = mapper;
    }

    public async Task<CarDto> GetFirstBookedCarAsync(int userId)
    {
        var cars = await _carRepository.GetUserByIdAsync(userId);
        var bookedCars = cars
            .Where(c => c.Bookings != null && c.Bookings.Any())
            .ToList();
        if (!bookedCars.Any())
            throw new InvalidOperationException("User not commit booking։");
        var firstCar = bookedCars
            .OrderBy(c => c.Bookings.Min(b => b.StartTime))
            .FirstOrDefault();
        return _mapper.Map<CarDto>(firstCar);
    }
    
    public async Task<CarDto> CreateCarAsync(CreateCarDto dto, int userId)
    {
        var entity = _mapper.Map<Car>(dto);
        entity.UserId = userId;

        await _carRepository.CreateAsync(entity);
        await _carRepository.SaveChangesAsync();

        return _mapper.Map<CarDto>(entity);
    }
}