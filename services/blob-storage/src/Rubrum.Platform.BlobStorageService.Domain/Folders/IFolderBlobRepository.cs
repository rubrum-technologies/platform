using Volo.Abp.Domain.Repositories;

namespace Rubrum.Platform.BlobStorageService.Folders;

public interface IFolderBlobRepository : IRepository<FolderBlob, Guid>;
