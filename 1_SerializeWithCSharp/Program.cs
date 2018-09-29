using System;
using Newtonsoft.Json;

namespace SerializeWithCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create product
            Product product1 = new Product { Id = 1, Name = "Red Apple", Price = 1.99 };
            // Serialize the Product object to JSON string
            string serializedProduct = JsonConvert.SerializeObject(product1);
            Console.WriteLine(serializedProduct);
            // Deserialize the JSON string back to the Product object.
            Product product2 = JsonConvert.DeserializeObject<Product>(serializedProduct);
            Console.WriteLine($"The product Id is {product2.Id}.");
            Console.WriteLine($"The product Name is {product2.Name}.");
            Console.WriteLine($"The product Price is {product2.Price}.");
        }
    }

    class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }
}
