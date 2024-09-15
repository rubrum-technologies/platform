using Volo.Abp.Application.Services;

namespace Rubrum.Platform.StoreAppsService.Apps;

public class AppApplicationService(
    IAppRepository appRepository,
    AppManager appManager) : ApplicationService
{
}
