using Volo.Abp.Content;

namespace Rubrum.Platform.BlobStorageService.Blobs;

public interface IBlobManager
{
    Task<IRemoteStreamContent> GetAsync(Guid id);

    Task<Blob> CreateAsync(Guid id, IRemoteStreamContent content);

    Task ChangeAsync(Blob blob, IRemoteStreamContent content);
}
