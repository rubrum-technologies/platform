using Volo.Abp.Application.Services;
using Volo.Abp.Content;

namespace Rubrum.Platform.BlobStorageService.Blobs;

public interface IBlobAppService : IApplicationService
{
    Task<IRemoteStreamContent> GetAsync(Guid id, CancellationToken ct = default);

    Task<string> UploadAsync(
        IRemoteStreamContent content,
        Guid? folderId = null,
        CancellationToken ct = default);
}
