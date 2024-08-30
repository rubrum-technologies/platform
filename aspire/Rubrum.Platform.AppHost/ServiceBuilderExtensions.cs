namespace Rubrum.Platform.AppHost;

public static class ServiceBuilderExtensions
{
    public static IResourceBuilder<T> DefaultServiceConfiguration<T>(
        this IResourceBuilder<T> project,
        IResourceBuilder<ParameterResource> authority,
        IResourceBuilder<ParameterResource> swaggerClient)
        where T : IResourceWithEnvironment, IResourceWithEndpoints
    {
        var clientId = project.Resource.Name;

        return project
            .WithOtlpExporter()
            .WithEnvironment("AuthServer__Authority", authority)
            .WithEnvironment("AuthServer__Audience", clientId)
            .WithEnvironment("Swagger__ClientId", swaggerClient);
    }

}
