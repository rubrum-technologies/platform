using HotChocolate.Types.Relay;
using MediatR;
using Rubrum.Graphql;
using Rubrum.Platform.BlobStorageService.Blobs.Commands;
using Rubrum.Platform.BlobStorageService.Blobs.Queries;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using Volo.Abp.Domain.Entities;

namespace Rubrum.Platform.BlobStorageService.Blobs;

public class BlobAppService(
    IMediator mediator,
    BlobManager manager,
    INodeIdSerializerAccessor idSerializerAccessor) : ApplicationService, IBlobAppService
{
    public async Task<IRemoteStreamContent> GetAsync(Guid id, CancellationToken ct = default)
    {
        var blob = await mediator.Send(new GetBlobByIdQuery { Id = id }, ct);

        if (blob is null)
        {
            throw new EntityNotFoundException { Id = id };
        }

        return await manager.GetFileAsync(blob, ct);
    }

    public async Task<string> UploadAsync(
        IRemoteStreamContent content,
        Guid? folderId = null,
        CancellationToken ct = default)
    {
        var idSerializer = idSerializerAccessor.Serializer;
        var blob = await mediator.Send(
            new UploadBlobCommand
            {
                FolderId = folderId,
                Content = content,
            },
            ct);

        return idSerializer.Format<Blob>(blob.Id);
    }
}
