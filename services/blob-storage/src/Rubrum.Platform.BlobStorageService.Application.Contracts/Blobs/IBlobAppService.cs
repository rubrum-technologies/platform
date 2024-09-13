using Volo.Abp.Application.Services;
using Volo.Abp.Content;

namespace Rubrum.Platform.BlobStorageService.Blobs;

public interface IBlobAppService : IApplicationService
{
    Task<IRemoteStreamContent> GetAsync(Guid id, CancellationToken ct = default);

    Task<string> UploadAsync(IRemoteStreamContent content, CancellationToken ct = default);

    Task<string> UploadAsync(Guid id, IRemoteStreamContent content, CancellationToken ct = default);
}
