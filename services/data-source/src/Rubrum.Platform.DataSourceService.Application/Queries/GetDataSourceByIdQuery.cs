using MediatR;
using Microsoft.AspNetCore.Authorization;
using Rubrum.Authorization.Relations;

namespace Rubrum.Platform.DataSourceService.Queries;

public class GetDataSourceByIdQuery : IRequest<DataSource?>
{
    public required Guid Id { get; init; }

    public class Handler(
        IAuthorizationService authorization,
        IDataSourceByIdDataLoader dataLoader) : IRequestHandler<GetDataSourceByIdQuery, DataSource?>
    {
        public async Task<DataSource?> Handle(GetDataSourceByIdQuery request, CancellationToken cancellationToken)
        {
            await authorization.CheckAsync<DataSource>(DataSourceDefinition.View, request.Id);

            return await dataLoader.LoadAsync(request.Id, cancellationToken);
        }
    }
}
