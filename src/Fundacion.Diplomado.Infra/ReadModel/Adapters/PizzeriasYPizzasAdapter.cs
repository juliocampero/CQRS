using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Fundacion.Diplomado.Domain.ReadModel;
using Fundacion.Diplomado.Domain.WriteModel;
using Fundacion.Diplomado.IntegrationModel;
using Newtonsoft.Json;
using Precio = Fundacion.Diplomado.Domain.ReadModel.Precio;

namespace Fundacion.Diplomado.Infra.ReadModel.Adapters
{
    /// <summary>
    /// Adapter between the Integration model (json external files) and the domain one.
    /// <remarks>Implementation of the Ports and Adapters patterns (a.k.a. hexagonal architecture).</remarks>
    /// </summary>
    public class PizzeriasYPizzasAdapter : IProveerPizzas, IProveerPizzeria
    {
        // TODO: extract behaviours from this adapter to put it on the domain-side
        private readonly ISubscribeToEvents eventsSubscriber;
        private readonly IAlmacenarYProveerPizzeriaYPizzas repository;

        public PizzeriasYPizzasAdapter(string integrationFilesDirectoryPath, ISubscribeToEvents eventsSubscriber)
        {
            this.IntegrationFilesDirectoryPath = integrationFilesDirectoryPath;
            this.repository = new PizzeriasYPizzasRepository();

            this.eventsSubscriber = eventsSubscriber;
            this.eventsSubscriber.RegisterHandler<PizzaEnviada>(this.Handle); 
            // TODO: question: should we 'functionally subscribe' within the domain code instead?
            // TODO: handle the unsubscription
        }

        private void Handle(PizzaEnviada pizzaEnviada)
        {
            this.repository.DeclararPizzaEntregada(pizzaEnviada.PizzeriaId, pizzaEnviada.Cantidad, pizzaEnviada.FechaDeEntrega);
        }

        public string IntegrationFilesDirectoryPath { get; }

        public IEnumerable<Pizzeria> Pizzerias => this.repository.Pizzerias;

        public void LoadPizzaFile(string pizzeriaFileNameOrFilePath)
        {
            if (!File.Exists(pizzeriaFileNameOrFilePath))
            {
                pizzeriaFileNameOrFilePath = Path.Combine(this.IntegrationFilesDirectoryPath, pizzeriaFileNameOrFilePath);
            }

            var integrationModelForThisPizzeria = GetIntegrationModelForThisPizzeria(pizzeriaFileNameOrFilePath);

            this.AdaptAndStoreData(integrationModelForThisPizzeria);
        }

        public void LoadAllPizzaFiles()
        {
            var filesNames = Directory.GetFiles(this.IntegrationFilesDirectoryPath);
            foreach (var fileName in filesNames)
            {
                LoadPizzaFile(fileName);
            }
        }

        private static DetallePizzeriaConPizzasDisponibles GetIntegrationModelForThisPizzeria(string pizzeriaFileNameOrFilePath)
        {
            IntegrationModel.DetallePizzeriaConPizzasDisponibles integrationFileAvailabilitieses = null;
            using (var streamReader = File.OpenText(pizzeriaFileNameOrFilePath))
            {
                var jsonContent = streamReader.ReadToEnd();
                integrationFileAvailabilitieses = JsonConvert.DeserializeObject<DetallePizzeriaConPizzasDisponibles>(jsonContent);
            }
            return integrationFileAvailabilitieses;
        }

        // TODO: get rid of regions by extracting more cohesive types

        public IEnumerable<PedidoOption> BuscarPizzeriasDisponiblesEnInsensitiveWay(string direccion, DateTime fechaDeEntrega)
        {
            return repository.BuscarPizzeriasDisponiblesEnInsensitiveWay(direccion, fechaDeEntrega);
        }

        public IEnumerable<Pizzeria> BuscarDireccion(string location)
        {
            return repository.BuscarDireccion(location);
        }

        public Pizzeria GetPizzeria(int pizzeriaId)
        {
            return repository.GetPizzeria(pizzeriaId);
        }

        #region adapter from integration model to domain model

        private void AdaptAndStoreData(DetallePizzeriaConPizzasDisponibles dataParaEstaPizeria)
        {
            var pizzeria = AdaptPizzeria(dataParaEstaPizeria.PizzeriaId, dataParaEstaPizeria.PizzeriaNombre, dataParaEstaPizeria.Direccion, dataParaEstaPizeria.Cantidad);
            this.AdaptAndStoreIntegrationFileContentForAPizzeria(pizzeria, dataParaEstaPizeria);

            this.repository.StorePizzeria(dataParaEstaPizeria.PizzeriaId, pizzeria);
        }

        private void AdaptAndStoreIntegrationFileContentForAPizzeria(Pizzeria pizzeria, DetallePizzeriaConPizzasDisponibles integrationFileAvailabilitieses)
        {
            var pizzasPorFechaDisponibilidad = AdaptPizzeriaAvailabilities(integrationFileAvailabilitieses.AvailabilitiesAt);

            this.repository.StorePizzeroaDisponibles(pizzeria, pizzasPorFechaDisponibilidad);
        }

        private Dictionary<DateTime, List<PizzaConPrecios>> AdaptPizzeriaAvailabilities(Dictionary<DateTime, PizzasEstadoYPrecios[]> receivedAvailabilities)
        {
            var result = new Dictionary<DateTime, List<PizzaConPrecios>>();

            foreach (var receivedAvailability in receivedAvailabilities)
            {
                result[receivedAvailability.Key] = AdaptAllPizzasStatusOfThisPizzeroaForThisDate(receivedAvailabilities);
            }

            return result;
        }

        private static List<PizzaConPrecios> AdaptAllPizzasStatusOfThisPizzeroaForThisDate(Dictionary<DateTime, PizzasEstadoYPrecios[]> receivedAvailabilities)
        {
            return (from receivedPizzaStatus in receivedAvailabilities.Values
                from pizzasStatusAndPrices in receivedPizzaStatus
                select AdaptPizzaStatus(pizzasStatusAndPrices)).ToList();
        }

        private static PizzaConPrecios AdaptPizzaStatus(PizzasEstadoYPrecios pizzaStatusAndPrices)
        {
            return new PizzaConPrecios(pizzaStatusAndPrices.Cantidad, AdaptPrice(pizzaStatusAndPrices.Mediana), AdaptPrice(pizzaStatusAndPrices.Grande));
        }

        private static Precio AdaptPrice(IntegrationModel.Precio price)
        {
            return new Precio(price.Moneda, price.Valor);
        }

        private static Pizzeria AdaptPizzeria(int pizzeriaId, string pizzeriaName, string direccion, int cantidad)
        {
            return new Pizzeria(pizzeriaId, pizzeriaName, direccion, cantidad);
        }

        #endregion
    }
}