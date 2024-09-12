using System;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;

namespace Rubrum.Platform.BlobStorageService.Blobs;

public sealed class BlobAppService : ApplicationService, IBlobAppService
{
    public Task<string> UploadAsync(IRemoteStreamContent content, bool isGraphql = false)
    {
        throw new NotImplementedException();
    }
}
