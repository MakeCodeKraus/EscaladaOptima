using Microsoft.AspNetCore.Mvc;
using EscaladaOptima.Models;
using EscaladaOptima.Services;
using System.Collections.Generic;

namespace EscaladaOptima.Controllers
{
    [ApiController]
    [Route("api/escalada")]
    public class EscaladaApiController : ControllerBase
    {
        private readonly EscaladaService _escaladaService;

        public EscaladaApiController(EscaladaService escaladaService)
        {
            _escaladaService = escaladaService;
        }

        [HttpGet("elementos")]
        public IActionResult ObtenerElementos()
        {
            var elementos = new List<Elemento>
            {
                new Elemento { Nombre = "E1", Peso = 5, Calorias = 3 },
                new Elemento { Nombre = "E2", Peso = 3, Calorias = 5 },
                new Elemento { Nombre = "E3", Peso = 5, Calorias = 2 },
                new Elemento { Nombre = "E4", Peso = 1, Calorias = 8 },
                new Elemento { Nombre = "E5", Peso = 2, Calorias = 3 }
            };

            return Ok(elementos);
        }

        [HttpPost("calcular")]
        public IActionResult CalcularCombinacionOptima([FromBody] CalcularRequest request)
        {
            if (request == null)
            {
                return BadRequest("El cuerpo de la solicitud no puede estar vacío.");
            }

            var elementos = new List<Elemento>
            {
                new Elemento { Nombre = "E1", Peso = 5, Calorias = 3 },
                new Elemento { Nombre = "E2", Peso = 3, Calorias = 5 },
                new Elemento { Nombre = "E3", Peso = 5, Calorias = 2 },
                new Elemento { Nombre = "E4", Peso = 1, Calorias = 8 },
                new Elemento { Nombre = "E5", Peso = 2, Calorias = 3 }
            };

            var mejorCombinacion = _escaladaService.EncontrarCombinacionOptima(elementos, request.MinCalorias, request.MaxPeso);

            return Ok(mejorCombinacion);
        }
    }
}
