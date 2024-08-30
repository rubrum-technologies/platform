using Yarp.ReverseProxy.Configuration;

namespace Rubrum.Platform.Gateway;

public static class YarpSwaggerUiBuilderExtensions
{
    public static IApplicationBuilder UseSwaggerUiWithYarp(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            var serviceProvider = app.ApplicationServices;

            options.RoutePrefix = "api/swagger";
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var proxyConfig = serviceProvider.GetRequiredService<IProxyConfigProvider>().GetConfig();

            var routes = proxyConfig.Routes;

            foreach (var metadata in routes.Select(x => x.Metadata))
            {
                if (metadata?.TryGetValue("swagger-url", out var url) != true)
                {
                    continue;
                }

                var name = metadata.GetOrDefault("swagger-name");

                options.SwaggerEndpoint(
                    $"{url}/swagger/v1/swagger.json",
                    $"{name} API");
                options.OAuthClientId(configuration["Swagger:ClientId"]);
            }
        });

        return app;
    }
}
