using Volo.Abp.Domain.Entities;

namespace Rubrum.Platform.DataSourceService;

public abstract class DataSourceEntity : Entity<Guid>
{
    protected DataSourceEntity(Guid id)
        : base(id)
    {
    }

    protected DataSourceEntity()
    {
    }

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
