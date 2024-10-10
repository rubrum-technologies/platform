using Rubrum.Auditing;
using Rubrum.Modularity;
using Volo.Abp.BlobStoring;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Rubrum.Platform.BlobStorageService;

[DependsOn<AbpDddDomainModule>]
[DependsOn<AbpBlobStoringModule>]
[DependsOn<RubrumAuditingContractsModule>]
public class BlobStorageServiceDomainModule : AbpModule;
