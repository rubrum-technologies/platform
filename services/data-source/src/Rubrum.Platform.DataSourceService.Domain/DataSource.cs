using System.Diagnostics.CodeAnalysis;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Rubrum.Platform.DataSourceService;

public abstract class DataSource : FullAuditedAggregateRoot<Guid>, IMultiTenant
{
    protected DataSource(
        Guid id,
        Guid? tenantId,
        string name,
        string connectionString,
        string? prefix = null)
        : base(id)
    {
        TenantId = tenantId;
        SetName(name);
        SetConnectionString(connectionString);
        SetPrefix(prefix);
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    protected DataSource()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }

    public Guid? TenantId { get; }

    public string Name { get; private set; }

    public string? Prefix { get; private set; }

    public string ConnectionString { get; private set; }

    public void SetPrefix(string? prefix)
    {
        Prefix = Check.Length(prefix, nameof(prefix), DataSourceConstants.PrefixLength);
    }

    [MemberNotNull(nameof(ConnectionString))]
    public void SetConnectionString(string connectionString)
    {
        ConnectionString = Check.NotNullOrWhiteSpace(connectionString, nameof(connectionString));
    }

    [MemberNotNull(nameof(Name))]
    internal void SetName(string name)
    {
        Name = Check.NotNullOrWhiteSpace(name, nameof(name), DataSourceConstants.NameLength);
    }
}
