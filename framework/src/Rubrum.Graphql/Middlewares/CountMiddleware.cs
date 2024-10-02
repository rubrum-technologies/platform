using HotChocolate.Resolvers;

namespace Rubrum.Graphql.Middlewares;

internal class CountMiddleware<T>(FieldDelegate next)
    where T : class
{
    public async Task InvokeAsync(IMiddlewareContext context)
    {
        await next(context);

        context.Result = context.Result switch
        {
            IAsyncEnumerable<T> ae => await ae.CountAsync(),
            IEnumerable<T> en => await Task.Run(() => en.Count(), context.RequestAborted),
            _ => context.Result,
        };
    }
}
