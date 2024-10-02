using Volo.Abp;

namespace Rubrum.Platform.DataSourceService.Database;

public class DatabaseColumnSystemNameAlreadyExistsException(string systemName) : BusinessException
{
    public string SystemName => systemName;
}
