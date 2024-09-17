using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace Rubrum.Authorization.Analyzers.Models;

public class DefinitionInfo : SyntaxInfo
{
    public DefinitionInfo(
        ITypeSymbol typeSymbol,
        ImmutableArray<RelationInfo> relations,
        ImmutableArray<PermissionInfo> permissions)
    {
        FullName = typeSymbol.ToDisplayString();
        ClassName = typeSymbol.Name;
        Namespace = typeSymbol.ContainingNamespace.ToDisplayString();
        Relations = relations;
        Permissions = permissions;
    }

    public string FullName { get; }

    public string ClassName { get; }

    public string Namespace { get; }

    public ImmutableArray<RelationInfo> Relations { get; }

    public ImmutableArray<PermissionInfo> Permissions { get; }

    public override bool Equals(SyntaxInfo other)
        => other is DefinitionInfo info && Equals(info);

    private bool Equals(DefinitionInfo other)
    {
        if (Relations.Length != other.Relations.Length || Permissions.Length != other.Permissions.Length)
        {
            return false;
        }

        for (var i = 0; i < Relations.Length; i++)
        {
            var r1 = Relations[i];
            var r2 = other.Relations[i];

            if (!r1.Equals(r2))
            {
                return false;
            }
        }

        for (var i = 0; i < Relations.Length; i++)
        {
            var p1 = Permissions[i];
            var p2 = other.Permissions[i];

            if (!p1.Equals(p2))
            {
                return false;
            }
        }

        return true;
    }
}
