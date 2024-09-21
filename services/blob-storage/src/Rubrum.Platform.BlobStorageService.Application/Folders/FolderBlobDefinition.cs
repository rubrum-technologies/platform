using Rubrum.Authorization.Relations;
using Rubrum.Platform.Relations;

namespace Rubrum.Platform.BlobStorageService.Folders;

[Definition]
[Relation("Owner", typeof(UserDefinition))]
[Permission("Edit")]
public static partial class FolderBlobDefinition
{
    public static partial Permission EditConfigure() => Owner;
}
