using Newtonsoft.Json;
using SuperShop.CustomException;
using SuperShop.Model;
using System.Net;

namespace SuperShop.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (CustomizedException ex)
            {
                await HandleExceptionAsync(context, ex.message,ex.statusCode );
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex.Message, 500);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, string message, long statusCode)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var result = JsonConvert.SerializeObject(new MessageHelperModel{ Message = message, StatusCode = (int)statusCode });
            return context.Response.WriteAsync(result);
        }
    }
}
