using SEAA.Astrodex.UI.Models.CuerpoCeleste;
using System.Net.Http.Json;
namespace SEAA.Astrodex.UI.Services

{
    public class CuerpoCelesteService
    {
        private readonly IHttpClientFactory _httpFactory;

        public CuerpoCelesteService(
            IHttpClientFactory httpFactory)
        {
            _httpFactory = httpFactory;
        }

        public async Task<List<CuerpoCelesteDto>>
            ObtenerPorTipoAsync(string tipo)
        {
            var client =
                _httpFactory.CreateClient(
                    "AstrodexAPI"
                );

            var response =
                await client.GetFromJsonAsync<ApiResponseDto>(
                     $"api/cuerposcelestes/tipo/{tipo}?pagina=1&tamanio=20"
                );

            return response?.cuerpos
                ?? new List<CuerpoCelesteDto>();
        }

    }
}
