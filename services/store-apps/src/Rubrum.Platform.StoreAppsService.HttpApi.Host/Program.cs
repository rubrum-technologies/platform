using Rubrum.Platform.Hosting;
using Rubrum.Platform.StoreAppsService;
using Rubrum.Platform.StoreAppsService.EntityFrameworkCore;

return await HostGraphqlHelper.RunServerAsync<GraphqlGenerationModule, PlatformStoreAppsServiceHttpApiHostModule>(
    args,
    builder =>
    {
        builder.AddServiceDefaults();
        builder.AddNpgsqlDbContext<StoreAppsServiceDbContext>(
            StoreAppsServiceDbProperties.ConnectionStringName);
    });
