using System.Collections.Concurrent;
using LinqToDB;
using Volo.Abp.DependencyInjection;

namespace Rubrum.Platform.DataSourceService.Database;

public class DatabaseSourceDataOptionsAccessor(
    IDatabaseSourceDataOptionsBuilder builder) : IDatabaseSourceDataOptionsAccessor, ITransientDependency
{
    private static readonly ConcurrentDictionary<Guid, DataOptions> Cache = [];

    public DataOptions Get(DatabaseSource databaseSource)
    {
        if (Cache.TryGetValue(databaseSource.Id, out var options))
        {
            return options;
        }

        options = builder.Build(databaseSource);

        Cache.AddOrUpdate(databaseSource.Id, options, (_, _) => options);

        return options;
    }
}
