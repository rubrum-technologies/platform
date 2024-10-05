using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace Rubrum.Platform.DataSourceService;

public class DataSourceInternalRelation : Entity<Guid>
{
    public DataSourceInternalRelation(
        Guid id,
        Guid dataSourceId,
        DataSourceRelationDirection direction,
        string name,
        DataSourceInternalLink left,
        DataSourceInternalLink right)
        : base(id)
    {
        DataSourceId = dataSourceId;
        Direction = direction;
        SetName(name);
        Left = left;
        Right = right;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    protected DataSourceInternalRelation()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }

    public Guid DataSourceId { get; }

    public DataSourceRelationDirection Direction { get; }

    public string Name { get; protected set; }

    public DataSourceInternalLink Left { get; }

    public DataSourceInternalLink Right { get; }

    [MemberNotNull(nameof(Name))]
    internal void SetName(string name)
    {
        if (!Regex.IsMatch(name, "^[a-zA-Z]+$"))
        {
            throw new ArgumentException(null, nameof(name));
        }

        Name = Check.NotNullOrWhiteSpace(name, nameof(name), DataSourceInternalRelationConstants.NameLength);
    }
}
