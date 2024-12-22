using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace FuelAcc.WebApi.Api
{
    public class SecurityRequirementsOperationFilter : IOperationFilter
    {
        private bool HasAttribute(MethodInfo methodInfo, Type type, bool inherit)
        {
            // inhertit = true also checks inherited attributes
            var actionAttributes = methodInfo.GetCustomAttributes(inherit);
            var controllerAttributes = methodInfo.DeclaringType.GetTypeInfo().GetCustomAttributes(inherit);
            var actionAndControllerAttributes = actionAttributes.Union(controllerAttributes);

            return actionAndControllerAttributes.Any(attr => attr.GetType() == type);
        }

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            bool hasAuthorizeAttribute = HasAttribute(context.MethodInfo, typeof(AuthorizeAttribute), true);
            bool hasAnonymousAttribute = HasAttribute(context.MethodInfo, typeof(AllowAnonymousAttribute), true);

            // so far as I understood the action/operation is public/unprotected
            // if there is no authorize or an allow anonymous (allow anonymous overrides all authorize)
            bool isAuthorized = hasAuthorizeAttribute && !hasAnonymousAttribute;

            if (isAuthorized)
            {
                operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
                operation.Responses.Add("403", new OpenApiResponse { Description = "Forbidden" });

                var scheme = new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
                };

                operation.Security = new List<OpenApiSecurityRequirement>
            {
                new OpenApiSecurityRequirement
                {
                     [scheme] = new List<string>()
                }
            };
            }
        }
    }
}