namespace Rubrum.Authorization.Permissions;

internal sealed class PermissionNamed(string name) : Permission
{
    public string Name => name;
}
