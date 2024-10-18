using FluentValidation;
using HotChocolate;
using MediatR;
using Rubrum.Platform.WindowsService.Windows.Commands.Validators;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Users;

namespace Rubrum.Platform.WindowsService.Windows.Commands;

[GraphQLName("CreateWindowInput")]
public sealed class CreateWindowCommand : IRequest<Window>
{
    public required string Name { get; init; }

    public required Guid AppId { get; init; }

    public required WindowPosition Position { get; init; }

    public required WindowSize Size { get; init; }

    public class Handler(
        ICurrentUser currentUser,
        WindowManager manager,
        IWindowRepository repository) : IRequestHandler<CreateWindowCommand, Window>, ITransientDependency
    {
        public async Task<Window> Handle(
            CreateWindowCommand request,
            CancellationToken cancellationToken)
        {
            var window = await manager.CreateAsync(
                currentUser.GetId(),
                request.Name,
                request.AppId,
                request.Position,
                request.Size,
                cancellationToken);

            window = await repository.InsertAsync(window, true, cancellationToken);

            return window;
        }
    }

    public sealed class Validator : AbstractValidator<CreateWindowCommand>
    {
        public Validator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(WindowsConstants.MaxNameLength);

            RuleFor(x => x.Size)
                .SetValidator(new WindowSizeValidator());
        }
    }
}
