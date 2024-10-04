using HotChocolate;
using HotChocolate.Types.Relay;
using MediatR;
using Volo.Abp.DependencyInjection;

namespace Rubrum.Platform.StoreAppsService.Apps.Commands;

[GraphQLName("DeactivateAppInput")]
public class DeactivateAppCommand : IRequest<App>
{
    [ID<App>]
    public required Guid Id { get; init; }

    public sealed class Handler(IAppRepository repository)
        : IRequestHandler<DeleteAppCommand, App>, ITransientDependency
    {
        public async Task<App> Handle(
            DeleteAppCommand request,
            CancellationToken cancellationToken)
        {
            var app = await repository.GetAsync(x => x.Id == request.Id, true, cancellationToken);

            app.Deactivate();

            await repository.UpdateAsync(app, true, cancellationToken);

            return app;
        }
    }
}
