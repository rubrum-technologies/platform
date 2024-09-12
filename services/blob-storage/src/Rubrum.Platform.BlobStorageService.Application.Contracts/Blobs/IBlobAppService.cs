using System;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;

namespace Rubrum.Platform.BlobStorageService.Blobs;

public interface IBlobAppService : IApplicationService
{
    Task<string> UploadAsync(IRemoteStreamContent content, bool isGraphql = false);
}
