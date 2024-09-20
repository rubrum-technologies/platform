using MediatR;
using Volo.Abp.Users;

namespace Rubrum.Platform.BlobStorageService.Folders.Queries;

public class GetFoldersQuery : IRequest<IQueryable<FolderBlob>>
{
    public required Guid? ParentId { get; init; }

    public class Handler(
        ICurrentUser currentUser,
        IFolderBlobRepository repository)
        : IRequestHandler<GetFoldersQuery, IQueryable<FolderBlob>>
    {
        public async Task<IQueryable<FolderBlob>> Handle(GetFoldersQuery request, CancellationToken cancellationToken)
        {
            return (await repository.GetQueryableAsync())
                .Where(x => x.OwnerId == currentUser.Id && x.ParentId == request.ParentId);
        }
    }
}
