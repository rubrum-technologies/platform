using HotChocolate;
using HotChocolate.Types.Relay;
using MediatR;
using Volo.Abp.DependencyInjection;

namespace Rubrum.Platform.StoreAppsService.Apps.Commands;

[GraphQLName("ActivateAppInput")]
public sealed class ActivateAppCommand : IRequest<App>
{
    [ID<App>]
    public required Guid Id { get; init; }

    public class Handler(IAppRepository repository)
        : IRequestHandler<ActivateAppCommand, App>, ITransientDependency
    {
        public async Task<App> Handle(
            ActivateAppCommand request,
            CancellationToken cancellationToken)
        {
            var app = await repository.GetAsync(x => x.Id == request.Id, true, cancellationToken);

            app.Activate();

            await repository.UpdateAsync(app, true, cancellationToken);

            return app;
        }
    }
}
