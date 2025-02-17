using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using TestUsersProject.EntityFrameworkCore;
using TestUsersProject.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace TestUsersProject.Web.Tests;

[DependsOn(
    typeof(TestUsersProjectWebMvcModule),
    typeof(AbpAspNetCoreTestBaseModule)
)]
public class TestUsersProjectWebTestModule : AbpModule
{
    public TestUsersProjectWebTestModule(TestUsersProjectEntityFrameworkModule abpProjectNameEntityFrameworkModule)
    {
        abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
    }

    public override void PreInitialize()
    {
        Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
    }

    public override void Initialize()
    {
        IocManager.RegisterAssemblyByConvention(typeof(TestUsersProjectWebTestModule).GetAssembly());
    }

    public override void PostInitialize()
    {
        IocManager.Resolve<ApplicationPartManager>()
            .AddApplicationPartsIfNotAddedBefore(typeof(TestUsersProjectWebMvcModule).Assembly);
    }
}