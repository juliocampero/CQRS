using System;

namespace Fundacion.Diplomado.IntegrationModel
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Integration files generator.");

            Console.WriteLine($"Generates integration (json) files para algunas pizzerias.{Environment.NewLine}");

            IntegrationFilesGenerator.GenerateJsonFileParaMalcriada();
            IntegrationFilesGenerator.GenerateJsonFileParaPaprica();
            IntegrationFilesGenerator.GenerateJsonFileParaTentazione();
            IntegrationFilesGenerator.GenerateJsonFileParaSoleMio();

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Type <enter> to exit:");
            Console.ReadLine();
        }
    }
}
