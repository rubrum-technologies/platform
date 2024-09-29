using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace Rubrum.Authorization.Analyzers.Models;

public sealed class DefinitionInfo : ISyntaxInfo, IEquatable<DefinitionInfo>, IEqualityComparer<DefinitionInfo>
{
    public DefinitionInfo(
        ITypeSymbol typeSymbol,
        ImmutableArray<RelationInfo> relations,
        ImmutableArray<PermissionInfo> permissions)
    {
        ClassName = typeSymbol.Name;
        Namespace = typeSymbol.ContainingNamespace.ToDisplayString();
        TypeSymbol = typeSymbol;
        Relations = relations;
        Permissions = permissions;
    }

    public string ClassName { get; }

    public string Namespace { get; }

    public ITypeSymbol TypeSymbol { get; }

    public ImmutableArray<RelationInfo> Relations { get; }

    public ImmutableArray<PermissionInfo> Permissions { get; }

    public override int GetHashCode()
    {
        return HashCode.Combine(ClassName, Namespace, Relations, Permissions);
    }

    public override bool Equals(object? obj)
        => obj is DefinitionInfo info && Equals(info);

    public bool Equals(ISyntaxInfo other)
        => other is DefinitionInfo info && Equals(info);

    public bool Equals(DefinitionInfo other)
    {
        if (!TypeSymbol.Equals(other.TypeSymbol, SymbolEqualityComparer.Default))
        {
            return false;
        }

        if (Relations.Length != other.Relations.Length || Permissions.Length != other.Permissions.Length)
        {
            return false;
        }

        for (var i = 0; i < Relations.Length; i++)
        {
            var r1 = Relations[i];
            var r2 = other.Relations[i];

            if (!r1.AttributeSyntax.IsEquivalentTo(r2.AttributeSyntax))
            {
                return false;
            }
        }

        for (var i = 0; i < Relations.Length; i++)
        {
            var p1 = Permissions[i];
            var p2 = other.Permissions[i];

            if (!p1.AttributeSyntax.IsEquivalentTo(p2.AttributeSyntax))
            {
                return false;
            }
        }

        return true;
    }

    public bool Equals(DefinitionInfo x, DefinitionInfo y)
    {
        return x.Equals(y);
    }

    public int GetHashCode(DefinitionInfo obj)
    {
        return HashCode.Combine(obj.ClassName, obj.Namespace, obj.Relations, obj.Permissions);
    }
}
