using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Rubrum.EntityFrameworkCore;

public class RubrumDbContext<TDbContext>(DbContextOptions<TDbContext> options) : AbpDbContext<TDbContext>(options)
    where TDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
#if DEBUG
        optionsBuilder.EnableSensitiveDataLogging();
#endif

        base.OnConfiguring(optionsBuilder);
    }
}
