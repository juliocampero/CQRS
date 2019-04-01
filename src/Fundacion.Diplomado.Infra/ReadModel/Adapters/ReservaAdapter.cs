using System;
using System.Collections.Generic;
using Fundacion.Diplomado.Domain.ReadModel;
using Fundacion.Diplomado.Domain.WriteModel;
using ISubscribeToEvents = Fundacion.Diplomado.Domain.ReadModel.ISubscribeToEvents;

namespace Fundacion.Diplomado.Infra.ReadModel.Adapters
{
    public class ReservaAdapter : IProveerPedido
    {
        private readonly ISubscribeToEvents eventsSubscriber;
        private readonly Dictionary<string, List<Reservacion>> perClientReservations = new Dictionary<string, List<Reservacion>>();

        public ReservaAdapter(ISubscribeToEvents eventsSubscriber)
        {
            this.eventsSubscriber = eventsSubscriber;
            this.eventsSubscriber.RegisterHandler<PizzaEnviada>(Handle);
        }

        private void Handle(PizzaEnviada @event)
        {
            if (!this.perClientReservations.ContainsKey(@event.ClienteId))
            {
                this.perClientReservations[@event.ClienteId] = new List<Reservacion>();
            }

            var reservation = new Reservacion(@event.Guid, @event.ClienteId, @event.PizzaNombre, @event.PizzeriaId, @event.Cantidad, @event.FechaDeEntrega);
            this.perClientReservations[@event.ClienteId].Add(reservation);
        }

        public IEnumerable<Reservacion> GetReservacionesDe(string clientId)
        {
            List<Reservacion> result;
            this.perClientReservations.TryGetValue(clientId, out result);

            if (result == null)
            {
                result = new List<Reservacion>();    
            }

            return result;
        }
    }
}