using Core;
using Scrutor.AspNetCore;

namespace Services.Services;

public class BaseService : ISelfTransientLifetime
{
    private readonly DataContext _context;
    
    public BaseService(DataContext context)
    {
        _context = context;
    }
}