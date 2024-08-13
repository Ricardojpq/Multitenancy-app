using Asp.Versioning;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace LookupTables.Api.Settings
{
    public class SwaggerConfiguration
    {
        public enum VersionType
        {
            None,
            CustomHeader,
            QueryString,
            AcceptHeader
        }

        public static VersionType CurrentVersioningMethod = VersionType.None;

        public static string QueryStringParam
        {
            get;
            private set;
        }

        public static string CustomHeaderParam
        {
            get;
            private set;
        }

        public static string AcceptHeaderParam
        {
            get;
            private set;
        }

        public static void UseCustomHeaderApiVersion(string parameterName)
        {
            CurrentVersioningMethod = VersionType.CustomHeader;
            CustomHeaderParam = parameterName;
        }

        public static void UseQueryStringApiVersion()
        {
            QueryStringParam = "api-version";
            CurrentVersioningMethod = VersionType.QueryString;
        }

        public static void UseQueryStringApiVersion(string parameterName)
        {
            CurrentVersioningMethod = VersionType.QueryString;
            QueryStringParam = parameterName;
        }

        public static void UseAcceptHeaderApiVersion(string paramName)
        {
            CurrentVersioningMethod = VersionType.AcceptHeader;
            AcceptHeaderParam = paramName;
        }
    }

    public class SwaggerParameterFilters : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            try
            {
                var maps = context.MethodInfo.GetCustomAttributes(true).OfType<MapToApiVersionAttribute>()
                    .SelectMany(attr => attr.Versions).ToList();
                var version = maps[0].MajorVersion;

                if (SwaggerConfiguration.CurrentVersioningMethod == SwaggerConfiguration.VersionType.CustomHeader &&
                    !context.ApiDescription.RelativePath.Contains("{version}"))
                {
                    operation.Parameters.Add(new OpenApiParameter
                    {
                        Name = SwaggerConfiguration.CustomHeaderParam,
                        In = ParameterLocation.Header,
                        Required = false,
                        Schema = new OpenApiSchema { Type = "String", Default = new OpenApiString(version.ToString()) }
                    });
                }
                else if (SwaggerConfiguration.CurrentVersioningMethod == SwaggerConfiguration.VersionType.QueryString &&
                         !context.ApiDescription.RelativePath.Contains("{version}"))
                {
                    operation.Parameters.Add(new OpenApiParameter
                    {
                        Name = SwaggerConfiguration.QueryStringParam,
                        In = ParameterLocation.Query,
                        Schema = new OpenApiSchema { Type = "String", Default = new OpenApiString(version.ToString()) }
                    });
                }
                else if (SwaggerConfiguration.CurrentVersioningMethod == SwaggerConfiguration.VersionType.AcceptHeader &&
                         !context.ApiDescription.RelativePath.Contains("{version}"))
                {
                    operation.Parameters.Add(new OpenApiParameter
                    {
                        Name = "Accept",
                        In = ParameterLocation.Header,
                        Required = false,
                        Schema = new OpenApiSchema
                        {
                            Type = "String",
                            Default = new OpenApiString($"application/json;{SwaggerConfiguration.AcceptHeaderParam}=" +
                                                        version)
                        }
                    });
                }

                var versionParameter = operation.Parameters.Single(p => p.Name == "version");

                if (versionParameter != null)
                {
                    operation.Parameters.Remove(versionParameter);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    public class SwaggerVersionMapping : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var pathLists = new OpenApiPaths();
            IDictionary<string, OpenApiPaths> paths = new Dictionary<string, OpenApiPaths>();
            var version = swaggerDoc.Info.Version.Replace("v", "").Replace("version", "").Replace("ver", "")
                .Replace(" ", "");
            foreach (var path in swaggerDoc.Paths)
            {
                pathLists.Add(path.Key.Replace("v{version}", swaggerDoc.Info.Version), path.Value);
            }

            swaggerDoc.Paths = pathLists;
        }
    }
}
