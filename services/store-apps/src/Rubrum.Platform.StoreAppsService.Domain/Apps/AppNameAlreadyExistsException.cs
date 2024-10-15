using Volo.Abp;

namespace Rubrum.Platform.StoreAppsService.Apps;

public class AppNameAlreadyExistsException(string name)
    : BusinessException("App:Error:NameAlreadyExists")
{
    public string Name => name;
}
