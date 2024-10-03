using Volo.Abp.Domain.Repositories;

namespace Rubrum.Platform.StoreAppsService.Apps;

public interface IAppRepository : IRepository<App, Guid>;
