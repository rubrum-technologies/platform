#pragma warning disable S4050

namespace Rubrum.Authorization.Relations;

public class RelationProperty(string name, string definition)
{
    public string Name => name;

    public string Definition => definition;

    public static implicit operator Permission(RelationProperty property) => property.ToPermission();

    public static Permission operator +(RelationProperty a, RelationProperty b) => a.ToPermission() + b.ToPermission();

    public static Permission operator -(RelationProperty a, RelationProperty b) => a.ToPermission() - b.ToPermission();

    public static Permission operator &(RelationProperty a, RelationProperty b) => a.ToPermission() & b.ToPermission();

    private PermissionExpression ToPermission()
    {
        return new PermissionExpression(
            new PermissionField(Definition),
            PermissionOperator.Arrow,
            new PermissionField(Name));
    }
}
