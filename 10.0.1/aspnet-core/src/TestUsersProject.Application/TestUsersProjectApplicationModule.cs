using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using TestUsersProject.Authorization;

namespace TestUsersProject;

[DependsOn(
    typeof(TestUsersProjectCoreModule),
    typeof(AbpAutoMapperModule))]
public class TestUsersProjectApplicationModule : AbpModule
{
    public override void PreInitialize()
    {
        Configuration.Authorization.Providers.Add<TestUsersProjectAuthorizationProvider>();
    }

    public override void Initialize()
    {
        var thisAssembly = typeof(TestUsersProjectApplicationModule).GetAssembly();

        IocManager.RegisterAssemblyByConvention(thisAssembly);

        Configuration.Modules.AbpAutoMapper().Configurators.Add(
            // Scan the assembly for classes which inherit from AutoMapper.Profile
            cfg => cfg.AddMaps(thisAssembly)
        );
    }
}
