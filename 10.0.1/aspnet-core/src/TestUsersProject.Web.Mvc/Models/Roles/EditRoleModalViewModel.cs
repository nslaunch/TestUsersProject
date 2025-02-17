using Abp.AutoMapper;
using TestUsersProject.Roles.Dto;
using TestUsersProject.Web.Models.Common;

namespace TestUsersProject.Web.Models.Roles;

[AutoMapFrom(typeof(GetRoleForEditOutput))]
public class EditRoleModalViewModel : GetRoleForEditOutput, IPermissionsEditViewModel
{
    public bool HasPermission(FlatPermissionDto permission)
    {
        return GrantedPermissionNames.Contains(permission.Name);
    }
}
