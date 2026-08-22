using Microsoft.EntityFrameworkCore;
using Scrutor.AspNetCore;

namespace Core.Entities;

public class BaseRepository<T> : ISelfTransientLifetime where T : Entity
{
    protected readonly DataContext _context;
    
    protected BaseRepository(DataContext context)
    {
        _context = context;
    }
    
    public IQueryable<T> GetAll() => _context.Set<T>().AsNoTracking().OrderBy(x => x.Title).AsNoTracking();
    
    public async Task<T?> GetById(int id) => await _context.Set<T>().FindAsync(id);

    public async Task Add(T entity)
    {
        _context.Set<T>().Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task AddRange(IEnumerable<T> entities)
    {
        _context.Set<T>().AddRange(entities);
        await _context.SaveChangesAsync();
    }

    public async Task Update(T entity)
    {
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();
    }
    
    public async Task UpdateRange(IEnumerable<T> entities)
    {
        _context.Set<T>().UpdateRange(entities);
        await _context.SaveChangesAsync();
    }

    public async Task Remove(T entity)
    {
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveRange(IEnumerable<T> entities)
    {
        _context.Set<T>().RemoveRange(entities);
        await _context.SaveChangesAsync();
    }
}