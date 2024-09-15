namespace Rubrum.Authorization.Permissions;

[AttributeUsage(AttributeTargets.Class)]
public class DefinitionAttribute(string? name = null) : Attribute
{
    public string? Name => name;
}
