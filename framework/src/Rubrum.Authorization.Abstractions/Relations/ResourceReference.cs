namespace Rubrum.Authorization.Relations;

public sealed record ResourceReference(string Type, string Id) : ObjectReference(Type, Id);
