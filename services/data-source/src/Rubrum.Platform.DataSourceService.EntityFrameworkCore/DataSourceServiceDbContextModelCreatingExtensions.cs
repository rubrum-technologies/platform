using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace Rubrum.Platform.DataSourceService.EntityFrameworkCore;

public static class DataSourceServiceDbContextModelCreatingExtensions
{
    public static void ConfigureDataSourceService(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
