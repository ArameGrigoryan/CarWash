using AutoMapper;
using CarWash.Application.IRepositoryInterfaces;
using CarWash.Application.IServiceInterfaces;
using CarWash.Core.DTOs;
using CarWash.Core.Models;

namespace CarWash.Application.Services;

public class WashStationService : Service<WashStationDto, WashStation, CreateWashStationDto>, IWashStationService
{
    private readonly IWashStationRepository _repository;
    private  readonly IMapper _mapper;
    public WashStationService(IWashStationRepository repository, IMapper mapper) : base(repository, mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<WashStationDto>> GetAvailableStationsAsync(DateTime startDate)
    {
        return _mapper.Map<IEnumerable<WashStationDto>>(await _repository.GetAvailableStationsAsync(startDate));
    }
}