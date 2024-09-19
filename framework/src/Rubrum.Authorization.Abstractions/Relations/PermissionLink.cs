#pragma warning disable S4050

namespace Rubrum.Authorization.Relations;

public sealed class PermissionLink(string name, Func<Permission> config)
{
    public string Name => name;

    public Func<Permission> Config => config;

    public static implicit operator Permission(PermissionLink link) => link.ToPermission();

    public static Permission operator +(Relation a, PermissionLink b) => a + b.ToPermission();

    public static Permission operator +(PermissionLink a, Relation b) => a.ToPermission() + b;

    public static Permission operator +(RelationProperty a, PermissionLink b) => a + b.ToPermission();

    public static Permission operator +(PermissionLink a, RelationProperty b) => a.ToPermission() + b;

    public static Permission operator +(PermissionLink a, PermissionLink b) => a.ToPermission() + b.ToPermission();

    public static Permission operator -(Relation a, PermissionLink b) => a - b.ToPermission();

    public static Permission operator -(PermissionLink a, Relation b) => a.ToPermission() - b;

    public static Permission operator -(RelationProperty a, PermissionLink b) => a - b.ToPermission();

    public static Permission operator -(PermissionLink a, RelationProperty b) => a.ToPermission() - b;

    public static Permission operator -(PermissionLink a, PermissionLink b) => a.ToPermission() - b.ToPermission();

    public static Permission operator &(Relation a, PermissionLink b) => a & b.ToPermission();

    public static Permission operator &(PermissionLink a, Relation b) => a.ToPermission() & b;

    public static Permission operator &(RelationProperty a, PermissionLink b) => a & b.ToPermission();

    public static Permission operator &(PermissionLink a, RelationProperty b) => a.ToPermission() & b;

    public static Permission operator &(PermissionLink a, PermissionLink b) => a.ToPermission() & b.ToPermission();

    private PermissionField ToPermission()
    {
        return new PermissionField(Name);
    }
}
