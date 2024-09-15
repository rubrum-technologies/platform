namespace Rubrum.Authorization.Permissions;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RelationAttribute(string name, params Type[] definitions) : Attribute
{
    public string Name => name;

    public Type[] Definitions => definitions;
}
