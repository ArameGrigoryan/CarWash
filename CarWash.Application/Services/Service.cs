using AutoMapper;
using CarWash.Application.IRepositoryInterfaces;
using CarWash.Application.IServiceInterfaces;

namespace CarWash.Application.Services;

public class Service<TDto, TEntity, TCreateDto> : IService<TDto, TCreateDto> where TDto : class where TEntity : class where TCreateDto : class 
{
    private readonly IRepository<TEntity> _repository;
    private readonly IMapper _mapper;
    public Service(IRepository<TEntity> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TDto>?> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        if (entities == null) { throw new ArgumentNullException(nameof(entities)); }
        return _mapper.Map<IEnumerable<TDto>>(entities);
    }

    public async Task<TDto> GetByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) { throw new ArgumentNullException(nameof(entity)); }
        return _mapper.Map<TDto>(entity);
    }

    public async Task<TDto> CreateAsync(TCreateDto dto)
    {
        var entity = _mapper.Map<TEntity>(dto);
        var created = await _repository.CreateAsync(entity);
        return _mapper.Map<TDto>(created);
    }


    public async Task<TDto?> UpdateAsync(int id, TCreateDto dto)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing == null) return default;

        _mapper.Map(dto, existing);
        var updated = await _repository.UpdateAsync(existing);
        return _mapper.Map<TDto>(updated);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return false;
        await _repository.DeleteAsync(id);
        return true;
    }
}