using Abp.AspNetCore.Mvc.ViewComponents;

namespace TestUsersProject.Web.Views;

public abstract class TestUsersProjectViewComponent : AbpViewComponent
{
    protected TestUsersProjectViewComponent()
    {
        LocalizationSourceName = TestUsersProjectConsts.LocalizationSourceName;
    }
}
