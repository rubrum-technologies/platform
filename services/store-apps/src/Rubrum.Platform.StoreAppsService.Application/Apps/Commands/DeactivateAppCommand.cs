using HotChocolate;
using HotChocolate.Types.Relay;
using MediatR;
using Volo.Abp.DependencyInjection;

namespace Rubrum.Platform.StoreAppsService.Apps.Commands;

[GraphQLName("DeactivateAppInput")]
public sealed class DeactivateAppCommand : IRequest<App>
{
    [ID<App>]
    public required Guid Id { get; init; }

    public class Handler(IAppRepository repository)
        : IRequestHandler<DeactivateAppCommand, App>, ITransientDependency
    {
        public async Task<App> Handle(
            DeactivateAppCommand request,
            CancellationToken cancellationToken)
        {
            var app = await repository.GetAsync(request.Id, true, cancellationToken);

            app.Deactivate();

            await repository.UpdateAsync(app, true, cancellationToken);

            return app;
        }
    }
}
