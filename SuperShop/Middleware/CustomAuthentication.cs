using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Memory;
using SuperShop.IService;
using System.IdentityModel.Tokens.Jwt;

namespace SuperShop.Middleware
{
    public class CustomAuthentication
    {
        private static readonly object cacheLock = new object();
        private readonly RequestDelegate _nextState;
        public CustomAuthentication(RequestDelegate nextSate)
        {
            _nextState = nextSate;
        }
        public async Task InvokeAsync(HttpContext httpContext, IUnitOfWorkService _unitOfWorkService, IMemoryCache _memoryCache)
        {
            try
            {

                var endPoint = httpContext.GetEndpoint();
                if (endPoint != null)
                {
                    var routePattern = (endPoint as RouteEndpoint)?.RoutePattern?.RawText;

                    var allowAnonymous = endPoint.Metadata.GetMetadata<AllowAnonymousAttribute>() != null;
                    var authorize = endPoint?.Metadata?.GetMetadata<AuthorizeAttribute>() != null;

                    if (allowAnonymous || (!allowAnonymous && !authorize))
                    {
                        await _nextState(httpContext);
                        return;
                    }

                    var token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                    if (token == null)
                    {
                        throw new Exception("Token Not Found");
                    }

                    var handler = new JwtSecurityTokenHandler();
                    var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

                    if (jsonToken == null)
                    {
                        throw new Exception("Invalid Token");
                    }



                    DateTime expirationTime = jsonToken.ValidTo;
                    var TokenType = jsonToken.Claims.FirstOrDefault(c => c.Type == "Type")?.Value??"";

                    if (expirationTime < DateTime.UtcNow && TokenType == "Access")
                    {
                        httpContext.Response.StatusCode = 401;
                        await httpContext.Response.WriteAsync("THIS TOKEN EXPIRED");
                        return;
                    }
                    else if (expirationTime > DateTime.UtcNow && TokenType == "Refresh")
                    {
                        if(routePattern == "Authentication/GetAccessToken")
                        {
                            await _nextState(httpContext);
                        }
                        else
                        {
                            httpContext.Response.StatusCode = 401;
                            await httpContext.Response.WriteAsync("Unauthorized Access");
                        }
                        return;
                    }
                    else
                    {
                        await _nextState(httpContext);
                        return;
                    }
                }
                else
                {
                    throw new Exception("URL Is Not Valid");
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Unable to decode the header");
            }
        }

    }
}
