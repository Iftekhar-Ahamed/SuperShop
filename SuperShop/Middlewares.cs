using SuperShop.Middleware;

namespace SuperShop
{
    public static class Middlewares
    {
        public static IApplicationBuilder UseCustomAuthorization(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomAuthentication>();
        }
    }
}
