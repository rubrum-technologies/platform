using HotChocolate;
using HotChocolate.Authorization;
using HotChocolate.Types;
using MediatR;
using Rubrum.Platform.DataSourceService.Database.Queries;
using Rubrum.Platform.DataSourceService.Database.Schema;

namespace Rubrum.Platform.DataSourceService.Database;

[QueryType]
public static class DatabaseSourceQueries
{
    [Authorize]
    public static async Task<DatabaseSchemaInformation> GetSchemaDatabaseAsync(
        DatabaseKind kind,
        string connectionString,
        [Service] IMediator mediator,
        CancellationToken ct = default)
    {
        return await mediator.Send(
            new GetSchemaDatabaseQuery
            {
                Kind = kind,
                ConnectionString = connectionString,
            },
            ct);
    }
}
