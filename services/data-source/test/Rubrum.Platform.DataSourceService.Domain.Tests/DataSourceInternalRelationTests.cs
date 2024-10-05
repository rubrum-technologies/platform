using Rubrum.Platform.DataSourceService.Database;
using Shouldly;
using Xunit;

namespace Rubrum.Platform.DataSourceService;

public class DataSourceInternalRelationTests
{
    [Theory]
    [InlineData("Cities")]
    [InlineData("Country")]
    [InlineData("Store")]
    public void SetName(string name)
    {
        var relation = CreateRelation();

        relation.SetName(name);

        relation.Name.ShouldBe(name);
    }

    [Fact]
    public void SetName_Empty()
    {
        var relation = CreateRelation();

        Assert.Throws<ArgumentException>(() => { relation.SetName(string.Empty.PadRight(10)); });
    }

    [Fact]
    public void SetName_MaxLength()
    {
        var relation = CreateRelation();

        Assert.Throws<ArgumentException>(() =>
        {
            relation.SetName(string.Empty.PadRight(DataSourceInternalRelationConstants.NameLength + 1, 'Z'));
        });
    }

    [Theory]
    [InlineData("Cities!")]
    [InlineData("Country@")]
    [InlineData("Store#")]
    [InlineData("@!#$!$")]
    [InlineData("Test1")]
    [InlineData("=Test=")]
    public void SetName_Regex(string name)
    {
        var relation = CreateRelation();

        Assert.Throws<ArgumentException>(() => { relation.SetName(name); });
    }

    private static DataSourceInternalRelation CreateRelation()
    {
        return new DataSourceInternalRelation(
            Guid.NewGuid(),
            Guid.NewGuid(),
            DataSourceRelationDirection.OneToMany,
            "Test",
            new DataSourceInternalLink(Guid.NewGuid(), Guid.NewGuid()),
            new DataSourceInternalLink(Guid.NewGuid(), Guid.NewGuid()));
    }
}
