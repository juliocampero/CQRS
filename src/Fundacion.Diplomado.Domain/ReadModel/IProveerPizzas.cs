using System;
using System.Collections.Generic;

namespace Fundacion.Diplomado.Domain.ReadModel
{
    /// <summary>
    /// Find pizzas.
    /// <remarks>Repository of AvailablePizzasWithPrices.</remarks>
    /// </summary>
    public interface IProveerPizzas
    {
        IEnumerable<PedidoOption> BuscarPizzeriasDisponiblesEnInsensitiveWay(string direccion, DateTime fechaDeEntrega);
    }
}