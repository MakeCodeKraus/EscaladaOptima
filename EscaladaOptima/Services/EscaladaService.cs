using EscaladaOptima.Models;
using System.Collections.Generic;
using System.Linq;

namespace EscaladaOptima.Services
{
    public class EscaladaService
    {
        public List<Elemento> EncontrarCombinacionOptima(List<Elemento> elementos, int minCalorias, int maxPeso)
        {
            var combinaciones = ObtenerCombinaciones(elementos);
            var combinacionesValidas = combinaciones
                .Where(c => c.Sum(e => e.Peso) <= maxPeso && c.Sum(e => e.Calorias) >= minCalorias)
                .OrderByDescending(c => c.Sum(e => e.Peso)) // Maximizar peso primero
                .ThenByDescending(c => c.Sum(e => e.Calorias)) // Luego maximizar calorías
                .FirstOrDefault();


            return combinacionesValidas ?? new List<Elemento>(); // Si no hay combinación válida, devuelve lista vacía.
        }

        private List<List<Elemento>> ObtenerCombinaciones(List<Elemento> elementos)
        {
            var resultado = new List<List<Elemento>>();
            int total = 1 << elementos.Count;

            for (int i = 1; i < total; i++)
            {
                var combinacion = new List<Elemento>();
                for (int j = 0; j < elementos.Count; j++)
                {
                    if ((i & (1 << j)) != 0)
                    {
                        combinacion.Add(elementos[j]);
                    }
                }
                resultado.Add(combinacion);
            }
            return resultado;
        }
    }
}
