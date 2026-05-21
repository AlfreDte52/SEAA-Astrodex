// API/Controllers/RutasController.cs
using Microsoft.AspNetCore.Mvc;
using SEAA.Astrodex.Core.Interfaces;

namespace SEAA.Astrodex.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RutasController : ControllerBase
    {
        private readonly IRutaService _service;

        public RutasController(IRutaService service)
        {
            _service = service;
        }

        // Operación 7: calcula la ruta directa y por tramos entre dos planetas
        [HttpGet("{origen}/{destino}")]
        public async Task<IActionResult> CalcularRuta(string origen, string destino)
        {
            try
            {
                var resultado = await _service.CalcularRutaAsync(origen, destino);

                if (resultado == null)
                    return NotFound(
                        $"No se pudo calcular la ruta entre {origen} y {destino}. " +
                        "Ambos deben ser planetas que orbitan al Sol");

                return Ok(resultado);
            }
            catch (HttpRequestException)
            {
                return StatusCode(503, "No se pudo conectar con la API externa.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }
    }
}