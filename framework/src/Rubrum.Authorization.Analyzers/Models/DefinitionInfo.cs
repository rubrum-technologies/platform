using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Rubrum.Analyzers.Models;

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
}
