using HotChocolate;
using HotChocolate.Types.Relay;
using MediatR;
using Volo.Abp.DependencyInjection;

namespace Rubrum.Platform.StoreAppsService.Apps.Commands;

[GraphQLName("ChangeAppNameInput")]
public sealed class ChangeAppNameCommand : IRequest<App>
{
    [ID<App>]
    public required Guid Id { get; init; }

    public required string Name { get; init; }

    public class Handler(
        AppManager manager,
        IAppRepository repository) : IRequestHandler<ChangeAppNameCommand, App>, ITransientDependency
    {
        public async Task<App> Handle(
            ChangeAppNameCommand request,
            CancellationToken cancellationToken)
        {
            var app = await repository.GetAsync(x => x.Id == request.Id, true, cancellationToken);

            await manager.ChangeNameAsync(app, request.Name, cancellationToken);

            return app;
        }
    }
}
