using System;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using J3m_BE.Repositories.Interfaces;
using J3m_BE.Services;
using J3m_BE.Models;
using Xunit;
using Assert = Xunit.Assert;
using J3m_BE.Exceptions;


namespace J3M.test.Jane.DietTest
{
    public class DietServiceTests
    {
        //Identifiera beroendet. DietServiceTests använder IDietRepository för att hämta data. 
        private readonly Mock<IDietRepository> _mockRepo;
        private readonly DietService _service;
        public DietServiceTests()
        {
            _mockRepo = new Mock<IDietRepository>();
            _service = new DietService(_mockRepo.Object);

        }


        [Fact]
        public async Task GetByIdAsync_ReturnDietDto_WhenDietExists()
        {
            //Arrange
            var diet = new Diet { DietId = 1, DietName = "Keto" };
            _mockRepo.Setup(repo => repo.GetWithDetailsAsync(1)).ReturnsAsync(diet);

            //Act
            var result = await _service.GetByIdAsync(1);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.DietId);
            Assert.Equal("Keto", result.DietName);

        }

        [Fact]
        public async Task GetByIdAsync_ThrowNotFoundDomainExeption_WhenDietDoesNotExist()
        {
            //Arrange
            _mockRepo.Setup(repo => repo.GetWithDetailsAsync(99)).ReturnsAsync((Diet?)null);

            //Act
            await Assert.ThrowsAsync<NotFoundDomainException>(async () => await _service.GetByIdAsync(99));
            //Assert
            _mockRepo.Verify(r => r.GetWithDetailsAsync(99), Times.Once);
        }
    }
}
