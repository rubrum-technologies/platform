namespace Rubrum.Authorization.Permissions;

internal sealed class PermissionField(string name) : Permission
{
    public string Name => name;
}
