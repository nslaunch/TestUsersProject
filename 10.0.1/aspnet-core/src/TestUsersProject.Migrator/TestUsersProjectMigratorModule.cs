using Abp.Events.Bus;
using Abp.Modules;
using Abp.Reflection.Extensions;
using TestUsersProject.Configuration;
using TestUsersProject.EntityFrameworkCore;
using TestUsersProject.Migrator.DependencyInjection;
using Castle.MicroKernel.Registration;
using Microsoft.Extensions.Configuration;

namespace TestUsersProject.Migrator;

[DependsOn(typeof(TestUsersProjectEntityFrameworkModule))]
public class TestUsersProjectMigratorModule : AbpModule
{
    private readonly IConfigurationRoot _appConfiguration;

    public TestUsersProjectMigratorModule(TestUsersProjectEntityFrameworkModule abpProjectNameEntityFrameworkModule)
    {
        abpProjectNameEntityFrameworkModule.SkipDbSeed = true;

        _appConfiguration = AppConfigurations.Get(
            typeof(TestUsersProjectMigratorModule).GetAssembly().GetDirectoryPathOrNull()
        );
    }

    public override void PreInitialize()
    {
        Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
            TestUsersProjectConsts.ConnectionStringName
        );

        Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        Configuration.ReplaceService(
            typeof(IEventBus),
            () => IocManager.IocContainer.Register(
                Component.For<IEventBus>().Instance(NullEventBus.Instance)
            )
        );
    }

    public override void Initialize()
    {
        IocManager.RegisterAssemblyByConvention(typeof(TestUsersProjectMigratorModule).GetAssembly());
        ServiceCollectionRegistrar.Register(IocManager);
    }
}
