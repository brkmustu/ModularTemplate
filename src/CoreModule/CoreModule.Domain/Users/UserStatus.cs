using System.ComponentModel.DataAnnotations;

namespace CoreModule.Domain.Users;

public class UserStatus : ActivetableEntity, IEntity<int>
{
    [Key]
    public int Id { get; set; }
    public string Value { get; private set; }
    public UserStatus(string value)
    {
        Value = value;
    }
}

public enum UserStatusType
{
    Active = 1,
    WaitingForApproval = 2,
    Inactive = 3,
    Deleted = 4,
}
