using CarWash.Application.IRepositoryInterfaces;
using CarWash.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CarWash.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly CarWashContext _baseContxt;
    protected readonly DbSet<T> _dbSet;
    
    public Repository(CarWashContext context)
    {
        _baseContxt = context;
        _dbSet = context.Set<T>();
    }

    public async Task<IEnumerable<T>?> GetAllAsync()
    {
        var entity =  await _dbSet.ToListAsync();
        return entity;
    }

    public async Task<T> GetByIdAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity == null) { throw new KeyNotFoundException($"Entity with id {id} was not found."); }
        return entity;
    }

    public async Task<T> CreateAsync(T entity)
    {
        if (entity == null) { throw new ArgumentNullException(nameof(entity)); }
        await _dbSet.AddAsync(entity);
        await _baseContxt.SaveChangesAsync();
        return entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        if (entity == null) { throw new ArgumentNullException(nameof(entity)); }
        _dbSet.Update(entity);
        await _baseContxt.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity == null) throw new KeyNotFoundException($"Entity with id {id} not found");
        _dbSet.Remove(entity);
        await _baseContxt.SaveChangesAsync();
    }
    public async Task SaveChangesAsync() =>
        await _baseContxt.SaveChangesAsync();
}