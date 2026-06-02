using SEAA.Astrodex.UI.Models.CuerpoCeleste;
using SEAA.Astrodex.UI.Models.Relaciones;
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
            ObtenerPorTipoAsync(
                string tipo)
        {
            try
            {
                var client =
                    _httpFactory.CreateClient(
                        "AstrodexAPI"
                    );

                var response =
                    await client.GetFromJsonAsync<
                        ApiResponseDto>(
                            $"api/cuerposcelestes/tipo/{tipo}?pagina=1&tamanio=20"
                    );

                return response?.cuerpos
                    ?? new();
            }
            catch
            {
                return new();
            }
        }

        public async Task<CuerpoCelesteDto?>
         ObtenerPorIdAsync(string id)
        {
            var client =
                _httpFactory.CreateClient(
                    "AstrodexAPI"
                );

            return await client
                .GetFromJsonAsync<CuerpoCelesteDto>(
                    $"api/cuerposcelestes/{id}"
                );
        }

        public async Task<List<RelacionResponseDto>>
            ObtenerRelacionesAsync(
                string tipo = "TODOS")
        {
            try
            {
                var client =
                    _httpFactory.CreateClient(
                        "AstrodexAPI"
                    );

                var response =
                    await client.GetFromJsonAsync<
                        RelacionApiResponseDto>(
                            $"api/cuerposcelestes/relaciones?tipo={tipo}&pagina=1&tamanio=20"
                    );

                return response?.relaciones
                    ?? new();
            }
            catch
            {
                return new();
            }
        }

        public async Task
    AnalizarRelacionAsync(
        string origen,
        string destino,
        string tipo)
        {
            try
            {
                var client =
                    _httpFactory.CreateClient(
                        "AstrodexAPI"
                    );

                await client.GetAsync(
                    $"api/relaciones/{origen}/{destino}/{tipo}"
                );
            }
            catch
            {
            }
        }

    }
}
