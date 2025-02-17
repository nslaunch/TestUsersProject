using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace TestUsersProject.Web.Views;

public abstract class TestUsersProjectRazorPage<TModel> : AbpRazorPage<TModel>
{
    [RazorInject]
    public IAbpSession AbpSession { get; set; }

    protected TestUsersProjectRazorPage()
    {
        LocalizationSourceName = TestUsersProjectConsts.LocalizationSourceName;
    }
}
