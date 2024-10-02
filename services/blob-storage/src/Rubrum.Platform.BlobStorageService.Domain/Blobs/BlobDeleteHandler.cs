using Microsoft.Extensions.Logging;
using Volo.Abp.BlobStoring;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.EventBus;
using Volo.Abp.Threading;

namespace Rubrum.Platform.BlobStorageService.Blobs;

public class BlobDeleteHandler(
    IBlobContainerFactory blobContainerFactory,
    ILogger<BlobDeleteHandler> logger,
    ICancellationTokenProvider cancellationTokenProvider)
    : ILocalEventHandler<EntityDeletedEventData<Blob>>, ITransientDependency
{
    public async Task HandleEventAsync(EntityDeletedEventData<Blob> eventData)
    {
        var ct = cancellationTokenProvider.Token;

        var blob = eventData.Entity;
        var blobContainer = blobContainerFactory.Create(blob);

        try
        {
            if (await blobContainer.ExistsAsync(blob.SystemFileName, ct))
            {
                await blobContainer.DeleteAsync(blob.SystemFileName, ct);
            }
        }
        catch (Exception ex)
        {
            logger.LogException(ex);
        }
    }
}
