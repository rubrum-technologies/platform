using HotChocolate;
using HotChocolate.Authorization;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using MediatR;
using Rubrum.Platform.DataSourceService.Queries;

namespace Rubrum.Platform.DataSourceService;

[QueryType]
public static class DataSourceQueries
{
    [Authorize]
    public static async Task<DataSource?> GetDataSourceByIdAsync(
        [ID<DataSource>] Guid id,
        [Service] IMediator mediator,
        CancellationToken ct = default)
    {
        return await mediator.Send(
            new GetDataSourceByIdQuery
            {
                Id = id,
            },
            ct);
    }
}
