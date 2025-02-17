using TestUsersProject.Roles.Dto;
using System.Collections.Generic;

namespace TestUsersProject.Web.Models.Users;

public class UserListViewModel
{
    public IReadOnlyList<RoleDto> Roles { get; set; }
}
