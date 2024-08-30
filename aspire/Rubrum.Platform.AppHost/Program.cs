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
        8080,
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

var administrationService = builder
    .AddProject<Rubrum_Platform_AdministrationService_HttpApi_Host>("administration-service")
    .WithReference(auth)
    .WithReference(broker)
    .WithReference(database.AddDatabase("administration-service-db"))
    .WithEnvironment("App__Name", "Платформа")
    .WithYarpDaprRoute("/api/administration/{**everything}")
    .WithYarpDaprRoute("/api/abp/api-definition/{**everything}", enableSwagger: false)
    .WithYarpDaprRoute("/api/abp/application-configuration/{**everything}", enableSwagger: false)
    .WithYarpDaprRoute("/api/abp/application-localization/{**everything}", enableSwagger: false)
    .WithYarpDaprRoute("/api/permission-management/{**everything}", enableSwagger: false)
    .WithYarpDaprRoute("/api/setting-management/{**everything}", enableSwagger: false)
    .DefaultMicroserviceConfiguration(authority, swaggerClient)
    .WithDaprSidecar(defaultDaprSidecarOptions);

var (graphql, gateway) = FusionHelper.AddFusionGateway<Rubrum_Platform_Gateway>(builder, "gateway");

graphql
    .WithOptions(new FusionCompositionOptions
    {
        EnableGlobalObjectIdentification = true,
    })
    .WithSubgraph(administrationService);

gateway
    .WithDaprSidecar(defaultDaprSidecarOptions with
    {
        DaprHttpPort = 10010,
        DaprGrpcPort = 10020,
    })
    .WithYarpDaprGateway(10010, [administrationService])
    .WithHttpEndpoint(10001)
    .WithHttpsEndpoint(10000)
    .WithEnvironment("App__CorsOrigins", "http://localhost:4200")
    .DefaultServiceConfiguration(authority, swaggerClient);

await builder
    .Build()
    .Compose()
    .RunAsync();
