using J3m_BE.Controllers;
using J3m_BE.DTOs.Allergies;
using J3m_BE.Services.Implementations;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using Assert = Xunit.Assert;

namespace J3m_BE.Tests.Controllers
{
   
    public class AllergyTestController
    {
        [Fact]
        public async Task GetAllAllergies_ReturnOkWithData()
        {
            var mockService = new Mock<AllergyService>();
            mockService.Setup(s => s.GetAllAllergiesAsync())
                .ReturnsAsync(new List<AllergyDto>
                {
                    new AllergyDto { AllergyId = 1, AllergyName = "Peanuts" },
                    new AllergyDto { AllergyId = 2, AllergyName = "Milk" }

                });

            var controller = new AllergyController(mockService.Object);
            var result = await controller.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var data = Assert.IsType<List<AllergyDto>>(okResult.Value);
            Assert.Equal(2, data.Count);
        }

        [Fact]

    }
}
