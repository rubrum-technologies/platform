namespace Rubrum.Platform.DataSourceService.OpenApi;

public class OpenApiSource : DataSource
{
    public override IReadOnlyList<DataSourceEntity> Entities { get; } = default!;
}
