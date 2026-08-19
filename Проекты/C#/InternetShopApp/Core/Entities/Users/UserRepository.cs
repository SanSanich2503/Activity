namespace Core.Entities.Users;

public class UserRepository : BaseRepository<User>
{
    public UserRepository(DataContext context) : base(context) {}

    public User? GetCurrentUser(string guid)
        => _context.Users.FirstOrDefault(u => u.UserGuid == guid);
}