using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.DependencyInjection;

namespace Rubrum.Platform.DataSourceService.Database.Schema;

public class DatabaseSchemaBuilderFactory(
    IServiceProvider serviceProvider) : IDatabaseSchemaBuilderFactory, ITransientDependency
{
    public Task<IDatabaseSchemaBuilder> GetAsync(DatabaseKind kind)
    {
        IDatabaseSchemaBuilder result = kind switch
        {
            DatabaseKind.MySql => serviceProvider.GetRequiredService<MySqlSchemaBuilder>(),
            DatabaseKind.PostgreSql => serviceProvider.GetRequiredService<PostgresqlSchemaBuilder>(),
            DatabaseKind.SqlServer => serviceProvider.GetRequiredService<SqlServerSchemaBuilder>(),
            _ => throw new ArgumentOutOfRangeException(nameof(kind), kind, null),
        };

        return Task.FromResult(result);
    }
}
