using Volo.Abp;

namespace Rubrum.Platform.WindowsService.Windows;

public class WindowNameAlreadyExistsException(string name)
    : BusinessException("Window:Error:NameAlreadyExists")
{
    public string Name => name;
}
