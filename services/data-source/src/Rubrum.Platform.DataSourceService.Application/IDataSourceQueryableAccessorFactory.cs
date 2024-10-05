namespace Rubrum.Platform.DataSourceService;

public interface IDataSourceQueryableAccessorFactory
{
    IDataSourceQueryableAccessor Get(DataSource dataSource);
}
