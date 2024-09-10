namespace Rubrum.Authorization.Permissions;

public abstract class ObjectReference(string type, string id)
{
    public string Type => type;

    public string Id => id;
}
