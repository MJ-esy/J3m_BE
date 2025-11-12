
using J3m_BE.DTOs.Allergies;
using J3m_BE.Exceptions;
using J3m_BE.Models;
using J3m_BE.Repositories.Interfaces;
using J3m_BE.Services.Implementations;
using J3m_BE.Mappers;
using Moq;
using System.Linq.Expressions;

using Assert = Xunit.Assert;

// Testing AllergyService
// Camilla Söderman  Net.24 2025

namespace TestJ3m_BE
{
    public class AllergyServiceTests
    {
        private readonly Mock<IAllergyRepository> _repositoryMock;
        private readonly AllergyService _service;
        public AllergyServiceTests()
        {
            _repositoryMock = new Mock<IAllergyRepository>();
            _service = new AllergyService(_repositoryMock.Object);
        }

        // Create allergy test
        [Fact]
        public async Task CreateAsync_ValidDto_ReturnNewId()
        {
            // Arrange
            var dto = new AllergyCreateDto { AllergyName = "Peanuts" };
            _repositoryMock.Setup(r => r.ExistsAsync(It.IsAny<Expression<Func<Allergy, bool>>>()))
                .ReturnsAsync(false);

            _repositoryMock.Setup(r => r.AddAsync(It.IsAny<Allergy>()))
                .Callback<Allergy>(a => a.AllergyId = 42); // Simulate DB-generated ID

            _repositoryMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

            // Act
            var result = await _service.CreateAsync(dto);

            // Assert
            Assert.Equal(42, result);
        }

        //Get by id with connected ingredients test
        [Fact]
        public async Task GetByIdAsync_ValidId_ReturnsDto()
        {
            //Arrange
            var allery = new Allergy { AllergyId = 1, AllergyName = "Milk" };
            _repositoryMock.Setup(r => r.GetWithIngredientsAsync(1))
                .ReturnsAsync(allery);

            //Act
            var result = await _service.GetByIdAsync(1);

            //Assert
            Assert.Equal("Milk", result.AllergyName);
        }

        //Get by id with connected ingredients invalid id test
        [Fact]
        public async Task GetByIdAsync_InvalidId_ThrowsNotFound()
        {
            //Arrange
            _repositoryMock.Setup(r => r.GetWithIngredientsAsync(99))
                .ReturnsAsync((Allergy?)null);

            //Act & Assert
            await Assert.ThrowsAsync<Exception>(async () => await _service.GetByIdAsync(99));

        }

        // Get all allergies test
        [Fact]
        public async Task GetAllAsync_VariousAllergies_ReturnsAllergyDtos()
        {
            // Arrange
            var mockData = new List<Allergy>
            {
                new Allergy { AllergyId = 1, AllergyName = "Peanuts" },
                new Allergy { AllergyId = 2, AllergyName = "Milk" }
            };
            _repositoryMock.Setup(r => r.GetAllAllergiesWithCountAsync())
                .ReturnsAsync(mockData.Select(a => a.ToDto()));
            // Act
            var result = await _service.GetAllAsync();
            // Assert
            Assert.Equal(2, result.Count());
            Assert.Contains(result, a => a.AllergyName == "Peanuts");
            Assert.Contains(result, a => a.AllergyName == "Milk");
        }

        //Delete allergy test valid id
        [Fact]
        public async Task DeleteAsync_ValidId_ReturnTrue()
        {
            // Arrange
            var allergy = new Allergy { AllergyId = 1, AllergyName = "Gluten" };

            _repositoryMock.Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(allergy);
            _repositoryMock.Setup(r => r.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            // Act
            var result = await _service.DeleteAsync(1);

            // Assert
            Assert.True(result);
        }

        // Delete allergy test invalid id
        [Fact]
        public async Task DeleteAsync_InValidId_ThrowsNotFound()
        {
            // Arrange
            _repositoryMock.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Allergy?)null);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundDomainException>(() => _service.DeleteAsync(99));

        }

    }
}
