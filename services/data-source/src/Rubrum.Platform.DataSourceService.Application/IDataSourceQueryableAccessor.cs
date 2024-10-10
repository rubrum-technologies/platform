namespace Rubrum.Platform.DataSourceService;

public interface IDataSourceQueryableAccessor
{
    Task<Func<Task<IQueryable>>> GetAsync(DataSourceEntity entity);
}
