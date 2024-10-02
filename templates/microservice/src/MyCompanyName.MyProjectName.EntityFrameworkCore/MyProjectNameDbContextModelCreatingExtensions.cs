using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace MyCompanyName.MyProjectName.EntityFrameworkCore;

public static class MyProjectNameDbContextModelCreatingExtensions
{
    public static void ConfigureMyProjectName(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
