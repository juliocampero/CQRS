using System;

namespace Fundacion.Diplomado.Domain.ReadModel
{
    public class Reservacion
    {
        public Guid Guid { get; private set; }
        public string ClienteId { get; }
        public int PizzeriaId { get; }
        public string PizzaName { get; set; }
        public int Cantidad { get; }
        public DateTime FechaDeEntrega { get; }

        public Reservacion(Guid guid, string clientId, string pizzaName, int pizeriaId, int amount, DateTime fechaDeEntrega)
        {
            this.Guid = guid;
            this.ClienteId = clientId;
            this.PizzaName = pizzaName;
            this.PizzeriaId = pizeriaId;
            this.Cantidad = amount;
            this.FechaDeEntrega = fechaDeEntrega;
        }

        public override string ToString()
        {
            return $"Pedido de:{ClienteId} de la pizza:{PizzaName} (id:{PizzeriaId}), Cantidad:{Cantidad} para entregar en:{FechaDeEntrega.ToString("d")}";
        }
    }
}