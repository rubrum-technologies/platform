using Microsoft.AspNetCore.Mvc;
using Rubrum.Platform.BlobStorageService.Blobs;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Content;

namespace Rubrum.Platform.BlobStorageService.Controllers;

[Controller]
[RemoteService(Name = BlobStorageServiceRemoteServiceConstants.RemoteServiceName)]
[Area(BlobStorageServiceRemoteServiceConstants.ModuleName)]
[Route("api/blob-storage/blobs")]
public class BlobAppController(IBlobAppService service) : AbpControllerBase, IBlobAppService
{
    [HttpGet("{id:guid}")]
    public async Task<IRemoteStreamContent> GetAsync(Guid id, CancellationToken ct = default)
    {
        return await service.GetAsync(id, ct);
    }

    [HttpPost("upload")]
    public async Task<string> UploadAsync(
        IRemoteStreamContent content,
        Guid? folderId = null,
        CancellationToken ct = default)
    {
        return await service.UploadAsync(content, folderId, ct);
    }
}
