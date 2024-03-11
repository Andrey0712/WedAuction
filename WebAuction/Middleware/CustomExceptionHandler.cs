using Google.Apis.Http;
using Newtonsoft.Json;
using System.Net;
using WebAuction.Exeption;

namespace WebAuction.Middleware
{
    public class CustomExceptionHandler
    {
        private readonly RequestDelegate _next;
        public CustomExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {

                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context,Exception exeption)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = string.Empty;
            switch (exeption)
            {
                case AppException appExeption:
                    code = HttpStatusCode.BadRequest;
                    result=JsonConvert.SerializeObject(new { error = exeption.Message });
                    break;
                default:
                    result = JsonConvert.SerializeObject(new {error=exeption.Message});
                    break;


            }
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode=(int)code;

            if (string.IsNullOrEmpty(result))
            {
                result = JsonConvert.SerializeObject(new {error=exeption.Message});
            }
            return response.WriteAsJsonAsync(result);
        }
    }

    public static class CustomExceptionHandlerExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandler>();
        }
    }
}
