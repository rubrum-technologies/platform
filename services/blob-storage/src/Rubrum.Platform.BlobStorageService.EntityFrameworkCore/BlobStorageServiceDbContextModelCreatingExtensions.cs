using Microsoft.EntityFrameworkCore;
using Rubrum.Platform.BlobStorageService.Blobs;
using Rubrum.Platform.BlobStorageService.Folders;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;
using static Rubrum.Platform.BlobStorageService.BlobStorageServiceDbProperties;

namespace Rubrum.Platform.BlobStorageService.EntityFrameworkCore;

public static class BlobStorageServiceDbContextModelCreatingExtensions
{
    public static void ConfigureBlobStorageService(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.Entity<FolderBlob>(b =>
        {
            b.ToTable(DbTablePrefix + "FoldersBlob", DbSchema);

            b.ConfigureByConvention();

            b.Property(x => x.OwnerId)
                .IsRequired();

            b.HasOne<FolderBlob>()
                .WithMany()
                .HasForeignKey(x => x.ParentId)
                .IsRequired(false);

            b.Property(x => x.Name)
                .HasMaxLength(FolderBlobConstants.NameLength)
                .IsRequired();
        });

        builder.Entity<Blob>(b =>
        {
            b.ToTable(DbTablePrefix + "Blobs", DbSchema);

            b.ConfigureByConvention();

            b.OwnsOne(x => x.Metadata, m =>
            {
                m.Property(x => x.Size)
                    .IsRequired();

                m.Property(x => x.MimeType)
                    .HasMaxLength(256)
                    .IsRequired();

                m.Property(x => x.Filename)
                    .HasMaxLength(256)
                    .IsRequired();
            });

            b.Property(x => x.OwnerId)
                .IsRequired();
        });
    }
}
