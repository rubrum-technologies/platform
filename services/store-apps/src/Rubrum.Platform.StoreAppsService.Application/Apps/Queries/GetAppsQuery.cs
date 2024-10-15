using MediatR;

namespace Rubrum.Platform.StoreAppsService.Apps.Queries;

public class GetAppsQuery : IRequest<IQueryable<App>>
{
    public class Handler(
        IAppRepository repository)
        : IRequestHandler<GetAppsQuery, IQueryable<App>>
    {
        public async Task<IQueryable<App>> Handle(GetAppsQuery request, CancellationToken cancellationToken)
        {
            return await repository.GetQueryableAsync();
        }
    }
}
