using System.Collections.Generic;

namespace Fundacion.Diplomado.Domain.ReadModel
{
    /// <summary>
    /// Interface to interact with our system in order to query booking options.
    /// </summary>
    public interface IQueryPedidoOpciones
    {
        IEnumerable<PedidoOption> BuscarPedidoOpciones(BuscarPedidoOpciones query);
    }
}