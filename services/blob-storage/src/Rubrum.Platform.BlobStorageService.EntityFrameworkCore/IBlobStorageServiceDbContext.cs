using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Rubrum.Platform.BlobStorageService.EntityFrameworkCore;

[ConnectionStringName(BlobStorageServiceDbProperties.ConnectionStringName)]
public interface IBlobStorageServiceDbContext : IEfCoreDbContext;
