using Rubrum.Authorization.Relations;

namespace Rubrum.Authorization.Analyzers.Models;

[Definition]
[Relation("Issue", typeof(IssueDefinition))]
[Permission("Delete")]
public static partial class CommentDefinition
{
    public static partial Permission DeleteConfigure()
    {
        return null!;
    }
}
