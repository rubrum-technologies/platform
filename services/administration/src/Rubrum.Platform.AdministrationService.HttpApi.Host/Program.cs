using Rubrum.Platform.AdministrationService;
using Rubrum.Platform.AdministrationService.EntityFrameworkCore;
using Rubrum.Platform.Hosting;

return await HostGraphqlHelper.RunServerAsync<GraphqlGenerationModule, PlatformAdministrationServiceHttpApiHostModule>(
    args,
    builder =>
    {
        builder.AddServiceDefaults();
        builder.AddNpgsqlDbContext<AdministrationServiceDbContext>(
            AdministrationServiceDbProperties.ConnectionStringName);
    });
