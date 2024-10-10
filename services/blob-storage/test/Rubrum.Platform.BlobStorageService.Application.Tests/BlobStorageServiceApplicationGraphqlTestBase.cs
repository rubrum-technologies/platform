using Rubrum.Graphql;
using Volo.Abp;

namespace Rubrum.Platform.BlobStorageService;

public class BlobStorageServiceApplicationGraphqlTestBase
    : RubrumGraphqlTestBase<BlobStorageServiceApplicationTestModule>
{
    protected override void SetAbpApplicationCreationOptions(AbpApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
