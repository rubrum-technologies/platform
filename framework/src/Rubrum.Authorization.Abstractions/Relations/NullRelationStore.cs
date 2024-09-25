using Volo.Abp.DependencyInjection;
using Volo.Abp.Threading;

namespace Rubrum.Authorization.Relations;

public class NullRelationStore : IRelationStore, ITransientDependency
{
    public Task<bool> IsGrantedAsync(
        PermissionLink permission,
        ResourceReference resource,
        SubjectReference subject,
        CancellationToken ct = default)
    {
        return TaskCache.TrueResult;
    }

    public Task<bool> IsGrantedWithUserAsync(
        PermissionLink permission,
        ResourceReference resource,
        CancellationToken ct = default)
    {
        return TaskCache.TrueResult;
    }

    public Task GiveGrantAsync(
        Relation relation,
        ResourceReference resource,
        SubjectReference subject,
        CancellationToken ct = default)
    {
        return Task.CompletedTask;
    }

    public Task GiveGrantWithUserAsync(
        Relation relation,
        ResourceReference resource,
        CancellationToken ct = default)
    {
        return Task.CompletedTask;
    }
}
