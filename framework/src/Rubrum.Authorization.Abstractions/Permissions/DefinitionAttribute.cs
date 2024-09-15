namespace Rubrum.Authorization.Permissions;

[AttributeUsage(AttributeTargets.Class)]
public sealed class DefinitionAttribute(string? name = null) : Attribute
{
    public string? Name => name;
}
