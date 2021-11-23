using Microsoft.AspNetCore.Http;
using System;

namespace VersionedApi.Infrastructure
{
    /// <summary>
    /// Extension methods for HttpContext Object
    /// </summary>
    public static class RequestExtensions
    {
        /// <summary>
        /// Finds the version from HttpContext.Request
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static TypedVersions GetRequestVersion(this HttpContext context)
        {
            var arr = context.Request.Path.Value.Split('/');
            var apiPosition = Array.IndexOf(arr, "api");
            return arr[apiPosition + 1].AsTypedVersion();
        }

        /// <summary>
        /// Sets the current version into Context.Items
        /// </summary>
        /// <param name="context"></param>
        /// <param name="typedVersion"></param>
        public static void SetVersionVariableToContext(this HttpContext context, TypedVersions typedVersion)
        {
            context.Items.Add(Constants.ContextKeyForVersion, typedVersion.ToString());
        }

        /// <summary>
        /// Gets the current version from Context.Items
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static TypedVersions GetVersionVariableFromContext(this HttpContext context)
        {
            context.Items.TryGetValue(Constants.ContextKeyForVersion, out object version);
            return version.ToString().AsTypedVersion();
        }
    }
}
