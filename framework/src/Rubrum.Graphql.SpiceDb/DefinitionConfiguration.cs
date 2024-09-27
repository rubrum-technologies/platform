namespace Rubrum.Graphql.SpiceDb;

internal sealed class DefinitionConfiguration(string name)
{
    private readonly List<RelationConfiguration> _relations = [];
    private readonly List<PermissionConfiguration> _permissions = [];

    public string Name { get; } = name.ToSnakeCase();

    public IReadOnlyList<RelationConfiguration> Relations => _relations.AsReadOnly();

    public IReadOnlyList<PermissionConfiguration> Permissions => _permissions.AsReadOnly();

    public void AddRelation(string name, string value)
    {
        _relations.Add(new RelationConfiguration(name.ToSnakeCase(), value.ToSnakeCase()));
    }

    public void AddPermission(string name, string value)
    {
        _permissions.Add(new PermissionConfiguration(name.ToSnakeCase(), value.ToSnakeCase()));
    }
}
