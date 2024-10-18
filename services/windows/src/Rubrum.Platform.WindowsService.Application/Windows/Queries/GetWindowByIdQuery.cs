using MediatR;

namespace Rubrum.Platform.WindowsService.Windows.Queries;

public class GetWindowByIdQuery : IRequest<Window?>
{
    public required Guid Id { get; init; }

    public class Handler(IWindowByIdDataLoader dataLoader) : IRequestHandler<GetWindowByIdQuery, Window?>
    {
        public async Task<Window?> Handle(GetWindowByIdQuery request, CancellationToken cancellationToken)
        {
            return await dataLoader.LoadAsync(request.Id, cancellationToken);
        }
    }
}
