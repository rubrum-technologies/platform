namespace Rubrum.Authorization.Relations;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public sealed class RelationAttribute(string name, params Type[] definitions) : Attribute
{
    public string Name => name;

    public Type[] Definitions => definitions;
}
