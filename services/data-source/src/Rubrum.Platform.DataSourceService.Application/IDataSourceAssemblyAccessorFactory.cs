namespace Rubrum.Platform.DataSourceService;

public interface IDataSourceAssemblyAccessorFactory
{
    IDataSourceAssemblyAccessor Get(DataSource dataSource);
}
