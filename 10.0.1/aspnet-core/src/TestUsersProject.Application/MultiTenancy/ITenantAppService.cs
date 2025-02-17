using Abp.Application.Services;
using TestUsersProject.MultiTenancy.Dto;

namespace TestUsersProject.MultiTenancy;

public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
{
}

