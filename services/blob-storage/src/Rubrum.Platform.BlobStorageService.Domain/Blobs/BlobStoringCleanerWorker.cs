using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.Linq;
using Volo.Abp.Threading;
using Volo.Abp.Uow;

namespace Rubrum.Platform.BlobStorageService.Blobs;

public class BlobStoringCleanerWorker : AsyncPeriodicBackgroundWorkerBase
{
    public BlobStoringCleanerWorker(AbpAsyncTimer timer, IServiceScopeFactory serviceScopeFactory)
        : base(timer, serviceScopeFactory)
    {
        timer.Period = 30000000;
    }

    protected override async Task DoWorkAsync(PeriodicBackgroundWorkerContext workerContext)
    {
        var serviceProvider = workerContext.ServiceProvider;
        var cancellationToken = workerContext.CancellationToken;

        var cancellationTokenProvider = serviceProvider.GetRequiredService<ICancellationTokenProvider>();
        var logger = serviceProvider.GetRequiredService<ILogger<BlobStoringCleanerWorker>>();
        var unitOfWorkManager = serviceProvider.GetRequiredService<IUnitOfWorkManager>();
        var repository = serviceProvider.GetRequiredService<IBlobRepository>();
        var asyncExecuter = serviceProvider.GetRequiredService<IAsyncQueryableExecuter>();
        var dateTime = DateTime.Now.AddDays(-1);

        logger.LogInformation("Start cleaning temporary files...");
        using var uow = unitOfWorkManager.Begin(true, true);
        using (cancellationTokenProvider.Use(cancellationToken))
        {
            var query = (await repository.GetQueryableAsync())
                .Where(x => x.IsDisposable && x.CreationTime < dateTime)
                .Select(x => x.Id);

            foreach (var id in await asyncExecuter.ToListAsync(query, cancellationToken))
            {
                await repository.DeleteAsync(id, true, cancellationToken);
            }
        }

        logger.LogInformation("Finish cleaning temporary images");

        await uow.CompleteAsync(cancellationToken);
    }
}
