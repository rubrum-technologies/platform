namespace Rubrum.Authorization.Relations;

public sealed record SubjectReference(string Type, string Id) : ObjectReference(Type, Id);
