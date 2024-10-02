using Volo.Abp;

namespace Rubrum.Platform.DataSourceService.Database;

public class DatabaseTableSystemNameAlreadyExistsException(string systemName) : BusinessException
{
    public string SystemName => systemName;
}
