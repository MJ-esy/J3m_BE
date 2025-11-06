//using J3m_BE.DTOs.Allergies;
//using J3m_BE.Repositories.Interfaces;
//using J3m_BE.Services.Implementations;
//using Moq;
//using Xunit;
//using Assert = Xunit.Assert;

//namespace J3m_BE.Tests.Services
//{
//    public class AllergyServiceTests
//    {
//        [Fact]
//        public async Task CreateAllergyAsync_ShouldReturnCreatedDto()
//        {
//            // Arrange
//            var mockRepo = new Mock<IAllergyRepository>();
//            var service = new AllergyService(mockRepo.Object);

//            // Act
//            var result = await service.CreateAsync(new AllergyCreateDto { AllergyName = "Milk" });

//            // Assert
//            Assert.NotNull(result);
//        }
//    }
//}
