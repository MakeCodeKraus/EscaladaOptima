using Microsoft.AspNetCore.Mvc;
using EscaladaOptima.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EscaladaOptima.Controllers
{
    public class EscaladaController : Controller
    {
        private readonly HttpClient _httpClient;

        public EscaladaController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }
        public async Task<IActionResult> Index(List<Elemento>? combinacionOptima = null)
        {
            List<Elemento> elementos = new();

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("http://localhost:5269/api/escalada/elementos");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    elementos = JsonSerializer.Deserialize<List<Elemento>>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }) ?? new List<Elemento>();
                }
            }
            catch
            {
                ViewBag.Error = "No se pudieron cargar los elementos.";
            }

            ViewBag.CombinacionOptima = combinacionOptima ?? new List<Elemento>();
            return View(elementos);
        }

        // Procesar la solicitud de cálculo y redirigir con los resultados
        [HttpPost]
        public async Task<IActionResult> Calcular(int minCalorias, int maxPeso)
        {
            var requestData = new
            {
                MinCalorias = minCalorias,
                MaxPeso = maxPeso
            };

            List<Elemento> combinacionOptima = new();

            try
            {
                var jsonContent = new StringContent(JsonSerializer.Serialize(requestData), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync("http://localhost:5269/api/escalada/calcular", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    combinacionOptima = JsonSerializer.Deserialize<List<Elemento>>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }) ?? new List<Elemento>();
                }
            }
            catch
            {
                ViewBag.Error = "Error al calcular la combinación óptima.";
            }

            return RedirectToAction("Index", new { combinacionOptima });
        }
    }
}
