using Volo.Abp.Domain.Entities;

namespace Rubrum.Platform.DataSourceService.Database;

public class DatabaseSource : DataSource
{
    private readonly List<DatabaseTable> _tables = [];

    internal DatabaseSource(
        Guid id,
        Guid? tenantId,
        DatabaseKind kind,
        string name,
        string prefix,
        string connectionString,
        IEnumerable<CreateDatabaseTable> tables)
        : base(id, tenantId, name, prefix, connectionString)
    {
        Kind = kind;

        foreach (var table in tables)
        {
            AddTable(table.Name, table.SystemName, table.Columns);
        }

        if (_tables.Count == 0)
        {
            throw new DatabaseSourceTablesEmptyException();
        }
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    protected DatabaseSource()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }

    public DatabaseKind Kind { get; }

    public IReadOnlyList<DatabaseTable> Tables => _tables.AsReadOnly();

    public override IReadOnlyList<DataSourceEntity> Entities => Tables;

    public DatabaseTable GetTableById(Guid id)
    {
        var table = _tables.Find(c => c.Id == id);

        if (table is null)
        {
            throw new EntityNotFoundException(typeof(DatabaseTable), id);
        }

        return table;
    }

    public DatabaseTable AddTable(string name, string systemName, IEnumerable<CreateDatabaseColumn> columns)
    {
        TableNameCheck(name);
        TableSystemNameCheck(systemName);

        var table = new DatabaseTable(Guid.NewGuid(), Id, name, systemName, columns);

        _tables.Add(table);

        return table;
    }

    public DatabaseTable UpdateTable(Guid id, string name, string systemName)
    {
        var table = GetTableById(id);

        if (table.Name != name)
        {
            TableNameCheck(name);
            table.SetName(name);
        }

        if (table.SystemName != systemName)
        {
            TableSystemNameCheck(systemName);
            table.SetSystemName(systemName);
        }

        return table;
    }

    public void DeleteTable(Guid id)
    {
        if (_tables.Count == 1)
        {
            throw new DatabaseSourceTablesEmptyException();
        }

        var table = GetTableById(id);

        _tables.Remove(table);
    }

    private void TableNameCheck(string name)
    {
        if (_tables.Exists(c => c.Name == name))
        {
            throw new DatabaseTableNameAlreadyExistsException(name);
        }
    }

    private void TableSystemNameCheck(string systemName)
    {
        if (_tables.Exists(c => c.SystemName == systemName))
        {
            throw new DatabaseTableSystemNameAlreadyExistsException(systemName);
        }
    }
}
