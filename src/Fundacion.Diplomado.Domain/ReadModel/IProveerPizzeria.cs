using System.Collections.Generic;

namespace Fundacion.Diplomado.Domain.ReadModel
{
    public interface IProveerPizzeria
    {
        IEnumerable<Pizzeria> BuscarDireccion(string location);
        Pizzeria GetPizzeria(int pizzeriaId);
    }
}