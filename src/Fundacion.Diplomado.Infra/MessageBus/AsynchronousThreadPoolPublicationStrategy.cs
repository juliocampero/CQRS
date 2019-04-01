using System;
using System.Threading;
using Fundacion.Diplomado.Domain;

namespace Fundacion.Diplomado.Infra.MessageBus
{
    public class AsynchronousThreadPoolPublicationStrategy : IPublishToHandlers
    {
        public void PublishTo<T>(Action<IMessage> handler, T @event) where T : IEvent
        {
            //dispatch on thread pool for added awesomeness
            ThreadPool.QueueUserWorkItem(x => handler(@event));
        }
    }
}