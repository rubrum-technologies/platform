using Aspire.Hosting;
using Aspire.Hosting.ApplicationModel;
using Yarp.ReverseProxy.Configuration;

namespace Rubrum.Aspire.Hosting;

public static class YarpGatewayExtensions
{
    public static IResourceBuilder<T> WithYarpGateway<T, TService>(
        this IResourceBuilder<T> builder,
        IEnumerable<IResourceBuilder<TService>> services)
        where T : IResourceWithEnvironment
        where TService : IResourceWithEndpoints
    {
        return builder.WithEnvironment(context =>
        {
            var environment = context.EnvironmentVariables;

            foreach (var annotations in services.Select(x => x.Resource.Annotations))
            {
                var clusters = annotations.OfType<YarpClusterAnnotation>().Select(x => x.Cluster).ToList();
                var routes = annotations.OfType<YarpRouterAnnotation>().Select(x => x.Route).ToList();

                foreach (var cluster in clusters)
                {
                    foreach (var (key, value) in AspireHelper.ObjectToConfig(cluster))
                    {
                        if (key.Contains("ClusterId"))
                        {
                            continue;
                        }

                        environment[$"ReverseProxy__Clusters__{cluster.ClusterId}__{key}"] = value;
                    }
                }

                foreach (var route in routes)
                {
                    foreach (var (key, value) in AspireHelper.ObjectToConfig(route))
                    {
                        if (key.Contains("RouteId"))
                        {
                            continue;
                        }

                        environment[$"ReverseProxy__Routes__{route.RouteId}__{key}"] = value;
                    }
                }
            }
        });
    }

    public static IResourceBuilder<T> WithYarpDaprGateway<T, TService>(
        this IResourceBuilder<T> builder,
        int daprPort,
        IEnumerable<IResourceBuilder<TService>> services,
        Func<EnvironmentCallbackContext, ClusterConfig, ClusterConfig>? config = null)
        where T : IResourceWithEnvironment, IResourceWithEndpoints
        where TService : IResourceWithEndpoints
    {
        return builder
            .WithYarpDaprCluster(daprPort, config)
            .WithYarpGateway([(IResourceBuilder<TService>)builder, ..services]);
    }
}
