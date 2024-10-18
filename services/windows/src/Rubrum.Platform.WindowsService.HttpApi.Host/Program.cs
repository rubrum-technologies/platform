using Rubrum.Platform.Hosting;
using Rubrum.Platform.WindowsService;
using Rubrum.Platform.WindowsService.EntityFrameworkCore;

return await HostGraphqlHelper.RunServerAsync<GraphqlGenerationModule, WindowsServiceHttpApiHostModule>(
    args,
    builder =>
    {
        builder.AddServiceDefaults();
        builder.AddNpgsqlDbContext<WindowsServiceDbContext>(WindowsServiceDbProperties.ConnectionStringName);
        builder.AddElasticsearchClient("elasticsearch");
    });
