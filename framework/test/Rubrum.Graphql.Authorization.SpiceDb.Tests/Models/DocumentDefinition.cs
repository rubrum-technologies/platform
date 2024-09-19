using Rubrum.Authorization.Relations;

namespace Rubrum.Authorization.Analyzers.Models;

[Definition]
[Relation("Writer", typeof(UserDefinition))]
[Relation("Reader", typeof(UserDefinition))]
[Permission("Edit")]
[Permission("View")]
public static partial class DocumentDefinition
{
    public static partial Permission EditConfigure() => Writer;

    public static partial Permission ViewConfigure() => Reader + Edit;
}
