using FluentValidation;
using HotChocolate;
using MediatR;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Users;

namespace Rubrum.Platform.StoreAppsService.Apps.Commands;

[GraphQLName("CreateAppInput")]
public sealed class CreateAppCommand : IRequest<App>
{
    public required string Name { get; init; }

    public required AppVersion Version { get; init; }

    public required bool Enabled { get; init; }

    public class Handler(
        ICurrentUser currentUser,
        AppManager manager,
        IAppRepository repository) : IRequestHandler<CreateAppCommand, App>, ITransientDependency
    {
        public async Task<App> Handle(
            CreateAppCommand request,
            CancellationToken cancellationToken)
        {
            var app = await manager.CreateAsync(
                currentUser.GetId(),
                request.Name,
                request.Version,
                request.Enabled,
                cancellationToken);

            app = await repository.InsertAsync(app, true, cancellationToken);

            return app;
        }
    }

    public sealed class Validator : AbstractValidator<CreateAppCommand>
    {
        public Validator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(AppConstants.MaxNameLength);
        }
    }
}
