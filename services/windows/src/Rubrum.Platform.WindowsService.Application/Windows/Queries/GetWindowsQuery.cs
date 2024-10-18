using MediatR;

namespace Rubrum.Platform.WindowsService.Windows.Queries;

public class GetWindowsQuery : IRequest<IQueryable<Window>>
{
    public class Handler(
        IWindowRepository repository)
        : IRequestHandler<GetWindowsQuery, IQueryable<Window>>
    {
        public async Task<IQueryable<Window>> Handle(GetWindowsQuery request, CancellationToken cancellationToken)
        {
            return await repository.GetQueryableAsync();
        }
    }
}
