namespace Rubrum.Platform.DataSourceService.Graphql;

public class GraphqlSource : DataSource
{
    public override IReadOnlyList<DataSourceEntity> Entities { get; } = default!;
}
