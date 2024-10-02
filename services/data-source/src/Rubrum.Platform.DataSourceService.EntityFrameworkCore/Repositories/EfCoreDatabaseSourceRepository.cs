using Rubrum.Platform.DataSourceService.Database;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Rubrum.Platform.DataSourceService.EntityFrameworkCore.Repositories;

public class EfCoreDatabaseSourceRepository(IDbContextProvider<DataSourceServiceDbContext> dbContextProvider)
    : EfCoreRepository<DataSourceServiceDbContext, DatabaseSource, Guid>(dbContextProvider), IDatabaseSourceRepository;
