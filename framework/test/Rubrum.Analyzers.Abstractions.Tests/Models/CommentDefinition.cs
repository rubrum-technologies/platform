using Rubrum.Authorization.Permissions;

namespace Rubrum.Authorization.Analyzers.Models;

[Definition]
[Relation("Issue", typeof(IssueDefinition))]
public static partial class CommentDefinition
{
    public static Permission Delete => Issue.ProjectCommentDeleter;
}
