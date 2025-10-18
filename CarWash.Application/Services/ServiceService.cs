using AutoMapper;
using CarWash.Application.IRepositoryInterfaces;
using CarWash.Application.IServiceInterfaces;
using CarWash.Core.DTOs;
using CarWash.Core.Models;

namespace CarWash.Application.Services;

public class ServiceService : Service<ServiceDto, Service, CreateServiceDto>, IServiceService
{
    private readonly IMapper _mapper;
    private readonly IServiceRepository _serviceRepository;
    public ServiceService(IServiceRepository repository, IMapper mapper) : base(repository, mapper)
    {
        _mapper = mapper;
        _serviceRepository = repository;
    }

    public async Task<int> GetTotalPriceByNameAsync(string names)
    {
        var serviceNames = names
            .Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(n => n.Trim())
            .ToList();

        if (serviceNames.Count == 0) return 0;

        int totalPrice = 0;
        foreach (var name in serviceNames)
        {
            try
            {
                int price = await _serviceRepository.GetPriceByNameAsync(name);
                totalPrice += price;
            }
            catch(InvalidOperationException)
            {
                continue;
            }
        }

        if (serviceNames.Count > 1)
        {
            return (int)(totalPrice * 0.9);
        }
        return totalPrice;
    }
}