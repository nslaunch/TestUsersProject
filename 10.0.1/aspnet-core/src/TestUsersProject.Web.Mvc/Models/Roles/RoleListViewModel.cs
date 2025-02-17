using TestUsersProject.Roles.Dto;
using System.Collections.Generic;

namespace TestUsersProject.Web.Models.Roles;

public class RoleListViewModel
{
    public IReadOnlyList<PermissionDto> Permissions { get; set; }
}
