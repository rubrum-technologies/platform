using MediatR;
using Volo.Abp.Content;
using Volo.Abp.Users;

namespace Rubrum.Platform.BlobStorageService.Blobs.Commands;

public class UploadBlobCommand : IRequest<Blob>
{
    public Guid? FolderId { get; init; }

    public required IRemoteStreamContent Content { get; init; }

    public class Handler(
        ICurrentUser currentUser,
        BlobManager manager) : IRequestHandler<UploadBlobCommand, Blob>
    {
        public async Task<Blob> Handle(UploadBlobCommand request, CancellationToken cancellationToken)
        {
            var blob = await manager.CreateAsync(
                currentUser.GetId(),
                request.FolderId,
                request.Content,
                cancellationToken);

            return blob;
        }
    }
}
