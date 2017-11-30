using Microsoft.AspNetCore.Mvc;
using RestfulAPIWithAspNet.Data.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestfulAPIWithAspNet.HATEOAS
{
    public class HATEOASHelper
    {
        private readonly IUrlHelper _URLHelper;

        public HATEOASHelper(IUrlHelper urlHelper)
        {
            _URLHelper = urlHelper;
        }

        public IEnumerable<Link> CreateLinks(BookVO book)
        {
            var links = new List<Link>();

            links.Add(new Link
            {
                Href = _URLHelper.Link("", new { id = book.Id }),
                Rel = "self",
                Method = "GET"
            });

            links.Add(new Link
            {
                Href = _URLHelper.Link("", new { id = book.Id }),
                Rel = "self",
                Method = "POST"
            });

            links.Add(new Link
            {
                Href = _URLHelper.Link("", new { id = book.Id }),
                Rel = "self",
                Method = "PUT"
            });

            links.Add(new Link
            {
                Href = _URLHelper.Link("", new { id = book.Id }),
                Rel = "self",
                Method = "PATCH"
            });

            links.Add(new Link
            {
                Href = _URLHelper.Link("", new { id = book.Id }),
                Rel = "self",
                Method = "DELETE"
            });

            return links;
        }
    }
}
