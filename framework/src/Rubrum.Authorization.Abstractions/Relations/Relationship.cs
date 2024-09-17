namespace Rubrum.Authorization.Relations;

public sealed record Relationship(PermissionLink Relation, ResourceReference Resource, SubjectReference Subject);
