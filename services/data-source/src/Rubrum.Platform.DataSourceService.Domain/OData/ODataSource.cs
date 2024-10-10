namespace Rubrum.Platform.DataSourceService.OData;

public class ODataSource : DataSource
{
    public override IReadOnlyList<DataSourceEntity> Entities { get; } = default!;
}
