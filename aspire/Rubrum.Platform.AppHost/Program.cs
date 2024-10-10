using Aspire.Hosting.Dapr;
using HotChocolate.Fusion.Aspire;
using Projects;
using Rubrum.Aspire.Hosting;
using Rubrum.Platform.AppHost;

var builder = DistributedApplication.CreateBuilder(args);
var defaultDaprSidecarOptions = new DaprSidecarOptions
{
    ResourcesPaths = [Path.Combine(Directory.GetCurrentDirectory(), "Dapr", "Resources")],
};

var authority = builder.AddParameter("authority");
var swaggerClient = builder.AddParameter("swagger-client");

builder.AddDapr(options => { options.EnableTelemetry = true; });

var auth = builder
    .AddKeycloak(
        "auth",
        9080,
        builder.AddParameter("admin-username"),
        builder.AddParameter("admin-password"))
    .WithDataVolume("rubrum-auth")
    .WithRealmImport(Path.Combine(Directory.GetCurrentDirectory(), "Realms"));

var database = builder
    .AddPostgres(
        "database",
        builder.AddParameter("database-username"),
        builder.AddParameter("database-password"))
    .WithInitBindMount("Postgres")
    .WithDataVolume("rubrum-database")
    .WithPgAdmin()
    .WithOtlpExporter();

builder
    .AddSpiceDb("spicedb-service")
    .WithPostgres(database.AddDatabase("spicedb-service-db"))
    .WithDaprSidecar(defaultDaprSidecarOptions with
    {
        AppProtocol = "grpc",
    });

var administrationService = builder
    .AddProject<Rubrum_Platform_AdministrationService_HttpApi_Host>("administration-service")
    .WithReference(auth)
    .WithReference(database.AddDatabase("administration-service-db"))
    .WithDaprSidecar(defaultDaprSidecarOptions)
    .WithYarpDaprRoute("/api/abp/{**everything}", enableSwagger: false)
    .WithYarpDaprRoute("/api/administration/{**everything}")
    .DefaultMicroserviceConfiguration(authority, swaggerClient);

var blobStorageService = builder
    .AddProject<Rubrum_Platform_BlobStorageService_HttpApi_Host>("blob-storage-service")
    .WithReference(auth)
    .WithReference(database.AddDatabase("blob-storage-service-db"))
    .WithDaprSidecar(defaultDaprSidecarOptions)
    .WithYarpDaprRoute("/api/blob-storage/{**everything}")
    .DefaultMicroserviceConfiguration(authority, swaggerClient);

var (graphql, gateway) = FusionHelper.AddFusionGateway<Rubrum_Platform_Gateway>(builder, "gateway");

graphql
    .WithOptions(new FusionCompositionOptions
    {
        EnableGlobalObjectIdentification = true,
    })
    .WithSubgraph(administrationService)
    .WithSubgraph(blobStorageService);

gateway
    .WithDaprSidecar(defaultDaprSidecarOptions with
    {
        DaprHttpPort = 12010,
        DaprGrpcPort = 12020,
    })
    .WithYarpDaprGateway(12010, [blobStorageService, administrationService])
    .WithHttpEndpoint(12001, name: "http-dev")
    .WithHttpsEndpoint(12000, name: "https-dev")
    .WithEnvironment("App__CorsOrigins", "http://localhost:4200")
    .DefaultServiceConfiguration(authority, swaggerClient);

builder
    .AddNpmApp("app", "../../app")
    .WithReference(gateway)
    .WithHttpEndpoint(port: 8000, env: "PORT")
    .WithExternalHttpEndpoints()
    .PublishAsDockerFile();

await builder
    .Build()
    .Compose()
    .RunAsync();
