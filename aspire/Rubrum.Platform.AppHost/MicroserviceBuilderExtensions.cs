namespace Rubrum.Platform.AppHost;

public static class MicroserviceBuilderExtensions
{
    public static IResourceBuilder<T> DefaultMicroserviceConfiguration<T>(
        this IResourceBuilder<T> project,
        IResourceBuilder<ParameterResource> authority,
        IResourceBuilder<ParameterResource> swaggerClient)
        where T : IResourceWithEnvironment, IResourceWithEndpoints
    {
        return project
            .WithHttpEndpoint()
            .WithEnvironment(context =>
            {
                ConfigurationDatabase(context);
            })
            .DefaultServiceConfiguration(authority, swaggerClient);
    }

    private static void ConfigurationDatabase(EnvironmentCallbackContext context)
    {
        context.EnvironmentVariables["Aspire__Npgsql__EntityFrameworkCore__PostgreSQL__DisableRetry"] = "true";
        context.EnvironmentVariables["Aspire__Npgsql__EntityFrameworkCore__PostgreSQL__CommandTimeout"] = "10000";
    }
}
