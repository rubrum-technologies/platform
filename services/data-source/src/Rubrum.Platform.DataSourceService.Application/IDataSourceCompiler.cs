using System.Diagnostics.CodeAnalysis;

namespace Rubrum.Platform.DataSourceService;

public interface IDataSourceCompiler
{
    bool TryCompile(DataSource dataSource, [NotNullWhen(true)] out Stream? dll);
}
