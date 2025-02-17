using Abp.Modules;
using Abp.Reflection.Extensions;
using TestUsersProject.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace TestUsersProject.Web.Host.Startup
{
    [DependsOn(
       typeof(TestUsersProjectWebCoreModule))]
    public class TestUsersProjectWebHostModule : AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public TestUsersProjectWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(TestUsersProjectWebHostModule).GetAssembly());
        }
    }
}
