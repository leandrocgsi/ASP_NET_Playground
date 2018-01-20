using HATEOAS;
using Microsoft.AspNetCore.Mvc;
using RestfulAPIWithAspNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestfulAPIWithAspNet.Middleware
{
    public class ProductModelEnricher : ObjectContentResponseEnricher<ProductModel>
    {
        protected override Task EnrichModel(ProductModel content, IUrlHelper urlHelper)
        {
            //https://blogs.msdn.microsoft.com/roncain/2012/07/17/using-the-asp-net-web-api-urlhelper/

            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.GET,
                Href = urlHelper.Link("DefaultApi", new { controller = "Products", id = content.Id }),
                Rel = RelationType.self,
                Type = RensponseTypeFormat.DefaultGet
            });
            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.POST,
                Href = urlHelper.Link("DefaultApi", new { controller = "Products", id = content.Id }),
                Rel = RelationType.self,
                Type = RensponseTypeFormat.DefaultPost
            });
            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.PUT,
                Href = urlHelper.Link("DefaultApi", new { controller = "Products", id = content.Id }),
                Rel = RelationType.self,
                Type = RensponseTypeFormat.DefaultPost
            });
            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.DELETE,
                Href = urlHelper.Link("DefaultApi", new { controller = "Products", id = content.Id }),
                Rel = RelationType.self,
                Type = "int"
            });
            return null;
        }
    }
}