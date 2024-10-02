using HotChocolate.Subscriptions;
using HotChocolate.Subscriptions.Diagnostics;
using Volo.Abp.DependencyInjection;

namespace Rubrum.Graphql.Subscriptions;

// TODO: Реализовать
[ExposeServices(typeof(DaprPubSub), typeof(ITopicEventSender), typeof(ITopicEventReceiver))]
public class DaprPubSub(SubscriptionOptions options, ISubscriptionDiagnosticEvents diagnosticEvents)
    : DefaultPubSub(options, diagnosticEvents), ISingletonDependency
{
    protected override async ValueTask OnSendAsync<TMessage>(
        string formattedTopic,
        TMessage message,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    protected override async ValueTask OnCompleteAsync(string formattedTopic)
    {
        throw new NotImplementedException();
    }

    protected override DefaultTopic<TMessage> OnCreateTopic<TMessage>(
        string formattedTopic,
        int? bufferCapacity,
        TopicBufferFullMode? bufferFullMode)
    {
        throw new NotImplementedException();
    }
}
