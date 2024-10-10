using Volo.Abp;

namespace Rubrum.Platform.DataSourceService.Database;

public class DatabaseTableNameAlreadyExistsException(string tableName) : BusinessException
{
    public string TableName => tableName;
}
