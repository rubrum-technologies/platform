namespace Rubrum.Platform.DataSourceService.Grpc;

public class GrpcSource : DataSource
{
    public override IReadOnlyList<DataSourceEntity> Entities { get; } = default!;
}
