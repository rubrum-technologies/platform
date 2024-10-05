namespace Rubrum.Platform.DataSourceService.Database.Schema;

public interface IDatabaseSchemaBuilderFactory
{
    Task<IDatabaseSchemaBuilder> GetAsync(DatabaseKind kind);
}
