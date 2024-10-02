using MediatR;
using Volo.Abp.Users;

namespace Rubrum.Platform.BlobStorageService.Folders.Queries;

public class GetFoldersBlobQuery : IRequest<IQueryable<FolderBlob>>
{
    public required Guid? ParentId { get; init; }

    public class Handler(
        ICurrentUser currentUser,
        IFolderBlobRepository repository)
        : IRequestHandler<GetFoldersBlobQuery, IQueryable<FolderBlob>>
    {
        public async Task<IQueryable<FolderBlob>> Handle(GetFoldersBlobQuery request, CancellationToken cancellationToken)
        {
            return (await repository.GetQueryableAsync())
                .Where(x => x.OwnerId == currentUser.Id && x.ParentId == request.ParentId);
        }
    }
}
