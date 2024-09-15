namespace Rubrum.Authorization.Permissions;

public class RelationProperty(string name, Type definition)
{
    public string Name => name;

    public Type Definition => definition;

    public static implicit operator Permission(RelationProperty property) => new();

    public static Permission operator +(RelationProperty a, RelationProperty b) => new();

    public static Permission operator -(RelationProperty a, RelationProperty b) => new();

    public static Permission operator &(RelationProperty a, RelationProperty b) => new();
}
