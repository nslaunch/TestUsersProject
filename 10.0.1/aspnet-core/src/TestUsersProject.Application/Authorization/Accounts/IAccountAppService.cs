using Abp.Application.Services;
using TestUsersProject.Authorization.Accounts.Dto;
using System.Threading.Tasks;

namespace TestUsersProject.Authorization.Accounts;

public interface IAccountAppService : IApplicationService
{
    Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

    Task<RegisterOutput> Register(RegisterInput input);
}
