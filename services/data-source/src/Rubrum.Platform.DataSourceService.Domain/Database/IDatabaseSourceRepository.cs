using Volo.Abp.Domain.Repositories;

namespace Rubrum.Platform.DataSourceService.Database;

public interface IDatabaseSourceRepository : IRepository<DatabaseSource, Guid>;
