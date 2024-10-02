using Rubrum.Platform.Gateway;
using Rubrum.Platform.Hosting;

return await HostHelper.RunServerAsync<PlatformGatewayModule>(
    args,
    builder =>
    {
        builder.AddServiceDefaults();
    });
