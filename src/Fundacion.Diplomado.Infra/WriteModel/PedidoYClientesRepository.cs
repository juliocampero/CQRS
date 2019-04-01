using System;
using System.Collections.Generic;
using Fundacion.Diplomado.Domain;
using Fundacion.Diplomado.Domain.WriteModel;

namespace Fundacion.Diplomado.Infra.WriteModel
{
    public class PedidoYClientesRepository : IPedidoRepository, IClienteRepository
    {
        private readonly Dictionary<string, List<Pedido>> pedidoPorClientes;
        private readonly Dictionary<string, List<ICommand>> comandosPorCliente;

        public PedidoYClientesRepository()
        {
            pedidoPorClientes = new Dictionary<string, List<Pedido>>();
            comandosPorCliente = new Dictionary<string, List<ICommand>>();
        }

        public void Grabar(Pedido pedido)
        {
            pedidoPorClientes[pedido.ClienteId].Add(pedido);
        }

        public void Grabar(ICommand comando)
        {
            var pedidoCommand = comando as PedidoCommand;
            comandosPorCliente[pedidoCommand.ClienteId].Add(comando);
        }

        public bool YaEsCliente(string clientIdentifier)
        {
            return pedidoPorClientes.ContainsKey(clientIdentifier);
        }

        public void CrearClient(string clientIdentifier)
        {
            if (!pedidoPorClientes.ContainsKey(clientIdentifier))
            { 
                pedidoPorClientes[clientIdentifier] = new List<Pedido>();
                comandosPorCliente[clientIdentifier] = new List<ICommand>();
            }
        }

        public IEnumerable<Pedido> GetPedidoDe(string clientIdentifier)
        {
            if (pedidoPorClientes.ContainsKey(clientIdentifier))
            { 
                return pedidoPorClientes[clientIdentifier];
            }

            return new List<Pedido>();
        }        
    }
}