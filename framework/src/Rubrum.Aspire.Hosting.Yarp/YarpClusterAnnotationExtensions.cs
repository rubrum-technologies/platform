using System.Net;
using Aspire.Hosting;
using Aspire.Hosting.ApplicationModel;
using Yarp.ReverseProxy.Configuration;
using Yarp.ReverseProxy.Forwarder;

namespace Rubrum.Aspire.Hosting;

public static class YarpClusterAnnotationExtensions
{
    public static IResourceBuilder<T> WithYarpCluster<T>(
        this IResourceBuilder<T> builder,
        Func<EnvironmentCallbackContext, ClusterConfig, ClusterConfig>? config = null)
        where T : IResourceWithEnvironment, IResourceWithEndpoints
    {
        var resource = builder.Resource;

        builder.WithEnvironment(context =>
        {
            var cluster = new ClusterConfig
            {
                ClusterId = $"{resource.Name}-cluster",
                HttpRequest = new ForwarderRequestConfig
                {
                    ActivityTimeout = TimeSpan.FromSeconds(300),
                    Version = resource.IsHttp2() ? HttpVersion.Version20 : HttpVersion.Version10,
                    VersionPolicy = HttpVersionPolicy.RequestVersionExact,
                },
                Destinations = new Dictionary<string, DestinationConfig>
                {
                    {
                        "Default", new DestinationConfig
                        {
                            Address = builder.GetUrl("http"),
                        }
                    },
                },
            };

            if (config is not null)
            {
                cluster = config(context, cluster);
            }

            builder.WithAnnotation(new YarpClusterAnnotation(cluster));
        });

        return builder;
    }

    public static IResourceBuilder<T> WithYarpDaprCluster<T>(
        this IResourceBuilder<T> builder,
        int daprPort,
        Func<EnvironmentCallbackContext, ClusterConfig, ClusterConfig>? config = null)
        where T : IResourceWithEnvironment, IResourceWithEndpoints
    {
        return builder.WithYarpCluster((context, cluster) =>
        {
            cluster = cluster with
            {
                ClusterId = "dapr-cluster",
                Destinations = new Dictionary<string, DestinationConfig>
                {
                    {
                        "Default",
                        new DestinationConfig
                        {
                            Address = $"http://localhost:{daprPort}",
                        }
                    },
                },
            };

            if (config is not null)
            {
                cluster = config(context, cluster);
            }

            return cluster;
        });
    }
}
