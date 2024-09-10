using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rubrum.Platform.StoreAppsService;

public interface IAppAppService :
    ICrudAppService<AppDto, Guid, PagedAndSortedResultRequestDto, CreateAppInput, UpdateAppInput>;
