using LinqToDB;
using LinqToDB.Data;

namespace Rubrum.Platform.DataSourceService.Database;

public interface IDatabaseSourceDataOptionsBuilder
{
    DataOptions Build(DatabaseSource source);
}
