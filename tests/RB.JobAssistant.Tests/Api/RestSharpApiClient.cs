using RB.JobAssistant.Models;
using RestSharp.Portable;

namespace RB.JobAssistant.Tests.Api
{
    public class RestSharpApiClientHelper
    {
        // TODO: Continue to envolve this helper implementation.
        
        public static RestRequest BuildBoschBlueRequest(Method method, string resourceUri)
        {
            var request = new RestRequest
            {
                Resource = resourceUri,
                Method = method
            };
            request.Parameters.Clear();
            request.AddHeader("Accept", "application/json");
            request.AddHeader(TenantModel.DomainField, BoschTenants.BoschBlueDomain);
            return request;
        }
    }
}