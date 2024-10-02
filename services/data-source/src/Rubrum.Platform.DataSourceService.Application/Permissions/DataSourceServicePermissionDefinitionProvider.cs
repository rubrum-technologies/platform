using Volo.Abp.Authorization.Permissions;

namespace Rubrum.Platform.DataSourceService.Permissions;

public class DataSourceServicePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var group = context.AddGroup(DataSourceServicePermissions.GroupName);

        var dataSources = group.AddPermission(DataSourceServicePermissions.DataSources.Default);
        dataSources.AddChild(DataSourceServicePermissions.DataSources.Create);
    }
}
