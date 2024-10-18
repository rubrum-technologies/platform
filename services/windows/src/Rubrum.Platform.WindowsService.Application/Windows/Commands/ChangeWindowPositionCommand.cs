using HotChocolate;
using HotChocolate.Types.Relay;
using MediatR;
using Volo.Abp.DependencyInjection;

namespace Rubrum.Platform.WindowsService.Windows.Commands;

[GraphQLName("ChangeWindowPositionInput")]
public sealed class ChangeWindowPositionCommand : IRequest<Window>
{
    [ID<Window>]
    public required Guid Id { get; init; }

    public required WindowPosition Position { get; init; }

    public class Handler(
        WindowManager manager,
        IWindowRepository repository) : IRequestHandler<ChangeWindowPositionCommand, Window>, ITransientDependency
    {
        public async Task<Window> Handle(
            ChangeWindowPositionCommand request,
            CancellationToken cancellationToken)
        {
            var window = await repository.GetAsync(request.Id, true, cancellationToken);

            manager.ChangePosition(window, request.Position);

            await repository.UpdateAsync(window, true, cancellationToken);

            return window;
        }
    }
}
