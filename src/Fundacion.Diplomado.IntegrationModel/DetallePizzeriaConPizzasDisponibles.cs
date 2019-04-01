using System;
using System.Collections.Generic;

namespace Fundacion.Diplomado.IntegrationModel
{
    public class DetallePizzeriaConPizzasDisponibles
    {
        public int PizzeriaId { get; }
        public string PizzeriaNombre { get; }
        public string Direccion { get; }
        public int Cantidad { get; set; }
        public Dictionary<DateTime, PizzasEstadoYPrecios[]> AvailabilitiesAt { get; }

        public DetallePizzeriaConPizzasDisponibles(int pizzeriaId, string pizzeriaNombre, string direccion, int cantidad)
        {
            this.PizzeriaId = pizzeriaId;
            this.PizzeriaNombre = pizzeriaNombre;
            this.Direccion = direccion;
            this.Cantidad = cantidad;
            this.AvailabilitiesAt = new Dictionary<DateTime, PizzasEstadoYPrecios[]>();
        }
    }
}