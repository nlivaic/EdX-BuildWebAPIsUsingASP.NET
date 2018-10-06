using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebServer.Models;

namespace WebServer.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        [HttpGet]
        public Product[] Get()
        {
            return FakeData.Products.Values.ToArray(); 
        }
        [HttpGet("{id}")]
        public Product Get(int id)
        {
            return FakeData.Products.Values.Single(p => p.ID == id);
        }
        [HttpPost]
        public Product Post([FromBody]Product product)
        {
            product.ID = FakeData.Products.Keys.Max()+1;
            FakeData.Products.Add(product.ID, product);
            return product;
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Product product)
        {
            if (FakeData.Products.ContainsKey(id))
            {
                FakeData.Products[product.ID] = product;
            }
        }
        [HttpDelete("{id}")]
        public void Delete([FromRoute]int id)
        {
            if (FakeData.Products.ContainsKey(id))
            {
                FakeData.Products.Remove(id);
            }
        }
    }
}