//using J3m_BE.Controllers;
//using J3m_BE.DTOs.Allergies;
//using J3m_BE.Services.Implementations;
//using J3m_BE.Services.Interfaces;
//using Microsoft.AspNetCore.Mvc;
//using Moq;
//using Xunit;
//using Assert = Xunit.Assert;

//namespace J3m_BE.Tests.Controllers
//{
//   // Test Controller for Allergies
//    public class AllergyTestController
//    {
//        private readonly Mock<IAllergyService> _serviceMock;
//        private readonly AllergyController _controller;

//        public AllergyTestController()
//        {
//            _serviceMock = new Mock<IAllergyService>(); //Creating a mock object for the service interface
//            _controller = new AllergyController(_serviceMock.Object);
//        }

//        [Fact]
//        public async Task GetAll_ReturnsOkResult_WithAlleries()
//        {
//            // Arrange
//            var mockData = new List<AllergyDto>
//            {
//                new AllergyDto { AllergyId = 1, AllergyName = "Peanuts" },
//                new AllergyDto { AllergyId = 2, AllergyName = "Milk" }
//            };

//            _serviceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(mockData); //Mocking the service method, tells mock to return mockData when GetAllAsync is called

//            // Act
//            var result = await _controller.GetAll();

//            // Assert
//            var okResult = Assert.IsType<OkObjectResult>(result); //Check for OkObjectResult 200 ok
//            var returnValue = Assert.IsAssignableFrom<IEnumerable<AllergyDto>>(okResult.Value);
//            Assert.Equal(2, returnValue.Count()); //Verify that the returned data has 2 items

//        }

//        //[Fact]
//        //public async Task GetAllAllergies_ReturnOkWithData()
//        //{
//        //    var mockService = new Mock<AllergyService>();
//        //    mockService.Setup(s => s.GetAllAsync())
//        //        .ReturnsAsync(new List<AllergyDto>
//        //        {
//        //            new AllergyDto { AllergyId = 1, AllergyName = "Peanuts" },
//        //            new AllergyDto { AllergyId = 2, AllergyName = "Milk" }

//        //        });

//        //    var controller = new AllergyController(mockService.Object);
//        //    var result = await controller.GetAll();

//        //    var okResult = Assert.IsType<OkObjectResult>(result);
//        //    var data = Assert.IsType<List<AllergyDto>>(okResult.Value);
//        //    Assert.Equal(2, data.Count);
//        //}


//    }
//}
