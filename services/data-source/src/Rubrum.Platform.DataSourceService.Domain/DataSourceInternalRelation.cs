using Volo.Abp.Domain.Entities;

namespace Rubrum.Platform.DataSourceService;

public class DataSourceInternalRelation : Entity<Guid>
{
    public DataSourceInternalRelation(
        Guid id,
        Guid dataSourceId,
        DataSourceRelationDirection direction,
        DataSourceInternalLink left,
        DataSourceInternalLink right)
        : base(id)
    {
        DataSourceId = dataSourceId;
        Direction = direction;
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

    public DataSourceInternalLink Left { get; }

    public DataSourceInternalLink Right { get; }
}
