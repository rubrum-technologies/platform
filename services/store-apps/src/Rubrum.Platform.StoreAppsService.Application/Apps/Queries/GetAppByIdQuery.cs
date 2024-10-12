using HotChocolate.Types.Relay;
using MediatR;

namespace Rubrum.Platform.StoreAppsService.Apps.Queries;

public class GetAppByIdQuery : IRequest<App?>
{
    public required Guid Id { get; init; }

    public class Handler(IAppByIdDataLoader dataLoader) : IRequestHandler<GetAppByIdQuery, App?>
    {
        public async Task<App?> Handle(GetAppByIdQuery request, CancellationToken cancellationToken)
        {
            return await dataLoader.LoadAsync(request.Id, cancellationToken);
        }
    }
}
