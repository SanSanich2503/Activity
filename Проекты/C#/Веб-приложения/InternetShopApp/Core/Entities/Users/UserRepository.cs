using Microsoft.EntityFrameworkCore;

namespace Core.Entities.Users;

public class UserRepository : BaseRepository<User>
{
    public UserRepository(DataContext context) : base(context) {}

    public new List<User> GetAll()
        => _context.Users.Include(x => x.Role).ToList();

    public User? GetCurrentUser(string guid)
        => _context.Users.Include(x => x.Role).FirstOrDefault(u => u.UserGuid == guid);
}