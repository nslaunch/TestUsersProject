using Abp.AutoMapper;
using Abp.Modules;
using TestUsersProject.Web.Helpers;

namespace TestUsersProject.Web.Startup
{
    [DependsOn(typeof(AbpAutoMapperModule))]
    public class AutoMapperModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.AbpAutoMapper().Configurators.Add(config =>
            {
                config.AddProfile(new EmployeeMappingProfile());
            });
        }
    }
}
