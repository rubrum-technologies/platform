using Rubrum.Platform.StoreAppsService;
using Rubrum.Platform.StoreAppsService.EntityFrameworkCore;
using Rubrum.Platform.Hosting;

return await HostGraphqlHelper.RunServerAsync<GraphqlGenerationModule, StoreAppsServiceHttpApiHostModule>(
    args,
    builder =>
    {
        builder.AddServiceDefaults();
        builder.AddNpgsqlDbContext<StoreAppsServiceDbContext>(
            StoreAppsServiceDbProperties.ConnectionStringName);
    });
