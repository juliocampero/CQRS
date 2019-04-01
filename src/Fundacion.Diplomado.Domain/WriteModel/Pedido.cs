using System;

namespace Fundacion.Diplomado.Domain.WriteModel
{
    public class Pedido
    {
        public static Pedido Null { get; } = new NullPedido();

        // We provide getters only so that the state of this domain object is only changed via one of its operations (methods)
        public Guid PedidoId { get; }
        public string ClienteId { get; }
        public int PizzeriaId { get; }
        public string PizzaNombre { get; }
        public DateTime FechaDeEntrega { get; }
        public int Cantidad { get; }
        public bool EsCancelado { get; private set; }

        public Pedido(Guid pepidoId, string clienteId, int pizeriaId, string pizzaName, int cantidad, DateTime fechaDeEntrega)
        {
            this.PedidoId = pepidoId;
            this.ClienteId = clienteId;
            this.PizzeriaId = pizeriaId;
            this.PizzaNombre = pizzaName;
            this.Cantidad = cantidad;
            this.FechaDeEntrega = fechaDeEntrega;
        }
        
        public virtual bool EsParaCliente(string clienteId)
        {
            if (this.ClienteId == clienteId)
            {
                return true;
            }

            return false;
        }
        
        public virtual void Cancelar()
        {
            this.EsCancelado = true;
        }

        private class NullPedido : Pedido
        {
            public NullPedido() : base(Guid.Empty, string.Empty, 0, string.Empty, 0, DateTime.Now)
            {
            }

            public override bool EsParaCliente(string clientId)
            {
                return false;
            }

            public override void Cancelar()
            {
            }
        }
    }
}