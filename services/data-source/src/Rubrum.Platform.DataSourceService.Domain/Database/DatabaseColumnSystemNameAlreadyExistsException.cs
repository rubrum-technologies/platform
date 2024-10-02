using Volo.Abp;

namespace Rubrum.Platform.DataSourceService.Database;

public class DatabaseColumnSystemNameAlreadyExistsException(string columnSystemName) : BusinessException
{
    public string ColumnSystemName => columnSystemName;
}
