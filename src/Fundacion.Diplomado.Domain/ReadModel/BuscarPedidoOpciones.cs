using System;

namespace Fundacion.Diplomado.Domain.ReadModel
{
    public class BuscarPedidoOpciones : Query
    {
        public DateTime fechaDeEntrega { get; }
        public string Direccion { get; }
        public string nombrePizza { get; }
        public int Cantidad { get; }

        public BuscarPedidoOpciones(DateTime fechaDeEntrega, string direccion, string nombrePizza, int cantidad = 1)
        {
            this.fechaDeEntrega = fechaDeEntrega;
            this.Direccion = direccion;
            this.nombrePizza = nombrePizza;
            this.Cantidad = cantidad;
        }
    }
}