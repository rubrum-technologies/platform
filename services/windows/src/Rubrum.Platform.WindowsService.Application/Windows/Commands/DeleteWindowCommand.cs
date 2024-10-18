using HotChocolate;
using HotChocolate.Types.Relay;
using MediatR;
using Volo.Abp.DependencyInjection;

namespace Rubrum.Platform.WindowsService.Windows.Commands;

[GraphQLName("DeleteWindowInput")]
public sealed class DeleteWindowCommand : IRequest<Window>
{
    [ID<Window>]
    public required Guid Id { get; init; }

    public class Handler(IWindowRepository repository)
        : IRequestHandler<DeleteWindowCommand, Window>, ITransientDependency
    {
        public async Task<Window> Handle(
            DeleteWindowCommand request,
            CancellationToken cancellationToken)
        {
            var window = await repository.GetAsync(request.Id, true, cancellationToken);

            await repository.DeleteAsync(window, true, cancellationToken);

            return window;
        }
    }
}
