using System.Diagnostics.CodeAnalysis;
using Volo.Abp;

namespace Rubrum.Platform.DataSourceService.Database;

public class DatabaseColumn : DataSourceEntityProperty
{
    internal DatabaseColumn(
        Guid id,
        Guid tableId,
        DataSourceEntityPropertyKind kind,
        string name,
        string systemName)
        : base(id, kind)
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

    public override string Name { get; protected set; }

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
