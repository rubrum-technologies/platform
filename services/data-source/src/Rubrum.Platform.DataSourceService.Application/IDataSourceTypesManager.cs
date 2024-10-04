namespace Rubrum.Platform.DataSourceService;

public interface IDataSourceTypesManager
{
    IReadOnlyList<Type> GetTypes(DataSource source);

    Task CompilationAsync(CancellationToken ct = default);

    Task ReCompilationAsync(DataSource source, CancellationToken ct = default);
}
