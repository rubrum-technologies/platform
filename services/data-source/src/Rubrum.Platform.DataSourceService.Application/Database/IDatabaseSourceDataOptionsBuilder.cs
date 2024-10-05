using LinqToDB;
using LinqToDB.Data;

namespace Rubrum.Platform.DataSourceService.Database;

public interface IDatabaseSourceDataOptionsBuilder
{
    Task<DataOptions> BuildAsync(DatabaseSource source);
}
