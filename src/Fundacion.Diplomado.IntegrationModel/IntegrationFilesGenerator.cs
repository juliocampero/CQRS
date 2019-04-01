using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace Fundacion.Diplomado.IntegrationModel
{
    /// <summary>
    /// Helper class to generate integration files for various Pizzerias
    /// </summary>
    public static class IntegrationFilesGenerator
    {
        private const string myFavorite2019Saturday = "2019-03-30";

        public static string WhereToGenerateDirectoryFullPath => Path.GetFullPath(AppContext.BaseDirectory + @"../../../../../Fundacion.Diplomado.Web/wwwroot/pizzerias/");

        public static void GenerateJsonFileParaMalcriada()
        {
            var pizzeriaId = 1;
            var pizzeriaNombre = "Malcriada";
            var direccion = "Cercado";
            var cantidadDePizzas = 405;

            var disponibilidadDePizzas =  new DetallePizzeriaConPizzasDisponibles(pizzeriaId, pizzeriaNombre, direccion, cantidadDePizzas);
            disponibilidadDePizzas.AvailabilitiesAt.Add(DateTime.Parse(myFavorite2019Saturday), new PizzasEstadoYPrecios[]
            {
                new PizzasEstadoYPrecios(101, new Precio("BOB", 109), new Precio("BOB", 140)),
                new PizzasEstadoYPrecios(102, new Precio("BOB", 109), new Precio("BOB", 140)),
                new PizzasEstadoYPrecios(201, new Precio("BOB", 209), new Precio("BOB", 240)),
                new PizzasEstadoYPrecios(301, new Precio("BOB", 109), new Precio("BOB", 140)),
                new PizzasEstadoYPrecios(302, new Precio("BOB", 109), new Precio("BOB", 140)),
                new PizzasEstadoYPrecios(303, new Precio("BOB", 109), new Precio("BOB", 140)),
                new PizzasEstadoYPrecios(304, new Precio("BOB", 109), new Precio("BOB", 140)),
                new PizzasEstadoYPrecios(305, new Precio("BOB", 109), new Precio("BOB", 140)),
                new PizzasEstadoYPrecios(306, new Precio("BOB", 109), new Precio("BOB", 140)),
                new PizzasEstadoYPrecios(307, new Precio("BOB", 109), new Precio("BOB", 140)),
                new PizzasEstadoYPrecios(308, new Precio("BOB", 109), new Precio("BOB", 140)),
                new PizzasEstadoYPrecios(405, new Precio("BOB", 145), new Precio("BOB", 170)),
                new PizzasEstadoYPrecios(501, new Precio("BOB", 12000), new Precio("BOB", 12000))
            });

            var generatedFilePath = SerializeToJsonFile(disponibilidadDePizzas);
            Console.WriteLine($"Integration file generated: {generatedFilePath}");
        }

        public static void GenerateJsonFileParaTentazione()
        {
            var pizzeriaId = 2;
            var pizzeriaNombre = "Tentazione";
            var direccion = "Sacaba";
            var cantidad = 240;

            var pisponibilidadDePizzas = new DetallePizzeriaConPizzasDisponibles(pizzeriaId, pizzeriaNombre, direccion, cantidad);
            pisponibilidadDePizzas.AvailabilitiesAt.Add(DateTime.Parse(myFavorite2019Saturday), 
                new PizzasEstadoYPrecios[] { new PizzasEstadoYPrecios(101, new Precio("BOB", 109), new Precio("BOB", 140)),
                new PizzasEstadoYPrecios(102, new Precio("BOB", 109), new Precio("BOB", 140)),
                new PizzasEstadoYPrecios(201, new Precio("BOB", 209), new Precio("BOB", 240)) });

            var generatedFilePath = SerializeToJsonFile(pisponibilidadDePizzas);
            Console.WriteLine($"Integration file generated: {generatedFilePath}");
        }

        public static void GenerateJsonFileParaPaprica()
        {
            var pizzeriaId = 3;
            var pizzeriaNombre = "Paprica";
            var direccion = "Prado";
            var cantidad = 10;

            var disponibilidadDePizzas = new DetallePizzeriaConPizzasDisponibles(pizzeriaId, pizzeriaNombre, direccion, cantidad);
            disponibilidadDePizzas.AvailabilitiesAt.Add(DateTime.Parse(myFavorite2019Saturday), 
                new PizzasEstadoYPrecios[] { new PizzasEstadoYPrecios(101, new Precio("BOB", 109), new Precio("BOB", 140)) });

            var generatedFilePath = SerializeToJsonFile(disponibilidadDePizzas);
            Console.WriteLine($"Integration file generated: {generatedFilePath}");
        }

        public static void GenerateJsonFileParaSoleMio()
        {
            var pizzeriaId = 4;
            var pizzeriaNombre = "Sole mio";
            var direccion = "America";
            var cantidad = 5;

            var disponibilidadDePizzas = new DetallePizzeriaConPizzasDisponibles(pizzeriaId, pizzeriaNombre, direccion, cantidad);
            disponibilidadDePizzas.AvailabilitiesAt.Add(DateTime.Parse(myFavorite2019Saturday), new PizzasEstadoYPrecios[] { });

            var generatedFilePath = SerializeToJsonFile(disponibilidadDePizzas);
            Console.WriteLine($"Integration file generated: {generatedFilePath}");
        }

        private static string SerializeToJsonFile(DetallePizzeriaConPizzasDisponibles detaillePizzeriaConPizzasDisponibles)
        {
            var jsonContent = JsonConvert.SerializeObject(detaillePizzeriaConPizzasDisponibles, Formatting.Indented);
            var fileName = detaillePizzeriaConPizzasDisponibles.PizzeriaNombre + "-availabilities.json";
            
            // ensures the full paht directory exist
            Directory.CreateDirectory(WhereToGenerateDirectoryFullPath);

            // Generate JSON file
            var fileFullPath = Path.Combine(WhereToGenerateDirectoryFullPath, fileName);
            File.WriteAllText(fileFullPath, jsonContent, Encoding.UTF8);

            return fileFullPath;
        }
    }
}