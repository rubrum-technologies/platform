using LinqToDB;

namespace Rubrum.Platform.DataSourceService.Database;

public interface IDatabaseSourceDataOptionsBuilder
{
    DataOptions Build(DatabaseSource source);
}
