namespace Fundacion.Diplomado.IntegrationModel
{
    public class PizzasEstadoYPrecios
    {
        public int Cantidad { get; }
        public Precio Mediana { get; }
        public Precio Grande { get; }

        public PizzasEstadoYPrecios(int amount, Precio mediana, Precio grande)
        {
            Cantidad = amount;
            Mediana = mediana;
            Grande = grande;
        }
    }
}