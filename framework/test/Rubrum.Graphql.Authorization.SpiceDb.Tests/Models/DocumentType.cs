using HotChocolate.Types;
using Rubrum.Graphql.Ddd;
using Rubrum.Graphql.Relations;

namespace Rubrum.Authorization.Analyzers.Models;

[ObjectType<Document>]
public static partial class DocumentType
{
    static partial void Configure(IObjectTypeDescriptor<Document> descriptor)
    {
        descriptor.Entity();

        descriptor
            .AddRelation(DocumentDefinition.Reader)
            .AddRelation(DocumentDefinition.Writer)
            .AddPermission(DocumentDefinition.View)
            .AddPermission(DocumentDefinition.Edit);
    }
}
