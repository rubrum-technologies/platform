using Volo.Abp;

namespace Rubrum.Platform.DataSourceService.Database;

public class DatabaseColumnNameAlreadyExistsException(string name) : BusinessException
{
    public string Name => name;
}
