namespace Rubrum.SpiceDb;

public class RubrumSpiceDbOptions
{
    public SpiceDbGrpcClientOptions? PermissionsClient { get; set; }

    public SpiceDbGrpcClientOptions? SchemaClient { get; set; }

    public SpiceDbGrpcClientOptions? WatchClient { get; set; }
}
