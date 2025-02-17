using Abp.AspNetCore.Mvc.Authorization;
using TestUsersProject.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace TestUsersProject.Web.Controllers;

[AbpMvcAuthorize]
public class AboutController : TestUsersProjectControllerBase
{
    public ActionResult Index()
    {
        return View();
    }
}
