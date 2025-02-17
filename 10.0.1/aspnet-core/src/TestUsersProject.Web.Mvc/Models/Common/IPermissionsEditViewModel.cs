using TestUsersProject.Roles.Dto;
using System.Collections.Generic;

namespace TestUsersProject.Web.Models.Common;

public interface IPermissionsEditViewModel
{
    List<FlatPermissionDto> Permissions { get; set; }
}