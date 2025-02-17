using Abp.Application.Services;
using TestUsersProject.Sessions.Dto;
using System.Threading.Tasks;

namespace TestUsersProject.Sessions;

public interface ISessionAppService : IApplicationService
{
    Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
}
