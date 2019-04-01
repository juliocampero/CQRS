using System;
using System.Collections.Generic;
using System.Linq;
using Fundacion.Diplomado.Domain.ReadModel;

namespace Fundacion.Diplomado.Infra.ReadModel
{
    public class PizzeriasYPizzasRepository : IAlmacenarYProveerPizzeriaYPizzas
    {
        public readonly Dictionary<Pizzeria, Dictionary<DateTime, List<PizzaConPrecios>>> pizzeriasConFechaPizzasStatus;
        private readonly Dictionary<int, Pizzeria> pizzeriasPorId = new Dictionary<int, Pizzeria>();

        public PizzeriasYPizzasRepository()
        {
            this.pizzeriasConFechaPizzasStatus = new Dictionary<Pizzeria, Dictionary<DateTime, List<PizzaConPrecios>>>();
        }

        public IEnumerable<Pizzeria> Pizzerias => this.pizzeriasConFechaPizzasStatus.Keys;

        public IEnumerable<PedidoOption> BuscarPizzeriasDisponiblesEnInsensitiveWay(string direccion, DateTime fechaDeEntrega)
        {
            var result = (from pizzeriasConDisponibilidad in this.pizzeriasConFechaPizzasStatus
                from fechasYPizzas in this.pizzeriasConFechaPizzasStatus.Values
                from date in fechasYPizzas.Keys
                from pizzasDisponibles in fechasYPizzas.Values
                where string.Equals(pizzeriasConDisponibilidad.Key.Direccion, direccion, StringComparison.CurrentCultureIgnoreCase)
                      && pizzasDisponibles.Count > 0
                      && fechasYPizzas.Values.Contains(pizzasDisponibles)
                      && pizzeriasConDisponibilidad.Value == fechasYPizzas
                select new PedidoOption(pizzeriasConDisponibilidad.Key, pizzasDisponibles) ).ToList().Distinct();

            return result;
        }

        public void DeclararPizzaEntregada(int pizzeriaId, int cantidad, DateTime fechaDeEntrega)
        {
            var pizzeria = this.pizzeriasPorId[pizzeriaId];
            var availabilitiesPerDate = this.pizzeriasConFechaPizzasStatus[pizzeria];

            var availabilities = availabilitiesPerDate[fechaDeEntrega];

            PizzaConPrecios pizzaAvailabilityToRemove = availabilities.FirstOrDefault(pizzasWithPrices => pizzasWithPrices.Cantidad == cantidad);

            availabilities.Remove(pizzaAvailabilityToRemove);
        }

        public void StorePizzeroaDisponibles(Pizzeria pizzeria, Dictionary<DateTime, List<PizzaConPrecios>> perDatePizzaAvailabilities)
        {
            this.pizzeriasConFechaPizzasStatus[pizzeria] = perDatePizzaAvailabilities;
        }

        public IEnumerable<Pizzeria> BuscarDireccion(string location)
        {
            return from pizzeria in this.pizzeriasConFechaPizzasStatus.Keys
                where pizzeria.Direccion == location
                select pizzeria;
        }

        public Pizzeria GetPizzeria(int pizzeriaId)
        {
            return this.pizzeriasPorId[pizzeriaId];
        }

        public void StorePizzeria(int pizzeriaId, Pizzeria pizzeria)
        {
            this.pizzeriasPorId[pizzeriaId] = pizzeria;
        }
    }
}