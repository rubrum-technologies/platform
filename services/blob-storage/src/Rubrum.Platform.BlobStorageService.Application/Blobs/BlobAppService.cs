using HotChocolate.Types.Relay;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;

namespace Rubrum.Platform.BlobStorageService.Blobs;

public class BlobAppService(
    IBlobRepository repository,
    IBlobManager manager,
    INodeIdSerializerAccessor idSerializerAccessor) : ApplicationService, IBlobAppService
{
    public async Task<IRemoteStreamContent> GetAsync(Guid id, CancellationToken ct = default)
    {
        return await manager.GetAsync(id, ct);
    }

    public async Task<string> UploadAsync(IRemoteStreamContent content, CancellationToken ct = default)
    {
        var idSerializer = idSerializerAccessor.Serializer;
        var blob = await manager.CreateAsync(GuidGenerator.Create(), content, ct);

        return idSerializer.Format(nameof(Blob), blob.Id);
    }

    public async Task<string> UploadAsync(Guid id, IRemoteStreamContent content, CancellationToken ct = default)
    {
        var idSerializer = idSerializerAccessor.Serializer;
        var blob = await repository.GetAsync(id, true, ct);

        await manager.ChangeAsync(blob, content, ct);

        return idSerializer.Format(nameof(Blob), blob.Id);
    }
}
