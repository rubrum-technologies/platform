using System.Diagnostics.CodeAnalysis;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace Rubrum.Platform.DataSourceService.Database;

public class DatabaseColumn : Entity<Guid>
{
    internal DatabaseColumn(
        Guid id,
        Guid tableId,
        DatabaseColumnKind kind,
        string name,
        string systemName)
        : base(id)
    {
        TableId = tableId;
        Kind = kind;
        SetName(name);
        SetSystemName(systemName);
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    protected DatabaseColumn()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }

    public Guid TableId { get; protected init; }

    public DatabaseColumnKind Kind { get; set; }

    public string Name { get; private set; }

    public string SystemName { get; private set; }

    [MemberNotNull(nameof(Name))]
    internal void SetName(string name)
    {
        Name = Check.NotNullOrWhiteSpace(name, nameof(name), DatabaseColumnConstants.NameLength);
    }

    [MemberNotNull(nameof(SystemName))]
    internal void SetSystemName(string systemName)
    {
        SystemName = Check.NotNullOrWhiteSpace(
            systemName,
            nameof(systemName),
            DatabaseColumnConstants.SystemNameLength);
    }
}
