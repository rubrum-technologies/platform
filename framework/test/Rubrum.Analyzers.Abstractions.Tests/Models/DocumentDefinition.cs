using Rubrum.Authorization.Permissions;

namespace Rubrum.Authorization.Analyzers.Models;

[Definition]
[Relation("Writer", typeof(UserDefinition))]
[Relation("Reader", typeof(UserDefinition))]
public static partial class DocumentDefinition
{
    public static Permission Edit => Writer;

    public static Permission View => Reader + Edit;
}
