using Volo.Abp;

namespace Rubrum.Platform.DataSourceService.Database;

public class DatabaseColumnNameAlreadyExistsException(string columnName) : BusinessException
{
    public string ColumnName => columnName;
}
