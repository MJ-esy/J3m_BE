using J3m_BE.Exceptions;
using J3m_BE.Models;
using J3m_BE.Repositories.Interfaces;
using J3m_BE.Services;
using Moq;

namespace J3M.tests.NutrientGroupTests
{
    public class NutrientGroupServiceTests
    {
        //Test for successful GetByIdAsync method
        [Fact]
        public async Task GetByIdAsync_ReturnDetails()
        {
            //Arrange
            //create fake database
            var mockRepository = new Mock<INutrientGroupRepository>();
            //setup fake data
            var data = new NutrientGroup { NutrientGroupId = 1, NutrientGroupName = "Carbs" };
            //setup mock behavior
            mockRepository.Setup(n => n.GetByIdAsync(It.Is<int>(i => i == 1)))
                .ReturnsAsync(data);

            //create service with mock repository
            var sut = new NutrientGroupService(mockRepository.Object);

            //Act
            // call the method
            var result = await sut.GetByIdAsync(1);

            //Assert
            Assert.NotNull(result);
        }

        //Test for unsuccessful GetByIdAsync method
        [Fact]
        public async Task GetByIdAsync_IdNotFound_ReturnNull()
        {
            //Arrange
            //create fake database
            var mockRepository = new Mock<INutrientGroupRepository>();
            //setup fake data
            //var dataList = new List<NutrientGroup> { 
            //    new() { NutrientGroupId = 1, NutrientGroupName = "Carbs" },
            //    new() { NutrientGroupId = 2, NutrientGroupName = "Protein" } };

            //////setup mock behavior
            ////mockRepository.Setup(n => n.GetByIdAsync(It.Is<int>(i=>i==3)));
            //mockRepository.Setup(n => n.(dataList));

            //create service with mock repository
            var sut = new NutrientGroupService(mockRepository.Object);

            //Act
            // call the method
            var result = await sut.GetByIdAsync(3);

            //Assert
            Assert.ThrowsAsync<NotFoundDomainException>(async () =>
            {
                await sut.GetByIdAsync(3);
            });

        }

    }
}
