using Microsoft.Extensions.DependencyInjection;
using Rubrum.EntityFrameworkCore;
using Rubrum.Modularity;
using Rubrum.Platform.BlobStorageService.Blobs;
using Rubrum.Platform.BlobStorageService.EntityFrameworkCore.Repositories;
using Volo.Abp.EntityFrameworkCore.PostgreSql;
using Volo.Abp.Modularity;

namespace Rubrum.Platform.BlobStorageService.EntityFrameworkCore;

[DependsOn<AbpEntityFrameworkCorePostgreSqlModule>]
[DependsOn<RubrumEntityFrameworkCoreModule>]
[DependsOn<PlatformBlobStorageServiceDomainModule>]
public class PlatformBlobStorageServiceEntityFrameworkCoreModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        // https://www.npgsql.org/efcore/release-notes/6.0.html#opting-out-of-the-new-timestamp-mapping-logic
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<BlobStorageServiceDbContext>(options =>
        {
            options.AddRepository<Blob, EfCoreBlobRepository>();

            options.AddDefaultRepositories();
        });
    }
}
