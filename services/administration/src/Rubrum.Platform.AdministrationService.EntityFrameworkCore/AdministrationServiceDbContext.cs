using Microsoft.EntityFrameworkCore;
using Rubrum.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.PermissionManagement;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;

namespace Rubrum.Platform.AdministrationService.EntityFrameworkCore;

[ConnectionStringName(AdministrationServiceDbProperties.ConnectionStringName)]
public class AdministrationServiceDbContext(DbContextOptions<AdministrationServiceDbContext> options)
    : RubrumDbContext<AdministrationServiceDbContext>(options), IPermissionManagementDbContext
{
    public DbSet<PermissionGroupDefinitionRecord> PermissionGroups => Set<PermissionGroupDefinitionRecord>();

    public DbSet<PermissionDefinitionRecord> Permissions => Set<PermissionDefinitionRecord>();

    public DbSet<PermissionGrant> PermissionGrants => Set<PermissionGrant>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigurePermissionManagement();

        modelBuilder.ConfigureAdministrationService();
    }
}
