using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System;
using WebServer.Controllers;
using WebServer.Models;
using Xunit;

namespace WebServer.Test
{
    public class ProductsControllerFunctionalTest
    {
        [Fact]
        public void Get_Action_Returns_Ok_Status()
        {
            // Arrange
            ProductsController target = new ProductsController();
            Assert.NotNull(target);
            // Act
            Assert.IsType<OkObjectResult>(target.Get());
        }
        [Fact]
        public void Get_Action_By_Id_Action_Returns_Ok_Status_With_Product()
        {
            // Arrange
            ProductsController target = new ProductsController();
            // Act
            foreach (int productId in Repository.Products.Keys)
            {
                Assert.IsType<OkObjectResult>(target.Get(productId));
            }
        }
        [Fact]
        public void Post_Action_Creates_Product_And_Returns_Created_Status()
        {
            // Arrange
            ProductsController target = new ProductsController();
            Product product = new Product { ID = 999, Name = "Some name", Price = 999 };
            int expectedProductCount = Repository.Products.Count;
            // Act
            Assert.IsType<CreatedResult>(target.Post(product));
            expectedProductCount++;
            // Assert
            Assert.Equal(expectedProductCount, Repository.Products.Count());
        }
        [Fact]
        public void Put_Action_Updates_Product_And_Returns_Ok_Status()
        {
            // Arrange
            ProductsController target = new ProductsController();
            Product product = new Product { ID = 3, Name = "Some name", Price = 999 };
            int expectedProductCount = Repository.Products.Count;
            int maxProductId = Repository.Products.Keys.Max();
            // Act
            OkObjectResult okObjectResult = target.Put(3, product) as OkObjectResult;
            // Assert
            Assert.Equal(expectedProductCount, Repository.Products.Count());
            Assert.Equal(maxProductId, Repository.Products.Keys.Max());
            Assert.Equal("Some name", Repository.Products[3].Name);
            Assert.Equal(999, Repository.Products[3].Price);
        }
        [Fact]
        public void Delete_Action_Deletes_Product_And_Returns_Ok_Status()
        {
            // Arrange
            ProductsController target = new ProductsController();
            int expectedProductCount = Repository.Products.Count;
            int maxProductId = Repository.Products.Keys.Max();
            // Act
            OkObjectResult okObjectResult = target.Delete(1) as OkObjectResult;
            expectedProductCount--;
            // Assert
            Assert.Equal(expectedProductCount, Repository.Products.Count);
            Assert.Equal(maxProductId, Repository.Products.Keys.Max());
            Assert.False(Repository.Products.Keys.Contains(1));
        }
    }
}