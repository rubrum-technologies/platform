using System.Linq.Expressions;
using System.Reflection;
using LinqToDB;
using LinqToDB.Mapping;
using Volo.Abp.DependencyInjection;

namespace Rubrum.Platform.DataSourceService.Database;

#nullable disable

public class DatabaseSourceDataOptionsBuilder(
    IDataSourceTypesManager typesManager) : IDatabaseSourceDataOptionsBuilder, ITransientDependency
{
    protected static readonly MethodInfo GetEntity = typeof(FluentMappingBuilder)
        .GetMethod("Entity");

    protected static readonly Func<Type, MethodInfo> HasTableName = type => type.GetMethod("HasTableName");

    protected static readonly Func<Type, MethodInfo> HasSchemaName = type => type.GetMethod("HasSchemaName");

    protected static readonly Func<Type, MethodInfo> GetProperty = type => type.GetMethod("Property");

    protected static readonly Func<Type, MethodInfo> HasColumnName = type => type.GetMethod("HasColumnName");

    protected static readonly Func<Type, MethodInfo> AssociationOneToMany = type =>
        type.GetMethods()
            .First(x => x.Name == "Association" &&
                x.GetGenericArguments().Length == 3 &&
                x.GetParameters().Length == 4);

    protected static readonly Func<Type, MethodInfo> AssociationManyToOne = type =>
        type.GetMethods()
            .First(x => x.Name == "Association" &&
                x.GetGenericArguments().Length == 1 &&
                x.GetParameters().Length == 3);

    protected static readonly MethodInfo Lambda = typeof(Expression).GetMethods().First(FilterLambda);

    public Task<DataOptions> BuildAsync(DatabaseSource source)
    {
        var builder = new FluentMappingBuilder();

        foreach (var table in source.Tables)
        {
            var type = typesManager.GetType(table);
            var entityBuilder = CreateEntityBuilder(builder, type, table);

            foreach (var column in table.Columns)
            {
                CreatePropertyBuilder(entityBuilder, type, column);
            }

            foreach (var relation in source.InternalRelations.Where(x => x.Left.EntityId == table.Id))
            {
                var leftTable = source.GetTableById(relation.Left.EntityId);
                var rightTable = source.GetTableById(relation.Right.EntityId);

                CreateLink(
                    entityBuilder,
                    leftTable,
                    typesManager.GetType(leftTable),
                    rightTable,
                    typesManager.GetType(rightTable),
                    relation);
            }
        }

        builder.Build();

        var options = new DataOptions()
            .UseMappingSchema(builder.MappingSchema);

        options = source.Kind switch
        {
            DatabaseKind.MySql => options.UseMySql(source.ConnectionString),
            DatabaseKind.Postgresql => options.UseMySql(source.ConnectionString),
            DatabaseKind.SqlServer => options.UseMySql(source.ConnectionString),
            _ => throw new ArgumentOutOfRangeException(nameof(source)),
        };

        return Task.FromResult(options);
    }

    private static object CreateEntityBuilder(FluentMappingBuilder builder, Type type, DatabaseTable table)
    {
        var entity = GetEntity.MakeGenericMethod(type).Invoke(builder, [null])!;
        HasTableName(entity.GetType()).Invoke(entity, [table.SystemName]);
        HasSchemaName(entity.GetType()).Invoke(entity, [table.Schema]);

        return entity;
    }

    private static void CreatePropertyBuilder(object entityBuilder, Type type, DatabaseColumn column)
    {
        var propertyType = type.GetProperty(column.Name)!.PropertyType;
        var lambda = CreateLambda(type, propertyType, column.Name);

        var propertyBuilder = GetProperty(entityBuilder.GetType())
            .MakeGenericMethod(propertyType)
            .Invoke(entityBuilder, [lambda])!;

        HasColumnName(propertyBuilder.GetType()).Invoke(propertyBuilder, [column.SystemName]);
    }

    private static void CreateLink(
        object entityBuilder,
        DatabaseTable leftTable,
        Type leftType,
        DatabaseTable rightTable,
        Type rightType,
        DataSourceInternalRelation relation)
    {
        var leftColumn = leftTable.GetColumnById(relation.Left.PropertyId);
        var leftPropertyType = leftType.GetProperty(leftColumn.Name)!.PropertyType;

        var rightColumn = rightTable.GetColumnById(relation.Right.PropertyId);
        var rightPropertyType = rightType.GetProperty(rightColumn.Name)!.PropertyType;

        if (relation.Direction == DataSourceRelationDirection.OneToMany)
        {
            var leftLambda = CreateLambda(leftType, leftPropertyType, leftColumn.Name);
            var rightLambda = CreateLambda(rightType, rightPropertyType, rightColumn.Name);
            var relationLambda = CreateLambda(leftType, rightType, relation.Name);

            AssociationOneToMany(entityBuilder.GetType())
                .MakeGenericMethod(rightType, leftPropertyType, rightPropertyType)
                .Invoke(entityBuilder, [relationLambda, leftLambda, rightLambda, true]);
        }
        else if (relation.Direction == DataSourceRelationDirection.ManyToOne)
        {
            var relationLambda = CreateLambda(leftType, typeof(IEnumerable<>).MakeGenericType(rightType), relation.Name);

            var func = typeof(Func<,,>).MakeGenericType(leftType, rightType, typeof(bool));
            var leftParameter = Expression.Parameter(leftType, "a");
            var rightParameter = Expression.Parameter(rightType, "b");

            var equal = Expression.Equal(
                Expression.Property(leftParameter, leftColumn.Name),
                Expression.Property(rightParameter, rightColumn.Name));

            var predicate = Lambda.MakeGenericMethod(func)
                .Invoke(null, [equal, new[] { leftParameter, rightParameter }]);

            AssociationManyToOne(entityBuilder.GetType())
                .MakeGenericMethod(rightType)
                .Invoke(entityBuilder, [relationLambda, predicate, true]);
        }
    }

    private static object CreateLambda(Type type, Type propertyType, string propertyName)
    {
        var func = typeof(Func<,>).MakeGenericType(type, propertyType);
        var parameter = Expression.Parameter(type, "x");
        return Lambda.MakeGenericMethod(func)
            .Invoke(null, [Expression.Property(parameter, propertyName), new[] { parameter }]);
    }

    private static bool FilterLambda(MethodInfo info)
    {
        var parameters = info.GetParameters();

        if (info.Name != "Lambda" || parameters.Length != 2)
        {
            return false;
        }

        return
            info.GetGenericArguments().Length == 1 &&
            parameters[0].ParameterType == typeof(Expression) &&
            parameters[1].ParameterType == typeof(ParameterExpression[]);
    }
}
