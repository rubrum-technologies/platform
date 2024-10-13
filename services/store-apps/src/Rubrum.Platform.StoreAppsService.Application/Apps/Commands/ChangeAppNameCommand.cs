using FluentValidation;
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
            var app = await repository.GetAsync(request.Id, true, cancellationToken);

            await manager.ChangeNameAsync(app, request.Name, cancellationToken);

            await repository.UpdateAsync(app, true, cancellationToken);

            return app;
        }
    }

    public sealed class Validator : AbstractValidator<ChangeAppNameCommand>
    {
        public Validator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(AppConstants.MaxNameLength);
        }
    }
}
