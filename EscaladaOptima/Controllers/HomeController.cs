using Microsoft.AspNetCore.Mvc;
using EscaladaOptima.Models;
using System.Collections.Generic;
using System.Linq;

namespace EscaladaOptima.Controllers
{
    public class HomeController : Controller
    {
        // Acción para mostrar la vista principal
        public IActionResult Index()
        {
            return View();
        }

        // Acción para calcular la combinación óptima (llamada desde el frontend)
        [HttpPost]
        public IActionResult CalcularCombinacionOptima([FromBody] CalcularRequest request)
        {
            var elementos = new List<Elemento>
            {
                new Elemento { Nombre = "E1", Peso = 5, Calorias = 3 },
                new Elemento { Nombre = "E2", Peso = 3, Calorias = 5 },
                new Elemento { Nombre = "E3", Peso = 5, Calorias = 2 },
                new Elemento { Nombre = "E4", Peso = 1, Calorias = 8 },
                new Elemento { Nombre = "E5", Peso = 2, Calorias = 3 }
            };

            var mejorCombinacion = EncontrarCombinacionOptima(elementos, request.MinCalorias, request.MaxPeso);

            return Ok(mejorCombinacion);
        }

        // Función para encontrar la combinación óptima
        private List<Elemento> EncontrarCombinacionOptima(List<Elemento> elementos, int minCalorias, int maxPeso)
        {
            List<Elemento> mejorCombinacion = new List<Elemento>();
            int mejorPeso = 0; // Ahora buscamos maximizar el peso
            int mejorCalorias = 0;

            // Generar todas las combinaciones posibles
            for (int i = 1; i <= elementos.Count; i++)
            {
                var combinaciones = GenerarCombinaciones(elementos, i);
                foreach (var combinacion in combinaciones)
                {
                    int pesoTotal = combinacion.Sum(e => e.Peso);
                    int caloriasTotales = combinacion.Sum(e => e.Calorias);

                    // Verificar si la combinación cumple con los requisitos
                    if (pesoTotal <= maxPeso && caloriasTotales >= minCalorias)
                    {
                        // Seleccionar la combinación con el mayor peso
                        if (pesoTotal > mejorPeso || (pesoTotal == mejorPeso && caloriasTotales > mejorCalorias))
                        {
                            mejorCombinacion = combinacion;
                            mejorPeso = pesoTotal;
                            mejorCalorias = caloriasTotales;
                        }
                    }
                }
            }

            return mejorCombinacion;
        }

        // Función para generar todas las combinaciones posibles
        private List<List<Elemento>> GenerarCombinaciones(List<Elemento> elementos, int tamanio)
        {
            if (tamanio == 0) return new List<List<Elemento>> { new List<Elemento>() };

            var combinaciones = new List<List<Elemento>>();
            for (int i = 0; i < elementos.Count; i++)
            {
                var elemento = elementos[i];
                var subCombinaciones = GenerarCombinaciones(elementos.Skip(i + 1).ToList(), tamanio - 1);
                foreach (var subCombinacion in subCombinaciones)
                {
                    subCombinacion.Add(elemento);
                    combinaciones.Add(subCombinacion);
                }
            }

            return combinaciones;
        }
    }

    // Clase para recibir los datos del frontend
    public class CalcularRequest
    {
        public int MinCalorias { get; set; }
        public int MaxPeso { get; set; }
    }
}