using System;

namespace Fundacion.Diplomado.Domain.WriteModel
{
    public class PizzaEnviada : IEvent
    {
        public Guid Guid { get; }
        public string PizzaNombre { get; }
        public int PizzeriaId { get; }
        public string ClienteId { get; }
        public int Cantidad { get; }
        public DateTime FechaDeEntrega { get; }

        public PizzaEnviada(Guid guid, string pizzaName, int pizeriaId, string clientId, int amount, DateTime deliveredDate)
        {
            this.Guid = guid;
            this.PizzaNombre = pizzaName;
            this.PizzeriaId = pizeriaId;
            this.ClienteId = clientId;
            this.Cantidad = amount;
            this.FechaDeEntrega = deliveredDate;
        }
    }
}