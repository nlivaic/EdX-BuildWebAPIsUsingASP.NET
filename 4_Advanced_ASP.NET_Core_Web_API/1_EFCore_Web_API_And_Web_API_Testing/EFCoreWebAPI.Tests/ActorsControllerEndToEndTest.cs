using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SampleApp.Controllers;
using SampleApp.Models;
using Xunit;

namespace EFCoreWebAPI.Tests
{
    public class ActorsControllerEndToEndTest
    {
        [Fact]
        public async void GetActorByIdSmokeTest()
        {
            // Arrange
            string jsonString = String.Empty;
            using (var httpClient = new HttpClient())
            {
                // Act
                httpClient.BaseAddress = new Uri("http://localhost:5000");
                var acceptType = new MediaTypeWithQualityHeaderValue("application/json");
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(acceptType);
                var response = await httpClient.GetAsync("api/actors/101");
                if (response.IsSuccessStatusCode)
                {
                    jsonString = await response.Content.ReadAsStringAsync();
                }
            }
            Actor result = JsonConvert.DeserializeObject<Actor>(jsonString);
            // Assert
            Assert.NotNull(jsonString);
            Assert.NotNull(result);
            Assert.Equal(101, result.ID);
            Assert.Equal("SUSAN", result.FirstName);
            Assert.Equal("DAVIS", result.LastName);
        }
    }
}