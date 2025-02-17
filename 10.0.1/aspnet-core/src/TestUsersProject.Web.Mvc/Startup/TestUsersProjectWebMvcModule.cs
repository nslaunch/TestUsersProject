using Abp.Modules;
using Abp.Reflection.Extensions;
using TestUsersProject.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using TestUsersProject.Web.Helpers;

namespace TestUsersProject.Web.Startup;

[DependsOn(typeof(TestUsersProjectWebCoreModule))]
public class TestUsersProjectWebMvcModule : AbpModule
{
    private readonly IWebHostEnvironment _env;
    private readonly IConfigurationRoot _appConfiguration;

    public TestUsersProjectWebMvcModule(IWebHostEnvironment env)
    {
        _env = env;
        _appConfiguration = env.GetAppConfiguration();
    }

    public override void PreInitialize()
    {
        Configuration.Navigation.Providers.Add<TestUsersProjectNavigationProvider>();
    }

    public override void Initialize()
    {
        IocManager.RegisterAssemblyByConvention(typeof(TestUsersProjectWebMvcModule).GetAssembly());
    }
}
