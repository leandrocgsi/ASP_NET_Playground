using System.Globalization;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;
using I18N;

namespace RestfulAPIWithAspNet.Controllers
{
    [Route("api/[controller]")]
    public class AboutController : Controller
    {
        [HttpGet]
        public string Get()
        {
            return Resource.WELCOME;
        }
    }
}