using Rubrum.Platform.BlobStorageService;
using Rubrum.Platform.BlobStorageService.EntityFrameworkCore;
using Rubrum.Platform.Hosting;

return await HostGraphqlHelper.RunServerAsync<GraphqlGenerationModule, PlatformBlobStorageServiceHttpApiHostModule>(
    args,
    builder =>
    {
        builder.AddServiceDefaults();
        builder.AddNpgsqlDbContext<BlobStorageServiceDbContext>(
            BlobStorageServiceDbProperties.ConnectionStringName);
    });
