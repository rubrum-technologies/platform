using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Rubrum.EntityFrameworkCore;

public class RubrumDbContext<TDbContext>(DbContextOptions<TDbContext> options) : AbpDbContext<TDbContext>(options)
    where TDbContext : DbContext;
