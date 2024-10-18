using FluentValidation;
using HotChocolate;
using HotChocolate.Types.Relay;
using MediatR;
using Volo.Abp.DependencyInjection;

namespace Rubrum.Platform.WindowsService.Windows.Commands;

[GraphQLName("ChangeWindowNameInput")]
public sealed class ChangeWindowNameCommand : IRequest<Window>
{
    [ID<Window>]
    public required Guid Id { get; init; }

    public required string Name { get; init; }

    public class Handler(
        WindowManager manager,
        IWindowRepository repository) : IRequestHandler<ChangeWindowNameCommand, Window>, ITransientDependency
    {
        public async Task<Window> Handle(
            ChangeWindowNameCommand request,
            CancellationToken cancellationToken)
        {
            var window = await repository.GetAsync(request.Id, true, cancellationToken);

            await manager.ChangeNameAsync(window, request.Name, cancellationToken);

            await repository.UpdateAsync(window, true, cancellationToken);

            return window;
        }
    }

    public sealed class Validator : AbstractValidator<ChangeWindowNameCommand>
    {
        public Validator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(WindowsConstants.MaxNameLength);
        }
    }
}
