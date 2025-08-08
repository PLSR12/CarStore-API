
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CarStore.API.Filters
{


    public class HealthCheckDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var pathItem = new OpenApiPathItem();
            pathItem.AddOperation(OperationType.Get, new OpenApiOperation
            {
                Tags = new List<OpenApiTag> { new OpenApiTag { Name = "Health" } },
                Responses = new OpenApiResponses
            {
                { "200", new OpenApiResponse { Description = "Serviço saudável" } },
                { "503", new OpenApiResponse { Description = "Serviço com falhas" } }
            }
            });

            swaggerDoc.Paths.Add("/health", pathItem);
        }
    }

}
