using Volo.Abp;

namespace Rubrum.Platform.DataSourceService;

public class DataSourceNameAlreadyExistsException(string name) : BusinessException
{
    public string Name => name;
}
