using Microsoft.EntityFrameworkCore;
using Rubrum.Platform.DataSourceService.Database;
using Rubrum.Platform.DataSourceService.Graphql;
using Rubrum.Platform.DataSourceService.Grpc;
using Rubrum.Platform.DataSourceService.OData;
using Rubrum.Platform.DataSourceService.OpenApi;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;
using static Rubrum.Platform.DataSourceService.DataSourceServiceDbProperties;

namespace Rubrum.Platform.DataSourceService.EntityFrameworkCore;

public static class DataSourceServiceDbContextModelCreatingExtensions
{
    public static void ConfigureDataSourceService(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.Ignore<DataSourceEntityProperty>();
        builder.Ignore<DataSourceEntity>();

        builder.Entity<DataSource>(b =>
        {
            b.ToTable(DbTablePrefix + "DataSources", DbSchema);

            b.ConfigureByConvention();

            b.Ignore(x => x.Entities);

            b.Navigation(x => x.InternalRelations)
                .HasField("_internalRelations")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .AutoInclude();

            b.Property(x => x.Name)
                .HasMaxLength(DataSourceConstants.NameLength)
                .IsRequired();

            b.Property(x => x.Prefix)
                .HasMaxLength(DataSourceConstants.PrefixLength)
                .IsRequired();

            b.Property(x => x.ConnectionString)
                .IsRequired();
        });

        builder.Entity<DataSourceInternalRelation>(b =>
        {
            b.ToTable(DbTablePrefix + "DataSourceInternalRelations", DbSchema);

            b.ConfigureByConvention();

            b.HasOne<DataSource>()
                .WithMany(x => x.InternalRelations)
                .HasForeignKey(x => x.DataSourceId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.Property(x => x.Direction)
                .IsRequired();

            b.Property(x => x.Name)
                .HasMaxLength(DataSourceInternalRelationConstants.NameLength)
                .IsRequired();

            b.ComplexProperty(x => x.Left);
            b.ComplexProperty(x => x.Right);
        });

        builder.Entity<GraphqlSource>();

        builder.Entity<GrpcSource>();

        builder.Entity<ODataSource>();

        builder.Entity<OpenApiSource>();

        builder.Entity<DatabaseSource>(b =>
        {
            b.Navigation(x => x.Tables)
                .HasField("_tables")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .AutoInclude();

            b.Property(x => x.Kind)
                .IsRequired();
        });

        builder.Entity<DatabaseTable>(b =>
        {
            b.ToTable(DbTablePrefix + "DatabaseTables", DbSchema);

            b.ConfigureByConvention();

            b.Ignore(x => x.Properties);

            b.HasOne<DatabaseSource>()
                .WithMany(x => x.Tables)
                .HasForeignKey(x => x.DatabaseSourceId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.Navigation(x => x.Columns)
                .HasField("_columns")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .AutoInclude();

            b.Property(x => x.Name)
                .HasMaxLength(DatabaseTableConstants.NameLength)
                .IsRequired();

            b.Property(x => x.SystemName)
                .HasMaxLength(DatabaseTableConstants.SystemNameLength)
                .IsRequired();
        });

        builder.Entity<DatabaseColumn>(b =>
        {
            b.ToTable(DbTablePrefix + "DatabaseColumns", DbSchema);

            b.ConfigureByConvention();

            b.HasOne<DatabaseTable>()
                .WithMany(x => x.Columns)
                .HasForeignKey(x => x.TableId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.Property(x => x.Name)
                .HasMaxLength(DatabaseColumnConstants.NameLength)
                .IsRequired();

            b.Property(x => x.SystemName)
                .HasMaxLength(DatabaseColumnConstants.SystemNameLength)
                .IsRequired();
        });
    }
}
