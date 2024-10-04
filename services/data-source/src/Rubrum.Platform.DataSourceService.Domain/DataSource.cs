using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Rubrum.Platform.DataSourceService;

public abstract class DataSource : FullAuditedAggregateRoot<Guid>, IMultiTenant
{
    private readonly List<DataSourceInternalRelation> _internalRelations = [];

    protected DataSource(
        Guid id,
        Guid? tenantId,
        string name,
        string prefix,
        string connectionString)
        : base(id)
    {
        TenantId = tenantId;
        SetName(name);
        SetPrefix(prefix);
        SetConnectionString(connectionString);
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    protected DataSource()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }

    public Guid? TenantId { get; }

    public string Name { get; private set; }

    public string Prefix { get; private set; }

    public string ConnectionString { get; private set; }

    public abstract IReadOnlyList<DataSourceEntity> Entities { get; }

    public IReadOnlyList<DataSourceInternalRelation> InternalRelations => _internalRelations;

    public DataSourceEntity GetEntityById(Guid id)
    {
        var table = Entities.FirstOrDefault(c => c.Id == id);

        if (table is null)
        {
            throw new EntityNotFoundException(typeof(DataSourceEntity), id);
        }

        return table;
    }

    public DataSourceInternalRelation GetInternalRelationById(Guid id)
    {
        var relation = _internalRelations.Find(c => c.Id == id);

        if (relation is null)
        {
            throw new EntityNotFoundException(typeof(DataSourceInternalRelation), id);
        }

        return relation;
    }

    public DataSourceInternalRelation AddInternalRelation(
        DataSourceRelationDirection direction,
        DataSourceInternalLink left,
        DataSourceInternalLink right)
    {
        InternalRelationCheck(left, right);

        var leftEntity = GetEntityById(left.EntityId);
        var leftProperty = leftEntity.GetPropertyById(left.PropertyId);

        var rightEntity = GetEntityById(right.EntityId);
        var rightProperty = rightEntity.GetPropertyById(right.PropertyId);

        var relation = new DataSourceInternalRelation(
            Guid.NewGuid(),
            Id,
            direction,
            new DataSourceInternalLink(leftEntity.Id, leftProperty.Id),
            new DataSourceInternalLink(rightEntity.Id, rightProperty.Id));

        _internalRelations.Add(relation);

        return relation;
    }

    public void DeleteInternalRelation(Guid id)
    {
        var relation = GetInternalRelationById(id);

        _internalRelations.Remove(relation);
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

    [MemberNotNull(nameof(Prefix))]
    internal void SetPrefix(string prefix)
    {
        if (!Regex.IsMatch(prefix, "^[a-zA-Z]+$"))
        {
            throw new ArgumentException(null, nameof(prefix));
        }

        Prefix = Check.NotNullOrWhiteSpace(prefix, nameof(prefix), DataSourceConstants.PrefixLength);
    }

    private void InternalRelationCheck(DataSourceInternalLink left, DataSourceInternalLink right)
    {
        if (_internalRelations.Exists(x => x.Left.Equals(left) && x.Right.Equals(right)))
        {
            throw new DataSourceInternalRelationAlreadyExistsException();
        }
    }

    //internal abstract bool CheckExternalRelation();
}
