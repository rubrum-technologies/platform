using HotChocolate.Types;

namespace Rubrum.Authorization.Analyzers.Models;

[QueryType]
public static class Query
{
    public static Document GetDocument()
    {
        return new Document
        {
            Name = string.Empty,
        };
    }
}
