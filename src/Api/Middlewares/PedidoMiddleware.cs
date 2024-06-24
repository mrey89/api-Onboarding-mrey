using apiOnBording.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Threading.Tasks;


namespace apiOnBording.Domain.Middlewares
{
    [ExcludeFromCodeCoverage]

    public class PedidoMiddleware : IMiddleware
    {
        private readonly ILogger<PedidoMiddleware> _logger;

        public PedidoMiddleware(ILogger<PedidoMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (ApiPedidoException ex)
            {
                _logger.LogError(ex, "{Message}", ex.ErrorMessage.Title + ": " + ex.ErrorMessage.Detail + "." + ex.Message);

                var jsonResponse = JsonSerializer.Serialize(ex.ErrorMessage);

                context.Response.StatusCode = ex.ErrorMessage.Status;

                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(jsonResponse);
            }
        }
    }
}
