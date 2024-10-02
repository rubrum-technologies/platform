using Volo.Abp.Domain.Repositories;

namespace Rubrum.Platform.BlobStorageService.Blobs;

public interface IBlobRepository : IRepository<Blob, Guid>;
