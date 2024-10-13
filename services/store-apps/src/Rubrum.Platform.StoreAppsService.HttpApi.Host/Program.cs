using Rubrum.Platform.Hosting;
using Rubrum.Platform.StoreAppsService;
using Rubrum.Platform.StoreAppsService.EntityFrameworkCore;

return await HostGraphqlHelper.RunServerAsync<GraphqlGenerationModule, StoreAppsServiceHttpApiHostModule>(
    args,
    builder =>
    {
        builder.AddServiceDefaults();
        builder.AddNpgsqlDbContext<StoreAppsServiceDbContext>(
            StoreAppsServiceDbProperties.ConnectionStringName);
        builder.AddElasticsearchClient("elasticsearch");
    });
