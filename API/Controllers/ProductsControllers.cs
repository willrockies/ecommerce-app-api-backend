using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductsControllers : ControllerBase
    {

        [HttpGet("/api/Products")]
        public string GetProducts()
        {
            return "This will be a list of products";
        }

        [HttpGet("/api/Products/{id}")]
        public string GetProduct(int id)
        {
            return "This will be a product";
        }

    }
}