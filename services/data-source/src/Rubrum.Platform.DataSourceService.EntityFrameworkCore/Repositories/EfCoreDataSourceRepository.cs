using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Rubrum.Platform.DataSourceService.EntityFrameworkCore.Repositories;

public class EfCoreDataSourceRepository(IDbContextProvider<DataSourceServiceDbContext> dbContextProvider)
    : EfCoreRepository<DataSourceServiceDbContext, DataSource, Guid>(dbContextProvider), IDataSourceRepository;
