using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace VersionedApi.Infrastructure
{
    /// <summary>
    /// Middleware to intercept the request version
    /// </summary>
    public class VersionInterceptorMiddleware
    {
        private readonly RequestDelegate next;

        public VersionInterceptorMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var version = context.GetRequestVersion();

            context.SetVersionVariableToContext(version);

            await next(context);
        }
    }
}
