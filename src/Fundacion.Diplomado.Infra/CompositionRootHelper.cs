using Fundacion.Diplomado.Domain;
using Fundacion.Diplomado.Domain.ReadModel;
using Fundacion.Diplomado.Domain.WriteModel;
using Fundacion.Diplomado.Infra.MessageBus;
using Fundacion.Diplomado.Infra.ReadModel.Adapters;

namespace Fundacion.Diplomado.Infra
{
    /// <summary>
    /// Ease the integration of the various hexagons (one for the read model, the other for the write model).
    /// </summary>
    public class CompositionRootHelper
    {
        public static ReadModelFacade BuildTheReadModelHexagon(IProveerPizzas pizzasAdapter, IProveerPizzeria pizzeriaAdapter, IProveerPedido pedidoAdapter = null, ISubscribeToEvents bus = null)
        {
            if (bus == null)
            {
                bus = new FakeBus();
            }

            if (pedidoAdapter == null)
            {
                pedidoAdapter = new ReservaAdapter(bus);
            }

            return new ReadModelFacade(pizzasAdapter, pizzeriaAdapter, pedidoAdapter, bus);
        }

        public static WriteModelFacade BuildTheWriteModelHexagon(IPedidoRepository grabarPedido, IClienteRepository manejarClientes, IPublishEvents eventPublisher, ISubscribeToEvents eventSubscriber)
        {
            var writeModelCommandHandler = new WriteModelFacade(new AlmacenarPedido(grabarPedido, manejarClientes, eventPublisher));
            CompositionRootHelper.SubscribeCommands(writeModelCommandHandler, eventSubscriber);

            return writeModelCommandHandler;
        }

        /// <summary>
        /// Subscribe the "command handler" to per-type command publication on the eventPublisher.
        /// </summary>
        /// <param name="writeModelFacade">The callback/handler provider.</param>
        /// <param name="bus">The eventPublisher to subscribe on.</param>
        private static void SubscribeCommands(WriteModelFacade writeModelFacade, ISubscribeToEvents bus)
        {
            bus.RegisterHandler<PedidoCommand>(writeModelFacade.Handle);
        }
    }
}