using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace MyCompanyName.MyProjectName.EntityFrameworkCore;

[ConnectionStringName(MyProjectNameDbProperties.ConnectionStringName)]
public class MyProjectNameDbContext(DbContextOptions<MyProjectNameDbContext> options)
    : AbpDbContext<MyProjectNameDbContext>(options), IMyProjectNameDbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureMyProjectName();
    }
}
