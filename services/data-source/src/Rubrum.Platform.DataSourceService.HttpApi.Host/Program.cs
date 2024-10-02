using Rubrum.Platform.DataSourceService;
using Rubrum.Platform.DataSourceService.EntityFrameworkCore;
using Rubrum.Platform.Hosting;

return await HostGraphqlHelper.RunServerAsync<GraphqlGenerationModule, DataSourceServiceHttpApiHostModule>(
    args,
    builder =>
    {
        builder.AddServiceDefaults();
        builder.AddNpgsqlDbContext<DataSourceServiceDbContext>(
            DataSourceServiceDbProperties.ConnectionStringName);
    });
