namespace Fundacion.Diplomado.Domain.ReadModel
{
    public class Pizzeria
    {
        public Pizzeria(int pizzeriaId, string nombre, string direccion, int cantidad)
        {
            this.Identificador = pizzeriaId;
            this.Nombre = nombre;
            this.Direccion = direccion;
            this.Cantidad = cantidad;
        }

        public string Direccion { get; }
        public string Nombre { get; }
        public int Identificador { get; }
        public int Cantidad { get; }

        public override string ToString()
        {
            return this.Nombre;
        }
    }
}