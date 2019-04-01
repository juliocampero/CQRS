namespace Fundacion.Diplomado.Domain.ReadModel
{
    public class PizzaConPrecios
    {
        public int Cantidad;
        public Precio Mediana;
        public Precio Grande;

        public PizzaConPrecios(int cantidad, Precio mediana, Precio grande)
        {
            this.Cantidad = cantidad;
            this.Mediana = mediana;
            this.Grande = grande;
        }
    }
}