using System.Text.RegularExpressions;
using Aspire.Hosting;
using Aspire.Hosting.ApplicationModel;
using Yarp.ReverseProxy.Configuration;

namespace Rubrum.Aspire.Hosting;

public static partial class YarpRouterAnnotationExtensions
{
    public static IResourceBuilder<T> WithYarpRoute<T>(
        this IResourceBuilder<T> builder,
        Func<EnvironmentCallbackContext, RouteConfig, RouteConfig>? config = null)
        where T : IResourceWithEnvironment, IResourceWithEndpoints
    {
        var resource = builder.Resource;

        builder.WithEnvironment(context =>
        {
            var cluster = resource.Annotations.OfType<YarpClusterAnnotation>().FirstOrDefault()?.Cluster;

            var routes = resource.Annotations.OfType<YarpRouterAnnotation>().ToList();

            var route = new RouteConfig
            {
                RouteId = $"{resource.Name}-route-{routes.Count + 1}",
                ClusterId = cluster?.ClusterId,
                Timeout = TimeSpan.FromSeconds(300),
            };

            if (config is not null)
            {
                route = config(context, route);
            }

            builder.WithAnnotation(new YarpRouterAnnotation(route));
        });

        return builder;
    }

    public static IResourceBuilder<T> WithYarpRoute<T>(
        this IResourceBuilder<T> builder,
        string path,
        Func<EnvironmentCallbackContext, RouteConfig, RouteConfig>? config = null,
        bool enableSwagger = true)
        where T : IResourceWithEnvironment, IResourceWithEndpoints
    {
        path = path.TrimStart('/').TrimEnd('/');

        return builder.WithYarpRoute((context, route) =>
        {
            var metadata = route.Metadata?.ToDictionary() ?? new Dictionary<string, string>();

            if (enableSwagger)
            {
                metadata.Add("swagger-name", builder.Resource.Name);
                metadata.Add("swagger-url", ContentInBracesRegex().Replace($"/{path}", string.Empty).TrimEnd('/'));
            }

            route = route with
            {
                Match = new RouteMatch
                {
                    Path = path,
                },
                Metadata = metadata,
            };

            if (config is not null)
            {
                route = config(context, route);
            }

            return route;
        });
    }

    public static IResourceBuilder<T> WithYarpDaprRoute<T>(
        this IResourceBuilder<T> builder,
        string path,
        Func<EnvironmentCallbackContext, RouteConfig, RouteConfig>? config = null,
        bool enableSwagger = true)
        where T : IResourceWithEnvironment, IResourceWithEndpoints
    {
        return builder.WithYarpRoute(path, enableSwagger: enableSwagger, config: (context, route) =>
        {
            route = route with
            {
                ClusterId = "dapr-cluster",
                Transforms =
                [
                    new Dictionary<string, string>
                    {
                        { "RequestHeader", "dapr-app-id" },
                        { "Append", builder.Resource.Name },
                    },
                ],
            };

            if (config is not null)
            {
                route = config(context, route);
            }

            return route;
        });
    }

    [GeneratedRegex(@"\{[^}]*\}")]
    private static partial Regex ContentInBracesRegex();
}
