using Rubrum.Platform.WindowsService.Windows;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Rubrum.Platform.WindowsService.EntityFrameworkCore.Repositories;

public class EfCoreWindowsRepository(IDbContextProvider<WindowsServiceDbContext> dbContextProvider)
    : EfCoreRepository<WindowsServiceDbContext, Window, Guid>(dbContextProvider), IWindowRepository;
