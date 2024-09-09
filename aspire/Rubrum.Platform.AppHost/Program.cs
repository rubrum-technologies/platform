using Aspire.Hosting.Dapr;
using HotChocolate.Fusion.Aspire;
using Projects;
using Rubrum.Aspire.Hosting;
using Rubrum.Platform.AppHost;

var builder = DistributedApplication.CreateBuilder(args);
var defaultDaprSidecarOptions = new DaprSidecarOptions
{
    ResourcesPaths = [Path.Combine(Directory.GetCurrentDirectory(), "dapr", "resources")],
};

var authority = builder.AddParameter("authority");
var swaggerClient = builder.AddParameter("swagger-client");

builder.AddDapr(options => { options.EnableTelemetry = true; });

var auth = builder.AddKeycloak(
        "auth",
        9080,
        builder.AddParameter("admin-username"),
        builder.AddParameter("admin-password"))
    .WithDataVolume("rubrum-auth")
    .WithRealmImport(Path.Combine(Directory.GetCurrentDirectory(), "realms"));

var database = builder
    .AddPostgres(
        "database",
        builder.AddParameter("database-username"),
        builder.AddParameter("database-password"))
    .WithDataVolume("rubrum-database")
    .WithPgAdmin()
    .WithOtlpExporter();

var broker = builder
    .AddRabbitMQ(
        "broker",
        builder.AddParameter("rabbitmq-username"),
        builder.AddParameter("rabbitmq-password"))
    .WithDataVolume("rubrum-broker")
    .WithManagementPlugin();

var blobStorageService = builder
    .AddProject<Rubrum_Platform_BlobStorageService_HttpApi_Host>("blob-storage-service")
    .WithReference(auth)
    .WithReference(broker)
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
    .WithSubgraph(blobStorageService);

gateway
    .WithDaprSidecar(defaultDaprSidecarOptions with
    {
        DaprHttpPort = 12010,
        DaprGrpcPort = 12020,
    })
    .WithYarpDaprGateway(12010, [blobStorageService])
    .WithHttpEndpoint(12001)
    .WithHttpsEndpoint(12000)
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
