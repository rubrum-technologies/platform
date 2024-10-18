using Microsoft.EntityFrameworkCore;
using Rubrum.EntityFrameworkCore;
using Rubrum.Platform.WindowsService.Windows;
using Volo.Abp.Data;

namespace Rubrum.Platform.WindowsService.EntityFrameworkCore;

[ConnectionStringName(WindowsServiceDbProperties.ConnectionStringName)]
public class WindowsServiceDbContext(DbContextOptions<WindowsServiceDbContext> options)
    : RubrumDbContext<WindowsServiceDbContext>(options)
{
    public DbSet<Window> Windows => Set<Window>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureWindowsService();
    }
}
