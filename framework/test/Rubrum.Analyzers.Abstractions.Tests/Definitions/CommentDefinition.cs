using Rubrum.Authorization.Relations;

namespace Rubrum.Authorization.Analyzers.Definitions;

[Definition]
[Relation<IssueDefinition.Ref>("Issue")]
[Permission("Delete")]
public static partial class CommentDefinition
{
    public static partial Permission DeleteConfigure()
    {
        return Issue;
    }
}
