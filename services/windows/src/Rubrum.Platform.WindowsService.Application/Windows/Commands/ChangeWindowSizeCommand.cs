using FluentValidation;
using HotChocolate;
using HotChocolate.Types.Relay;
using MediatR;
using Rubrum.Platform.WindowsService.Windows.Commands.Validators;
using Volo.Abp.DependencyInjection;

namespace Rubrum.Platform.WindowsService.Windows.Commands;

[GraphQLName("ChangeWindowSizeInput")]
public sealed class ChangeWindowSizeCommand : IRequest<Window>
{
    [ID<Window>]
    public required Guid Id { get; init; }

    public required WindowSize Size { get; init; }

    public class Handler(
        WindowManager manager,
        IWindowRepository repository) : IRequestHandler<ChangeWindowSizeCommand, Window>, ITransientDependency
    {
        public async Task<Window> Handle(
            ChangeWindowSizeCommand request,
            CancellationToken cancellationToken)
        {
            var window = await repository.GetAsync(request.Id, true, cancellationToken);

            manager.ChangeSize(window, request.Size);

            await repository.UpdateAsync(window, true, cancellationToken);

            return window;
        }

        public sealed class Validator : AbstractValidator<CreateWindowCommand>
        {
            public Validator()
            {
                RuleFor(x => x.Size)
                    .SetValidator(new WindowSizeValidator());
            }
        }
    }
}
