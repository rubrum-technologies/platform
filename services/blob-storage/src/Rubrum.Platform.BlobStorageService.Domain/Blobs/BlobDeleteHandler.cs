using Volo.Abp.BlobStoring;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.EventBus;
using Volo.Abp.Threading;

namespace Rubrum.Platform.BlobStorageService.Blobs;

public class BlobDeleteHandler(
    IBlobContainerFactory blobContainerFactory,
    ICancellationTokenProvider cancellationTokenProvider)
    : ILocalEventHandler<EntityDeletedEventData<Blob>>, ITransientDependency
{
    public async Task HandleEventAsync(EntityDeletedEventData<Blob> eventData)
    {
        var cancellationToken = cancellationTokenProvider.Token;

        var blob = eventData.Entity;
        var blobContainer = blobContainerFactory.Create();

        if (await blobContainer.ExistsAsync(blob.SystemFileName, cancellationToken))
        {
            await blobContainer.DeleteAsync(blob.SystemFileName, cancellationToken);
        }
    }
}
