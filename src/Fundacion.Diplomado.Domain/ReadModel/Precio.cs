namespace Fundacion.Diplomado.Domain.ReadModel
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

        public override string ToString()
        {
            return $"{Valor} {Moneda}";
        }
    }
}