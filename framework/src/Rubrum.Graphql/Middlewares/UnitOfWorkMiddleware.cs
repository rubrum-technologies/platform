using HotChocolate.Resolvers;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Uow;

namespace Rubrum.Graphql.Middlewares;

internal class UnitOfWorkMiddleware<T>(FieldDelegate next)
{
    public async Task InvokeAsync(IMiddlewareContext context)
    {
        var unitOfWorkManager = context.Services.GetRequiredService<IUnitOfWorkManager>();
        using var uow = unitOfWorkManager.Begin(true, true);

        await next(context);

        context.Result = context.Result switch
        {
            IAsyncEnumerable<T> ae => await ae.ToListAsync(),
            ICollection<T> => context.Result,
            IEnumerable<T> e => e.ToList(),
            _ => context.Result,
        };

        await uow.CompleteAsync(context.RequestAborted);
    }
}
