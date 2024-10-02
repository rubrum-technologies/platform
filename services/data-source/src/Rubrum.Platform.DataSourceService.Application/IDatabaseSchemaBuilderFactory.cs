using Rubrum.Platform.DataSourceService.Database;
using Rubrum.Platform.DataSourceService.Database.Schema;

namespace Rubrum.Platform.DataSourceService;

public interface IDatabaseSchemaBuilderFactory
{
    Task<IDatabaseSchemaBuilder> GetAsync(DatabaseKind kind);
}
