using HotChocolate.Types.Relay;
using Rubrum.Graphql;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using Volo.Abp.Users;

namespace Rubrum.Platform.BlobStorageService.Blobs;

public class BlobAppService(
    IBlobRepository repository,
    BlobManager manager,
    INodeIdSerializerAccessor idSerializerAccessor) : ApplicationService, IBlobAppService
{
    public async Task<IRemoteStreamContent> GetAsync(Guid id, CancellationToken ct = default)
    {
        var blob = await repository.GetAsync(id, true, ct);

        return await manager.GetFileAsync(blob, ct);
    }

    public async Task<string> UploadAsync(
        IRemoteStreamContent content,
        Guid? folderId = null,
        CancellationToken ct = default)
    {
        var idSerializer = idSerializerAccessor.Serializer;
        var blob = await manager.CreateAsync(CurrentUser.GetId(), folderId, content, ct);

        return idSerializer.Format<Blob>(blob.Id);
    }
}
