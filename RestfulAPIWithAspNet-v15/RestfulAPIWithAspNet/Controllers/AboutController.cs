using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using I18N;

namespace RestfulAPIWithAspNet.Controllers
{
    // SEE: HATEOAS https://shatzisblog.wordpress.com/2017/09/01/generating-hypermedia-links-for-an-asp-net-core-web-api/ 
    // and https://github.com/SotirisH/HyperMedia
    // and http://pracujlabs.io/2015/04/30/becoming-hateoas-with-webapi.html
    // and https://www.codeproject.com/Articles/1204190/ASP-NET-Core-Web-API-and-HATEOAS
    // and http://benfoster.io/blog/generating-hypermedia-links-in-aspnet-web-api
    [Route("api/[controller]")]
    public class AboutController : Controller
    {
        private readonly IStringLocalizer<SharedResource> _localizer;

        public AboutController(IStringLocalizer<SharedResource> localizer)
        {
            _localizer = localizer;
        }

        [HttpGet]
        public string Get()
        {
            return Resource.WELCOME;
        }
    }
}