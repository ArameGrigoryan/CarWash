namespace CarWash.Application.IServiceInterfaces;

public interface IService<TDto, TCreateDto>
{
    Task<IEnumerable<TDto>?> GetAllAsync();
    Task<TDto> GetByIdAsync(int id);
    Task<TDto> CreateAsync(TCreateDto entity);
    Task<TDto?> UpdateAsync(int id, TCreateDto entity);
    Task<bool> DeleteAsync(int entityId);
}