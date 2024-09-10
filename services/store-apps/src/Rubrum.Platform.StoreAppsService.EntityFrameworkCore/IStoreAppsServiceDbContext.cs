using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Rubrum.Platform.StoreAppsService.EntityFrameworkCore;

[ConnectionStringName(StoreAppsServiceDbProperties.ConnectionStringName)]
public interface IStoreAppsServiceDbContext : IEfCoreDbContext;
