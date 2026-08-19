using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Entities.Roles;

namespace Core.Entities.Users;

public class User : Entity
{
    public string? UserGuid { get; set; }
    public string? Surname { get; set; }
    public string? Name { get; set; }
    public string? Patronymic { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Password { get; set; }
    
    [DataType("ForeignKey")]
    [ForeignKey("Role")]
    public int RoleId { get; set; }

    [DataType("Reference")]
    public virtual Role Role { get; set; }
}