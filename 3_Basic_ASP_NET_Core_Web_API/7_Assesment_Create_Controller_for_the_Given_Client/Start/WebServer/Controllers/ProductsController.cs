using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebServer.Models;

namespace WebServer.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        [HttpGet]
        public ActionResult Get()
        {
            if (FakeData.Products == null || FakeData.Products.Count() == 0)
            {
                return NotFound();
            }
            return Ok(FakeData.Products.Values.ToArray());
        }
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            if (FakeData.Products == null || 
                FakeData.Products.Count() == 0 || 
                FakeData.Products.Keys.Contains(id) == false)
            {
                return NotFound();
            }
            Product product = FakeData.Products[id];
            return Ok(product);
        }
        [HttpGet("price/{low}/{high}")]
        public ActionResult Get(double low, double high)
        {
            if (FakeData.Products == null || FakeData.Products.Count() == 0)
            {
                return NotFound();
            }
            Product[] products = FakeData.Products.Values.Where(p => p.Price >= low && p.Price <= high).ToArray();
            if (products.Length == 0)
            {
                return NotFound();
            }
            return Ok(products);
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (FakeData.Products == null || FakeData.Products.Count() == 0 || !FakeData.Products.Keys.Contains(id))
            {
                return NotFound();
            }
            FakeData.Products.Remove(id);
            return Ok();
        }
        [HttpPost]
        public IActionResult Post([FromBody]Product product)
        {
            int id = FakeData.Products.Keys.Max() + 1;
            product.ID = id;
            FakeData.Products.Add(product.ID, product);
            return Created($"api/products/{product.ID}", product);
        }
        [HttpPut("{id}")]
        public IActionResult Put([FromRoute]int id, [FromBody]Product product)
        {
            if (FakeData.Products == null || FakeData.Products.Count() == 0 || !FakeData.Products.Keys.Contains(id))
            {
                return NotFound();
            }
            FakeData.Products[id] = product;
            return Ok();
        }
        [HttpPut("raise/{amount}")]
        public IActionResult Put([FromRoute]int amount)
        {
            if (FakeData.Products == null || FakeData.Products.Count() == 0)
            {
                return NotFound();
            }
            foreach (Product product in FakeData.Products.Values)
            {
                product.Price += amount;
            }
            return Ok();
        }
    }
}