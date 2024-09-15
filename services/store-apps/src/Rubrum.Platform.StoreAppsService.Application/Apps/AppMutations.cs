using HotChocolate;
using HotChocolate.Types;

namespace Rubrum.Platform.StoreAppsService.Apps;

[MutationType]
public static class AppMutations
{
    [Mutation]
    public static async Task<CreateAppPayload> CreateAppAsync(
        CreateAppInput input,
        [Service] IAppRepository appRepository,
        [Service] AppManager appManager,
        CancellationToken cancellationToken = default)
    {
        var app = await appManager.CreateAsync(
            null,
            input.Name,
            input.Version,
            input.Enabled);

        await appRepository.InsertAsync(app);

        return new CreateAppPayload(app);
    }
}
