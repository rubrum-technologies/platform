using Volo.Abp.Content;

namespace Rubrum.Platform.BlobStorageService.Blobs;

public interface IBlobManager
{
    Task<IRemoteStreamContent> GetAsync(Guid id, CancellationToken ct = default);

    Task<Blob> CreateAsync(Guid id, IRemoteStreamContent content, CancellationToken ct = default);

    Task ChangeAsync(Blob blob, IRemoteStreamContent content, CancellationToken ct = default);
}
