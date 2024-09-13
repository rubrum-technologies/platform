using Aspire.Hosting;
using Aspire.Hosting.ApplicationModel;

namespace Rubrum.Aspire.Hosting;

public class SpiceDbResource(string name) : ContainerResource(name), IResourceWithServiceDiscovery;
