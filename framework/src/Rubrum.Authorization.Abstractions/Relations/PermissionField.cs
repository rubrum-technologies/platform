using Volo.Abp;

namespace Rubrum.Authorization.Relations;

public sealed class PermissionField : Permission
{
    internal PermissionField(string name)
    {
        Name = Check.NotNullOrWhiteSpace(name, nameof(name));
    }

    public string Name { get; }
}
