using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace TestUsersProject.Controllers
{
    public abstract class TestUsersProjectControllerBase : AbpController
    {
        protected TestUsersProjectControllerBase()
        {
            LocalizationSourceName = TestUsersProjectConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
