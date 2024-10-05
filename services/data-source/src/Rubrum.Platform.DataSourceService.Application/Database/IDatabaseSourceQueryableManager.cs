namespace Rubrum.Platform.DataSourceService.Database;

public interface IDatabaseSourceQueryableManager
{
    Task<Func<Task<IQueryable>>> GetFactoryQueryableAsync(DatabaseTable table);

    Task BuildAsync();
}
