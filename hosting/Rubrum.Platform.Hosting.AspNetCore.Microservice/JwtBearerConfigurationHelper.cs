using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Rubrum.Platform.Hosting;

public static class JwtBearerConfigurationHelper
{
    public static void Configure(ServiceConfigurationContext context, string audience)
    {
        var configuration = context.Services.GetConfiguration();

        var requireHttpsMetadata = Convert.ToBoolean(configuration["AuthServer:RequireHttpsMetadata"]);

        context.Services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddKeycloakJwtBearer("auth", "rubrum", options =>
            {
                options.RequireHttpsMetadata = requireHttpsMetadata;
                options.Audience = audience;
                options.Authority = configuration["AuthServer:Authority"]!;
            });
    }
}
