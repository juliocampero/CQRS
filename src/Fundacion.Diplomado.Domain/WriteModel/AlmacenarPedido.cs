using System;

namespace Fundacion.Diplomado.Domain.WriteModel
{
    public class AlmacenarPedido : IEntregarPizzas
    {
        private readonly IPedidoRepository pedidoRepository;
        private readonly IClienteRepository clienteRepository;
        private readonly IPublishEvents publicarEventos;

        public AlmacenarPedido(IPedidoRepository pedidoRepository, IClienteRepository clienteRepository,  IPublishEvents publicarEventos)
        {
            this.pedidoRepository = pedidoRepository;
            this.clienteRepository = clienteRepository;
            this.publicarEventos = publicarEventos;
        }

        public void EntregarLaPizza(PedidoCommand command)
        {
            if (!this.clienteRepository.YaEsCliente(command.ClienteId))
            {
                this.clienteRepository.CrearClient(command.ClienteId);    
            }

            Guid guid = Guid.NewGuid();
            var enviarPedido = new Pedido(guid, command.ClienteId, command.PizzeriaId, command.PizzaNombre, command.Cantidad, command.FechaDeEntrega);
            this.pedidoRepository.Grabar(enviarPedido);
            command.Guid = guid;    // Setear para saber el guid del pedido. Improve later
            this.pedidoRepository.Grabar(command);

            // we could enrich the event from here
            var pizzaEnviada = new PizzaEnviada(guid, command.PizzaNombre, command.PizzeriaId, command.ClienteId, command.Cantidad, command.FechaDeEntrega);
            this.publicarEventos.PublishTo(pizzaEnviada);
        }

    }
}
