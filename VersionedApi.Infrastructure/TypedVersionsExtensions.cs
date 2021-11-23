using System;

namespace VersionedApi.Infrastructure
{
    /// <summary>
    /// Extension methods around TypedVersions enum
    /// </summary>
    public static class TypedVersionsExtensions
    {
        /// <summary>
        /// Convert string  into a typed version
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        public static TypedVersions AsTypedVersion(this string version)
        {
            if (version.Equals("v1", StringComparison.OrdinalIgnoreCase))
            {
                return TypedVersions.V1;
            }

            if (version.Equals("v2", StringComparison.OrdinalIgnoreCase))
            {
                return TypedVersions.V2;
            }

            throw new InvalidOperationException("Version is not valid");
        }
    }
}
