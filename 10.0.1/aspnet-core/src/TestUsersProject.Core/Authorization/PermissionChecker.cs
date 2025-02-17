using Abp.Authorization;
using TestUsersProject.Authorization.Roles;
using TestUsersProject.Authorization.Users;

namespace TestUsersProject.Authorization;

public class PermissionChecker : PermissionChecker<Role, User>
{
    public PermissionChecker(UserManager userManager)
        : base(userManager)
    {
    }
}
