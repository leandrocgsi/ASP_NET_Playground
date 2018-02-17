using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HATEOAS;
using RestfulAPIWithAspNet.Models;

namespace RestfulAPIWithAspNet.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {

        // GET api/values/5
        [HttpGet("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public async Task<IActionResult> GetAsync(int id)
        {
            var product = new ProductModel();
            product.Id = 100;
            product.Name = "test";
            return await Task.FromResult(new OkObjectResult(product));
        }
        
    }
}
