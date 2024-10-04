using Volo.Abp.Domain.Entities;

namespace Rubrum.Platform.DataSourceService;

public abstract class DataSourceEntity : Entity<Guid>
{
    protected DataSourceEntity(Guid id, Guid databaseSourceId)
        : base(id)
    {
        DatabaseSourceId = databaseSourceId;
    }

    protected DataSourceEntity()
    {
    }

    public Guid DatabaseSourceId { get; }

    public abstract string Name { get; protected set; }

    public abstract IReadOnlyList<DataSourceEntityProperty> Properties { get; }

    public DataSourceEntityProperty GetPropertyById(Guid id)
    {
        var column = Properties.FirstOrDefault(c => c.Id == id);

        if (column is null)
        {
            throw new EntityNotFoundException(typeof(DataSourceEntityProperty), id);
        }

        return column;
    }
}
