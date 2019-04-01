using System;

namespace Fundacion.Diplomado.Domain.WriteModel
{
    public class PedidoCommand : ICommand
    {
        public string ClienteId { get; }
        public int PizzeriaId { get; }
        public string PizzaNombre { get; }
        public int Cantidad { get; }
        public DateTime FechaDeEntrega { get; }
        public Guid Guid { get; set; }

        public PedidoCommand(string clienteId, int pizzeriaId, string pizzaNombre, int cantidad, DateTime fechaDeEntrega)
        {
            this.ClienteId = clienteId;
            this.PizzaNombre = pizzaNombre;
            this.PizzeriaId = pizzeriaId;
            this.Cantidad = cantidad;
            this.FechaDeEntrega = fechaDeEntrega;
        }
    }
}