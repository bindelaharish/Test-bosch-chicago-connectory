using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace RB.JobAssistant.Util
{
    internal class OptionalPathParamDocumentFilter : IDocumentFilter
    {
        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext docFilterContext)
        {
            // BY DEFAULT SWASHBUCKLE MAKES ALL ENDPOINT PARAMETERS REQUIRED. THIS IS A WORKAROUND THAT
            // ITERATES THROUGH EACH PATH AND SETS EACH PATH'S PARAMETER TO OPTIONAL

            foreach (var path in swaggerDoc.Paths)
            {
                var pathObject = swaggerDoc.Paths[path.Key].Get;

                if (pathObject?.Parameters == null) continue;
                foreach (var param in pathObject.Parameters)
                    param.Required = false;
            }
        }
    }
}