using Volo.Abp;

namespace Rubrum.Platform.DataSourceService.Database;

public class DatabaseTableNameAlreadyExistsException(string name) : BusinessException
{
    public string Name => name;
}
