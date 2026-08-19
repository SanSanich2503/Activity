namespace Core.Entities.Roles;

public class RoleRepository : BaseRepository<Role>
{
    public RoleRepository(DataContext context) : base(context) {}
}