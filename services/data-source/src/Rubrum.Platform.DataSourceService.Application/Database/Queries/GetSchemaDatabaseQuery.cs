using MediatR;
using Rubrum.Platform.DataSourceService.Database.Schema;

namespace Rubrum.Platform.DataSourceService.Database.Queries;

public class GetSchemaDatabaseQuery : IRequest<DatabaseSchemaInformation>
{
    public required DatabaseKind Kind { get; init; }

    public required string ConnectionString { get; init; }

    public class Handler(IDatabaseSchemaBuilderFactory schemaBuilderFactory)
        : IRequestHandler<GetSchemaDatabaseQuery, DatabaseSchemaInformation>
    {
        public async Task<DatabaseSchemaInformation> Handle(
            GetSchemaDatabaseQuery request,
            CancellationToken cancellationToken)
        {
            var builder = await schemaBuilderFactory.GetAsync(request.Kind);

            return await builder.BuildAsync(request.ConnectionString);
        }
    }
}
