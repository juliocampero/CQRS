using System;
using System.Collections.Generic;

namespace Fundacion.Diplomado.Domain.ReadModel
{
    /// <summary>
    /// Allow to search PedidoOptions or to get details about Pizzerias.
    /// </summary>
    public class ReadModelFacade : IQueryPedidoOpciones, IProveerPizzeria, IProveerPedido
    {
        // TODO: question: find a domain name instead or keep focus on the CQRS pattern to ease understanding of the MS experiences'16 audience?

        private readonly IProveerPizzas pizzasProvider;
        private readonly IProveerPizzeria pizzeriasProvider;
        private readonly IProveerPedido pedidosProvider;

        public ReadModelFacade(IProveerPizzas pizzasProvider, IProveerPizzeria pizzeriaProvider, IProveerPedido pedidoProvider, ISubscribeToEvents bus)
        {
            this.pizzasProvider = pizzasProvider;
            this.pizzeriasProvider = pizzeriaProvider;
            this.pedidosProvider = pedidoProvider;
        }

        public IEnumerable<PedidoOption> BuscarPedidoOpciones(BuscarPedidoOpciones query)
        {
            return this.SearchBookingOptions(query.fechaDeEntrega, query.Direccion, query.nombrePizza, query.Cantidad);
        }

        private IEnumerable<PedidoOption> SearchBookingOptions(DateTime fechaDeEntrega, string direccion, string pizzaNombre, int cantidad =1)
        {
            return this.pizzasProvider.BuscarPizzeriasDisponiblesEnInsensitiveWay(direccion, fechaDeEntrega);
        }

        public IEnumerable<Pizzeria> BuscarDireccion(string location)
        {
            throw new NotImplementedException();
        }

        public Pizzeria GetPizzeria(int pizzeriaId)
        {
            return this.pizzeriasProvider.GetPizzeria(pizzeriaId);
        }

        public IEnumerable<Reservacion> GetReservacionesDe(string clientId)
        {
            return this.pedidosProvider.GetReservacionesDe(clientId);
        }
    }
}