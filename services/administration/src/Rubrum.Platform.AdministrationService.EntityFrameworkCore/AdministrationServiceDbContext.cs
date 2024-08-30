using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.FeatureManagement;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.PermissionManagement;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement;
using Volo.Abp.SettingManagement.EntityFrameworkCore;

namespace Rubrum.Platform.AdministrationService.EntityFrameworkCore;

[ConnectionStringName(AdministrationServiceDbProperties.ConnectionStringName)]
public class AdministrationServiceDbContext(DbContextOptions<AdministrationServiceDbContext> options) :
    AbpDbContext<AdministrationServiceDbContext>(options),
    IFeatureManagementDbContext,
    ISettingManagementDbContext,
    IPermissionManagementDbContext
{
    public DbSet<FeatureGroupDefinitionRecord> FeatureGroups => Set<FeatureGroupDefinitionRecord>();

    public DbSet<FeatureDefinitionRecord> Features => Set<FeatureDefinitionRecord>();

    public DbSet<FeatureValue> FeatureValues => Set<FeatureValue>();

    public DbSet<Setting> Settings => Set<Setting>();

    public DbSet<SettingDefinitionRecord> SettingDefinitionRecords => Set<SettingDefinitionRecord>();

    public DbSet<PermissionGroupDefinitionRecord> PermissionGroups => Set<PermissionGroupDefinitionRecord>();

    public DbSet<PermissionDefinitionRecord> Permissions => Set<PermissionDefinitionRecord>();

    public DbSet<PermissionGrant> PermissionGrants => Set<PermissionGrant>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureFeatureManagement();
        modelBuilder.ConfigureSettingManagement();
        modelBuilder.ConfigurePermissionManagement();

        modelBuilder.ConfigureAdministrationService();
    }
}
