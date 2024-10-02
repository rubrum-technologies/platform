using System.Threading.Channels;
using HotChocolate.Subscriptions;
using HotChocolate.Subscriptions.Diagnostics;

namespace Rubrum.Graphql.Subscriptions;

// TODO: Реализовать
public class DaprTopic<T> : DefaultTopic<T>
{
    public DaprTopic(
        string name,
        int capacity,
        TopicBufferFullMode fullMode,
        ISubscriptionDiagnosticEvents diagnosticEvents)
        : base(name, capacity, fullMode, diagnosticEvents)
    {
    }

    public DaprTopic(
        string name,
        int capacity,
        TopicBufferFullMode fullMode,
        ISubscriptionDiagnosticEvents diagnosticEvents,
        Channel<T> incomingMessages)
        : base(name, capacity, fullMode, diagnosticEvents, incomingMessages)
    {
    }
}
