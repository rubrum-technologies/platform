using Volo.Abp;

namespace Rubrum.Platform.DataSourceService.Database;

public class DatabaseTableSystemNameAlreadyExistsException(string tableSystemName) : BusinessException
{
    public string TableSystemName => tableSystemName;
}
