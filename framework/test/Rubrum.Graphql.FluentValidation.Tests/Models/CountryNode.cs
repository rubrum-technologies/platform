using HotChocolate;
using HotChocolate.Types;
using Rubrum.Graphql.Errors;

namespace Rubrum.Graphql.Models;

[ObjectType<Country>]
public static partial class CountryNode
{
    [Query]
    public static Country GetCountry()
    {
        return new Country { Name = "Test" };
    }

    [Mutation]
    [UseAbpError]
    [UseMutationConvention]
    public static Country CreateCountry(CreateCountryInput input)
    {
        return new Country { Name = input.Name };
    }

    static partial void Configure(IObjectTypeDescriptor<Country> descriptor)
    {
        descriptor
            .Field(x => x.Id)
            .ID("Country");
    }
}
