using Volo.Abp.Domain.Entities;

namespace Rubrum.Platform.DataSourceService;

public abstract class DataSourceEntityProperty : Entity<Guid>
{
    protected DataSourceEntityProperty(Guid id, DataSourceEntityPropertyKind kind)
        : base(id)
    {
        Kind = kind;
    }

    protected DataSourceEntityProperty()
    {
    }

    public DataSourceEntityPropertyKind Kind { get; protected set; }

    public abstract string Name { get; protected set; }

    public bool IsNotNull { get; set; }
}
