using TestUsersProject.Configuration.Dto;
using System.Threading.Tasks;

namespace TestUsersProject.Configuration;

public interface IConfigurationAppService
{
    Task ChangeUiTheme(ChangeUiThemeInput input);
}
