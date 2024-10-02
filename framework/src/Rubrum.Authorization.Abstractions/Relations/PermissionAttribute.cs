namespace Rubrum.Authorization.Relations;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public sealed class PermissionAttribute(string name) : Attribute
{
    public string Name => name;
}
