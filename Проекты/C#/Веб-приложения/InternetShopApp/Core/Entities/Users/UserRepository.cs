using Microsoft.EntityFrameworkCore;

namespace Core.Entities.Users;

public class UserRepository : BaseRepository<User>
{
    public UserRepository(DataContext context) : base(context) {}

    public new IQueryable<User> GetAll()
        => _context.Users.Include(u => u.Role).AsNoTracking();

    public User? GetCurrentUser(string guid)
        => GetAll().FirstOrDefault(u => u.UserGuid == guid);
    
    public User? GetByEmail(string email)
        => GetAll().AsEnumerable().FirstOrDefault(u => u.Email?.ToLower() == email.ToLower());
    
    public User? GetByEmailAndPassword(string email, string password)
        => GetAll().AsEnumerable().FirstOrDefault(u => u.Email?.ToLower() == email.ToLower() && u.Password == password);
}