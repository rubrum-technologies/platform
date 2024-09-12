using System;
using Aspire.Hosting;
using Aspire.Hosting.ApplicationModel;

namespace Rubrum.Aspire.Hosting;

public static class SpiceDbBuilderExtensions
{
    public static IResourceBuilder<SpiceDbResource> AddSpiceDb(
        this IDistributedApplicationBuilder builder,
        string name,
        int? port = null)
    {
        var spiceDb = new SpiceDbResource(name);

        return builder
            .AddResource(spiceDb)
            .WithHttpEndpoint(port, targetPort: 50051, name: "http")
            .WithAnnotation(new ContainerImageAnnotation
            {
                Registry = "quay.io",
                Image = "authzed/spicedb",
                Tag = "v1.35.3",
            })
            .WithArgs("serve")
            .WithEnvironment("SPICEDB_GRPC_PRESHARED_KEY", "хер")
            .PublishAsContainer();
    }

    public static IResourceBuilder<SpiceDbResource> WithPostgres(
        this IResourceBuilder<SpiceDbResource> builder,
        IResourceBuilder<PostgresDatabaseResource> database,
        bool isMigrate = true)
    {
        var postgres = database.Resource.Parent;

        if (isMigrate)
        {
            builder.AddSpiceDbMigrate(database);
        }

        return builder
            .WithEnvironment("SPICEDB_DATASTORE_ENGINE", "postgres")
            .WithEnvironment(context =>
            {
                var endpoint = postgres.GetEndpoint("tcp");

                if (context.ExecutionContext.IsPublishMode)
                {
                    // TODO: Доделать
                    return;
                }

                context.EnvironmentVariables["SPICEDB_DATASTORE_CONN_URI"] = ReferenceExpression.Create(
                    $"postgresql://{postgres.UserNameParameter}:{postgres.PasswordParameter}@{endpoint.ContainerHost}:{endpoint.Port.ToString()}/{database.Resource.DatabaseName}?sslmode=disable");
            });
    }

    private static IResourceBuilder<SpiceDbResource> AddSpiceDbMigrate(
        this IResourceBuilder<SpiceDbResource> builder,
        IResourceBuilder<PostgresDatabaseResource> database)
    {
        return builder.ApplicationBuilder
            .AddResource(new SpiceDbResource("spicedb-migrate"))
            .WithAnnotation(new ContainerImageAnnotation
            {
                Registry = "quay.io",
                Image = "authzed/spicedb",
                Tag = "v1.35.3",
            })
            .WithArgs("migrate", "head")
            .WithPostgres(database, false);
    }
}
