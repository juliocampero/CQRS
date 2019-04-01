using System;
using Fundacion.Diplomado.Domain;

namespace Fundacion.Diplomado.Infra.MessageBus
{
    public interface IPublishToHandlers
    {
        void PublishTo<T>(Action<IMessage> handler, T @event) where T : IEvent;
    }
}