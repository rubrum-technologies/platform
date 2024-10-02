using MyCompanyName.MyProjectName;
using MyCompanyName.MyProjectName.EntityFrameworkCore;
using Rubrum.Platform.Hosting;

return await HostGraphqlHelper.RunServerAsync<GraphqlGenerationModule, MyProjectNameHttpApiHostModule>(
    args,
    builder =>
    {
        builder.AddServiceDefaults();
        builder.AddNpgsqlDbContext<MyProjectNameDbContext>(
            MyProjectNameDbProperties.ConnectionStringName);
    });
