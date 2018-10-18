#pragma warning disable 1591
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using RB.JobAssistant.Models;

namespace RB.JobAssistant.Links
{
    public class JobLinksBuilder
    {
        public LinksWrapper<JobModel> ToModelWithLinks(JobModel model, HttpRequest request)
        {
            return new LinksWrapper<JobModel>
            {
                Value = model,
                Links = GenerateJobModelLinks(model, request)
            };
        }

        private List<LinkInfo> GenerateJobModelLinks(JobModel model, HttpRequest request)
        {
            var links = new List<LinkInfo>
            {
                new LinkInfo
                {
                    Href = $"{request.Scheme}://{request.Host}{request.Path}{request.QueryString}",
                    Rel = "self",
                    Method = "GET"
                },
                new LinkInfo
                {
                    Href = $"{request.Scheme}://{request.Host}/api/jobs/{model.JobId}/materials",
                    Rel = "materials",
                    Method = "GET"
                }
            };
            return links;
        }
    }
}