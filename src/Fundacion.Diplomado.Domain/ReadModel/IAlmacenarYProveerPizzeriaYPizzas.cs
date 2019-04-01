using System;
using System.Collections.Generic;

namespace Fundacion.Diplomado.Domain.ReadModel
{
    public interface IAlmacenarYProveerPizzeriaYPizzas : IProveerPizzeria
    {
        IEnumerable<Pizzeria> Pizzerias { get; }

        IEnumerable<PedidoOption> BuscarPizzeriasDisponiblesEnInsensitiveWay(string direccion, DateTime fechaPedido);

        void StorePizzeria(int pizzeriaId, Pizzeria pizzeria);
        void StorePizzeroaDisponibles(Pizzeria pizzeria, Dictionary<DateTime, List<PizzaConPrecios>> porFechaPizzasDisponibles);

        void DeclararPizzaEntregada(int pizzeriaId, int cantidad, DateTime fechaDeEntrega);
    }
}
