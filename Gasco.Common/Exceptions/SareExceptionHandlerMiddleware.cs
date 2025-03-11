

using Gasco.Common.Helpers;
using Gasco.Common.ServerResponses;
using Gasco.Common.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Gasco.Common.Exceptions
{
    public class GascoExceptionHandlerMiddleware
    {
        private readonly ILogger<IApplicationBuilder> _logger;
        private readonly bool _sendExceptionToFront;
        private readonly RequestDelegate _next;
        public GascoExceptionHandlerMiddleware(RequestDelegate next, ILogger<IApplicationBuilder> logger) 
        {
            _next = next;
            var globalConfig = ServiceProviderHelper.GetService<IOptions<GlobalConfig>>()!;
            _sendExceptionToFront = globalConfig.Value.SendExceptionToFront;
            _logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
                if (context.Response.StatusCode == StatusCodes.Status403Forbidden)
                    throw new GascoException(GascoExceptionCodes.GascoEx1010);
            }
            catch (Exception exception)
            {
                context.Response.ContentType = "application/json";
                JSendResponse<string?> jSendRespons;
                if (exception != null && typeof(GascoException).IsAssignableFrom(exception.GetType()))
                {
                    var ex = (GascoException)exception;
                    context.Response.StatusCode = StatusCodes.Status200OK;

                    jSendRespons = new JSendResponse<string?> { Status = ResponseStatus.FAIL, Message = ex.Message, Data = null, Code = ex.Code };

                    _logger.LogError("Error Gasco: {ex}", ex.Message);
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    var data = _sendExceptionToFront ? exception?.ToString() : null;
                    jSendRespons = new JSendResponse<string?> { Status = ResponseStatus.FAIL, Message = "Error interno del servidor.", Data = data };

                    _logger.LogError(exception, "Error Gasco (No Controlado):");
                }


                var json = JsonSerializer.Serialize(jSendRespons);
                await context.Response.WriteAsync(json);
            }

        }
    }
}
