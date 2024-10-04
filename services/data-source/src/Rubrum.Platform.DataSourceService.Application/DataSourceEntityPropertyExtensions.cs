using HotChocolate.Types.Descriptors;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Rubrum.Platform.DataSourceService.Database;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Rubrum.Platform.DataSourceService;

public static class DataSourceEntityPropertyExtensions
{
    public static SyntaxTypeReference ToGraphqlType(this DataSourceEntityProperty property)
    {
        var type = property.Kind switch
        {
            DataSourceEntityPropertyKind.Boolean =>"Boolean",
            DataSourceEntityPropertyKind.Int => "Int",
            DataSourceEntityPropertyKind.Float => "Float",
            DataSourceEntityPropertyKind.String => "String",
            DataSourceEntityPropertyKind.Uuid => "UUID",
            DataSourceEntityPropertyKind.DateTime => "DateTime",
            DataSourceEntityPropertyKind.Unknown => "Any",
            _ => throw new ArgumentOutOfRangeException(nameof(property), property, null),
        };

        if (property.IsNotNull)
        {
            type += "!";
        }

        return TypeReference.Parse(type);
    }

    public static PropertyDeclarationSyntax ToPropertySyntax(this DataSourceEntityProperty property)
    {
        return PropertyDeclaration(property.ParseTypeName(), property.Name);
    }

    public static TypeSyntax ParseTypeName(this DataSourceEntityProperty property)
    {
        return property.Kind switch {
            DataSourceEntityPropertyKind.Boolean => SyntaxFactory.ParseTypeName(nameof(Boolean)),
            DataSourceEntityPropertyKind.Int => SyntaxFactory.ParseTypeName(nameof(Int32)),
            DataSourceEntityPropertyKind.Float => SyntaxFactory.ParseTypeName(nameof(Double)),
            DataSourceEntityPropertyKind.String => SyntaxFactory.ParseTypeName(nameof(String)),
            DataSourceEntityPropertyKind.Uuid => SyntaxFactory.ParseTypeName(nameof(Guid)),
            DataSourceEntityPropertyKind.DateTime => SyntaxFactory.ParseTypeName(nameof(DateTime)),
            DataSourceEntityPropertyKind.Unknown => SyntaxFactory.ParseTypeName(nameof(Object)),
            _ => throw new ArgumentOutOfRangeException(nameof(property), property, null)
        };
    }
}
