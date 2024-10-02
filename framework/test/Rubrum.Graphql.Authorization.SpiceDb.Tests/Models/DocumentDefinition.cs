using Rubrum.Authorization.Relations;

namespace Rubrum.Authorization.Analyzers.Models;

[Definition]
[Relation<UserDefinition.Ref>("Writer")]
[Relation<UserDefinition.Ref>("Reader")]
[Permission("Edit")]
[Permission("View")]
public static partial class DocumentDefinition
{
    public static partial Permission EditConfigure() => Writer;

    public static partial Permission ViewConfigure() => Reader + Edit;
}
