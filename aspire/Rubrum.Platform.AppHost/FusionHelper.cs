using HotChocolate.Fusion.Aspire;
using HotChocolate.Fusion.Composition;

namespace Rubrum.Platform.AppHost;

public static class FusionHelper
{
    public static (IResourceBuilder<FusionGatewayResource> Graphql, IResourceBuilder<ProjectResource> Project)
        AddFusionGateway<TProject>(IDistributedApplicationBuilder builder, string name)
        where TProject : IProjectMetadata, new()
    {
        var gateway = GatewayInfo.Create<TProject>(name, new FusionCompositionOptions());
        var project = builder.AddProject<TProject>(name).WithAnnotation(gateway);
        return (new FusionGatewayResourceBuilder(project), project);
    }
}
