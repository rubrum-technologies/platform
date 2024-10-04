using Volo.Abp.Domain.Entities;

namespace Rubrum.Platform.DataSourceService;

public class DataSourceExternalRelation : Entity<Guid>
{
    public Guid LeftDataSourceId { get; }

    public Guid RightDataSourceId { get; }

    public string LeftEntityName { get; }

    public string RightEntityName { get; }

    public string LeftPropertyName { get; }

    public string RightPropertyName { get; }
}
