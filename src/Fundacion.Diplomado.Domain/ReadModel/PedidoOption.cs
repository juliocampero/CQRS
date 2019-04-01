using System.Collections.Generic;
using System.Linq;

namespace Fundacion.Diplomado.Domain.ReadModel
{
    public class PedidoOption
    {
        public Pizzeria Pizzeria { get; }

        public IEnumerable<PizzaConPrecios> DisponiblesPizzasConPrecios { get; }

        public PedidoOption(Pizzeria pizzeria, IEnumerable<PizzaConPrecios> availablePizzasConPrecios)
        {
            this.Pizzeria = pizzeria;
            this.DisponiblesPizzasConPrecios = availablePizzasConPrecios;
        }

        public override string ToString()
        {
            return $"Pedido option de la pizzeria: '{this.Pizzeria}' - {this.DisponiblesPizzasConPrecios.Count()} posible cantidades";
        }
    }
}
