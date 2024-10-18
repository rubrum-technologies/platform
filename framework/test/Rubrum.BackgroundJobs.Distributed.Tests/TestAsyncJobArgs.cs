using Volo.Abp.MultiTenancy;

namespace Rubrum.BackgroundJobs;

public class TestAsyncJobArgs: IMultiTenant
{
    public string Value { get; set; }

    public Guid? TenantId { get; }

    public TestAsyncJobArgs()
    {

    }

    public TestAsyncJobArgs(string value, Guid? tenantId = null)
    {
        Value = value;
        TenantId = tenantId;
    }
}
