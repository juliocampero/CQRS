using System;

namespace Fundacion.Diplomado.Domain.WriteModel
{
    public interface IPedidoRepository
    {
        void Grabar(Pedido pedido);
        void Grabar(ICommand comando);
    }
}