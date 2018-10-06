using System;
using Microsoft.AspNetCore.Mvc;
using SampleApp.Controllers;
using SampleApp.Models;
using Xunit;

namespace EFCoreWebAPI.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void GetActorByIdSmokeTest()
        {
            // Arrange
            ActorsController target = new ActorsController();
            // Act
            OkObjectResult okObjectResult = target.Get(101) as OkObjectResult;
            Actor result = (okObjectResult.Value) as Actor;
            // Assert
            Assert.Equal(101, result.ID);
            Assert.Equal("SUSAN", result.FirstName);
            Assert.Equal("DAVIS", result.LastName);
        }
    }
}
