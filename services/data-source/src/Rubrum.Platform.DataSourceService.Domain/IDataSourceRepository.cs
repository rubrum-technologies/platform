using Volo.Abp.Domain.Repositories;

namespace Rubrum.Platform.DataSourceService;

public interface IDataSourceRepository : IReadOnlyRepository<DataSource, Guid>;
