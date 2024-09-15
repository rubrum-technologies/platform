#pragma warning disable S4050

namespace Rubrum.Authorization.Permissions;

public class RelationProperty(string name, Type definition)
{
    public string Name => name;

    public Type Definition => definition;

    public static implicit operator Permission(RelationProperty property) => property.ToPermission();

    public static Permission operator +(RelationProperty a, RelationProperty b) => a.ToPermission() + b.ToPermission();

    public static Permission operator -(RelationProperty a, RelationProperty b) => a.ToPermission() - b.ToPermission();

    public static Permission operator &(RelationProperty a, RelationProperty b) => a.ToPermission() & b.ToPermission();

    private PermissionExpression ToPermission()
    {
        return new PermissionExpression(
            new PermissionNamed(Definition.Name),
            PermissionOperator.Arrow,
            new PermissionNamed(Name));
    }
}
