using System.Diagnostics.CodeAnalysis;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace Rubrum.Platform.DataSourceService.Database;

public class DatabaseTable : DataSourceEntity
{
    private readonly List<DatabaseColumn> _columns = [];

    public DatabaseTable(
        Guid id,
        Guid databaseSourceId,
        string name,
        string systemName,
        IEnumerable<CreateDatabaseColumn> columns)
        : base(id)
    {
        DatabaseSourceId = databaseSourceId;
        SetName(name);
        SetSystemName(systemName);

        foreach (var column in columns)
        {
            AddColumn(column.Kind, column.Name, column.SystemName);
        }

        if (_columns.Count == 0)
        {
            throw new DatabaseTableColumnsEmptyException();
        }
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    protected DatabaseTable()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }

    public Guid DatabaseSourceId { get; }

    public override string Name { get; protected set; }

    public string SystemName { get; private set; }

    public string? Schema { get; set; }

    public IReadOnlyList<DatabaseColumn> Columns => _columns.AsReadOnly();

    public override IReadOnlyList<DataSourceEntityProperty> Properties => Columns;

    public DatabaseColumn GetColumnById(Guid id)
    {
        var column = _columns.Find(c => c.Id == id);

        if (column is null)
        {
            throw new EntityNotFoundException(typeof(DatabaseColumn), id);
        }

        return column;
    }

    public DatabaseColumn AddColumn(DataSourceEntityPropertyKind kind, string name, string systemName)
    {
        ColumnNameCheck(name);
        ColumnSystemNameCheck(systemName);

        var column = new DatabaseColumn(Guid.NewGuid(), Id, kind, name, systemName);

        _columns.Add(column);

        return column;
    }

    public DatabaseColumn UpdateColumn(Guid id, string name, string systemName)
    {
        var column = GetColumnById(id);

        if (column.Name != name)
        {
            ColumnNameCheck(name);
            column.SetName(name);
        }

        if (column.SystemName != systemName)
        {
            ColumnSystemNameCheck(systemName);
            column.SetSystemName(systemName);
        }

        return column;
    }

    public void DeleteColumn(Guid id)
    {
        if (_columns.Count == 1)
        {
            throw new DatabaseTableColumnsEmptyException();
        }

        var column = GetColumnById(id);

        _columns.Remove(column);
    }

    [MemberNotNull(nameof(Name))]
    internal void SetName(string name)
    {
        Name = Check.NotNullOrWhiteSpace(name, nameof(name), DatabaseTableConstants.NameLength);
    }

    [MemberNotNull(nameof(SystemName))]
    internal void SetSystemName(string systemName)
    {
        SystemName = Check.NotNullOrWhiteSpace(systemName, nameof(systemName), DatabaseTableConstants.SystemNameLength);
    }

    private void ColumnNameCheck(string name)
    {
        if (_columns.Exists(c => c.Name == name))
        {
            throw new DatabaseColumnNameAlreadyExistsException(name);
        }
    }

    private void ColumnSystemNameCheck(string systemName)
    {
        if (_columns.Exists(c => c.SystemName == systemName))
        {
            throw new DatabaseColumnSystemNameAlreadyExistsException(systemName);
        }
    }
}
