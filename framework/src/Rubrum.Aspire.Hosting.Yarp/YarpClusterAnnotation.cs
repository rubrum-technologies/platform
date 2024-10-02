using Aspire.Hosting.ApplicationModel;
using Yarp.ReverseProxy.Configuration;

namespace Rubrum.Aspire.Hosting;

public class YarpClusterAnnotation(ClusterConfig cluster) : IResourceAnnotation
{
    public ClusterConfig Cluster => cluster;
}
