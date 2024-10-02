namespace Rubrum.Authorization.Relations;

public abstract record DefinitionReference(string Name, bool IsAll, string? Relation = null);
