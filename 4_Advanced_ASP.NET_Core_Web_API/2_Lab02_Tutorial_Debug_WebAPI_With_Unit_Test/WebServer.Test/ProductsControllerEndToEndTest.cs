using System;
using Xunit;
using WebServer.Models;
using WebServer.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;

namespace WebServer.Test
{
    public class ProductsControllerEndToEndTest
    {
        private HttpClient GetHttpClient()
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:5000");
            var acceptType = new MediaTypeWithQualityHeaderValue("application/json");
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(acceptType);
            return httpClient;
        }
        private bool SameProduct(Product p1, Product p2) {
            return p1.ID == p2.ID && p1.Name == p2.Name && p1.Price == p2.Price;
        }
        [Fact]
        public async void Get_Action_Returns_All_Products()
        {
            HttpClient client = GetHttpClient();
            HttpResponseMessage response = await client.GetAsync("api/products");
            Assert.True(response.IsSuccessStatusCode);
            string allProductsJsonString = await response.Content.ReadAsStringAsync();
            Product[] products = JsonConvert.DeserializeObject<Product[]>(allProductsJsonString);
            Assert.NotNull(products);
            Assert.True(products.Length > 0);
        }
        [Fact]
        public async void Post_Action_Creates_New_Product_And_Returns_Created_With_Location()
        {
            int oldCount = Repository.Products.Count;
            int maxId = Repository.Products.Keys.Max();
            HttpClient client = GetHttpClient();
            Product product = new Product { ID = 999, Name = "New Product", Price = 9.99 };
            string productJsonString = JsonConvert.SerializeObject(product);
            StringContent httpContent = new StringContent(productJsonString, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("api/products", httpContent);
            Assert.True(response.IsSuccessStatusCode);
            string newProductJsonString = await response.Content.ReadAsStringAsync();
            Product newProductFromAPI = JsonConvert.DeserializeObject<Product>(newProductJsonString);
            Assert.NotNull(newProductFromAPI);
            product.ID = Repository.Products.Keys.Max();
            Assert.True(SameProduct(product, newProductFromAPI));
            Assert.Equal(oldCount + 1, Repository.Products.Count);
        }
        [Fact]
        public async void PutActionTest() {
            var httpClient = GetHttpClient();
            var product = Repository.Products[0];
            var productJson = JsonConvert.SerializeObject(product);
            var httpContent = new StringContent(productJson, Encoding.UTF8, "application/json");
            var putResponse = await httpClient.PutAsync("api/products/0", httpContent);
            Assert.True(putResponse.IsSuccessStatusCode);
        }
        [Fact]
        public async void DeleteActionTest() {
            var httpClient = GetHttpClient();
            var deleteResponse = await httpClient.DeleteAsync("api/products/4");
            Assert.True(deleteResponse.IsSuccessStatusCode);
            deleteResponse = await httpClient.DeleteAsync("api/products/101");
            Assert.False(deleteResponse.IsSuccessStatusCode);
        }
    }
}