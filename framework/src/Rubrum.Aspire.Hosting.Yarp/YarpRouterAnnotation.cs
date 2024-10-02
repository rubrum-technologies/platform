using Aspire.Hosting.ApplicationModel;
using Yarp.ReverseProxy.Configuration;

namespace Rubrum.Aspire.Hosting;

public class YarpRouterAnnotation(RouteConfig route) : IResourceAnnotation
{
    public RouteConfig Route => route;
}
