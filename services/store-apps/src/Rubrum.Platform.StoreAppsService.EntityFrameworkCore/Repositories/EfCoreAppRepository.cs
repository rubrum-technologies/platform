using Rubrum.Platform.StoreAppsService.Apps;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Rubrum.Platform.StoreAppsService.EntityFrameworkCore.Repositories;

public class EfCoreAppRepository(IDbContextProvider<IStoreAppsServiceDbContext> dbContextProvider) :
    EfCoreRepository<IStoreAppsServiceDbContext, App, Guid>(dbContextProvider),
    IAppRepository;
