namespace Rubrum.Platform.DataSourceService;

public interface IDataSourceAssemblyAccessor
{
    Type GetType(DataSourceEntity entity);
}
