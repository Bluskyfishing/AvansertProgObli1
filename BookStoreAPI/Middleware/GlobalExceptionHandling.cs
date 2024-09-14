
using System.Security.Principal;
using Microsoft.AspNetCore.Diagnostics;
using Serilog;

namespace BookStoreAPI.Middleware
{
    public class GlobalExceptionHandling : IExceptionHandler
    {

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            Log.Error(exception, "Could not process a request on Machine {MachineName}. TraceId:{TraceId}",
                Environment.MachineName, httpContext.TraceIdentifier);

            var (statusCode, title) = MapException(exception);

            await Results.Problem(
                title: title,
                statusCode: statusCode,
                extensions: new Dictionary<string, object?>()
                {
                { "traceId", httpContext.TraceIdentifier }
                }
            ).ExecuteAsync(httpContext);

            return true;
        }

        private static (int statusCode, string title) MapException(Exception exception)
        {
            switch (exception)
            {
                case ArgumentNullException:
                    Log.Error( "Theres something wrong with your client!", StatusCodes.Status400BadRequest);
                    return (StatusCodes.Status400BadRequest, "Theres something wrong with your client!");

                case Microsoft.AspNetCore.Http.BadHttpRequestException:
                    Log.Error("Theres something wrong with your client!", StatusCodes.Status400BadRequest);
                    return (StatusCodes.Status400BadRequest, exception.Message);

                default:
                    Log.Error("rogram has failed on server side WHOPS!", StatusCodes.Status500InternalServerError);
                    return (StatusCodes.Status500InternalServerError, "Program has failed on server side WHOPS!");
            }

        }

    }
}
