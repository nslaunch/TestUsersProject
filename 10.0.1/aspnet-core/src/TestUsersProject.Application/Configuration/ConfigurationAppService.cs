using Abp.Authorization;
using Abp.Runtime.Session;
using TestUsersProject.Configuration.Dto;
using System.Threading.Tasks;

namespace TestUsersProject.Configuration;

[AbpAuthorize]
public class ConfigurationAppService : TestUsersProjectAppServiceBase, IConfigurationAppService
{
    public async Task ChangeUiTheme(ChangeUiThemeInput input)
    {
        await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
    }
}
