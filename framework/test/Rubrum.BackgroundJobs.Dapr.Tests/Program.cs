using Rubrum.BackgroundJobs;
using Rubrum.Platform.Hosting;

return await HostHelper.RunServerAsync<RubrumBackgroundJobsDaprTestModule>(
    args,
    builder =>
    {
        builder.AddServiceDefaults();
    });

