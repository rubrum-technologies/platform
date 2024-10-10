using LinqToDB;

namespace Rubrum.Platform.DataSourceService.Database;

public interface IDatabaseSourceDataOptionsAccessor
{
    DataOptions Get(DatabaseSource databaseSource);
}
