using System.Collections.Generic;

namespace Fundacion.Diplomado.Domain.ReadModel
{
    public interface IProveerPedido
    {
        IEnumerable<Reservacion> GetReservacionesDe(string clienteId);
    }
}