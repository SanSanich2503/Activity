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
    
    public T? GetById(int id) => _context.Set<T>().Find(id);

    public void Add(T entity)
    {
        _context.Set<T>().Add(entity);
        _context.SaveChanges();
    }

    public void AddRange(IEnumerable<T> entities)
    {
        _context.Set<T>().AddRange(entities);
        _context.SaveChanges();
    }

    public void Update(T entity)
    {
        _context.Set<T>().Update(entity);
        _context.SaveChanges();
    }
    
    public void UpdateRange(IEnumerable<T> entities)
    {
        _context.Set<T>().UpdateRange(entities);
        _context.SaveChanges();
    }

    public void Remove(T entity)
    {
        _context.Set<T>().Remove(entity);
        _context.SaveChanges();
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
        _context.Set<T>().RemoveRange(entities);
        _context.SaveChanges();
    }
}