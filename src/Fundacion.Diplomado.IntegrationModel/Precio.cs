namespace Fundacion.Diplomado.IntegrationModel
{
    public class Precio
    {
        public string Moneda;
        public double Valor;

        public Precio(string moneda, double valor)
        {
            Moneda = moneda;
            Valor = valor;
        }
    }
}