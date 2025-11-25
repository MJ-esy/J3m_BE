using System;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using J3m_BE.Controllers;
using J3m_BE.Services.Interfaces;
using J3M.Shared.DTOs.Diets;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Assert = Xunit.Assert;

namespace J3M.test.Jane.DietTest
{
    public class DietControllerTest
    {
        private readonly Mock<IDietService> _mockService;
        private readonly DietController _controller;

        public DietControllerTest()
        {
            _mockService = new Mock<IDietService>();
            _controller = new DietController (_mockService.Object);
        }

        [Fact]
        public async Task GetById_ReturnsOk_WhenDietExists()
        {
            //Arrange
            var dietDto = new DietDto { DietId = 1, DietName = "Keto" };
            _mockService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(dietDto);

            //Act
            var result = await _controller.GetById(1);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedDiet = Assert.IsType<DietDto>(okResult.Value);
            Assert.Equal(1, returnedDiet.DietId);
            Assert.Equal("Keto", returnedDiet.DietName);

        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenDietDoesNotExist()
        {
            //Arrange 
            _mockService.Setup(s => s.GetByIdAsync(99)).ReturnsAsync((DietDto?)null);

            //Act
            var result = await _controller.GetById(99);

            //Assert
            Assert.IsType<NotFoundResult>(result.Result);

        }

    }
}
