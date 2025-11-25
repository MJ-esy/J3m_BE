using J3m_BE.Exceptions;
using J3m_BE.Models;
using J3m_BE.Repositories.Interfaces;
using J3m_BE.Services;
using Moq;

namespace J3M.tests.MJ.NutrientGroupTests
{
    public class NutrientGroupServiceTests
    {
        private readonly Mock<INutrientGroupRepository> _mockRepo;

        public NutrientGroupServiceTests()
        {
            _mockRepo = new Mock<INutrientGroupRepository>();
        }

        private NutrientGroupService CreateSut() => new(_mockRepo.Object);

        // Test for successful GetByIdAsync method
        [Fact]
        public async Task GetByIdAsync_ReturnsDetails_WhenFound()
        {
            // Arrange
            var nutrientData = new NutrientGroup { NutrientGroupId = 1, NutrientGroupName = "Carbs" };
            // Mock the repository method that the service actually calls
            _mockRepo.Setup(n => n.GetWithDetailsAsync(1)).ReturnsAsync(nutrientData);

            var sut = CreateSut();

            // Act
            var result = await sut.GetByIdAsync(1);

            // Assert
            Xunit.Assert.NotNull(result);
            _mockRepo.Verify(r => r.GetWithDetailsAsync(1), Times.Once);
        }

        // Test for unsuccessful GetByIdAsync method (not found)
        [Fact]
        public async Task GetByIdAsync_ThrowsNotFoundDomainException_WhenNotFound()
        {
            // Arrange
            _mockRepo.Setup(n => n.GetWithDetailsAsync(3)).ReturnsAsync((NutrientGroup?)null);
            var sut = CreateSut();

            // Act & Assert
            await Xunit.Assert.ThrowsAsync<NotFoundDomainException>(() => sut.GetByIdAsync(3));
            _mockRepo.Verify(r => r.GetWithDetailsAsync(3), Times.Once);
        }
    }
}
