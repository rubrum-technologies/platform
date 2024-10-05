using System.Diagnostics.CodeAnalysis;

namespace Rubrum.Platform.DataSourceService;

public interface IDataSourceAssemblyCompiler
{
    bool TryCompile(DataSource dataSource, [NotNullWhen(true)] out Stream? dll);
}
