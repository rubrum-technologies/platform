using Rubrum.Authorization.Relations;
using Rubrum.Platform.Relations;

namespace Rubrum.Platform.BlobStorageService.Folders;

[Definition]
[Relation<UserDefinition.Ref>("Owner")]
[Permission("View")]
[Permission("Edit")]
public static partial class FolderBlobDefinition
{
    public static partial Permission ViewConfigure() => Owner;

    public static partial Permission EditConfigure() => Owner;
}
