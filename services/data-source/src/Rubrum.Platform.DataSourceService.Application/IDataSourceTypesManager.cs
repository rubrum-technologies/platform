namespace Rubrum.Platform.DataSourceService;

public interface IDataSourceTypesManager
{
    Type GetType(DataSourceEntity entity);

    Task CompilationAsync(CancellationToken ct = default);
}
