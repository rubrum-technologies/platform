namespace Rubrum.Authorization.Relations;

[AttributeUsage(AttributeTargets.Class)]
public sealed class DefinitionAttribute(string? name = null) : Attribute
{
    public string? Name => name;
}
