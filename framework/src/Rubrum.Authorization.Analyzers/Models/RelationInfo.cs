using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace Rubrum.Authorization.Analyzers.Models;

public sealed class RelationInfo
{
    public RelationInfo(AttributeData attributeData)
    {
        PropertyName = attributeData.ConstructorArguments[0]
            .Value!
            .ToString()
            .Trim('"');
        ClassName = $"{PropertyName}Relation";

        Values = GetValues(attributeData);
        AttributeData = attributeData;
    }

    public string ClassName { get; }

    public string PropertyName { get; }

    public ImmutableArray<ITypeSymbol> Values { get; }

    public AttributeData AttributeData { get; }

    private static ImmutableArray<ITypeSymbol> GetValues(AttributeData attributeData)
    {
        var builder = ImmutableArray.CreateBuilder<ITypeSymbol>();

        foreach (var typedConstant in attributeData.ConstructorArguments[1].Values)
        {
            if (typedConstant.Value is ITypeSymbol typeSymbol)
            {
                builder.Add(typeSymbol);
            }
        }

        return builder.ToImmutable();
    }
}
